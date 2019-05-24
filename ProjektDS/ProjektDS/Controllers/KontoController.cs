using ProjektDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;



namespace ProjektDS.Controllers
{
    public class KontoController : Controller
    {
        // GET: Konto
       
      
        [HttpGet]
        public ActionResult Rejestracja()
        {
            return View();
        }

      
         [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rejestracja( Users u)
        {
            
            string message = "";

            //Walidacja Modelu
            if(ModelState.IsValid)
            {
                #region   //Sprawdzenie czy Email istnieje
                var czyIstnieje = EmailIstnieje(u.Email);
                if (czyIstnieje)
                {
                    ModelState.AddModelError("MailIstnieje", "Email już istnieje");
                    return View(u);
                }
                #endregion

                  #region // Szyfrowanie hasla
                u.Haslo = Crypto.Szyfro(u.Haslo);
                u.PowtorzHaslo = Crypto.Szyfro(u.PowtorzHaslo);
                #endregion

                #region // Zapisanie danych do bazy danych
                using (PLANZAJECEntities dc = new PLANZAJECEntities())
                {
                    dc.Users.Add(u);
                    dc.SaveChanges();
                }
                #endregion
            }
            else
            {
                message = "Nieprawidłowa odpowiedz";
                return View(u);
            }
            ViewBag.Message = message;

            return RedirectToAction("Logowanie");
        }



         [NonAction]
         public bool EmailIstnieje(string mail)
         {
             using (PLANZAJECEntities dc = new PLANZAJECEntities ())
             {
                 var v = dc.Users.Where(a => a.Email == mail).FirstOrDefault();
                 return v != null;
             }
         }

         [HttpGet]
         public ActionResult Logowanie()
         {
             return View();
         }

         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Logowanie(Login login, string ReturnUrl = "")
         {
             string message = "";
             using (PLANZAJECEntities dc = new PLANZAJECEntities())
             {
                 var v = dc.Users.Where(a => a.Email == login.Email).FirstOrDefault();
                 if (v != null)
                 {


                     if (string.Compare(Crypto.Szyfro(login.Haslo), v.Haslo) == 0)
                     {
                         int timeout = login.Zapamietaj ? 525600 : 20; // 525600 min = 1 rok
                         var ticket = new FormsAuthenticationTicket(login.Email, login.Zapamietaj, timeout);
                         string encrypted = FormsAuthentication.Encrypt(ticket);
                         var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                         cookie.Expires = DateTime.Now.AddMinutes(timeout);
                         cookie.HttpOnly = true;
                         Response.Cookies.Add(cookie);


                         if (Url.IsLocalUrl(ReturnUrl))
                         {
                             return Redirect(ReturnUrl);
                         }
                         else
                         {
                             return RedirectToAction("Index", "Home");
                         }
                     }
                     else
                     {
                         message = "Nieprawidłowe";
                     }
                 }
                 else
                 {
                     message = "Nieprawidłowe";
                 }
             }
             ViewBag.Message = message;
             return View();
         }

         //Wylogowanie
         [Authorize]
         public ActionResult Wylogowywanie()
         {
             FormsAuthentication.SignOut();
             return RedirectToAction("Logowanie", "Konto");
         }
    }
}