using BasharAndBilal.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasharAndBilal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerDetailsService _customerDetailsService;

        public CustomerController(ICustomerDetailsService customerDetailsService)
        {
            _customerDetailsService = customerDetailsService;
        }

        [HttpGet("GetCustomerDetails")]
        public IActionResult GetCustomerDetails()
        {
            // Path to your XML file
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "GetCustomerByCifResponse.xml");

            try
            {
                var customerDetails = _customerDetailsService.GetCustomerDetails(filePath);
                return Ok(customerDetails);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
