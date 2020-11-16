using Microsoft.AspNetCore.Mvc;
using Sorting.Controllers;
using Sorting.Models;
using System;
using Xunit;

namespace Tests
{
    public class SortingControllerTest
    {
        SortingController _controller;

        public SortingControllerTest()
        {
            _controller = new SortingController();
        }
        #region Tests for requested functionality
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            var vResult = _controller.GetEndpointsList();
            Assert.IsType<OkObjectResult>(vResult.Result);
        }

        [Fact]
        public void SortArray_ReturnOkResult()
        {
            int[] vItemArray = new int[] { 10, 1, 26, 13, 3, 6, 48, -1 };
            var vResult = _controller.SortNumbersList(new DataToSort { InputData = vItemArray });
            Assert.IsType<OkObjectResult>(vResult.Result);
            _controller.DeleteAllFiles();
        }

        [Fact]
        public void SortEmptyArray_Returns_BadRequest()
        {
            int[] vItemArray = new int[] { };
            var vResult = _controller.SortNumbersList(new DataToSort { InputData = vItemArray });
            Assert.IsType<BadRequestResult>(vResult.Result);
        }

        [Fact]
        public void GetLastRecord_WhenEmpty_ReturnsNotFound()
        {
            var vResult = _controller.GetLastSortRecord();
            Assert.IsType<NotFoundObjectResult>(vResult.Result);
        }

        [Fact]
        public void GetLastRecord_WhenNotEmpty_ReturnsOkResult()
        {
            int[] vItemArray = new int[] { 10, 1, 26, 13, 3, 6, 48, -1 };
            _controller.SortNumbersList(new DataToSort { InputData = vItemArray });
            var vResult = _controller.GetLastSortRecord();
            Assert.IsType<OkObjectResult>(vResult.Result);
            _controller.DeleteAllFiles();
        }
        #endregion

        #region Tests for additional functionality
        [Fact]
        public void GetList_WhenEmpty_ReturnsNotFound()
        {
            _controller.DeleteAllFiles();
            var vResult = _controller.GetListOfRecords();
            Assert.IsType<NotFoundObjectResult>(vResult.Result);
        }

        [Fact]
        public void GetByUnknownID_ReturnsNotFound()
        {
            int vTestID = -1;
            var vResult = _controller.GetRecord(vTestID);
            Assert.IsType<NotFoundObjectResult>(vResult.Result);
        }

        [Fact]
        public void DeleteAll_WhenEmpty_ReturnsNotFound()
        {
            _controller.DeleteAllFiles();
            var vResult = _controller.DeleteAllFiles();
            Assert.IsType<NotFoundObjectResult>(vResult.Result);
        }

        [Fact]
        public void DeleteByID_WhenEmpty_ReturnsNotFound()
        {
            var vResult = _controller.DeleteRecord(1);
            Assert.IsType<NotFoundObjectResult>(vResult.Result);
        }

        [Fact]
        public void DeleteByID_WhenDoNotExists_ReturnsNotFound()
        {
            var vResult = _controller.DeleteRecord(-1);
            Assert.IsType<NotFoundObjectResult>(vResult.Result);
        }

        [Fact]
        public void GetByID_WhenExitst_ReturnsOk()
        {
            int[] vItemArray = new int[] { 10, 1, 26, 13, 3, 6, 48, -1 };
            _controller.SortNumbersList(new DataToSort { InputData = vItemArray });
            int vTestID = 1;
            var vResult = _controller.GetRecord(vTestID);
            Assert.IsType<OkObjectResult>(vResult.Result);
            _controller.DeleteAllFiles();
        }

        [Fact]
        public void GetList_WhenExists_ReturnsOk()
        {
            int[] vItemArray = new int[] { 10, 1, 26, 13, 3, 6, 48, -1 };
            _controller.SortNumbersList(new DataToSort { InputData = vItemArray });
            var vResult = _controller.GetListOfRecords();
            Assert.IsType<OkObjectResult>(vResult.Result);
            _controller.DeleteAllFiles();
        }

        [Fact]
        public void DeleteByID_WhenExists_ReturnsOk()
        {
            int[] vItemArray = new int[] { 10, 1, 26, 13, 3, 6, 48, -1 };
            _controller.SortNumbersList(new DataToSort { InputData = vItemArray });
            var vResult = _controller.DeleteRecord(1);
            Assert.IsType<OkObjectResult>(vResult.Result);
        }

        [Fact]
        public void DeleteAll_WhenNotEmpty_Ok()
        {
            int[] vItemArray = new int[] { 10, 1, 26, 13, 3, 6, 48, -1 };
            _controller.SortNumbersList(new DataToSort { InputData = vItemArray });
            var vResult = _controller.DeleteAllFiles();
            Assert.IsType<OkObjectResult>(vResult.Result);
        }
        #endregion
    }
}
