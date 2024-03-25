using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nexus_Mostafa.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly UserController _userController;
        public UserAPIController(UserController userController) 
        {
            _userController = userController;
        } 

        [Route("Search/{field}/{value}")]
        [HttpGet]
        public List<User> search(string field, string value)
        {
            return _userController.Search(field, value);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
