using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyShoppingCart.Repository.SqlServer
{
    public class ProductRepository
    {
        private readonly string _cnf;
        public ProductRepository(string cnf)
        {
            _cnf = cnf;
        }


        public List<Product> GetAll()
        {
            var ProductList = new List<Product>();
            try
            {
                using (SqlConnection con = new SqlConnection(_cnf))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [Product]", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var Product = new Product();

                        Product.productId = Convert.ToInt32(rdr["ProductId"]);
                        Product.code = rdr["Code"].ToString();
                        Product.name = rdr["Name"].ToString();
                        Product.price = Convert.ToDecimal(rdr["Price"]);
                        Product.type = Convert.ToInt32(rdr["Type"].ToString());
                        ProductList.Add(Product);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            return ProductList;
        }

    }



}
