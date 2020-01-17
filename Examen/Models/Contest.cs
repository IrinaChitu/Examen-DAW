using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examen.Models
{
    public class Contest
    {
        [Key]
        public int IdConcurs { get; set; }

        [Required(ErrorMessage = "Titlul este obligatoriu")]
        public string Titlu { get; set; }

        [Required(ErrorMessage = "Descrierea este obligatorie")]
        public string Descriere { get; set; }

        [Required(ErrorMessage = "Numarul de participanti este obligatoriu")]
        [Range(1, int.MaxValue)]
        public int NrParticipanti { get; set; }

        //[Required]
        [DataType(DataType.DateTime, ErrorMessage = "Campul trebuie sa contina data si ora si sa fie de tipul data")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Organizatorul este obligatoriu")]
        public int IdOrganizator { get; set; }

        public virtual Organizer Organizator{ get; set; }
		
        public IEnumerable<SelectListItem> Organizers { get; set; }
    }
}