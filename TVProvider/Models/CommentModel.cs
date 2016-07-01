using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVProvider.Models
{
    public class CommentModel
    {
        public string Content 
        {
            get { return ""; }
            set { Comment = value; }
        }
        public string Comment { get; set; }
        public bool Spoiler { get; set; }
        public bool Review { get; set; }
        public int Parend_Id { get; set; }
        public DateTime Created_At { get; set; }
    }
}
