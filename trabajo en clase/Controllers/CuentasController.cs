using Datos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trabajo_en_clase.Data;

namespace trabajo_en_clase.Controllers
{
    public class CuentasController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CuentasController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // GET: CuentasController
        public ActionResult Index()
        {

            List<Cuenta> ltscuenta = new List<Cuenta>();
            ltscuenta = _applicationDbContext.Cuenta.ToList();
            return View(ltscuenta);
        }

        // GET: CuentasController/Details/5
        public ActionResult Details(string id)
        {
            Cuenta cuenta = _applicationDbContext.Cuenta.Where(q => q.Numero == id).FirstOrDefault();
            return View(cuenta);
        }

        // GET: CuentasController/Create
        public ActionResult Create()
        {
            ViewData["CodigoSocio"]=new SelectList(_applicationDbContext.Socios.Where(q=>q.Estado==1).ToList(),"Cedula","Cedula");
            return View();
        }

        // POST: CuentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cuenta cuenta)
        {
            try
            {
                cuenta.Estado = 1;
                _applicationDbContext.Add(cuenta);
                _applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CuentasController/Edit/5
        public ActionResult Edit(string id)
        {

            Cuenta cuenta = _applicationDbContext.Cuenta.Where(q => q.Numero == id).FirstOrDefault();
            ViewData["CodigoSocio"] = new SelectList(_applicationDbContext.Socios.Where(q => q.Estado == 1).ToList(), "Cedula", "Cedula",cuenta.CodigoSocio);
            
            return View(cuenta);
        }

        // POST: CuentasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Cuenta cuenta)
        {
            if(id != cuenta.CodigoSocio)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _applicationDbContext.Update(cuenta);
                _applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["CodigoSocio"] = new SelectList(_applicationDbContext.Socios.Where(q => q.Estado == 1).ToList(), "Cedula", "Cedula", cuenta.CodigoSocio);
                return View(cuenta);
            }
        }

        // GET: CuentasController/Delete/5
        public IActionResult Desactivar(string id)
        {
            Cuenta cuenta = _applicationDbContext.Cuenta.Where(q => q.Numero == id).FirstOrDefault();
            cuenta.Estado = 0;
            _applicationDbContext.Update(cuenta);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Activar(string id)
        {
            Cuenta cuenta = _applicationDbContext.Cuenta.Where(q => q.Numero == id).FirstOrDefault();
            cuenta.Estado = 1;
            _applicationDbContext.Update(cuenta);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
