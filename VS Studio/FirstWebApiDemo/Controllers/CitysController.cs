using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitysController : ControllerBase
    {
        List<string> cityList = new List<string>
        {
            "Delhi",
            "Pune",
            "Mumbai",
            "Chennai",
            "Hyderabad"
        };
        [Route("/JoiningCitys")]
        [Route("/CgLocations")]
        [HttpGet]
        public List<string> ShowAllCitys()
        {
            return cityList; 
        }
    }
}
