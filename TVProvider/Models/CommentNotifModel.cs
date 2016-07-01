using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVProvider.Models
{
    public class CommentNotifModel
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string Comment { get; set; }
        public DateTime Created_At { get; set; }
    }
}
