using Microsoft.AspNet.Identity;
using PWebTudoDeNovo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PWebTudoDeNovo.Controllers
{
    public class ShoppingCarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCars
        public ActionResult Index()
        {
            string loggeduserId = User.Identity.GetUserId();
            var loggedUser = db.Users.FirstOrDefault(i => i.Id == loggeduserId);

            var ShoppingCar = db.ShoppingCars.FirstOrDefault(i => i.Id == loggeduserId && i.Pago == false);

            var Compras = db.Purchases.Where(i => i.IdShoppingCar == ShoppingCar.IdShoppingCar).ToList();
            float totalpagar = 0;
            foreach(Purchase item in Compras)
            {
                totalpagar += item.precoComDesconto;
            }
            ShoppingCar.PrecoTotal = totalpagar;
            db.Entry(ShoppingCar).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.PrecoTotal = totalpagar;
            ViewBag.idCarrinho = ShoppingCar.IdShoppingCar;
            return View(Compras);
        }

        


             public ActionResult ChangeEntrega(int? id, string MEntrega)
        {
            if (MEntrega == null || id == null)
            {
                return RedirectToAction("Index");
            }
            Purchase compra = db.Purchases.Find(id);
            if (compra == null)
            {
                return RedirectToAction("Index");
            }
            compra.MetedoDeEntrega = MEntrega;
   
            db.Entry(compra).State = EntityState.Modified;
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }



        public ActionResult Edit(string MEntrega, int? id, int? valor)
        {
            if (id == null || valor ==null)
            {
                return RedirectToAction("Index");
            }
            Purchase compra = db.Purchases.Find(id);
            if (compra == null)
            {
                return RedirectToAction("Index");
            }
            if (valor > compra.Product.Stock)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (MEntrega != null)
                {
                    compra.MetedoDeEntrega = MEntrega;
                }
                
                compra.QuantidadeAComprar = (int)valor;
                compra.preco = compra.QuantidadeAComprar * compra.Product.Preco;
                if (compra.Product.Promocao == 0)
                {
                    compra.precoComDesconto = compra.preco;
                }
                else
                {
                    compra.precoComDesconto = compra.preco - (compra.preco * ((float)compra.Product.Promocao / 100));
                }
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                return RedirectToAction("Index");
            }
            Purchase compra = db.Purchases.Find(id);
            if (compra == null)
                {
                return RedirectToAction("Index");
            }
            
            db.Purchases.Remove(compra);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FinalizarCompra(int? shoppingCarId)
        {
            if(shoppingCarId == null)
            {
                return RedirectToAction("Index");
            }
            //aqui é onde deve estar a consulta do saldo do utilizador
            var shoppingCar = db.ShoppingCars.FirstOrDefault(i => i.IdShoppingCar == shoppingCarId);
            if(shoppingCar == null)
            {
                return RedirectToAction("Index");
            }
            var listaPurchases = db.Purchases.Where(i => i.IdShoppingCar == shoppingCar.IdShoppingCar).ToList();
            if(listaPurchases.Count == 0)
            {
                return RedirectToAction("Index");
            }
            shoppingCar.Pago = true;
            shoppingCar.DatadeCompra = DateTime.UtcNow.Date;
            db.Entry(shoppingCar).State = EntityState.Modified;
            db.SaveChanges();

            foreach(Purchase item in listaPurchases)
            {
                item.EstadoCompra = "A Processar";
                item.Product.Stock -= item.QuantidadeAComprar;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }

            var newshoppingCar = new ShoppingCar { DatadeCompra = null, Pago = false, PrecoTotal = 0, Id = User.Identity.GetUserId() };
            db.ShoppingCars.Add(newshoppingCar);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        
        public ActionResult FuncionariosAceitaoCompra()
        {
            string loggeduserId = User.Identity.GetUserId();
            var loggedUser = db.Users.FirstOrDefault(i => i.Id == loggeduserId);

            
            var Compras = db.Purchases.Where(i => i.EstadoCompra == "A Processar" && i.Product.Empresa == loggedUser.NomeCompleto).ToList();
            return View(Compras);
        }

        public ActionResult AceitarACompra(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("FuncionariosAceitaoCompra");
            }
            var item = db.Purchases.FirstOrDefault(i => i.IdPurchase == id);
            if(item == null)
            {
                return RedirectToAction("FuncionariosAceitaoCompra");
            }
            item.EstadoCompra = "Aceite";
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("FuncionariosAceitaoCompra");
        }



        //ConcluiCompra
        //FuncionariosConcluiCompra
        public ActionResult FuncionariosConcluiCompra()
        {
            string loggeduserId = User.Identity.GetUserId();
            var loggedUser = db.Users.FirstOrDefault(i => i.Id == loggeduserId);


            var Compras = db.Purchases.Where(i => i.EstadoCompra == "Aceite" && i.Product.Empresa == loggedUser.NomeCompleto).ToList();
            return View(Compras);
        }

        public ActionResult ConcluiCompra(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("FuncionariosConcluiCompra");
            }
            var item = db.Purchases.FirstOrDefault(i => i.IdPurchase == id);
            if (item == null)
            {
                return RedirectToAction("FuncionariosConcluiCompra");
            }
            item.EstadoCompra = "Entregue";
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("FuncionariosConcluiCompra");
        }

        public ActionResult UtilizadorHistoricoCompras()
        {
            string loggeduserId = User.Identity.GetUserId();

            ViewBag.Compras = db.Purchases.Where(i=>i.EstadoCompra == "A Processar" || i.EstadoCompra == "Aceite" || i.EstadoCompra == "Entregue").ToList();
            ViewBag.Carrinhos = db.ShoppingCars.Where(i => i.Id == loggeduserId && i.Pago == true).ToList();

            return View();
        }
    }
}