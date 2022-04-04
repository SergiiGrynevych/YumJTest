
namespace YumJTest.Models
{
    public class Address
    {
        private string street;
        private string suite;
        private string city;
        private string zipcode;
        private Geo geo;

        public string Street { get => street; set => street = value; }
        public string Suite { get => suite; set => suite = value; }
        public string City { get => city; set => city = value; }
        public string Zipcode { get => zipcode; set => zipcode = value; }
        public Geo Geo { get => geo; set => geo = value; }
    }
}
