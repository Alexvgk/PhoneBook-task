using Microsoft.AspNetCore.Mvc;
using PhoneBook_task.DTO;
using PhoneBook_task.Models;
using PhoneBook_task.Models.BaseModel;
using PhoneBook_task.Repository;
using System.Diagnostics;

namespace PhoneBook_task.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;

        private IBaseRepository<Contact> Repo { get; set; }

        public ContactController(ILogger<ContactController> logger, IBaseRepository<Contact> baseData)
        {
            Repo = baseData;
            _logger = logger;
        }

        public IActionResult All()
        {
            var contList = Repo.getData();
            return View(contList.Result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DtoContact cont)
        {
            try
            {
                var result = await Repo.CreateContact(cont);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Failed to create contact");
                }
            }
            catch (Exception ex)
            {
                // Обработка других исключений, если необходимо
                return StatusCode(500, "Internal server error");
            }
        }

            public Contact GetContactById(int id) {
            var contact = Repo.GetDataById(id);
            return contact.Result;
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContact(int id)
        {
            await Repo.DeleteContact(id);
            return Ok(new { id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}