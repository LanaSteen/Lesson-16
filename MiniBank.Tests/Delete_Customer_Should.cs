using MiniBankFiles.Models;
using MiniBankRepository;

namespace MiniBank.Tests
{
    public class Dleete_Customer_Should
    {
        [Fact]
        public void Delete_Customer()
        {
      
            string _filePath = @"../../../Data/Customers.csv";


            // Arrange
            var repo = new CustomerCSVRepository(_filePath);
           

                // Act
                repo.Delete(2);

                // Assert
              
            
        }
    }
}