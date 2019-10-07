using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSDF_CertificateAdmin.ViewModels;
using SSDF_CertificateAdmin.Models;
using System.Data.Entity;

namespace SSDF_CertificateAdmin.Repository
{
    public class CertificateRepository
    {
        private SSDF_CERTEntities1 db = new SSDF_CERTEntities1();
        private List<CertificateViewModel> vmCertifikats = new List<CertificateViewModel>();
        private DateTime dateLimit = DateTime.Parse("2010-01-01");
        public List<CertificateViewModel> CreateCertificateViewModel()
        {
            var sSDF_Certificate = db.SSDF_Certificate.Include(s => s.SSDF_CertCodes);
            foreach (var cert in sSDF_Certificate)
            {
                var tmpVMCert = new CertificateViewModel(cert);
                vmCertifikats.Add(tmpVMCert);
            }
           
            return vmCertifikats;
        }

        public List<CertificateViewModel> SearchCertificate(string serachTerm, List<CertificateViewModel> listToSerach)
        {
            string a;


            if (!string.IsNullOrEmpty(serachTerm))
            {
                var newlist = listToSerach.Where(s => s.Personnummer.Contains(serachTerm)).ToList();
                foreach (var m in newlist)
                    a = m.Efternamn;
                return newlist;
            }
            return listToSerach;
        }

        public CertificateViewModel FindCertificateById(int? id)
        {
            if (id == null) { return null; }

            SSDF_Certificate sSDF_Certificate = db.SSDF_Certificate.Find(id);

            var vmcert = new CertificateViewModel(sSDF_Certificate);

            vmcert.CertCodes = db.SSDF_CertCodes.Select(x => new System.Web.Mvc.SelectListItem
            {
                Value = x.CertCode,
                Text = x.Description
            });

            return vmcert;
        }

        public int SaveEditCertificate(CertificateViewModel vmCert, string loggedInUser)
        {
            SSDF_Certificate sDF_Certificate = db.SSDF_Certificate.Find(vmCert.CertificateID);

            sDF_Certificate.CertificateDate = vmCert.CertificateDate;
            sDF_Certificate.CertificateID = vmCert.CertificateID;
            sDF_Certificate.CertificateLevel = vmCert.Certifikat;
            sDF_Certificate.CertificateNumber = vmCert.CertifikatNummer;
            sDF_Certificate.FirstName = vmCert.Förnamn;
            sDF_Certificate.LastName = vmCert.Efternamn;
            sDF_Certificate.PersonNo = vmCert.Personnummer;
            sDF_Certificate.Instructor = vmCert.Instruktör;

            //TODO Fixa
            sDF_Certificate.LastEditBy = loggedInUser;
            sDF_Certificate.LastEditDate = DateTime.Now;

            try
            {
                db.Entry(sDF_Certificate).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                return 99;
            }
  
           return 0;
        }

        public int SaveNewCertificate(CertificateViewModel vmCert, string loggedInUser)
        {
            SSDF_Certificate sDF_Certificate = new SSDF_Certificate();

            sDF_Certificate.CertificateDate = vmCert.CertificateDate;
            //sDF_Certificate.CertificateID = vmCert.CertificateID;
            sDF_Certificate.CertificateLevel = vmCert.Certifikat;
            sDF_Certificate.CertificateNumber = vmCert.CertifikatNummer;
            sDF_Certificate.FirstName = vmCert.Förnamn;
            sDF_Certificate.LastName = vmCert.Efternamn;
            sDF_Certificate.PersonNo = vmCert.Personnummer;
            sDF_Certificate.Instructor = vmCert.Instruktör;

            //TODO Fixa
            sDF_Certificate.CreatedBy = loggedInUser;
            sDF_Certificate.CreatedDate = DateTime.Now;
            sDF_Certificate.LastEditBy = loggedInUser;
            sDF_Certificate.LastEditDate = DateTime.Now;

            try
            {
                db.SSDF_Certificate.Add(sDF_Certificate);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                return 99;
            }

            return 0;
           
        }
    }
}