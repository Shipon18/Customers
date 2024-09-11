using Microsoft.AspNetCore.Mvc;
using System;

namespace CompanyShamim.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private static readonly List<Customer> customers = new List<Customer>
        {
            new Customer
            {
                Id = 1,
                CustomerId = "CUST001",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            },
            new Customer
            {
                Id = 2,
                CustomerId = "CUST002",
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com"
            },
            new Customer
            {
                Id = 3,
                CustomerId = "CUST003",
                FirstName = "Michael",
                LastName = "Johnson",
                Email = "michael.johnson@example.com"
            },
            new Customer
            {
                Id = 4,
                CustomerId = "CUST004",
                FirstName = "Emily",
                LastName = "Davis",
                Email = "emily.davis@example.com"
            },
            new Customer
            {
                Id = 5,
                CustomerId = "CUST005",
                FirstName = "David",
                LastName = "Wilson",
                Email = "david.wilson@example.com"
            }
        };

        public CustomerController() { }

        [HttpGet]
        [Route("GetCustomers")]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email
            }).ToList();
        }

        [HttpGet]
        [Route("GetCustomer/id")]
        public CustomerDto GetCustomer(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer is null)
            {
                return null;
            }
            return new CustomerDto()
            {
                Id = customer.Id,
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };
        }

        [HttpPost]
        [Route("AddCustomer")]
        public int AddCustomer(CustomerUpsertRequest request)
        {
            var newCustomer = new Customer()
            {
                Id = GetId(),
                CustomerId = GenerateCustomerId(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };
            customers.Add(newCustomer);
            return newCustomer.Id;
        }

        [HttpPut]
        [Route("UpdateCustomer")]
        public bool UpdateCustomer(int id, [FromBody] CustomerUpsertRequest request)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer is null)
            {
                return false;
            }
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Email = request.Email;

            var index = customers.FindIndex(c => c.Id == id);
            customers[index] = customer;
            return true;
        }

        [HttpDelete]
        [Route("DeleteCustomer")]
        public bool DeleteCustomer(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer is null)
            {
                return false;
            }
            customers.Remove(customer);
            return true;
        }

        private int GetId()
        {
            return customers.Count + 1;
        }

        public static string GenerateCustomerId()
        {
            var random = new Random();
            int randomNumber = random.Next(1000000, 10000000);
            return randomNumber.ToString();
        }
    }
}
