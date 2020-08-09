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
        private static readonly string USER_KEY = "Auth";
        private static readonly string CART_KEY = "KEY_CART";


        public static object GetObject<T>(ISession session, string key) {
          
            string dataAsString = session.GetString(key);
            try
            {
               return JsonConvert.DeserializeObject<T>(dataAsString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
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
            var obj = GetObject<User>(session, USER_KEY);
            User user = obj == null ? null :(User) obj;
            return user;
        }
        public static void SetUser(ISession session, Object user) {
            SetObject(session,USER_KEY, user);
        }
       
        public static Cart GetCart(ISession session)
        {
            var obj = GetObject<Cart>(session, CART_KEY);
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
