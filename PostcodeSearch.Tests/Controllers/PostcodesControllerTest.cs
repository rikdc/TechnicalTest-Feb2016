using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PostcodeSearch.Models;
using PostcodeSearch.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PostcodeSearch.Tests.Controllers
{
    [TestClass]
    public class PostcodesControllerTest
    {
        private PostcodeService GetService(List<Postcodes> listOfRecords)
        {
            var mockSet = new Mock<DbSet<Postcodes>>();

            var data = listOfRecords.AsQueryable();

            mockSet.As<IQueryable<Postcodes>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Postcodes>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Postcodes>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Postcodes>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PostcodeSearchContext>();
            mockContext.Setup(c => c.Postcodes).Returns(mockSet.Object);

            return new PostcodeService(mockContext.Object); 
        }

        [TestMethod]
        public void Index_ReturnsSourceAndOneResult()
        {
            // https://www.ordnancesurvey.co.uk/resources/maps-and-geographic-resources/calculating-distances-using-grid-references.html
            var data = new List<Postcodes> 
            { 
                new Postcodes { Posttown = "Southampton", Postcode = "AB19YX", Easting = 4387, Northing = 1148 }, 
                new Postcodes { Posttown = "Tower of London", Easting = 5336, Northing = 1805}, 
            };

            var service = GetService(data);            
            var postcodes = service.GetWithinDistance("AB19YX", 1155);

            Assert.AreEqual(2, postcodes.ToList().Count()); 
        }

        [TestMethod]
        public void Index_ReturnsOnlyTheSource()
        {
            // https://www.ordnancesurvey.co.uk/resources/maps-and-geographic-resources/calculating-distances-using-grid-references.html
            var data = new List<Postcodes> 
            { 
                new Postcodes { Posttown = "Southampton", Postcode = "AB19YX", Easting = 4387, Northing = 1148 }, 
                new Postcodes { Posttown = "Tower of London", Easting = 5336, Northing = 1805}, 
            };

            var service = GetService(data);
            var postcodes = service.GetWithinDistance("AB19YX", 0);

            Assert.AreEqual(1, postcodes.Count());
            Assert.AreEqual(data[0], postcodes.First());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Index_ExceptionWhenNoPostcodeFound()
        {
            // https://www.ordnancesurvey.co.uk/resources/maps-and-geographic-resources/calculating-distances-using-grid-references.html
            var data = new List<Postcodes> 
            { 
                new Postcodes { Posttown = "Southampton", Postcode = "AB19YX", Easting = 4387, Northing = 1148 }, 
                new Postcodes { Posttown = "Tower of London", Easting = 5336, Northing = 1805}, 
            };

            var service = GetService(data);
            var postcodes = service.GetWithinDistance("DOESNT_EXIST", 0);

            Assert.AreEqual(0, postcodes.ToList().Count());
        }
    }
}
