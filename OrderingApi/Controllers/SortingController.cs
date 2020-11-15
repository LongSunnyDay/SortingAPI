using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace Sorting.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SortingController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> SortNumbersList(int[] pNumbers)
        {
            try
            {
                if (pNumbers.Length == 0 || pNumbers == null)
                {
                    return BadRequest();

                }
                Models.Sorting vSorter = new Models.Sorting();
                var vSortedArray = vSorter.SortNumbers(pNumbers);

                return "Sorted array of numbers:" +
                    Environment.NewLine + string.Join(", ", vSortedArray);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error during array sorting");
            }
        }

        [HttpGet]
        public ActionResult<string> GetLastSortRecord()
        {
            return "Hello there";
        }
    }
}
