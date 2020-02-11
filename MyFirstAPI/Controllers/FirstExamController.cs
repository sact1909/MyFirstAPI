using MyFirstAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

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


            var UsuarioBusqueda = db.T_USER.Where(w => w.CODIGO == ID).Select(s=> new {s.NOMBRE,s.APELLIDO}).FirstOrDefault();

            return Json(UsuarioBusqueda);
        }

        [Route("api/Values/busqueda/{id}")]
        [HttpGet]
        public IHttpActionResult Busqueda(int ID, string NombreConsulta) {

            var UsuarioBusqueda = db.T_USER.Where(w => w.CODIGO == ID).FirstOrDefault();
            int[] grupo = new int[] {5,2,3,7,8};


            return Json(
                    new { 
                        ConsultorDetalle = 
                        new {
                            Nombre = NombreConsulta,
                            Grupos = new { grupo }
                        }, 
                        Busqueda = UsuarioBusqueda
                    }
                );
        }

        public IHttpActionResult Post([FromBody]T_USER Usuarios)
        {

            return Json(new { Resultado = "Ok"});
        }


    }
}
