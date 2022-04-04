using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YumJTest.Models
{
    public class Company
    {
        private string name;
        private string catchPhrase;
        private string bs;
        public string Name { get => name; set => name = value; }
        public string CatchPhrase { get => catchPhrase; set => catchPhrase = value; }
        public string Bs { get => bs; set => bs = value; }
    }
}
