using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVProvider.Models
{
    public class MovieListResult
    {
        public int Page { get; set; }
        public IEnumerable<Movie> Results { get; set; }
    }
}
