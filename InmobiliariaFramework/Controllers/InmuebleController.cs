using InmobiliariaFramework.Models;
using InmobiliariaFramework.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InmobiliariaFramework.Controllers
{
    public class InmuebleController : Controller
    {
        // GET: Inmueble
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            List<InmuebleViewModel> lista = new List<InmuebleViewModel>();
            using (BDInmobiliariaEntities db = new BDInmobiliariaEntities())
            {

                lista = (from d in db.inmueble
                         orderby d.idInmueble ascending
                         select new InmuebleViewModel
                         {
                             IdInmueble = d.idInmueble,
                             Direccion = d.direccion,
                             Ambientes = d.ambientes,
                             Tipo = d.tipo,
                             Uso = d.uso,
                             Precio = d.precio,
                             Disponible = d.disponible,
                             IdPropietario= d.idPropietario,


                         }).ToList();

            }
            return View(lista);
        }
        public ActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(InmuebleViewModel model)
        {
            try
            {
                using (BDInmobiliariaEntities db = new BDInmobiliariaEntities())
                {
                    var miInmueble = new inmueble();
                    miInmueble.direccion = model.Direccion; 
                    miInmueble.ambientes = Convert.ToInt32(model.Ambientes);
                    miInmueble.tipo = model.Tipo;
                    miInmueble.uso = model.Uso;
                    miInmueble.precio = Convert.ToDecimal(model.Precio);
                    miInmueble.disponible = Convert.ToInt32(model.Disponible);
                    miInmueble.idPropietario = Convert.ToInt32(model.IdPropietario);
                    db.inmueble.Add(miInmueble);
                    db.SaveChanges();
                }
                return Content("1");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Edit(int idInmueble)
        {
           InmuebleViewModel model = new InmuebleViewModel();
            using (BDInmobiliariaEntities db = new BDInmobiliariaEntities())
            {
                var oInmueble = db.inmueble.Find(idInmueble);
                model.Direccion = oInmueble.direccion;
                model.Ambientes = oInmueble.ambientes;
                model.Tipo = oInmueble.tipo;
                model.Uso = oInmueble.uso;
                model.Precio = oInmueble.precio;
                model.Disponible = oInmueble.disponible;
                model.IdPropietario = oInmueble.idPropietario;
                model.IdInmueble = oInmueble.idInmueble;



                /* miInmueble.direccion ="direccion";
                 miInmueble.ambientes = 20;
                 miInmueble.tipo = "local";
                 miInmueble.uso = "comercial";
                 miInmueble.precio =10200;
                 miInmueble.disponible = 0;
                 miInmueble.idPropietario = 1;*/
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(InmuebleViewModel model)
        {
            try
            {
                using (BDInmobiliariaEntities db = new BDInmobiliariaEntities())
                {
                    var miInmueble = db.inmueble.Find(model.IdInmueble);
                    miInmueble.direccion = model.Direccion;
                    miInmueble.ambientes = Convert.ToInt32(model.Ambientes);
                    miInmueble.tipo = model.Tipo;
                    miInmueble.uso = model.Uso;
                    miInmueble.precio = Convert.ToDecimal(model.Precio);
                    miInmueble.disponible = Convert.ToInt32(model.Disponible);
                    miInmueble.idPropietario = Convert.ToInt32(model.IdPropietario);
                    db.Entry(miInmueble).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Content("1");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            try
            {
                using (BDInmobiliariaEntities db = new BDInmobiliariaEntities()) //dentro de las llaves que siguen existe la conexión
                {
                    var oInmueble = db.inmueble.Find(Id);
                    db.inmueble.Remove(oInmueble);
                    db.SaveChanges();
                }
                return Content("1");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}