using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    public class Rekening
    {
        //Properties
        public string RekeningNr { get; set; }

        public int KlantId { get; set; }  //Oorspronkelijk KlantNr

        public decimal Saldo { get; set; } //Met constructor initialiseren op 0?

        public SoortRekening SoortRekening { get; set; } //In cursus en DB: type char

        //Navigation Properties
        public virtual Klant Klant { get; set; }

        //Methods
        public void Storten(decimal bedrag)
        {
            this.Saldo += bedrag;
        }
    }
}
