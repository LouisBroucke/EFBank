using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    public class Klant
    {
        //Constructor
        public Klant()
        {
            Rekeningen = new List<Rekening>();
        }

        //Properties
        public int KlantId { get; set; } //Oorspronkelijk KlantNr

        public string Naam { get; set; }

        //Navigation properties
        public virtual ICollection<Rekening> Rekeningen { get; set; }
    }
}
