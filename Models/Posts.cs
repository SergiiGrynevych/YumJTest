using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YumJTest.Models
{
    public class Posts
    {
        private int userId;
        private int id;
        private string title;
        private string body;

        public int UserId { get => userId; set => userId = value; }
        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Body { get => body; set => body = value; }
    }
}
