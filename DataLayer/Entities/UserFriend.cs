using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class UserFriend
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("AppUser")]
        public int FriendAppUserId { get; set; }
        public int UserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}
