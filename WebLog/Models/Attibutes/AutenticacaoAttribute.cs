using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebLog.Models
{
    public class AutenticacaoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Session.isLogado())
                if (!context.HttpContext.Request.Method.Equals("post", StringComparison.InvariantCultureIgnoreCase))
                {
                    context.HttpContext.Session.Set("Voce precisa estar logado para acessar esta pagina.", "msg");
                    context.Result = new RedirectResult("/Login/Login");
                }
                else
                    context.Result = new RedirectResult("/Autenticacao/Deslogado");
            base.OnActionExecuting(context);
        }

    }
}