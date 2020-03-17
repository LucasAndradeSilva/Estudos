﻿using System.Diagnostics;
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

        public IActionResult ChatHub()
        {
            return View();
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