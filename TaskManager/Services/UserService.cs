using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _UsersCollection;

        public UserService(
            IOptions<UserDatabaseSettings> UserDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                UserDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                UserDatabaseSettings.Value.DatabaseName);

            _UsersCollection = mongoDatabase.GetCollection<User>(
                UserDatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<List<User>> GetAsync() =>
            await _UsersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string UserName) =>
            await _UsersCollection.Find(x => x.UserName == UserName).FirstOrDefaultAsync();

        public async System.Threading.Tasks.Task CreateAsync(User user) =>
            await _UsersCollection.InsertOneAsync(user);

        public async System.Threading.Tasks.Task UpdateAsync(string id, User updateduser) =>
            await _UsersCollection.ReplaceOneAsync(x => x._id == id, updateduser);

        public async System.Threading.Tasks.Task RemoveAsync(string id) =>
            await _UsersCollection.DeleteOneAsync(x => x._id == id);
    }
}
