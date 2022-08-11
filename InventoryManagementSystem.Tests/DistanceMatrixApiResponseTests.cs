using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem.Models;
using Xunit;

namespace InventoryManagementSystem.Tests
{
    public class DistanceMatrixApiResponseTests
    {

        [Theory]
        [InlineData(10, 3.5, 2.06)]
        [InlineData(300, 2.94, 51.88)]
        public void ShouldGetTripCost(double distance, double fuelPrice, double expected)
        {
            //Act
            double actual = DistanceMatrixApiResponse.GetTripCost(distance, fuelPrice);
            //Assert
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(5, 20)]
        [InlineData(1, 20)]
        [InlineData(10, 40)]
        [InlineData(7.3, 28)]
        [InlineData(7.8, 32)]
        public void ShouldGetSuggestedDeliveryFee(double distance, int expected)
        {
            //Act
            double actual = DistanceMatrixApiResponse.GetSuggestedDeliveryFee(distance);
            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
