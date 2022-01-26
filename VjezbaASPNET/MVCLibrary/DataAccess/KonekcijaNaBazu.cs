using MVCLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MVCLibrary.DataAccess
{
    public static class KonekcijaNaBazu
    {
        public static string GetConnectionString(string cnn= "XHOSTDB")
        {
            return ConfigurationManager.ConnectionStrings[cnn].ConnectionString;
        }

        public static void SaveCustomer(string code,string ime,string prezime, string phone,string email,string password)
        {
            CustomerModel model = new CustomerModel()
            {
                CustomerCode = code,
                FirstName = ime,
                LastName = prezime,
                PhoneNumber = phone,
                Email = email,
                Password = password
            };
            using (IDbConnection db = new SqlConnection(GetConnectionString()))
            {
                db.Execute("insert into Customer (CustomerCode, FirstName, LastName, Phone, Email, Password) " +
                "values (@CustomerCode, @FirstName, @LastName, @PhoneNumber, @Email, @Password)",model);
            }
        }

        public static bool TrueCustomer(string email, string password)
        {
            using (IDbConnection db = new SqlConnection(GetConnectionString()))
            {
                var output = db.Query<LoginModel>("select Email,Password from Customer where Email='"+email+"' and Password='"+password+"'", new DynamicParameters()).ToList();
                if (output.Count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static List<CustomerModel> LoadCustomers()
        {
            using (IDbConnection db = new SqlConnection(GetConnectionString()))
            {
                var output = db.Query<CustomerModel>("select * from Customer", new DynamicParameters()).ToList();
                return output;
            }
        }

        public static List<CustomersPacketsModel> LoadCustomersPackets(int id)
        {
            using (IDbConnection db = new SqlConnection(GetConnectionString()))
            {
                var output = db.Query<CustomersPacketsModel>("select * from CustomersPackets where CustomerId="+id, new DynamicParameters()).ToList();
                return output;

            }
        }


        public static List<PacketModel> LoadPacketsWithId(int id)
        {
            using (IDbConnection db = new SqlConnection(GetConnectionString()))
            {
                var output = db.Query<PacketModel>("select * from Packet where Id=" + id, new DynamicParameters()).ToList();
                return output;
            }
        }

        public static List<PacketModel> LoadAllPackets()
        {
            using (IDbConnection db = new SqlConnection(GetConnectionString()))
            {
                var output = db.Query<PacketModel>("select * from Packet", new DynamicParameters()).ToList();
                return output;
            }
        }

        public static void SaveCustomerPacket(int cId,int pId)
        {
            CustomersPacketsModel model = new CustomersPacketsModel() { CustomerId = cId, PacketId = pId };
            using (IDbConnection db = new SqlConnection(GetConnectionString()))
            {
                db.Execute("insert into CustomersPackets values (@CustomerId, @PacketId)",model);
            }
        }

        public static CustomerModel GetCustomerByMail(string email)
        {
            var temp = LoadCustomers();
            CustomerModel output = new CustomerModel();

            foreach(var customer in temp)
            {
                if(customer.Email==email)
                {
                    output = customer;
                    break;
                }
            }
            return output;

        }
    }
}
