using Microsoft.AspNetCore.Mvc;
using Regestration.Models;

namespace Regestration.Controllers
{
    public class DataTableController : Controller
    {
        private readonly DataBaseContext _context;

        public DataTableController(DataBaseContext context)
        {
            _context = context;
        }

        public IActionResult Data()
        {
            var data = _context.UserRegistration;
            List<UserRegister> model = new List<UserRegister>();
            foreach (var item in data)
            {
                UserRegister userRegister = new UserRegister()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    CreatedTime = item.CreatedTime
                };
                model.Add(userRegister);
            }
            return View(model);
        }
    }
}