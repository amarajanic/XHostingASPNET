using MVCApp.Models;
using MVCLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SignUp()
        {
            ViewBag.Message = "Your signup page.";

            return View();
        }
        public ActionResult LogIn()
        {
            ViewBag.Message = "Your login page";

            return View();
        }
        public ActionResult AddCustomerPacket()
        {
            ViewBag.Message = "Your Add Customer Packet page";
            

            return View();
        }
        [HttpPost]
        public ActionResult AddCustomerPacket(MVCLibrary.Models.CustomerModel model)
        {
            var packetId = Request.Form["hosting"];
            var userEmail = Request.Form["mail"];
            MVCLibrary.Models.CustomerModel temp = new MVCLibrary.Models.CustomerModel();

            temp = KonekcijaNaBazu.GetCustomerByMail(userEmail);

            if(temp==null)
            {
                MessageBox.Show("Korisnik ne postoji");
            }
            else
            {
                KonekcijaNaBazu.SaveCustomerPacket(temp.Id, int.Parse(packetId));
            }

            

            return View();
        }
        public ActionResult DashBoard()
        {
            ViewBag.Message = "Dash Board";
         
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(CustomerModel model)
        {
            List<MVCLibrary.Models.CustomerModel> temp = new List<MVCLibrary.Models.CustomerModel>();
            MVCLibrary.Models.CustomerModel c = new MVCLibrary.Models.CustomerModel();

            bool postojeciKorisnik=KonekcijaNaBazu.TrueCustomer(model.Email, model.Password);
            if(!postojeciKorisnik)
            {
                MessageBox.Show("Korisnik ne postoji!");
            }
            else
            {
                temp = KonekcijaNaBazu.LoadCustomers();
                foreach (var customer in temp)
                {
                    if(customer.Email==model.Email && customer.Password==model.Password)
                    {
                        c = customer;
                        break;
                    }
                }
                List<MVCLibrary.Models.CustomersPacketsModel> customersPackets = new List<MVCLibrary.Models.CustomersPacketsModel>();
                List<MVCLibrary.Models.PacketModel> tempPackets = new List<MVCLibrary.Models.PacketModel>();
                customersPackets= KonekcijaNaBazu.LoadCustomersPackets(c.Id);

                tempPackets = KonekcijaNaBazu.LoadAllPackets();

                List<MVCLibrary.Models.PacketModel> packets = new List<MVCLibrary.Models.PacketModel>();


                foreach (var customer in customersPackets)
                {
                    foreach (var tempPacket in tempPackets)
                    {
                        if(customer.PacketId==tempPacket.Id)
                        {
                            packets.Add(tempPacket);
                        }
                    }
                }


                return View("DashBoard",packets);
               
            }
            return View();
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(CustomerModel model)
        {

            if(ModelState.IsValid)
            {
                model.CustomerCode = RandomString(10);
                KonekcijaNaBazu.SaveCustomer(model.CustomerCode, model.FirstName, model.LastName,model.PhoneNumber, model.Email,model.Password);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}