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
            try
            {
                var contList = Repo.getData();
                return View(contList.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in All action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        public IActionResult Task()
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
                _logger.LogError($"Error in Create action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        public IActionResult GetContactById(int id)
        {
            try
            {
                var contact = Repo.GetDataById(id);
                return Ok(contact.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetContactById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                await Repo.DeleteContact(id);
                return Ok(new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteContact action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] DtoContact cont)
        {
            try
            {
                var resp = await Repo.UpdateContact(id, cont);
                return Ok(new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateContact action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}