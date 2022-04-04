using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using YumJTest.Models;
using static YumJTest.Controllers.AdditionalProcessing;

namespace YumJTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        #region GetUsers +++++++++++ Used to get all Users from db
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            string result;

            string url = "https://jsonplaceholder.typicode.com/users";

            HttpWebResponse httpResponse = RequestResponce(url);

            StreamRead(httpResponse, out result);

            User[] users = JsonConvert.DeserializeObject<User[]>(result);

            return users;
        }
        #endregion

        #region     CreateUser POST  ++++++++++++++++++++++++++  Used to create user in db
        // Make request to endpoint: 
        // https://jsonplaceholder.typicode.com/users
        // and select user by Id; Save this user to table users
        // return success response. If user with this Id already
        // exists - return response as BadRequest with some message;

        // POST users/5
        [HttpPost("{id}")]
        public IActionResult Post(int id)      //  public async Task<ActionResult<User>> Post(int id)
        {
            string result = "";

                    #region (поиск по id) получаю 1 запись в базе данных чтоб проверить на наличие по id

            List<User> users = new List<User>();

            var urlToGetData = $"https://jsonplaceholder.typicode.com/users/{id}";

            var httpRequestToGetData = (HttpWebRequest)WebRequest.Create(urlToGetData);

            string resultToGetData = "";

            try
            {
                var httpResponseToGetData = (HttpWebResponse)httpRequestToGetData.GetResponse();

                StreamRead(httpResponseToGetData, out resultToGetData);

                User? obj = System.Text.Json.JsonSerializer.Deserialize<User>(resultToGetData);

                return BadRequest($"Id: {id} is already exists databases, please try enother id");
                    
                     #endregion
            }
            catch (WebException)
            {
                var url = "https://jsonplaceholder.typicode.com/users"; // "https://my-json-server.typicode.com/SergiiGrynevych/YumJTest/users";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                httpRequest.ContentType = "application/json";

                User user = new User() { Id = id
                                        ,Name = $"Thomas{id}"
                                        ,Username = $"userThomas{id}"
                                        ,Email = $"Thomas{id}.gmail.com" };

                var data = System.Text.Json.JsonSerializer.Serialize(user);

                StreamWrite(httpRequest, data);

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                StreamRead(httpResponse, out result);
            }
            return new ObjectResult(result);
        }
        #endregion

        #region GetUser  ++++++++++++++++++++++ Used to get user by id from db
        // Get user object from table by Id.
        // Return success response with user object;
        // If user not found, return response NotFound;

        // GET users/5
        [HttpGet("{id}")]   
        public IActionResult Get(int id) 
        {
            string result;

            var url = $"https://jsonplaceholder.typicode.com/users/{id}";

            try
            {
                HttpWebResponse httpResponse = RequestResponce(url);

                StreamRead(httpResponse, out result);
            }
            catch (WebException)
            {
                return NotFound($"Id: {id} does not exist in databases, please try enother id");
            }
            return new ObjectResult(result);
        }
        #endregion

        #region DeleteUser ++++++++++ Used to delete user from db
        // Delete user from table by Id.
        // Return success response with NoContent.
        // If user not found, return response NotFound;

        // DELETE users/5
        [HttpDelete("{id}")] 
        public IActionResult Delete(int id)   // ++++++++++++++++
        {
            bool isFind = false;
            #region получаю данные с базы данных для сравнения по id 
            string result;

            var url = "https://jsonplaceholder.typicode.com/users"; 

            var httpResponse = RequestResponce(url); 

            StreamRead(httpResponse, out result);

            User[] users1 = JsonConvert.DeserializeObject<User[]>(result);

            foreach(var user in users1)
            {
                if(user.Id == id)
                {
                    isFind = true;
                    break;
                }
            }
            #endregion

            if (!isFind)
                return NotFound($"Id: {id} does not exist in databases, please try enother id");

            var urlToDelete = $"https://jsonplaceholder.typicode.com/users/{id}";

            var httpRequestToDelete = (HttpWebRequest)WebRequest.Create(urlToDelete);
            httpRequestToDelete.Method = "DELETE";

            var httpResponseToDelete = (HttpWebResponse)httpRequestToDelete.GetResponse();

            StreamRead(httpResponseToDelete, out result);

            if (httpResponseToDelete.StatusCode.ToString() == "OK")
                return Ok();

            return null;

        }
        #endregion

    }
}
