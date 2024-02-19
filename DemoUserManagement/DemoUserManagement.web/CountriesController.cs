using DemoUserManagement.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DemoUserManagement.web
{
    public class CountriesController : ApiController
    {
        [HttpGet]
        [Route("api/countries")]
        public IHttpActionResult GetCountries()
        {
            try
            {
                // Call your business logic to get countries data
                var countries = UserBusiness.GetCountries();

                // Transform the list of CountryViewModel to an anonymous object with required properties
                var countryData = countries.Select(c => new { countryID = c.CountryID, countryName = c.CountryName });

                // Serialize the data to JSON and return as IHttpActionResult
                return Ok(countryData);
            }
            catch (Exception ex)
            {
                // Log the error
                System.Diagnostics.Debug.WriteLine("Error fetching countries: " + ex.Message);

                // Return an HTTP error response with appropriate status code
                return InternalServerError(ex);
            }
        }
    }
}
