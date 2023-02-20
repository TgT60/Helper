using ForTestIdeas.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.DataProtection;
using ForTestIdeas.Models;
using ForTestIdeas.ActionFilters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using ForTestIdeas.Domain.Entities;

namespace ForTestIdeas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly ILogger<UserController> _logger;
        private readonly TestContext _dbContext;

        public UserController(ILogger<UserController> logger, IDataProtectionProvider protectionProvider, TestContext dbContext)
        {
            _logger = logger;
            _protectionProvider = protectionProvider;
            this._dbContext = dbContext;
        }

        [HttpGet("Register")]
        public IActionResult Register() => View();

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] UserViewModel userViewModel)
        {
            if (_dbContext.Users.Any(x => x.Login == userViewModel.Login))
            {
                return BadRequest();
            }
            var userInfo = new User()
            {
                Id = Guid.NewGuid(),
                Login = userViewModel.Login,
                Password = userViewModel.Password,
                Role = userViewModel.Role,
                Name = userViewModel.Name,
                SureName = userViewModel.SureName,
                PersonImg = userViewModel.PersonImg
            };

            await _dbContext.Users.AddAsync(userInfo);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Login");
        }

        [HttpGet("CreateEquipment")]
        [UserAuthorization("adjuster,emploer")]
        public IActionResult CreateEquipment() => View();

        [HttpPost("CreateEquipment")]
        [UserAuthorization("adjuster,emploer")]
        public async Task<IActionResult> CreateEquipment([FromForm] EquipmentViewModel equipmentViewModel)
        {
            var equipment = new Equipment
            {
                Id = Guid.NewGuid(),
                Title = equipmentViewModel.Title,
                Description = equipmentViewModel.Description,
                Img = equipmentViewModel.Img
            };

            await _dbContext.Equipments.AddAsync(equipment);
            await _dbContext.SaveChangesAsync();
            return View();
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();  
        }

        [HttpPost("Login")]  
        public ActionResult<string> Login([FromForm] User userParam)
        {
            var userInfo = _dbContext.Users.SingleOrDefault(x => x.Password == userParam.Password && x.Login == userParam.Login);

            if (userInfo != null)
            {
                var key = JsonConvert.SerializeObject(userInfo);
                var protector = _protectionProvider.CreateProtector("User-auth");
                var encryptedKey = protector.Protect(key);
                HttpContext.Response.Cookies.Append("Name", userInfo.Name);
                HttpContext.Response.Cookies.Append("authKey", encryptedKey);
                return RedirectToAction("Login");
            }
            return View();
        }
        
        [HttpGet("UserProfile")] 
        [UserAuthorization("adjuster,emploer")]
        public ActionResult<string> UserProfile()
        {
            GetNameAndSureName();
            return View();
        }
        
        [HttpGet("Logout")] 
        [UserAuthorization("adjuster,emploer")]
        public ActionResult<string> Logout()
        {
            return View();
        }
        

        [HttpGet("CreateServiceItem")]
        [UserAuthorization("adjuster")]
        public IActionResult CreateServiceItem()
        {
            var userName = _dbContext.Users.ToList();
            
            var name = userName.Select(user => user.Name).ToList();
            ViewBag.Name = name;
            var sureName = userName.Select(user => user.SureName).ToList();
            ViewBag.SureName = sureName;
            
            return View();
        }

        [HttpPost("CreateServiceItem")]
        [UserAuthorization("adjuster")]
        public async Task<IActionResult> CreateServiceItem([FromForm] ServiceItemViewModel serviceItemViewModel, [FromForm] User userParam)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Name == userParam.Name && x.SureName == userParam.SureName);
            if (user != null)
            {
                var item = new ServiceItem
                {
                    Id = Guid.NewGuid(),
                    Title = serviceItemViewModel.Title,
                    ShortDescription = serviceItemViewModel.ShortDescription,
                    LongDescripton = serviceItemViewModel.LongDescripton
                };

                var assignTicket = new TaskTiket
                {
                    Id = Guid.NewGuid(),
                    ServiceItemId = item.Id,
                    UserId = user.Id
                };

                await _dbContext.ServiceItems.AddAsync(item);
                await _dbContext.TaskTikets.AddAsync(assignTicket);
                await _dbContext.SaveChangesAsync();
                return Ok(item);
            }

            return Ok();
        }


        [HttpGet("GetEquipment")]
        [UserAuthorization("emploer")]
        public ActionResult<IEnumerable<TestContext>> GetEquipment()
        {
            var equipments = _dbContext.Equipments.ToList();
            return View();
        }

        [HttpGet("GetWorker")]
        [UserAuthorization("emploer")]
        public ActionResult<IEnumerable<TestContext>> GetWorker()
        {
            var workers = _dbContext.Users.ToList();
            return View(workers);
        }
        
        public  void GetNameAndSureName()
        {
            HttpContext.Request.Cookies.TryGetValue("authKey", out var name);
            var protector = _protectionProvider.CreateProtector("User-auth");
            var decryptedKey = protector.Unprotect(name);
            var user = JsonConvert.DeserializeObject<User>(decryptedKey);
            var userSureName = user.SureName;
            var userName = user.Name;
            ViewBag.UserSureName = userSureName;
            ViewBag.UserName = userName;
        }
    }
}
