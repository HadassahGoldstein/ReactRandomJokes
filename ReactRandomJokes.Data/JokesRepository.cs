using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace ReactRandomJokes.Data
{
   public class JokesRepository
    {
        private readonly string _connectionString;
        public JokesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Joke GetRandomJoke()
        {
            var client = new HttpClient();
            var json = client.GetStringAsync(
                   "https://official-joke-api.appspot.com/jokes/programming/random")
                .Result;
            var joke = JsonConvert.DeserializeObject<List<Joke>>(json).First();
            using var context = new JokesDbContext(_connectionString);
            joke.Id = 0;
            if (!context.Jokes.Any(j => j.Punchline == joke.Punchline))
            {
                context.Jokes.Add(joke);
                context.SaveChanges();
            }
            return context.Jokes.Include(j => j.UserLikedJokes).FirstOrDefault(j=>j.Punchline==joke.Punchline);            
        }           
        public List<Joke> ViewJokes()
        {
            using var context = new JokesDbContext(_connectionString);
            var jokes= context.Jokes.Include(j=>j.UserLikedJokes).ToList();
            return jokes;
        }
        public void LikeJoke(UserLikedJokes liked)
        {          
            using var context = new JokesDbContext(_connectionString);
            if (context.UserLikedJokes.Any(ulj => ulj.UserId == liked.UserId && ulj.JokeId == liked.JokeId))
            {
                context.Database.ExecuteSqlInterpolated(@$"UPDATE UserLikedJokes 
                                                          SET Liked={liked.Liked}, Date=GETDATE() 
                                                          WHERE UserId={liked.UserId} AND JokeId={liked.JokeId}");
                return;
            }
            context.UserLikedJokes.Add(liked);
            context.SaveChanges();
        }
        public Joke GetJokeById(int id)
        {
            using var context = new JokesDbContext(_connectionString);
            return context.Jokes.Include(j=>j.UserLikedJokes).FirstOrDefault(j => j.Id == id);
        }
    }
}
