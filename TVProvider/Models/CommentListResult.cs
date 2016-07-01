using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVProvider.Models
{
    public class CommentListResult
    {
        public int Page { get; set; }
        public IEnumerable<CommentModel> Results { get; set; }
    }
}
