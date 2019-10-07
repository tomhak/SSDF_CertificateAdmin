using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using SSDF_CertificateAdmin.Models;
using System.Linq;
using SSDF_CertificateAdmin.Repository;
using SSDF_CertificateAdmin.ViewModels;
using Microsoft.AspNet.Identity;

namespace SSDF_CertificateAdmin.Controllers
{
    [Authorize]
    public class CertificateController : Controller
    {
         
        
        private SSDF_CERTEntities1 db = new SSDF_CERTEntities1();
        //private List<CertificateViewModel> vmCertifikats = new List<CertificateViewModel>();
        //private List<CertificateViewModel> vmSerchedList = new List<CertificateViewModel>();
        private CertificateRepository  certResp = new CertificateRepository();

        // GET: SSDF_Certificate
        public ActionResult Index(int? page, string SearchText)
        {
            List<CertificateViewModel> vmCertifikats = new List<CertificateViewModel>();
            List<CertificateViewModel> vmSerchedList = new List<CertificateViewModel>();
            int pageNo = 0;
            pageNo = page == null ? 1 : int.Parse(page.ToString());

            //var sSDF_Certificate = db.SSDF_Certificate.Include(s => s.SSDF_CertCodes);
            //foreach(var cert in sSDF_Certificate)
            //{
            //    var tmpVMCert = new CertificateViewModel(cert);
            //    vmCertifikats.Add(tmpVMCert);
            //}

            vmCertifikats = certResp.CreateCertificateViewModel();

            vmSerchedList = certResp.SearchCertificate(SearchText, vmCertifikats);

            //if (!string.IsNullOrEmpty(SearchText))
            //{
            //    vmSerchedList = vmCertifikats.Where(s => s.Personnummer.Contains(SearchText)).ToList();
            //}
            //else
            //{
            //    vmSerchedList = vmCertifikats.ToList();
            //}

            int totalCertificates = vmSerchedList.Count;
            int certificatePerPage = 50;
            int inEachPageCertificateEndAt = pageNo * certificatePerPage;
            int inEachPagecertificateStarsFrom = inEachPageCertificateEndAt - certificatePerPage;
            var certificates = vmSerchedList.OrderByDescending(e => e.CertificateDate).Skip(inEachPagecertificateStarsFrom).Take(certificatePerPage).ToList();
            Pager<CertificateViewModel> pager = new Pager<CertificateViewModel>(certificates.AsQueryable(), pageNo, certificatePerPage, totalCertificates,SearchText);

            return View(pager);

            //return View(sSDF_Certificate.ToList());
        }

        // GET: SSDF_Certificate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //SSDF_Certificate sSDF_Certificate = db.SSDF_Certificate.Find(id);
            //CertificateViewModel vmCertificate = new CertificateViewModel(sSDF_Certificate);
            var vmCertificate = certResp.FindCertificateById(id);
            if (vmCertificate == null)
            {
                return HttpNotFound();
            }
            return View(vmCertificate);
        }

        // GET: SSDF_Certificate/Create
        public ActionResult Create()
        {
            ViewBag.Certifikat = new SelectList(db.SSDF_CertCodes, "CertCode", "Description");
            return View();
        }

        // POST: SSDF_Certificate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CertificateID, CertificateDate, Certifikat, Förnamn, Efternamn, Personnummer, OriginalPersNo, CertifikatNummer, Instruktör")] CertificateViewModel vmCert)
        {
            if (ModelState.IsValid)
            {
                var ok = certResp.SaveNewCertificate(vmCert, User.Identity.GetUserName());
                return RedirectToAction("Index");
            }

            ViewBag.Certifikat = new SelectList(db.SSDF_CertCodes, "CertCode", "Description", vmCert.Certifikat);
            return View(vmCert);
        }

        // GET: SSDF_Certificate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vmCertificate = certResp.FindCertificateById(id);
            if (vmCertificate == null)
            {
                return HttpNotFound();
            }
           
            return View(vmCertificate);
        }

        // POST: SSDF_Certificate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "CertificateID,CertificateDate,Certifikat,Förnamn,Efternamn,Personnummer,OriginalPersNo,CertifikatNummer,Instruktör")] CertificateViewModel vmCert)
        {
            if (ModelState.IsValid)
            {
                //TODO fixa felhantering
                var ok = certResp.SaveEditCertificate(vmCert, User.Identity.GetUserName());
                //db.Entry(sSDF_Certificate).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(vmCert);
        }

        // GET: SSDF_Certificate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SSDF_Certificate sSDF_Certificate = db.SSDF_Certificate.Find(id);
            if (sSDF_Certificate == null)
            {
                return HttpNotFound();
            }
            return View(sSDF_Certificate);
        }

        // POST: SSDF_Certificate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SSDF_Certificate sSDF_Certificate = db.SSDF_Certificate.Find(id);
            db.SSDF_Certificate.Remove(sSDF_Certificate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
