using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TVApi.Models
{
    public class FavouriteUserMovieModel
    {
        [Required]
        [Display(Name = "Title")]
        public string title { get; set; }

        [Required]
        [Display(Name = "User Na,e")]
        public string userName { get; set; }
    }
}