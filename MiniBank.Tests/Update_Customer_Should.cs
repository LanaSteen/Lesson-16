using MiniBankFiles.Models;
using MiniBankRepository;

namespace MiniBank.Tests
{
    public class Update_Customer_Should
    {
        [Fact]
        public void Update_Customer()
        {
      
            string _filePath = @"../../../TestData/CustomersTest.csv";


            // Arrange
            var repo = new CustomerCSVRepository(_filePath);

            var customerToUpdate = repo.GetCustomer(1);
            customerToUpdate.Name = "Step Academy";  

            // Act
            repo.Update(customerToUpdate);

            // Assert
            var expected = "Step Academy";
            var updatedCustomer = repo.GetCustomer(1);
            Assert.NotNull(updatedCustomer);
            Assert.Equal(expected, updatedCustomer.Name);  

        }
    }
}