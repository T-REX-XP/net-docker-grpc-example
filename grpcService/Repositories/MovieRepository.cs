using AutoMapper;
using GrpcService1.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcService1.Repositories
{
    public class MovieRepository
    {
        private readonly IMongoCollection<Movie> _moviesCollection;
        private readonly IMongoClient _client;

        #region Async 
        public MovieRepository(IMoviesDatabaseSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            var database = _client.GetDatabase(settings.DatabaseName);
            _moviesCollection = database.GetCollection<Movie>(settings.MoviesCollectionName);
        }

        public async Task<IEnumerable<Movie>> GetAsync(GetMoviesRequest request)
        {
            Console.WriteLine("---------GRPC: MovieRepository BEFORE GetAsync");
            var filter = new BsonDocument();
            var collection = await _moviesCollection.Find(filter).Skip(request.Skip).Limit(request.Take).ToListAsync();

            Console.WriteLine("---------GRPC: MovieRepository AFTER GetAsync");
            return collection;
        }


        public async Task<Movie> GetAsync(string id)
        {
            Console.WriteLine("---GRPC: id===" + id);
            var record = await _moviesCollection.Find(_ => _.Id == id).SingleAsync();
            return record;
        }


        public async Task<Movie> CreateAsync(Movie movie)
        {
            await _moviesCollection.InsertOneAsync(movie);
            return movie;
        }


        public async void RemoveAsync(Movie movieIn)
        {
            await RemoveAsync(movieIn?.Id);
        }


        public async Task RemoveAsync(string id) =>
              await _moviesCollection.DeleteOneAsync(a => a.Id == id);
        #endregion

        public async Task UpdateAsync(string id, Movie bookIn) =>
            await _moviesCollection.ReplaceOneAsync(movie => movie.Id == id, bookIn);
    }
}