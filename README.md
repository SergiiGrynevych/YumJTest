# YumJTest

# Solution:
Implemented controllers:
- UserController, which implements all the logic for working with the user table
- PostsController, which implements all the logic for working with the posts table
- DBInitController, which implements all the logic for creating tables: users and posts
Folder "Controllers" have class AdditionalProcessing with basic logic
Folder "Models" have all basic classes needed at work


# Task:
Test Project
Create Web API integrated with https://jsonplaceholder.typicode.com/.
Recommendations:
.NET 6, Dapper, Postman, MSSQL.

DB structure:
Table Users:
 - Id; - FullName; - Username; - Phone; - Website; - Street; - Suite; - City; - Zipcode;

Table Posts:
 - Id; - UserId; - Title; - Body;

API endpoints:
DBInit
- Method: POST;
- Url: init/;
- Create tables Users and Posts;

CreateUser
- Method: POST;
- Url: users/[id];
- Make request to endpoint: https://jsonplaceholder.typicode.com/users and se-lect user by Id; Save this user to table users return success response. If user with this Id already exists - return response as BadRequest with some message;

GetUser
- Method: GET;
- Url: users/[id];
- Get user object from table by Id. Return success response with user object; If user not found, return response NotFound;

DeleteUser
- Method: DELETE;
- Url: users/[id];
- Delete user from table by Id. Return success response with NoContent.  If user not found, return response NotFound;

CreatePost
- Method: POST;
- Url: posts/[userId]/[id]
- Make request to endpoint: https://jsonplaceholder.typicode.com/posts and se-lect post by Id; Insert into Posts with UserId. If post with this Id already exists - return response BadRequest with message. Otherwise - return success re-sponse.

GetUserPosts
- Method: GET;
- Url: posts/[userId];
- Get list of posts by UserId. Return success response with list of posts;

GetPost
- Method: GET;
- Url: posts/[id];
- Get post by Id; If post is not found - return Not Found response;

UpdatePost
- Method: PUT;
- Url: posts/[id];
- Params: title, body
- Update title and body for post by Id. If post not found return Not Found re-sponse. If updated - return No Content;

DeletePost
- Method: DELETE;
- Url: posts/[id];
- Delete post by Id. If post not found return Not Found response. If deleted - return No Content;
