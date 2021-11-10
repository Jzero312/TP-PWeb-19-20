using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PWebTudoDeNovo.Models
{
    public class ShoppingCar
    {
        [Key]
        public int IdShoppingCar { get; set; }

        [Required]
        public string Id { get; set; }
        virtual public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public float PrecoTotal { get; set; }

        [Required]
        public bool Pago { get; set; }
        
        public DateTime? DatadeCompra { get; set; }


    }
}