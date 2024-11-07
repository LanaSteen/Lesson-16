using MiniBankFiles.Models;
using MiniBankRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MiniBankRepository
{
    public class CustomerCSVRepository
    {

        
        string _filePath;


        public CustomerCSVRepository(string filePath)
        {
            _filePath = filePath;
        }
        public List<Customer> GetCustomers()
        {
            var customers = new List<Customer>();
            try
            {
                var lines = File.ReadAllLines(_filePath);
                for (int i = 1; i < lines.Length; i++)  
                {
                    customers.Add(Customer.Parse(lines[i]));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
            return customers;
        }

  
        public Customer GetCustomer(int id)
        {
            var customers = GetCustomers();
            return customers.FirstOrDefault(c => c.Id == id);
        }

      
        public void Create(Customer customer)
        {
            try
            {
              
                int newId = Customer.AddCustomer(_filePath, customer);
                Console.WriteLine($"Customer added with ID: {newId}");
            }
          
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }

    
        public void Update(Customer customer)
        {
            try
            {
                var customers = GetCustomers();
                var customerToUpdate = customers.FirstOrDefault(c => c.Id == customer.Id);

                if (customerToUpdate != null)
                {
                    var updatedCustomers = customers.Select(c => c.Id == customer.Id ? customer : c).ToList();

                    WriteAllCustomersToFile(updatedCustomers);
                    Console.WriteLine($"Customer with ID {customer.Id} updated.");
                }
                else
                {
                    Console.WriteLine($"Customer with ID {customer.Id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating customer: {ex.Message}");
            }
        }

    
        public void Delete(int id)
        {
            try
            {
                var customers = GetCustomers();
                var customerToDelete = customers.FirstOrDefault(c => c.Id == id);

                if (customerToDelete != null)
                {
                    
                    customers.Remove(customerToDelete);

                    WriteAllCustomersToFile(customers);
                    Console.WriteLine($"Customer with ID {id} deleted.");
                }
                else
                {
                    Console.WriteLine($"Customer with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting customer: {ex.Message}");
            }
        }


        private void WriteAllCustomersToFile(List<Customer> customers)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("Id,Name,IdentityNumber,PhoneNumber,Email,Type");  
                foreach (var customer in customers)
                {
                    sb.AppendLine($"{customer.Id},{customer.Name},{customer.IdentityNumber},{customer.PhoneNumber},{customer.Email},{(int)customer.Type}");
                }

                File.WriteAllText(_filePath, sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing customers to file: {ex.Message}");
            }
        }

    }
}




//var repo = new CustomerCSVRepository(@"C:\path\to\Customers.csv");

//// Get all customers
//var customers = repo.GetCustomers();
//foreach (var customer in customers)
//{
//    Console.WriteLine(customer);
//}

//// Get a customer by ID
//var customerById = repo.GetCustomer(1);
//Console.WriteLine(customerById);

//// Create a new customer
//var newCustomer = new Customer
//{
//    Name = "John Doe",
//    IdentityNumber = "1234567890",
//    PhoneNumber = "555-5555",
//    Email = "johndoe@example.com",
//    Type = CustomerType.Personal
//};
//repo.Create(newCustomer);

//// Update an existing customer
//var existingCustomer = repo.GetCustomer(1);
//if (existingCustomer != null)
//{
//    existingCustomer.PhoneNumber = "555-9999";
//    repo.Update(existingCustomer);
//}

//// Delete a customer by ID
//repo.Delete(1);