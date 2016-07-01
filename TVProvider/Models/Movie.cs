using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVProvider.Models
{
    public class Movie
    {
        public string Id
        {
            get { return ""; }
            set {
                Ids = new MovieIds();
                Ids.Tmdb = value;
            }
        }
        public string Title { get; set; }
        public int Year { get; set; }
        public MovieIds Ids { get; set; }
    }
}
