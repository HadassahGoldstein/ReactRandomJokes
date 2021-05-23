using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReactRandomJokes.Data;
using ReactRandomJokes.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactRandomJokes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly string _connectionString;
        public JokesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [HttpGet]
        [Route("GetRandomJoke")]
        public Joke GetRandomJoke()
        {
            var repo = new JokesRepository(_connectionString);
            return repo.GetRandomJoke();
        }
        [HttpGet]
        [Route("GetJokeById")]
        public Joke GetJokeById(int id)
        {
            var repo = new JokesRepository(_connectionString);
            return repo.GetJokeById(id);
        }
        [HttpGet]
        [Route("ViewJokes")]
        public List<Joke> ViewJokes()
        {
            var repo = new JokesRepository(_connectionString);
            return repo.ViewJokes();
        }
        [HttpPost]
        [Route("LikeJoke")]
        [Authorize]
        public void LikeJoke(UserLikedJokes ulj)
        {
            var repo = new JokesRepository(_connectionString);
            var repo2 = new UserRepository(_connectionString);
            ulj.UserId = repo2.GetByEmail(User.FindFirst("user")?.Value).Id;            
            ulj.Date = DateTime.Now;
            repo.LikeJoke(ulj);
        }
    }
}
