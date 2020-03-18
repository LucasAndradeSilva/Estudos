using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebLog.Common.ReCaptha;
using WebLog.Models.Entities;
using WebLog.Models.Interfaces.Repository;

namespace WebLog.Controllers
{    
    public class LoginController : Controller
    {
        private readonly IReCaptcha reCaptcha;
        private readonly IUsuarioRepository _usuarioRepository;

        private string msg;

        public LoginController(IUsuarioRepository usuarioRepository, IReCaptcha captcha)
        {
            _usuarioRepository = usuarioRepository;
            reCaptcha = captcha;
        }

        public IActionResult Login()
        {
            ViewBag.erro = TempData["Erro"];
            return View();
        }

        public IActionResult Logar(string json)
        {
            var formulario = JObject.Parse(json);
            if (!reCaptcha.ValidarCaptcha(formulario["g-recaptcha-response"].ToString()))
            {
                return View();
            }
            else
            {
                ListUsuarios login = _usuarioRepository.Logar(new Login() { Email = formulario["txtEmail"].ToString(), Senha = formulario["txtPass"].ToString() });
                if (login != null)
                {
                    HttpContext.Session.Set(login);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    TempData["Erro"] = "Usuario ou senha estão incorretos!";
                    return RedirectToAction("Login");
                }
            }

        }
    }
}