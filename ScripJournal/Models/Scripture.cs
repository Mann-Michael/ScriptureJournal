using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ScripJournal.Models
{
    public class Scripture
    {
        // int ID, string book, string note, Datetime dateAdded

        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Book { get; set; }

        [Required]
        public int Verse { get; set; }

        [StringLength(244)]
        [Required]
        public string Note { get; set; }

        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }


    }
}
