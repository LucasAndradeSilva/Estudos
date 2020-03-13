using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebLog.Models;
using System;
using System.IO;
using System.Text;
using WebLog.Models.Entities;

namespace WebLog {
    public static class SessionHelper {
        
        //================
        //=== GET TYPE ===
        //================
        public static T Get<T>(this ISession session, string key = null) {
            if (string.IsNullOrEmpty(key))
                key = typeof(T).FullName;
            return session.TryGetValue(key, out byte[] bytes) ? JsonConvert.DeserializeObject<T>(GetString(bytes)) : default(T);
        }
        //================
        //=== SET TYPE ===
        //================
        public static void Set<T>(this ISession session, T instance, string key = null) {
            if (string.IsNullOrEmpty(key))
                key = typeof(T).FullName;
            session.Set(key, GetBytes(JsonConvert.SerializeObject(instance)));
        }
        //====================
        //=== GET USUARIO ====
        //====================
        public static ListUsuarios GetUsuario(this ISession session) => session.Get<ListUsuarios>() == null ? session.Get<ListUsuarios>(SessionKeys.Usuario) : session.Get<ListUsuarios>();
        public static bool isLogado(this ISession session) {
            if (session.TryGetValue(typeof(ListUsuarios).FullName, out byte[] bytesUsr))
                if (!string.IsNullOrEmpty(GetString(bytesUsr)))
                    return true;
            if (session.TryGetValue(SessionKeys.Usuario, out byte[] bytes))
                if (!string.IsNullOrEmpty(GetString(bytes)))
                    return true;
            return false;
        }

        private static byte[] GetBytes(string str) => Encoding.UTF8.GetBytes(str);
        private static string GetString(byte[] bytes) => Encoding.UTF8.GetString(bytes);
    }
    public static class SessionKeys {
        public static readonly string Usuario = "Usuario";

    }
}