//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSDF_CertificateAdmin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SSDF_Certificate
    {
        public int CertificateID { get; set; }
        public Nullable<System.DateTime> CertificateDate { get; set; }
        public string CertificateLevel { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonNo { get; set; }
        public string OriginalPersNo { get; set; }
        public string CertificateNumber { get; set; }
        public string Instructor { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime LastEditDate { get; set; }
        public string LastEditBy { get; set; }
        public string InstructorNo { get; set; }
        public string MachineID { get; set; }
    
        public virtual SSDF_CertCodes SSDF_CertCodes { get; set; }
    }
}
