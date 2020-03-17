using System.Diagnostics;
using WebLog.Models;
using WebLog.DB;
using WebLog.Models.Entities;
using WebLog.Models.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebLog.Controllers
{
    public class AutenticacaoController : Controller
    {
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }
    }
}