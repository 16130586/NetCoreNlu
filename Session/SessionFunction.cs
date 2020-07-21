using Microsoft.AspNetCore.Http;
using NetProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.Session
{
    public static class SessionFunction
    {
        private static readonly string USER_KEY = "KEY_USER";
        private static readonly string CART_KEY = "KEY_CART";


        public static object GetObject(ISession session, string key) {
            object retuzn = null;
            string dataAsString = session.GetString(key);
            try
            {
                retuzn = JsonConvert.DeserializeObject(dataAsString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retuzn;
        }

        public static void SetObject(ISession session, string key, object data) {
            string dataAsString = null;
            try
            {
                dataAsString = JsonConvert.SerializeObject(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            session.SetString(key, dataAsString);
        }
        public static User GetUser(ISession session) {
            var obj = GetObject(session, USER_KEY);
            User user = obj == null ? null : obj as User;
            return user;
        }
        public static Cart GetCart(ISession session)
        {
            var obj = GetObject(session, CART_KEY);
            Cart cart = obj == null ? null : obj as Cart;
            return cart;
        }
        public static string GetString(ISession session, string key) {
            return session.GetString(key);
        }
        public static void SetString(ISession session, string key, string value) {
            session.SetString(key, value);
        }
        public static void Remove(ISession session, string key) {
            session.Remove(key);
        }
    }
}
