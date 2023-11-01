using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract6
{
    public class Film
    {
        public string name;
        public int duration;
        public int rating;

        public Film(string name, int duration, int rating)
        {
            this.name = name;
            this.duration = duration;
            this.rating = rating;
        }
        public Film(){ }
    }
}