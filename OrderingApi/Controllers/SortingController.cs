using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sorting.Helpers;
using Sorting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sorting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortingController : Controller
    {
        private FilesHelper gFilesHelper = new FilesHelper();

        #region Requested functionality
        [HttpPost] // Sorts passed list and saves to file
        public ActionResult<string> SortNumbersList([FromBody] DataToSort pDataToSort)
        {
            try
            {
                if (pDataToSort.InputData.Length == 0 || pDataToSort.InputData == null)
                {
                    return BadRequest();
                }
                SortingHelper vSorter = new SortingHelper();
                int[] vSortedArray;
                try
                {
                    vSortedArray = vSorter.SortNumbers(pDataToSort.InputData);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error during array sorting: +" +
                        Environment.NewLine + ex.StackTrace);
                }
                
                return Ok(gFilesHelper.SaveSortedData(vSortedArray));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("latestRecord")] // Returns latest record
        public ActionResult<string> GetLastSortRecord()
        {
            try
            {
                return Ok(gFilesHelper.GetLatestRecord());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        #endregion

        #region Additional functionality
        [HttpGet] // Returns all endpoins
        public ActionResult<string> GetEndpointsList()
        {
            return Ok("EndpointsList:" +
                Environment.NewLine + "Endpoints list: \"\\\"" + 
                Environment.NewLine + "Records list: " + "\"\\list\"" +
                Environment.NewLine + "Record by ID: " + "\"\\{id}\"" +
                Environment.NewLine + "Latest record: " + "\"\\latestRecord\"" +
                Environment.NewLine + "Sort new list: POST to \"\\\" with JSON payload \"{ \"InputData\": [int array] }\"" +
                Environment.NewLine + "Delete record: DELETE to \"\\{id}\"" + 
                Environment.NewLine + "Delete to " + "\"\\all\"");
        }  

        [HttpGet("list")] // Returns of all available records
        public ActionResult<string> GetListOfRecords()
        {
            try
            {
                return Ok(gFilesHelper.GetAllRecordsList());
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")] // Returns single record by passed ID
        public ActionResult<string> GetRecord(int id)
        {
            try
            {
                return Ok(gFilesHelper.GetRecord(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")] // Removes records by passed ID
        public ActionResult<string> DeleteRecord(int id)
        {
            try
            {
                return Ok(gFilesHelper.DeleteFile(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("all")]
        public ActionResult<string> DeleteAllFiles()
        {
            try
            {
                return Ok(gFilesHelper.DeleteAllFiles());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        #endregion
    }
}
