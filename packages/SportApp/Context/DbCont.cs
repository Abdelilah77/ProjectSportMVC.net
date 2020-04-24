using SportApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace SportApp.Context
{
    public class DbCont:DbContext
    {
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Entraineur> Entraineurs { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
    }
}