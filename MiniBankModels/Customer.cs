using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBankFiles.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public CustomerType Type { get; set; }
        //public List<Account> Accounts { get; set; } = new List<Account>();

        public static Customer Parse(string line)
        {
            string[] values = line.Split(',');
            return new Customer
            {
                Id = int.Parse(values[0]),
                Name = values[1],
                IdentityNumber = values[2],
                PhoneNumber = values[3],
                Email = values[4],
                Type = (CustomerType)int.Parse(values[5])
            };
        }

        public static Customer[] ParseAll(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var customers = new Customer[lines.Length - 1];

            for (int i = 1; i < lines.Length; i++)
            {
                customers[i - 1] = Parse(lines[i]);
            }

            return customers;
        }

        public override string ToString()
        {
            return $"(ID: {Id}, Name: {Name}, Phone: {PhoneNumber}, Email: {Email}, Type: {Type})";
        }

        public static int AddCustomer(string filePath, Customer newCustomer)
        {
           
            var customers = ParseAll(filePath);
            int nextId = (customers.Length > 0) ? customers[^1].Id + 1 : 1; 

            
            foreach (var customer in customers)
            {
                if (customer.IdentityNumber.Equals(newCustomer.IdentityNumber, StringComparison.OrdinalIgnoreCase))
                {
                    throw new ArgumentException("Identity Number must be unique.");
                }
            }

            newCustomer.Id = nextId;

            string line = $"{newCustomer.Id},{newCustomer.Name},{newCustomer.IdentityNumber},{newCustomer.PhoneNumber},{newCustomer.Email},{(int)newCustomer.Type}";
            File.AppendAllText(filePath, line + Environment.NewLine);
            return newCustomer.Id;
        }


    }

    public enum CustomerType
    {
        Personal = 0,
        Business = 1
    }
}
