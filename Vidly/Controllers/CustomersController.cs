using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        private ApplicationDbContext _Context;

        public CustomersController()
        {
            _Context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _Context.Dispose();
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult New()
        {
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = _Context.MembershipTypes,
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CustomerFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.MembershipTypes = _Context.MembershipTypes;
                return View("CustomerForm", viewModel);
            }

            if (viewModel.Id == 0)    // a customer with id = 0 is a new customer
                _Context.Customers.Add(viewModel.CustomerBasedOnViewModel);
            else
            {
                var customerInDb = _Context.Customers.Single(c => c.Id == viewModel.Id);
                customerInDb.Name = viewModel.Name;
                customerInDb.BirthDate = viewModel.BirthDate;
                customerInDb.MembershipTypeId = viewModel.MembershipTypeId.Value;
                customerInDb.IsSubscribedToNewsletter = viewModel.IsSubscribedToNewsletter;
            }

            _Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit( int id)
        {
            var customer = _Context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel(customer)
            {
                MembershipTypes = _Context.MembershipTypes.ToList(),
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult Details( int id)
        {
            var customer = _Context.Customers.Include( c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
    }
}