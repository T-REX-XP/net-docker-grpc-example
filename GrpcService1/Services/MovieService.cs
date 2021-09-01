using GrpcService1.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace GrpcService1.Services
{
    public class MovieService
    {
        private readonly IMongoCollection<Movie> _movies;

        public MovieService(IMoviesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _movies = database.GetCollection<Movie>(settings.MoviesCollectionName);
        }

        public List<Movie> Get() =>
            _movies.Find(book => true).ToList();

        public Movie Get(string id) =>
            _movies.Find<Movie>(movie => movie.Id == id).FirstOrDefault();

        public Movie Create(Movie movie)
        {
            _movies.InsertOne(movie);
            return movie;
        }

        public void Update(string id, Movie bookIn) =>
            _movies.ReplaceOne(movie => movie.Id == id, bookIn);

        public void Remove(Movie bookIn) =>
            _movies.DeleteOne(movie => movie.Id == bookIn.Id);

        public void Remove(string id) =>
            _movies.DeleteOne(movie => movie.Id == id);
    }
}