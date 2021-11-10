using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PWebTudoDeNovo.Models
{
    public class Purchase
    {
        [Key]
        public int IdPurchase { get; set; }

        public string Id { get; set; } //id do funcionario
        virtual public ApplicationUser ApplicationUser { get; set; }

        public int IdShoppingCar { get; set; } //id do carrinho de compras

        virtual public ShoppingCar ShoppingCar { get;set; }

        public int QuantidadeAComprar { get; set; }

        public float preco { get; set; }

        public String EstadoCompra { get; set; }

        public string MetedoDeEntrega { get; set; }

        public int idProduto { get; set; } //id do produto
        virtual public Product Product { get; set; }

        public float precoComDesconto { get; set; }

    }
}