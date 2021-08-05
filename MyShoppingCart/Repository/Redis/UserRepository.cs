using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShoppingCart.Repository.Redis
{
    public class UserRepository
    {
        private readonly ICacheService _cache;
        private string sKey = "user";
        public UserRepository(ICacheService cache)
        {
            _cache = cache;
        }

        public List<User> GetListUsers()
        {

            try
            {
                var cached = _cache.Get<List<User>>(sKey);

                if (cached == null)
                {
                    _cache.Set(sKey, ListUserData());
                    cached = _cache.Get<List<User>>(sKey);

                }



                return cached;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

        }


        private List<User> ListUserData()
        {
            var list = new List<User>();

            list.Add(new User { name = "Jhon Doe", email="jd@gmail.com",userId=1, password= "123456" });
            list.Add(new User { name = "Me", email = "me@gmail.com", userId = 2, password = "123456" });
            list.Add(new User { name = "Customer1", email = "cust1@gmail.com", userId = 2, password = "123456" });
            list.Add(new User { name = "Customer2", email = "cust2@gmail.com", userId = 3, password = "123456" });

            return list;
        }

    }
}
