using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YumJTest.Models
{
    public class User
    {
        private int id; //{ get; set; }
        private string name; //{ get; set; }
        private string username;//{ get; set; }
        private string email; //{ get; set; }
        private Address address; //{ get; set; }  //  public string[] address { get; set; }
        private string phone; //{ get; set; }
        private string website; //{ get; set; }
        private Company Company; //{ get; set; } // public string[] company { get; set; }
        public User() { }
        public User(int id, string name, string username, string email, Address address, string phone, string website)
        {
            this.id = id;
            this.name = name;
            this.username = username;
            this.email = email;
            this.address = new Address();
            this.phone = phone;
            this.website = website;
        }
        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Username { get { return username; } set { username = value; } }
        public string Email { get { return email; } set { email = value; } }

    }
}
