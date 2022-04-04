using Microsoft.AspNetCore.Mvc;
using System.Net;
using static YumJTest.Controllers.AdditionalProcessing;

namespace YumJTest.Controllers
{
    [Route("init")]
    [ApiController]
    public class DBInitController : Controller //ControllerBase
    {        
        #region     DBInit
        // Create tables Users and Posts;
        // POST init
        [HttpPost]
        public IActionResult Post()
        {
            #region Create Table Users
            string result;
            var urlUsers = "https://jsonplaceholder.typicode.com/users";

            var httpRequestUsers = (HttpWebRequest)WebRequest.Create(urlUsers);

            httpRequestUsers.Method = "POST";

            httpRequestUsers.ContentType = "application/json";

            var dataUserTable = @"{""users"":{""id"": 1111,
                          ""name"": """",
                          ""username"": """",
                          ""email"": """",
                          ""address"": """",
                          ""phone"": """",
                          ""website"": """",
                          ""company"": """"
                         }}";


            StreamWrite(httpRequestUsers, dataUserTable);

            var httpResponseUsers = (HttpWebResponse)httpRequestUsers.GetResponse();

            StreamRead(httpResponseUsers, out result);

            #endregion

            #region Create table posts

            var urlPosts = "https://jsonplaceholder.typicode.com/posts";

            var httpRequestPosts = (HttpWebRequest)WebRequest.Create(urlPosts);
            httpRequestPosts.Method = "POST";

            httpRequestPosts.ContentType = "application/json";

            var dataPosts = @"{""posts"":{
                          ""id"": 11111,
                          ""userId"": ""1111111"",
                          ""title"": """",
                          ""body"": """"
                         }}";

            StreamWrite(httpRequestPosts, dataPosts);

            var httpResponsePosts = (HttpWebResponse)httpRequestPosts.GetResponse();

            StreamRead(httpResponsePosts, out result);

            #endregion
            
            return StatusCode(201);
        }
        #endregion
    }
}
