using MiniBankFiles.Models;
using MiniBankRepository;

namespace MiniBank.Tests
{
    public class Get_Customer_Should
    {
        [Fact]
        public void Get_Customer()
        {
      
            string _filePath = @"../../../Data/Customers.csv";


            // Arrange
            var repo = new CustomerCSVRepository(_filePath);




            var expected = "Beso";
                // Act
                var retrievedCustomer = repo.GetCustomer(2);

            // Assert
            //Assert.NotNull(retrievedCustomer);
           // Assert.Equal(2, retrievedCustomer.Id);
            Assert.Equal(expected, retrievedCustomer.Name.Split(" ")[0]);
            //Assert.Equal("12345", retrievedCustomer.IdentityNumber);

        }
    }
}