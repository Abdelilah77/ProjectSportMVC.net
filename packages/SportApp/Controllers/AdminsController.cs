using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportApp.Context;
using SportApp.Models;

namespace SportApp.Controllers
{
    public class AdminsController : Controller
    {
        private DbCont db = new DbCont();

        // GET: Admins
        public ActionResult Index()
        {
            ViewBag.ControlLog = Session["name"].ToString();
            ViewBag.Check = "ok";
            return View(db.Admins.ToList());
        }


        public PartialViewResult SeeClient()
        {
            var Dt = (from a in db.Entraineurs select a.Sport).Distinct();
            ViewBag.sport = new SelectList(Dt.ToList());
            return PartialView("_SeeClient", db.Clients.ToList());
        }

        public IEnumerable<Client> sportSearch_textresult(string resulttext, string sport, string S1, string S2, string S3)
        {

            bool A = S1 == null ? false : true;
            bool B = S2 == null ? false : true;
            bool C = S3 == null ? false : true;

            bool assiste = sport.ToUpper().Equals("ALL");
            var Search = (IEnumerable<Client>)null;

            if (assiste)
            {
                Search = db.Clients.Where(delegate (Client Cli)
                {
                    int days = Cli.Dateè_Entre.AddDays(30).Subtract(@DateTime.Now).Days;
                    var V = Cli.Name.StartsWith(resulttext);
                    return (A ? days >= 4 && V : false) || (B ? days < 4 && days > 0 && V : false)
                                       || (C ? days < 0 && V : false);
                           
                }).ToList();
            }
            else
            {
                Search = db.Clients.Where(delegate (Client Cli)
                {
                    int days = Cli.Dateè_Entre.AddDays(30).Subtract(@DateTime.Now).Days;
                    var V = Cli.Sport == sport && Cli.Name.StartsWith(resulttext);
                    return (A ? days >= 4 && V : false) || (B ? days < 4 && days > 0 && V : false)
                                        || (C ? days < 0 && V : false);
                }).ToList();
            }


            return Search;


        }


        [HttpPost]
        public PartialViewResult SeeClient(string sport, string resulttext, string S1, string S2, string S3)
        {
            var Dt = (from a in db.Entraineurs select a.Sport).Distinct();
            ViewBag.sport = new SelectList(Dt.ToList());
            var Tester = sportSearch_textresult(resulttext, sport, S1, S2, S3);
            return PartialView("_SeeClient", Tester);
        }
        // GET: Clients/Details/5
        public PartialViewResult Details(int? id)
        {
            if (id == null)
            {

            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {

            }
            return PartialView("_Details", client);
        }

        // GET: Clients/Create
        public PartialViewResult AddClient()
        {
            List<string> sexe = new List<string>();
            sexe.Add("Male");
            sexe.Add("Female");
            ViewBag.sexe = new SelectList(sexe);
            var Dt = (from a in db.Entraineurs select a.Sport).Distinct();
            ViewBag.sports = new SelectList(Dt.ToList());
            return PartialView("_AddClient");
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClient([Bind(Include = "Id_cli,Img,Name,Date_naiss,Dateè_Entre,Sport,Prix,Sexe")] Client client)
        {
            if (Request.Files.Count > 0)
            {

                HttpPostedFileBase files = Request.Files[0];

                string chemin = "";

                if (files.ContentLength > 0)
                {
                    chemin = files.FileName;
                    client.Img = chemin;

                    var path = Path.Combine(Server.MapPath("~/Img"), chemin);
                    files.SaveAs(path);

                  


                }
            }

            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }

        // GET: Clients/Edit/5
        public PartialViewResult UpdateClient(int? id)
        {
            if (id == null)
            {

            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {

            }
            return PartialView("_UpdateClient", client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateClient([Bind(Include = "Id_cli,Img,Name,Date_naiss,Dateè_Entre,Sport,Prix,Sexe")] Client client)
        {
            var a = db.Clients.AsNoTracking().FirstOrDefault(c => c.Id_cli == client.Id_cli).Id_cli;

            string fullPath = Request.MapPath("~/img/" + a);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            if (Request.Files.Count > 0)
            {

                HttpPostedFileBase files = Request.Files[0];

                if (files.ContentLength > 0)
                {
                    client.Img= files.FileName;



                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        // GET: Clients/Delete/5
        public PartialViewResult DeleteClient(int? id)
        {
            if (id == null)
            {

            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {

            }
            return PartialView("_DeleteClient", client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("DeleteClient")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult SeeEntre()
        {
            return PartialView("_SeeEntre", db.Entraineurs.ToList());
        }

        // GET: Entraineurs/Details/5
        public PartialViewResult DetailsEntr(int? id)
        {
            if (id == null)
            {
                
            }
            Entraineur entraineur = db.Entraineurs.Find(id);
            if (entraineur == null)
            {
                
            }
            return PartialView("_DetailsEntr", entraineur);
        }

        // GET: Entraineurs/Create
        public PartialViewResult AddEntre()
        {
            return PartialView("_AddEntre");
        }

        // POST: Entraineurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEntre([Bind(Include = "Id_En,Name,Img,User_name,Password,Sport")] Entraineur entraineur)
        {

            if (Request.Files.Count > 0)
            {

                HttpPostedFileBase files = Request.Files[0];

                string chemin = "";

                if (files.ContentLength > 0)
                {
                    chemin = files.FileName;
                    entraineur.Img = chemin;

                    var path = Path.Combine(Server.MapPath("~/Img"), chemin);
                    files.SaveAs(path);
                }
            }

            if (ModelState.IsValid)
            {
                db.Entraineurs.Add(entraineur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(entraineur);
        }

        // GET: Entraineurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                
            }
            Entraineur entraineur = db.Entraineurs.Find(id);
            if (entraineur == null)
            {
               
            }
            return View("_Edit", entraineur);
        }

        // POST: Entraineurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_En,Name,Img,User_name,Password,Sport")] Entraineur entraineur)
        {
            var a = db.Entraineurs.AsNoTracking().FirstOrDefault(c => c.Id_En == entraineur.Id_En).Id_En;

            string fullPath = Request.MapPath("~/img/" + a);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            if (Request.Files.Count > 0)
            {

                HttpPostedFileBase files = Request.Files[0];

                if (files.ContentLength > 0)
                {
                    entraineur.Img = files.FileName;
                }
            }
                if (ModelState.IsValid)
            {
                db.Entry(entraineur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entraineur);
        }

        // GET: Entraineurs/Delete/5
        public PartialViewResult Delete(int? id)
        {
            if (id == null)
            {
               
            }
            Entraineur entraineur = db.Entraineurs.Find(id);
            if (entraineur == null)
            {
                
            }
            return PartialView("_Delete", entraineur);
        }

        // POST: Entraineurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmede(int id)
        {
            Entraineur entraineur = db.Entraineurs.Find(id);
            db.Entraineurs.Remove(entraineur);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult Chart()
        {
            return PartialView("_Chart",db.Clients.ToArray());
        }

    }
}
