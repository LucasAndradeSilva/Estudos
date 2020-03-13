using System.Diagnostics;
using WebLog.Models;
using WebLog.DB;
using WebLog.Models.Entities;
using WebLog.Models.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLog.Common.ReCaptha;
using Newtonsoft.Json.Linq;

namespace WebLog.Controllers
{
    public class HomeController : Controller 
    {
        private readonly IReCaptcha reCaptcha;
        IUsuarioRepository _usuarioRepository;

        private string msg;

        public HomeController(IUsuarioRepository usuarioRepository, IReCaptcha captcha)
        {
           _usuarioRepository = usuarioRepository;
            reCaptcha = captcha;
        }
   
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChatHub()
        {
            return View();
        }

        public IActionResult Login()
        {
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
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.erro = "Usuario ou senha estão incorretos!";
                    return View();
                }
            }

        }

        public IActionResult Cadastro()
        {
            ViewBag.Message = msg;
            ViewBag.List = _usuarioRepository.GetUsuarios();
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario usuario, IFormCollection frm)
        {
            var count = _usuarioRepository.Add(usuario, new Login { Email = frm["txtEmail"], Senha = frm["txtpass"] });
            if (count > 0)
            {
                msg = "Usuário cadastrado com Sucesso!";
                return Cadastro();
            }

            return Cadastro();
        }

        public IActionResult Deletar(int id) 
        {
            var count = _usuarioRepository.Delete(id);
            if (count > 0)
            {
               msg = "Usuario deletado com Sucesso!";
                return RedirectToAction("Cadastro");
            }

            return RedirectToAction("Cadastro");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
