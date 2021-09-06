using AutoMapper;
using GrpcService1.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace GrpcService1.Repositories
{
    public class MovieRepository
    {
        private readonly IMongoCollection<Movie> _moviesCollection;
        private readonly IMongoClient _client;
       

        public MovieRepository(IMoviesDatabaseSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            var database = _client.GetDatabase(settings.DatabaseName);
            _moviesCollection = database.GetCollection<Movie>(settings.MoviesCollectionName);
        }

        public List<Movie> Get() =>
            _moviesCollection.Find(movie => true).ToList();

        public Movie Get(string id) =>
            _moviesCollection.Find<Movie>(movie => movie.Id == id).FirstOrDefault();

        public Movie Create(Movie movie)
        {
            _moviesCollection.InsertOne(movie);
            return movie;
        }


        public void Update(string id, Movie bookIn) =>
            _moviesCollection.ReplaceOne(movie => movie.Id == id, bookIn);

        public void Remove(Movie movieIn) =>
            _moviesCollection.DeleteOne(movie => movie.Id == movieIn.Id);

        public void Remove(string id) =>
            _moviesCollection.DeleteOne(movie => movie.Id == id);
    }
}