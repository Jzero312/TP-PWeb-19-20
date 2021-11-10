using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PWebTudoDeNovo.Models
{
    public class Product
    {
        [Key]
        public int idProduto { get; set; }


        [Required]
        public int idCategoria { get; set; }
        virtual public Categoria Categoria { get; set; }

        [Required]
        public String Nome { get; set; }

        [Required]
        public float Preco { get; set; }

        [Required]
        public int Stock { get; set; }

        public string Descricao { get; set; }

        public string Empresa { get; set; }

        public int Promocao { get; set; }
    }
}