using Datos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trabajo_en_clase.Data;

namespace trabajo_en_clase.Controllers
{
    public class SociosController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SociosController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // GET: SocioController
        public ActionResult Index()
        {
            List<Socio> socios = new List<Socio>();
            socios = _applicationDbContext.Socios.Select(q => new Socio
            {
                Cedula = q.Cedula,
                Nombre = q.Nombre,
                Apellido = q.Apellido,
                Direccion = q.Direccion,
                Estado = q.Estado,
            }).ToList();

            return View(socios);
        }

        // GET: SocioController/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index");
            Socio socio = _applicationDbContext.Socios.Where(q => q.Cedula == id).FirstOrDefault();
            if (socio == null)
                return RedirectToAction("Index");
            return View(socio);
        }

        // GET: SocioController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: SocioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Socio socio)
        {
            try
            {
                socio.Estado = 1;
                _applicationDbContext.Add(socio);
                _applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {

                return View(socio);

            }
            return RedirectToAction("Index");


        }

        // GET: SocioController/Edit/5
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index");
            Socio socio = _applicationDbContext.Socios.Where(q => q.Cedula == id).FirstOrDefault();
            if (socio == null)
                return RedirectToAction("Index");
            return View(socio);


        }

        // POST: SocioController/Edit/5
        [HttpPost]

        public ActionResult Edit(string id, Socio socio)
        {
            if (id != socio.Cedula)
                return RedirectToAction("Index");
            try
            {
                _applicationDbContext.Update(socio);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception exc)
            {
                _applicationDbContext.Socios.Where(q => q.Cedula == id).FirstOrDefault();
                return View(socio);

            }
            return RedirectToAction("Index");
        }

        // POST: SocioController/Delete/5



        public IActionResult Desactivar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index");
            Socio socio = _applicationDbContext.Socios.Where(q => q.Cedula == id).FirstOrDefault();
            try
            {
                socio.Estado = 0;
                _applicationDbContext.Update(socio);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception exc)
            {

                return RedirectToAction("Index");

            }


            return RedirectToAction("Index");

        }

        public IActionResult Activar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index");
            Socio socio = _applicationDbContext.Socios.Where(q => q.Cedula == id).FirstOrDefault();
            try
            {
                socio.Estado = 1;
                _applicationDbContext.Update(socio);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception exc)
            {

                return RedirectToAction("Index");

            }


            return RedirectToAction("Index");

        }
    }
}
