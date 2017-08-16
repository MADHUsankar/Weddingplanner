using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 using System;
using System.Globalization;
using weddingplanner.Models;
using System.Collections.Generic;
using System.Linq;
 using System.Threading.Tasks;
 using Microsoft.EntityFrameworkCore;
//  using Microsoft.AspNetCore.Identity;

namespace weddingplanner.Controllers
{
    public class WeddingController : Controller
    {
          private weddingContext _context;
         
        public WeddingController(weddingContext context) {
            _context = context;
        }
[HttpGet]
[Route("Dashboard")]
        public IActionResult Dashboard()
        {
                int userid =  (int)HttpContext.Session.GetInt32("uid");
                List<weddingrecords> Allweddingrecords = _context.weddings
                .Include(w =>w.Invitationsinfo)
                .ToList();

                System.Console.WriteLine(Allweddingrecords);
              ViewBag.Allweddingrecords = Allweddingrecords;
              ViewBag.curruser = userid;
                return View("dashboard");}


[HttpGet]
[Route("addwedding")]
        public IActionResult addwedding()
        {
                return View("addwedding");}

[HttpGet]
[Route("wedding/deletewed/{weddingid}")]
        public IActionResult deletewed(int weddingid)
        {
                weddingrecords weddingrecord = _context.weddings.SingleOrDefault(w => w.WeddingId == weddingid);
                _context.weddings.Remove(weddingrecord);
                _context.SaveChanges();
                return RedirectToAction("Dashboard"); }

[HttpPost]
[Route("addwedding")]
        public IActionResult addwedding(weddingrecords newwed)
        {
        if(ModelState.IsValid)
        {
                 int userid =  (int)HttpContext.Session.GetInt32("uid");
                newwed.UserId = userid;
                if (newwed.WeddingDate > DateTime.Now)
                {
                // newwed.CreatedAt = DateTime.Now;
                // newwed.UpdatedAt = DateTime.Now;
                _context.Add(newwed);
                _context.SaveChanges(); 
                return RedirectToAction("Dashboard"); 
                }
                else{
                        ViewBag.errors = "Date should be future dated";
        ViewBag.status="wedaddfaildate";
        return View("addwedding");
                }
        }
        else{
               ViewBag.errors = ModelState.Values;
        ViewBag.status="wedaddfail";
        return View("addwedding");
        }
}
[HttpGet]
[Route("wedding/attending/{weddingid}")]
        public IActionResult attending(int weddingid)
        {
                Invitationsinfo newinv = new Invitationsinfo();
                int userid =  (int)HttpContext.Session.GetInt32("uid");
                newinv.UserId = userid;
                newinv.WeddingId = weddingid;
                
                _context.Add(newinv);
                _context.SaveChanges();
                return RedirectToAction("Dashboard"); }
[HttpGet]
[Route("wedding/notattending/{weddingid}")]
        public IActionResult notattending(int weddingid)
        {
                Invitationsinfo newinv = new Invitationsinfo();
                int userid =  (int)HttpContext.Session.GetInt32("uid");
                 Invitationsinfo invitationrecord = _context.invitations.SingleOrDefault(w => w.WeddingId == weddingid && w.UserId == userid);
                _context.invitations.Remove(invitationrecord);
                _context.SaveChanges();

                return RedirectToAction("Dashboard"); }
[HttpGet]
[Route("wedding/show/{weddingid}")]
        public IActionResult show(int weddingid)
        {
                  int userid =  (int)HttpContext.Session.GetInt32("uid");
                  weddingrecords weddingrecord = _context.weddings.SingleOrDefault(w => w.WeddingId == weddingid);
                  List<Invitationsinfo> invitationrecords = _context.invitations.Where(w => w.WeddingId == weddingid)
                  .Include(p=>p.Userrecord)
                  .ToList();
                  
                  ViewBag.invitationrecords = invitationrecords;
                  ViewBag.weddingrecord = weddingrecord;
                return View("show"); }



}
}
// addwedding
// [HttpPost]
// [Route("processuser")]
// public IActionResult processuser(RegisterViewModel user)
// {
// }
