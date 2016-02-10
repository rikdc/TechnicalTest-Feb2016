using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using PostcodeSearch.Models;

namespace PostcodeSearch.Services
{
    public class PostcodeService
    {
         static PostcodeSearchContext _db = new PostcodeSearchContext();
        public PostcodeService(PostcodeSearchContext db)
        {
            _db = db;
        }

        public IQueryable<Postcodes> GetWithinDistance(string postcode, int distance)
        {
            var postcodeObj = _db.Postcodes.FirstOrDefault(p => p.Postcode == postcode);
            if (postcodeObj == null)
            {
                throw new ArgumentException("Postcode could not be found");
            }

            var items = _db.Postcodes
                .ToList()
                .Where(p => postcodeObj.DistanceFrom(p) <= distance);

            return items.AsQueryable();

        }
    }
}