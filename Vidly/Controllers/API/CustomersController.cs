using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class CustomersController : ApiController
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
        
        // GET /api/customers
        public IHttpActionResult GetCustomers()
        {
            return Ok(_Context.Customers.ToList().Select( Mapper.Map<Customer, CustomerDTO>));
        }

        // GET /api/customer/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _Context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null) return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDTO>(customer));
        }

        // POST /api/customers
        public IHttpActionResult CreateCustomer(CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            var customer = Mapper.Map<CustomerDTO, Customer>(customerDTO);
            _Context.Customers.Add(customer);
            _Context.SaveChanges();

            customerDTO.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customerDTO.Id), customerDTO);
        }

        // PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _Context.Customers.SingleOrDefault(c => c.Id == customerDTO.Id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Mapper.Map(customerDTO, customerInDb);

            _Context.SaveChanges();

            return Ok();

        }

        // DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customer = _Context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _Context.Customers.Remove(customer);
            _Context.SaveChanges();

            return Ok();

        }


    }
}
