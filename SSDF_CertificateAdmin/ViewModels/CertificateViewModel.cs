using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SSDF_CertificateAdmin.Models;
using System.Web.Mvc;

namespace SSDF_CertificateAdmin.ViewModels
{
    public class CertificateViewModel
    {
        public int CertificateID { get; set; }
        [Required]
        [DisplayName("Certifikat datum")]
        [DataType(DataType.Date)]
        public DateTime? CertificateDate  { get; set; }
        [Required]
        public string Certifikat { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Förnamn får vara max 50 tecken långt", MinimumLength = 1)]
        public string Förnamn { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Efternamn får vara max 50 tecken långt", MinimumLength = 1)]
        public string Efternamn { get; set; }
        [Required]
        [DisplayName("Personnummer (ÅÅÅÅMMDD-NNNN)")]
        [RegularExpression(@"/(19|20)?[0-9]{8}-[0-9]{4}/", ErrorMessage ="Personnummer skall ha formen ÅÅÅÅMMDD-NNNN" )]
        public string Personnummer  { get; set; }
        [Required]
        [RegularExpression(@"/(SWE)\/(F00)\/[A-Z]{2,}\/(1|2)?[0-9]{2}\/[0-9]{6}/", ErrorMessage = "Ett certifikatsnummer skall skrivas SWE/F00/XX/ÅÅ/000001")]
        public string CertifikatNummer { get; set; }
        [Required]
        //[RegularExpression(@"/(SWE)\/(F00)\/[A-Z]{2,}\/(1|2)?[0-9]{2}\/[0-9]{6}/", ErrorMessage = "Ett certifikatsnnmmer skall skrivas SWE/F00/XX/ÅÅ/000001")]
        public string Instruktör { get; set; }

        [Required]
        public string Instruktörsnummer { get; set; }

        [DisplayName("RB maskintyp")]
        public string MachineId { get; set; }

        public string CertPicture { get; set; }

        public IEnumerable<SelectListItem> CertCodes { get; set; }

        public CertificateViewModel(SSDF_Certificate cert)
        {
            CertificateID = cert.CertificateID;
            CertificateDate = cert.CertificateDate;
            Certifikat = cert.CertificateLevel;
            Förnamn = cert.FirstName;
            Efternamn = cert.LastName;
            Personnummer = cert.PersonNo;
            CertifikatNummer = cert.CertificateNumber;
            Instruktör = cert.Instructor;
            CertPicture = addPicture(cert.CertificateLevel);
            Instruktörsnummer = cert.InstructorNo;
            MachineId = cert.MachineID;
        }

        private string addPicture(string cert)
        {
            string picPath = "../../Media/CertPictures/";
            string picType = ".jpg";
            switch(cert)
            {
                case "AEAN":
                    return picPath + cert + picType;
                    break;
                case "AEANI":
                    return picPath + cert + picType;
                    break;
                case "CCR":
                    return picPath + cert + picType;
                case "CCRI":
                    return picPath + cert + picType;
                    break;
                case "CDB":
                    return picPath + cert + picType;
                    break;
                case "CDG":
                    return picPath + cert + picType;
                    break;
                case "CDS":
                    return picPath + cert + picType;
                case "CDI":
                    return picPath + cert + picType;
                    break;
                case "CDTI":
                    return picPath + cert + picType;
                    break;
                case "DRY":
                    return picPath + cert + picType;
                    break;
                case "SAM":
                    return picPath + "DS" + picType;
                    break;
                case "EAN":
                    return picPath + cert + picType;
                    break;
                case "EANI":
                    return picPath + cert + picType;
                    break;
                case "GBN":
                    return picPath + cert + picType;
                    break;
                case "GBNI":
                    return picPath + cert + picType;
                    break;
                case "GBT":
                    return picPath + cert + picType;
                    break;
                case "GBTI":
                    return picPath + cert + picType;
                    break;
                case "COMP":
                    return picPath + cert + picType;
                    break;
                case "DL":
                    return picPath + cert + picType;
                    break;
                case "GD":
                    return picPath + "CDG" + picType;
                    break;
                case "ICE":
                    return picPath + cert + picType;
                    break;
                case "M1":
                    return picPath + cert + picType;
                    break;
                case "M2":
                    return picPath + cert + picType;
                    break;
                case "M3":
                    return picPath + cert + picType;
                    break;
                case "NTX":
                    return picPath + cert + picType;
                    break;
                case "NTXI":
                    return picPath + cert + picType;
                    break;
                case "OXY":
                    return picPath + cert + picType;
                    break;
                case "P1":
                    return picPath + cert + picType;
                    break;
                case "P2":
                    return picPath + cert + picType;
                    break;
                case "P3":
                    return picPath + cert + picType;
                    break;
                case "P4":
                    return picPath + cert + picType;
                    break;
                case "RSD":
                    return picPath + cert + picType;
                    break;
                case "RSDI":
                    return picPath + cert + picType;
                    break;
                case "SCR":
                    return picPath + cert + picType;
                    break;
                case "SCRI":
                    return picPath + cert + picType;
                    break;
                case "SERV":
                    return picPath + cert + picType;
                    break;
                case "TX50":
                    return picPath + cert + picType;
                    break;
                case "VRAK B":
                    return "noPic";
                    break;
                default:
                    return "noPic";
                    break;

            }
        }

        public CertificateViewModel(){}
    }
}