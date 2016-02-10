using PostcodeSearch.Models;
using PostcodeSearch.Services;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PostcodeSearch.Controllers
{
    public class PostcodesController : ApiController
    {
        static PostcodeSearchContext _db = new PostcodeSearchContext();
        private PostcodeService _service = new PostcodeService(_db);

        public IQueryable<Postcodes> GetPostcodes(string postCode, int distance)
        {
            try
            {
                return _service.GetWithinDistance(postCode, distance);
            }
            catch (ArgumentException)
            {
                throw new HttpException("That postcode does not exist in the database");
            }
        }
    }
}