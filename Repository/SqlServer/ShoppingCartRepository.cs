using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyShoppingCart.Repository.SqlServer
{
    public class ShoppingCartRepository
    {
        private readonly string _cnf;
        public ShoppingCartRepository(string cnf)
        {
            _cnf = cnf;
        }


        //public List<ShoppingCart> GetAll()
        //{
        //    var shoppingList = new List<User>();
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(_cnf))
        //        {
        //            SqlCommand cmd = new SqlCommand("SELECT * FROM ShoppingCart_Users", con);
        //            cmd.CommandType = CommandType.Text;
        //            con.Open();

        //            SqlDataReader rdr = cmd.ExecuteReader();

        //            while (rdr.Read())
        //            {
        //                var shopping = new ShoppingCart();

        //                shopping.shoppingCartId = Convert.ToInt32(rdr["ShoppingCartId"]);
        //                user.name = rdr["ProductId"].ToString();
        //                user.email = rdr["Email"].ToString();
        //                user.password = rdr["Password"].ToString();
        //                shoppingList.Add(user);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //    }

        //    return shoppingList;
        //}


        public ShoppingCart Post(ShoppingCart shopping)
        {
            var save_shoppingCart = new ShoppingCart();
            using (SqlConnection con = new SqlConnection(_cnf))
            {
                var squery = @"

                    INSERT INTO [dbo].[ShoppingCart]
                               ([ProductId]
                               ,[UserId]
                               ,[Quantity]
                               ,[CreationDate])
                         VALUES
                               (@productid
                               ,@userid
                               ,@qty
                               ,GETDATE())


                ";
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@productid", shopping.productId);
                cmd.Parameters.AddWithValue("@userid", shopping.userId);
                cmd.Parameters.AddWithValue("@qty", shopping.quantity);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Records Inserted Successfully");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
                finally
                {
                    con.Close();
                }
            }

            return shopping;
        }

    }
}
