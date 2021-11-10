using Microsoft.AspNet.Identity;
using PWebTudoDeNovo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PWebTudoDeNovo.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            var produtos = db.Products.ToList();
            return View(produtos);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult ComprarItem(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string loggeduserId = User.Identity.GetUserId();
                var loggedUser = db.Users.FirstOrDefault(i => i.Id == loggeduserId);

                var ShoppingCar = db.ShoppingCars.FirstOrDefault(i => i.Id == loggeduserId && i.Pago == false);

                var ActualProduct = db.Products.FirstOrDefault(i => i.idProduto == id);

                if(ShoppingCar == null || ActualProduct == null || ActualProduct.Stock < 1)
                {
                    return RedirectToAction("Index");
                }

                //ver se ja existe uma compra deste produto
                var CompraJaExiste = db.Purchases.FirstOrDefault(i => i.IdShoppingCar == ShoppingCar.IdShoppingCar && i.idProduto == id);
                if(CompraJaExiste == null)
                {
                    var newPurchaseNow = new Purchase { ShoppingCar = ShoppingCar,Product = ActualProduct,IdShoppingCar = ShoppingCar.IdShoppingCar, QuantidadeAComprar = 1, preco = ActualProduct.Preco, EstadoCompra = "Por Pagar", MetedoDeEntrega = "Levantar na Loja", idProduto = ActualProduct.idProduto, precoComDesconto = ActualProduct.Preco};
                    if (ActualProduct.Promocao == 0)
                    {
                        newPurchaseNow.precoComDesconto = ActualProduct.Preco;
                    }
                    else
                    {
                        newPurchaseNow.precoComDesconto = newPurchaseNow.preco - (newPurchaseNow.preco * ((float)newPurchaseNow.Product.Promocao / 100));
                    }
                    db.Purchases.Add(newPurchaseNow);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
               


            }
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult FuncionariosEditDescontos()
        {
            ViewBag.Message = "AdicionarDescontos";
            
            string loggeduserId = User.Identity.GetUserId();
            var loggedUser = db.Users.FirstOrDefault(i => i.Id == loggeduserId);

            var produtos = db.Products.Where(i => i.Empresa == loggedUser.NomeCompleto).ToList();

            return View(produtos);
        }


        public ActionResult AlteraDescontoDoProduto(int? id, int? Desconto)
        {
            if (id == null || Desconto == null || Desconto < 0 || Desconto > 100)
            {
                return RedirectToAction("FuncionariosEditDescontos");
            }
            else
            {
                var ActualProduct = db.Products.FirstOrDefault(i => i.idProduto == id);

                if (ActualProduct == null)
                {
                    return RedirectToAction("FuncionariosEditDescontos");
                }

                ActualProduct.Promocao = (int)Desconto;
                db.Entry(ActualProduct).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("FuncionariosEditDescontos");
            }

        }

    }
}