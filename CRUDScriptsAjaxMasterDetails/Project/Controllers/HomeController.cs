using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MyDatabaseEntities entities = new MyDatabaseEntities();
            List<Customer> customers = entities.Customers.ToList();

            //Add a Dummy Row.
            customers.Insert(0, new Customer());
            return View(customers);
        }

        [HttpPost]
        public JsonResult InsertCustomer(Customer customer)
        {
            using (MyDatabaseEntities entities = new MyDatabaseEntities())
            {
                entities.Customers.Add(customer);
                entities.SaveChanges();
            }

            return Json(customer);
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int customerId)
        {
            using (MyDatabaseEntities entities = new MyDatabaseEntities())
            {
                Customer customer = (from c in entities.Customers
                                     where c.CustomerId == customerId
                                     select c).FirstOrDefault();
                entities.Customers.Remove(customer);
                entities.SaveChanges();
            }

            Response.Redirect("http://www.microsoft.com");
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult UpdateCustomer(Customer customer)
        {
            using (MyDatabaseEntities entities = new MyDatabaseEntities())
            {
                Customer updatedCustomer = (from c in entities.Customers
                                            where c.CustomerId == customer.CustomerId
                                            select c).FirstOrDefault();
                updatedCustomer.Name = customer.Name;
                updatedCustomer.Address = customer.Address;
                updatedCustomer.OrderDate = customer.OrderDate;
                updatedCustomer.Phone = customer.Phone;
                entities.SaveChanges();
            }

            return new EmptyResult();
        }
    }
}