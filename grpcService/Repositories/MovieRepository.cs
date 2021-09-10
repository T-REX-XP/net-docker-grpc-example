using GrpcService1.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Movie>> GetAsync(GetMoviesRequest request)
        {
            var filter = new BsonDocument();
            var collection = await _moviesCollection.Find(filter).Skip(request.Skip).Limit(request.Take).ToListAsync();
            return collection;
        }

        public async Task<Movie> GetAsync(string id)
        {
            var record = await _moviesCollection.Find(_ => _.Id == id).SingleAsync();
            return record;
        }

        public async Task<Movie> CreateAsync(Movie movie)
        {
            await _moviesCollection.InsertOneAsync(movie);
            return movie;
        }

        public async void RemoveAsync(Movie movieIn) =>
            await RemoveAsync(movieIn?.Id);

        public async Task RemoveAsync(string id) =>
              await _moviesCollection.DeleteOneAsync(a => a.Id == id);     

        public async Task UpdateAsync(string id, Movie bookIn) =>
            await _moviesCollection.ReplaceOneAsync(movie => movie.Id == id, bookIn);
    }
}