using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyShoppingCart.Repository.SqlServer
{
    public class UserRepository
    {
        private readonly string _cnf;
        public UserRepository(string cnf)
        {
            _cnf = cnf;
        }


        public List<User> GetAll()
        {
            var userList = new List<User>();
            try
            {
                using (SqlConnection con = new SqlConnection(_cnf))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ShoppingCart_Users", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var user = new User();

                        user.userId = Convert.ToInt32(rdr["UserId"]);
                        user.name = rdr["Name"].ToString();
                        user.email = rdr["Email"].ToString();
                        user.password = rdr["Password"].ToString();
                        userList.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }

            return userList;
        }

    }

        
    
}
