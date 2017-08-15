using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
using weddingplanner.Models;
using System.Collections.Generic;
using System.Linq;
 using System.Threading.Tasks;
//  using Microsoft.AspNetCore.Identity;

namespace weddingplanner.Controllers
{
    public class UserController : Controller
    {
          private weddingContext _context;
         
        public UserController(weddingContext context) {
            _context = context;
        }
[HttpGet]
 
[Route("")]
        public IActionResult Index()
        {return View("Register");}

[HttpPost]
[Route("processuser")]
public IActionResult processuser(RegisterViewModel user)
{
     if(ModelState.IsValid)
     {
           
        List<Userrecord> existinguser = _context.user.Where(u=>u.EmailAddress==user.EmailAddress).ToList();
        if (existinguser.Count == 0)
        {
             Userrecord newUser = new Userrecord {FirstName = user.FirstName, LastName= user.LastName, EmailAddress = user.EmailAddress, };
            //  newUser.Password = Hasher.HashPassword(newUser, user.Password);
            newUser.Password =  user.Password;
            _context.Add(newUser);
            _context.SaveChanges();        
            Userrecord logUser = _context.user.SingleOrDefault(u => u.EmailAddress == user.EmailAddress);
            HttpContext.Session.SetInt32("uid", logUser.UserId);
            System.Console.WriteLine("here");
            return RedirectToAction("Dashboard","Wedding");
        }
        else
        {
            ViewBag.status="regfailspecific";
            ViewBag.regerror = "User already exists";
            return View("Register");
        }

    }
    else{
        ViewBag.errors = ModelState.Values;
        ViewBag.status="regfail";
        return View("Register");

    }
}

[HttpPost]
[Route("processlogin")]
public IActionResult processlogin(string EmailAddress,string Password)
{
    Userrecord existingloginuser = _context.user.SingleOrDefault(user => user.EmailAddress == EmailAddress);
         
if (existingloginuser == null)
            {
                ViewBag.status="loginfailspecific";
                ViewBag.loginerror = "Please register!";
                return View("Register");
            }   
    else    
        {
            if(Password != null)
            {
            // var Hasher = new PasswordHasher<User>();
            // if(0 != Hasher.VerifyHashedPassword(existingloginuser, existingloginuser.Password, Password)){
            if(existingloginuser.Password==Password){
                HttpContext.Session.SetInt32("uid",(int)existingloginuser.UserId);
                HttpContext.Session.SetString("username", (string)existingloginuser.FirstName);
                return RedirectToAction("Dashboard","Wedding");
                }
            else{
                        ViewBag.status="loginfailspecific";
                        ViewBag.loginerror = "Invalid Credentials!";
                    return View("Register");
                    }
            }
            else{
                ViewBag.status="loginfailspecific";
                        ViewBag.loginerror = "Provide password!";
                    return View("Register");
            }

        
    }
}

[HttpGet]
[Route("logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");}
                // return View("Register");}

}
}
