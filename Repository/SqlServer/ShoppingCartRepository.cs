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


        //public List<ShoppingCart> GetShoppingCartById()
        //{
        //    var shoppingList = new List<User>();
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(_cnf))
        //        {
        //            SqlCommand cmd = new SqlCommand("SELECT * FROM ShoppingCart_Users WHERE", con);
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
            try
            {
               
                using (SqlConnection con = new SqlConnection(_cnf))
                {
                    var squery = @"

                    INSERT INTO [dbo].[ShoppingCart]
                               ([ProductId]
                               ,[UserId]
                               ,[Quantity]
                               ,[CreationDate])
                    output INSERTED.ShoppingCartId 
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
                        int modified = (int)cmd.ExecuteScalar();

                        Console.WriteLine("Records Inserted Successfully");
                        save_shoppingCart = GetShoppingCartById(modified);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                        
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
           

            return save_shoppingCart;
        }

        public ShoppingCart GetShoppingCartById(int id)
        {
            var shoppingList = new List<ShoppingCart>();
            try
            {

                using (SqlConnection con = new SqlConnection(_cnf))
                {
                    var squery = @" SELECT  TOP 1 * FROM ShoppingCart WITH(NOLOCK) WHERE  ShoppingCartId = @id";
                    SqlCommand cmd = new SqlCommand(squery, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id);

                    try
                    {
                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            var shopping = new ShoppingCart();

                            shopping.shoppingCartId = Convert.ToInt32(rdr["ShoppingCartId"]);
                            shopping.productId = Convert.ToInt32(rdr["ProductId"]);
                            shopping.userId = Convert.ToInt32(rdr["UserId"]);
                            shopping.quantity = Convert.ToInt32(rdr["Quantity"]);
                            shoppingList.Add(shopping);
                        }

                        if (shoppingList.Count > 0)
                        {
                            return shoppingList[0];
                        }
                        else
                        {
                            return null;
                        }
                       
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);

                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
