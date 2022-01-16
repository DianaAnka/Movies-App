using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Api.Controllers.Dtos;
using MovieInfo.Api.Controllers.Movies.Dtos;
using MovieInfo.Domain.Models;
using MovieInfo.EntityFramework;
using MovieInfo.EntityFramework.Repositories;
using MovieInfo.EntityFramework.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieInfo.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IRepositiry<Movie> _repositiry;
        private readonly IMovRepository _movRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IRepositiry<Comment> _commentRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public MoviesController(IMovieRepository movieRepository, IMapper mapper, IRepositiry<Movie> repositiry, IMovRepository movRepository, IWebHostEnvironment env, IRepositiry<Comment> commentRepo, UserManager<ApplicationUser> userManager)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _repositiry = repositiry;
            _movRepository = movRepository;
            _env = env;
            _commentRepo = commentRepo;
            _userManager = userManager;
        }

        [HttpPost(nameof(AddMovie))]
        public async Task<MovieDto> AddMovie([FromForm] CreateMovieDto input)
        {
            string uniqueFileName = GetMovieImageName(input.Image);
            var movie = Movie.Create(input.Name, input.Description, uniqueFileName, input.RelaseDate);
            var result = await _movRepository.AddAsync(movie);
            return _mapper.Map<Movie, MovieDto>(result);
        }



        [HttpGet(nameof(GetAllMovies))]
        public async Task<IEnumerable<MovieDto>> GetAllMovies()
        {
            var movies = await _movRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Movie>, IEnumerable<MovieDto>>(movies);
        }

        [HttpDelete(nameof(Delete))]
        public async Task Delete(int id)
        {
            await _movRepository.Delete(id);
        }

        [AllowAnonymous]
        [HttpGet(nameof(GetMovie))]
        public async Task<MovieDto> GetMovie(int id)
        {
            var movie = await _repositiry.GetAsync(id, m => m.Comments);
            var movieDto = _mapper.Map<Movie, MovieDto>(movie);
            if (movieDto.Comments != null)
            {
                movieDto.Comments.ForEach(item =>
                {
                    var user = _userManager.FindByIdAsync(item.UserId).Result;
                    item.UserName = user.UserName;
                });
            }

            return movieDto;
        }

        [HttpPut(nameof(UpdateMovie))]
        public async Task<MovieDto> UpdateMovie(UpdateMovieDto input)
        {
            var movie = await _movRepository.GetAsync(input.Id);
            movie.Update(input.Name, input.Description, "", input.RelaseDate);
            var result = await _movRepository.Update(movie);
            return _mapper.Map<Movie, MovieDto>(result);
        }

        [HttpPost(nameof(AddCommentToMovie))]
        public async Task AddCommentToMovie(AddCommentDto input)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var comment = Comment.Create(input.Text, input.MovieId, userId);
            await _commentRepo.AddAsync(comment);
        }

        private string GetMovieImageName(IFormFile image)
        {
            string uniqueFileName = null;
            if (image != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                uniqueFileName = DateTime.Now.Ticks.ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                image.CopyTo(fileStream);
            }

            return uniqueFileName;
        }

    }
}
