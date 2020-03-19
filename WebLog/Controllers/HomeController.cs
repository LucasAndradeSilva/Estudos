using System.Diagnostics;
using WebLog.Models;
using WebLog.DB;
using WebLog.Models.Entities;
using WebLog.Models.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLog.Common.ReCaptha;
using Newtonsoft.Json.Linq;
using System;

namespace WebLog.Controllers
{
    [Autenticacao]
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

        public IActionResult FaleConosco()
        {
            return View();
        }

        public IActionResult ChatHub()
        {
            return View();
        }    

        public IActionResult Cadastro()
        {
            ViewBag.Message = string.IsNullOrEmpty(msg) ? TempData["msg"] : msg;
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

        public IActionResult PreecheEditar(int id)
        {
            var usuario = _usuarioRepository.GetUsuario(id);
            if (usuario != null)
            {
                return Json(usuario);
            }

            return null;
        }

        public IActionResult Editar(string json)        
        {
            var formulario = JObject.Parse(json);
            
            TempData["msg"]  = _usuarioRepository.Edit(new ListUsuarios { 
                idUser = Convert.ToInt32(formulario["txtId"]),
                Nome = formulario["txtNome"].ToString(),
                Idade = Convert.ToInt32(formulario["txtIdade"]),
                CPF = formulario["txtCpf"].ToString(),
                Email = formulario["txtEmail"].ToString(),
                Senha = formulario["txtSenha"].ToString()
            });
           return RedirectToAction("Cadastro"); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
