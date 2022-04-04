using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using YumJTest.Models;
using static YumJTest.Controllers.AdditionalProcessing;

namespace YumJTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : Controller
    {   
        #region CreatePost  ++++++++++++++++++++++ Create post in db if post with this id isn`t exist in db
        // Make request to endpoint: https://jsonplaceholder.typicode.com/posts
        // and select post by Id;
        // Insert into Posts with UserId.
        // If post with this Id already exists - return response BadRequest with message.
        // Otherwise - return success re-sponse.

        // POST Url: posts/[userId]/[id]
        // posts/1/1
        [HttpPost("{userId}/{id}")]
        public IActionResult Post(int userId, int id)
        {
            Posts? obj;
            string result = "";
            string status;

            var urlToGetData = $"https://jsonplaceholder.typicode.com/posts/{id}";

            var httpRequestToGetData = (HttpWebRequest)WebRequest.Create(urlToGetData);

            string resultToGetData = "";

            try
            {
                var httpResponseToGetData = (HttpWebResponse)httpRequestToGetData.GetResponse();

                StreamRead(httpResponseToGetData, out resultToGetData);

                obj = System.Text.Json.JsonSerializer.Deserialize<Posts>(resultToGetData); // вытянул пост по id 

                return BadRequest($"Id: {id} is already exists databases, please try enother id"); // If post with this Id already exists - return response BadRequest with message.

            }
            catch (WebException)
            {
                var url = "https://jsonplaceholder.typicode.com/posts";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                httpRequest.ContentType = "application/json";

                Posts post = new Posts() { Id = id, UserId = userId, Body = $"body{id}", Title = $"title{userId}" };

                var data = System.Text.Json.JsonSerializer.Serialize(post);

                StreamWrite(httpRequest, data);

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                StreamRead(httpResponse, out result);

                status = httpResponse.StatusCode.ToString();
            }
            return new ObjectResult($"Success response, {status}");
        }

        #endregion


        #region GetUserPosts    ++++++++++++++++++++++++++++++++ Get all posts by userId
        // Get list of posts by UserId.
        // Return success response with list of posts;

        // GET posts/[userId]
        // example: posts/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            string result;
            List<Posts> posts = new List<Posts>();
            string resultSendToClient;

            var url = $"https://jsonplaceholder.typicode.com/posts";

            try
            {
                var httpResponse = RequestResponce(url); 

                StreamRead(httpResponse, out result);

                var list = JsonConvert.DeserializeObject<List<Posts>>(result);
                posts = list.FindAll(x => x.UserId == id);  

                resultSendToClient = JsonConvert.SerializeObject(posts);
            }
            catch (WebException e)
            {
                return NotFound(e.Message);
            }

            if (posts.Count == 0)
                return NotFound($"Id: {id} does not exist in databases, please try enother id");

            return new ObjectResult(resultSendToClient);
        }
        #endregion


        #region GetPost  +++++++++++++++++++
        // Get post by Id;
        // If post is not found - return Not Found response;

        // GET posts/post/1
        [HttpGet("{post}/{id}")]
        public ActionResult Get(string post, int id)
        {
            string result;

            var url = $"https://jsonplaceholder.typicode.com/posts/{id}";

            try
            {
                var httpResponse = RequestResponce(url);

                StreamRead(httpResponse, out result);
            }
            catch (WebException)
            {
                return NotFound("Not found!"); //BadRequest("No found!");
            }
            return new ObjectResult(result);
        }
        #endregion


        #region UpdatePost  +++++++++++++++++++++++++++++++ 
        // Update title and body for post by Id.
        // If post not found return Not Found re-sponse.
        // If updated - return No Content

        // PUT posts/5
        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            List<Posts> posts = new List<Posts>();
            bool isFind = false;
            string status;
            string result;

            #region ищу по всем, если нахожу делаю update, если нет - NotFound($"No Content!");
            var urlGetAllData = "https://jsonplaceholder.typicode.com/posts";

            var httpResponseGetAllData = RequestResponce(urlGetAllData);

            StreamRead(httpResponseGetAllData, out result);

            var list = JsonConvert.DeserializeObject<List<Posts>>(result);
            posts = list.FindAll(x => (x.Id == id));

            if(posts.Count != 0)
            {
                foreach (var post in posts)
                {
                    if (post.Id == id)
                    {
                        isFind = true;
                        break;
                    }
                }
            }

            if (!isFind)
                return NotFound($"No Content!");
            #endregion

            var url = $"https://jsonplaceholder.typicode.com/posts/{id}";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "PUT";

            httpRequest.ContentType = "application/json";

            var data = @"{
                ""title"": ""Updated title"",
                ""body"": ""Updated body""
            }";

            StreamWrite(httpRequest, data);

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

            StreamRead(httpResponse, out result);

            return Ok(); 
        }

        #endregion


        #region DeletePost  +++++++++++++++++++++++++++++++++++++++ 
        // Delete post by Id.
        // If post not found return Not Found response.
        // If deleted - return No Content;

        // DELETE posts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isFind = false;
            List<Posts> posts;

            #region получаю данные с базы данных для сравнения по id 
            string result;

            var url = "https://jsonplaceholder.typicode.com/posts";

            var httpResponse = RequestResponce(url);
 
            StreamRead(httpResponse, out result);

            posts = JsonConvert.DeserializeObject<List<Posts>>(result);

            foreach (var post in posts)
            {
                if (post.Id == id)
                {
                    isFind = true;
                    break;
                }
            }

            if (!isFind)
                return NotFound($"No Content! \nPost with id: {id} does not exist in databases, please try enother id");
            #endregion

            var urlToDelete = $"https://jsonplaceholder.typicode.com/posts/{id}";

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