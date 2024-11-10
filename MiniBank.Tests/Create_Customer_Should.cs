using MiniBankFiles.Models;
using MiniBankRepository;

namespace MiniBank.Tests
{
    public class Create_Customer_Should
    {
        [Fact]
        public void Create_Customer()
        {
      
            string _filePath = @"../../../TestData/CustomersTest.csv";


            // Arrange
            var repo = new CustomerCSVRepository(_filePath);

            var newCustomer = new Customer
            {
                Name = "Lana Steen",
                IdentityNumber = "123465",
                PhoneNumber = "599-765-4321",
                Email = "Lana@gmail.com",
                Type = CustomerType.Personal
            };
            var expected = "Lana Steen";
            var expected2 = "123456";
            // Act
            repo.Create(newCustomer);

            // Assert
            var customers = repo.GetCustomers();
            var createdCustomer = customers.FirstOrDefault(c => c.Name == "Lana Steen");

            Assert.NotNull(createdCustomer);
            Assert.Equal(expected, createdCustomer.Name);
            Assert.Equal(expected2, createdCustomer.IdentityNumber);


        }
    }
}