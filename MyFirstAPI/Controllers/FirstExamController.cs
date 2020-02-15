using MyFirstAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Data.Entity;

namespace MyFirstAPI.Controllers
{
    public class FirstExamController : ApiController
    {
        private readonly MyFirstDbEntities db = new MyFirstDbEntities();
        public List<T_USER> Get() {

            //using (var db = new MyFirstDbEntities()) { 

            //}
            var Usuario = db.T_USER.ToList();

            return Usuario;
        }

        public IHttpActionResult Get(int ID)
        {


            var UsuarioBusqueda = db.T_USER.Where(w => w.CODIGO == ID).FirstOrDefault();

            return Json(UsuarioBusqueda);
        }

        //[Route("api/FirstExam/busqueda/{id}")]
        //[HttpGet]
        //public IHttpActionResult Busqueda(int ID, string NombreConsulta) {

        //    var UsuarioBusqueda = db.T_USER.Where(w => w.CODIGO == ID).FirstOrDefault();
        //    int[] grupo = new int[] {5,2,3,7,8};


        //    return Json(UsuarioBusqueda);
        //}

        public IHttpActionResult Post([FromUri]T_USER Usuarios)
        {
            if (ModelState.IsValid)
            {
                db.T_USER.Add(Usuarios);
                db.SaveChanges();
                return Json(new { Resultado = "Registro guardado" });
            }
            else {
                return Json(new { Resultado = "Error, Los datos no concuerdan" });
            }
            
        }


        public IHttpActionResult Put([FromUri]T_USER Usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Resultado = "Registro Actualizado" });
            }
            else
            {
                return Json(new { Resultado = "Error, Los datos no concuerdan" });
            }

        }

        public IHttpActionResult Delete(int ID)
        {

            try
            {

                var UsuarioBusqueda = db.T_USER.Where(w => w.CODIGO == ID).FirstOrDefault();
                //db.Entry(UsuarioBusqueda).State = EntityState.Deleted;
                db.T_USER.Remove(UsuarioBusqueda);
                db.SaveChanges();
                return Json(new { Resultado = "Registro Eliminado" });

            }
            catch (Exception e) {
                return Json(new { Resultado = e.ToString() });
            }
                
           

        }

    }
}
