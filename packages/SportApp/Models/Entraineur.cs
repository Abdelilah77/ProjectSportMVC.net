using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportApp.Models
{
    public class Entraineur
    {
        [Key]
        public int Id_En { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string User_name { get; set; }
        public string Password { get; set; }
        public string Sport { get; set; }
        public virtual Admin _ { get; set; }
    }
}