using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Examen.Models
{
    public class Organizer
    {
        [Key]
        public int IdOrganizator { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string Nume { get; set; }

        public virtual ICollection<Contest> Contests { get; set; }

        public class OrganizatorConcursDBContext : DbContext
        {
            public OrganizatorConcursDBContext() : base("DBConnectionString") { }
            public DbSet<Organizer> Organizers { get; set; }
            public DbSet<Contest> Contests { get; set; }
        }
    }
}