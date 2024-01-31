using Microsoft.AspNetCore.Mvc;
using Regestration.Models;
using System.Security.Cryptography;
using System.Text;

namespace Regestration.Controllers
{
    public class AuthController : Controller
    {
        private readonly DataBaseContext _context;

        public AuthController(DataBaseContext context)
        {
            _context = context;
        }

        public IActionResult LogingIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Loging(LoginModel login)
        {
            var data = _context.UserRegistration;
            var existingUser = data.FirstOrDefault(x => x.Email == login.Email);

            if (existingUser != null && VerifyPassword(login.Password, existingUser.Password))
            {
                existingUser.LastLoggedOn = System.DateTime.Now;
                _context.SaveChanges();

                return RedirectToAction("Data", "DataTable");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View("LogingIn");
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
                string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").Substring(0, 10);
                return hashedPassword == storedHashedPassword;
            }
        }

        /*private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                string enteredPasswordHash = builder.ToString();

                return enteredPasswordHash == storedHashedPassword;
            }
        }*/

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegister data)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var IsExist = _context.UserRegistration.Any(x => x.Email.ToLower() == data.Email.ToLower());

            if (IsExist)
            {
                ViewBag.dataError = "The User is Already Exist";
                return View();
            }

            string hashedPassword = HashPassword(data.Password);

            UserRegister userRegister = new UserRegister()
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                Password = hashedPassword,
                ConfirmPassword = data.ConfirmPassword,
                CreatedTime = System.DateTime.Now
            };

            _context.UserRegistration.Add(userRegister);
            _context.SaveChanges();
            ModelState.Clear();

            ViewBag.dataSucess = "User Register is SucessFully!!";
            //return RedirectToAction("LogingIn", "Auth");
            return View();
        }

        /*private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }*/

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").Substring(0, 10);
                return hashedPassword;
            }
        }
    }
}