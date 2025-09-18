using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly ContactRepository _contactRepository;
        public ContactUsController(ContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        [HttpPost("Contact")]
        public async Task<IActionResult> Contact(Contact contact)
        {
            var contactUs = new ContactUs
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = contact.Name,
                Email = contact.Email,
                Phone = contact.Phone,
                Message = contact.Message,
                SubmittedAt = DateTime.UtcNow
            };
            await _contactRepository.CreateAsync(contactUs);
            return Ok("Contact Submitted SuccessFully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactRepository.GetAllAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ContactUs updatedContact)
        {
            var existing = await _contactRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            updatedContact.Id = id;
            await _contactRepository.UpdateAsync(id, updatedContact);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _contactRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _contactRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
