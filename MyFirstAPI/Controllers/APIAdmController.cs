using MyFirstAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MyFirstAPI.Controllers
{
    public class APIAdmController : Controller
    {
        private readonly MyFirstDbEntities db = new MyFirstDbEntities();
        // GET: APIAdm
        public ActionResult Index()
        {
            var Usuarios = db.T_USER.ToList();
            return View(Usuarios);
        }

        public ActionResult Create()
        {



            return View();
        }

        [HttpPost]
        public ActionResult Create(T_USER usuario)
        {

            if (ModelState.IsValid)
            {

                usuario.APIKEY = CreateApiKey();
                db.T_USER.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }


        }




        public static string CreateApiKey()
        {
            var bytes = new byte[256 / 8];
            using (var random = RandomNumberGenerator.Create())
                random.GetBytes(bytes);
            return ToBase62String(bytes);
        }

        static string ToBase62String(byte[] toConvert)
        {
            const string alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            BigInteger dividend = new BigInteger(toConvert);
            var builder = new StringBuilder();
            while (dividend != 0)
            {
                dividend = BigInteger.DivRem(dividend, alphabet.Length, out BigInteger remainder);
                builder.Insert(0, alphabet[Math.Abs(((int)remainder))]);
            }
            return builder.ToString();
        }


    }
}