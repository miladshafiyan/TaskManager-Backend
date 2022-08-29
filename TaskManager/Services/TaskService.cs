using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskService
    {
        private readonly IMongoCollection<task> _TaskPropsCollection;

        public TaskService(
            IOptions<TaskDatabaseSettings> TaskDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                TaskDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                TaskDatabaseSettings.Value.DatabaseName);

            _TaskPropsCollection = mongoDatabase.GetCollection<task>(
                TaskDatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<List<task>> GetAsync() =>
            await _TaskPropsCollection.Find(_ => true).ToListAsync();

        public async Task<task?> GetAsync(string id) =>
            await _TaskPropsCollection.Find(x => x._id == id).FirstOrDefaultAsync();

        public async System.Threading.Tasks.Task CreateAsync(task task) =>
            await _TaskPropsCollection.InsertOneAsync(task);

        public async System.Threading.Tasks.Task UpdateAsync(string id, task updatedtask) =>
            await _TaskPropsCollection.ReplaceOneAsync(x => x._id == id, updatedtask);

        public async System.Threading.Tasks.Task RemoveAsync(string id) =>
            await _TaskPropsCollection.DeleteOneAsync(x => x._id == id);
    }
}
