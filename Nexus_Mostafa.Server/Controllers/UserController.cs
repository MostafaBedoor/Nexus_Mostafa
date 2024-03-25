using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Nexus_Mostafa.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _config;
        string usersJSON = "";

        public UserController(ILogger<UserController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            usersJSON = System.IO.File.ReadAllText(Path.GetFullPath(_config["FileRelativePath"]));
            return JsonConvert.DeserializeObject<List<User>>(usersJSON);
        }

        [Route("Search/{field}/{value}")]
        [HttpGet]
        public List<User> Search(string field, object value)
        {
            usersJSON = System.IO.File.ReadAllText(Path.GetFullPath(_config["FileRelativePath"]));
            PropertyInfo property = typeof(User).GetProperty(field);
            List<User> myList = JsonConvert.DeserializeObject<List<User>>(usersJSON);

            if (property.PropertyType.Equals(typeof(string)))
                myList = myList.Where(u => property.GetValue(u, null).ToString().Contains(value.ToString(), StringComparison.InvariantCultureIgnoreCase)).ToList();

            else if (property.PropertyType.Equals(typeof(int)))
                myList = myList.Where(u => Convert.ToInt32(property.GetValue(u, null)).Equals(Convert.ToInt32(value))).ToList();

            else if (property.PropertyType.Equals(typeof(bool)))
                myList = myList.Where(u => Convert.ToBoolean(property.GetValue(u, null)).Equals(Convert.ToBoolean(value))).ToList();

            else if (property.PropertyType.Equals(typeof(DateTime)))
                myList = myList.Where(u => Convert.ToDateTime(property.GetValue(u, null)).Equals(Convert.ToDateTime(value))).ToList();
            return myList;
        }
    }
}
