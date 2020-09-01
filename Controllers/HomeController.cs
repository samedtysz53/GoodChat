using Microsoft.Ajax.Utilities;
using özel_projem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace özel_projem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]
        public void Update() 
        {
        
        

        
        }

     
        public ActionResult profil() 
            //kayıt olunca profile gidip hata veriyor
        {
            string email = Session["mail"].ToString();
            //   String email = Request.Cookies["email"].Value;
            // çerezleri sil yapabilirsen session kullan 
            GoodChatEntities7 entities4 = new GoodChatEntities7();
            var context = entities4.Account.Where(e=>e.mail==email).FirstOrDefault();
            HttpCookie cookie = new HttpCookie("mail", email);

            var name = context.user_name.ToString();
            var hakk = context.About_me.ToString();
            var age = context.age.ToString();
            var gender = context.gender.ToString();
            Session.Add("names", context.user_name.ToString());

            ViewBag.Hak = hakk;
            ViewBag.yas = age;
            ViewBag.gender = gender;

            ViewBag.Value = name;
            Response.Cookies.Add(cookie);
            return View();

        
        }

      
        public ActionResult explore() {
            
            
            
            return View(); }
        public ActionResult Mesajlar()
        {
            // arkadaşları listele
            // ve mesaj gönder
            string email=null;

            if (Session["mail"] == null) 
            {
                email = Request.Cookies["mail"].Value;
            }
            else
            {
                  email = Session["mail"].ToString();
            }

            GoodChatEntities7 entities7 = new GoodChatEntities7();
            var Context = entities7.Account.Where(e=>e.mail==email).FirstOrDefault();
            var ContextiD = Context.user_id;
            var ContextName = Context.user_name;


            ViewBag.name = ContextName;


            Account account = new Account();
            friends friends = new friends();
            var innerJoinQuery = (from Account in entities7.Account
                                  join sup in entities7.friends
                                  on account.user_id equals sup.Userid
                                  where account.user_id==ContextiD
                                  select new
                                  {
                                      proid = friends.fid,
                                      
                                
                                  }).ToList();

     

           

            foreach (var item in innerJoinQuery) 
            {
                ViewBag.friend = item.ToString();
                Response.Write(item.ToString());
            }

           
            return View();
        
        }


        
        [HttpPost]
        public ActionResult Login(String pass,String mail) 
        {



              pass = Request.Form["pass"];
               mail = Request.Form["mail"];
              GoodChatEntities7 goodChatEntities4 = new GoodChatEntities7();
            var context = goodChatEntities4.Account;
             var result = context.Where(e=>e.mail==mail && e.user_password==pass).FirstOrDefault();

             if (result!=null) 
             {
                 Session.Add("mail",mail);
                  return Redirect("profil");


             }
             else 
             {




               ViewBag.error = "Hatalı eposta veya şifre";
                
            }

            return View();

     
        }
       
        public ActionResult Login()
        {

            return View();


        }

        

            [HttpPost]
        public ActionResult SavesLogin(String name,String tel,String pass,String mail) 
        {
              name = Request.Form["user"];
              tel =  Request.Form["tel"];
              pass = Request.Form["pass"];
              mail = Request.Form["mail"];

            Account account = new Account();
            GoodChatEntities7 db = new GoodChatEntities7();
            account.user_name = name;
            account.user_password = pass;
            account.phone = tel;
            account.mail = mail;
            account.About_me = "Bilinmiyor";
            account.age = 0;
            account.gender = "Bilinmiyor";
            db.Account.Add(account);
            db.SaveChanges();

            var Context = new GoodChatEntities7();
            var sorgu = Context.Account.Where(e=>e.user_name==name&& e.user_password==pass);
            var result = sorgu.Count();
            if (result>0) 
            {

                Session.Add("names", name);
                Session.Add("mail", mail);







                GoodChatEntities7 entities4 = new GoodChatEntities7();
        

              

               


                return Redirect("profil");


            }
            else 
            {

                Response.Write("kayıt eklenemedi");
            
            }
            return null;

            


        }
        public ActionResult profiles() 
        {
            string adsoyad = Session["names"].ToString();
       

            ViewBag.Value = adsoyad;
            return View();
        
        }

        public ActionResult SavesLogin()
        {
            return View();
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult profiles(String Hak,String yas,String cinsiyet,String tel) 
        {
            GoodChatEntities7 db = new GoodChatEntities7();

              Hak = Request.Form["Hak"];
              yas = Request.Form["yas"];
           
              cinsiyet = Request.Form["Cinsiyet"];
              tel = Request.Form["tel"];
            String mail = Session["mail"].ToString();
            Account account = new Account();


            int ys;
            DateTime dogumtarihi;
            dogumtarihi = Convert.ToDateTime(yas);
            ys = DateTime.Now.Year - dogumtarihi.Year;

            var Context = db.Account.Where(s=>s.mail==mail).FirstOrDefault();
            Context.About_me = Hak;
            Context.age = ys;

            Context.gender = cinsiyet;
            
          

         
            db.SaveChanges();

            return View();




        }


    }
}