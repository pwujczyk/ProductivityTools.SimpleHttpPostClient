using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductivityTools.SimpleHttpPostClient.CommonObjects;

namespace ProductivityTools.SimpleHttpPostClient.WebApi.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class TestController : Controller
    {
        [HttpPost]
        [Route("Index")]
        public string Index()
        {
            return "Test";
        }

        [HttpPost]
        [Route("Null")]
        public void Null(string s)
        {
            
        }

        [HttpPost]
        [Route("FillNameOut")]
        public ComplexObject FillNameOut([FromBody]ComplexObject obj)
        {
            var result = new ComplexObject();
            result.NameOut = obj.NameIn;
            return result;
        }
    }
}