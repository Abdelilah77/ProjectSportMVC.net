using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportApp.Models
{
    public class Client
    {
        [Key]
        public int Id_cli { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date_naiss { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dateè_Entre { get; set; }
        public string Sport { get; set; }
        public double Prix { get; set; }
        public string Sexe { get; set; }
        public virtual Entraineur _ { get; set; }
    }
}