using WebLog.Models.Entities;
using WebLog.Models.Interfaces.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebLog.DB.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private IConfiguration _configuration;
        private string Connection { get; }
        
        public UsuarioRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
            Connection = _configuration["ConnectionStrings"];
        }
        public int Add(Usuario usuario, Login login)
        {
            int count = 0;
            using (var con = new SqlConnection(Connection))
            {
                try
                {
                    ExecutarComando("insert into tbLogin values(@Email, @Senha, (select max(idUser) from tbUsuario));", login);
                    ExecutarComando("insert into tbUsuario values(@Nome, @Idade, @CPF);", usuario);
                }
                catch (Exception ex)
                {
                    throw ex;                    
                }
                finally
                {
                    con.Dispose();
                }

                void ExecutarComando(string sql, object parametro) 
                {
                    con.Open();                    
                    count = con.Execute(sql, parametro);
                    con.Close();
                }

                return count;
            }
        }

        public List<ListUsuarios> GetUsuarios()
        {
            using (var con = new SqlConnection(Connection))
            {
                List<ListUsuarios> ListUsuarios;
                try
                {
                    con.Open();
                    ListUsuarios = con.Query<ListUsuarios>("select * from tbUsuario inner join tbLogin on tbLogin.Id_User = tbUsuario.idUser;").ToList();                   
                    con.Close();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Dispose();
                }

                return ListUsuarios;
            }
        }

        public Usuario GetUsuario(int id)
        {
            throw new NotImplementedException();
        }

        public int Edit(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            using (var con = new SqlConnection(Connection))
            {
                var count = 0;
                try
                {
                    con.Open();
                    count = con.Execute("delete tbUsuario where idUser = @ID;", new { ID = id});
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Dispose();
                }

                return count;
            }
        }

        public ListUsuarios Logar(Login login)
        {
            using (var con = new SqlConnection(Connection))
            {
                ListUsuarios login1 = new ListUsuarios();
                try
                {
                    con.Open();
                    login1 = con.QuerySingle<ListUsuarios>("select * from tbUsuario inner join tbLogin on tbLogin.Id_User = tbUsuario.idUser where tbLogin.Email = @Email and tbLogin.Senha = @Senha; ", new { Email = login.Email, Senha = login.Senha });
                    con.Close();
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    con.Dispose();
                }

                return login1;
            }
        }
    }
}
