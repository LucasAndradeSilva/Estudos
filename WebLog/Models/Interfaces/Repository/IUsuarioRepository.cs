using WebLog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLog.Models.Interfaces.Repository
{
    public interface IUsuarioRepository
    {
        int Add(Usuario usuario, Login login);
        List<ListUsuarios> GetUsuarios();
        ListUsuarios GetUsuario(int id);
        int Edit(Usuario usuario);
        int Delete(int id);
        ListUsuarios Logar(Login login);
    }
}
