using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Portfolio
{
    public class ContactRepository
    {
        private readonly IMongoCollection<ContactUs> _contacts;

        public ContactRepository(IOptions<MongoDbSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            _contacts = mongoDatabase.GetCollection<ContactUs>(settings.Value.ContactCollectionName);
        }

        public async Task<List<ContactUs>> GetAllAsync() =>
            await _contacts.Find(_ => true).ToListAsync();

        public async Task<ContactUs?> GetByIdAsync(string id) =>
            await _contacts.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ContactUs contact) =>
            await _contacts.InsertOneAsync(contact);

        public async Task UpdateAsync(string id, ContactUs contact) =>
            await _contacts.ReplaceOneAsync(c => c.Id == id, contact);

        public async Task DeleteAsync(string id) =>
            await _contacts.DeleteOneAsync(c => c.Id == id);
    }
}
