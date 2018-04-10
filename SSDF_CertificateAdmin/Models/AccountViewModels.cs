using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SSDF_CertificateAdmin.Models
{
    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nuvarande lösenord")]
        public string oldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Lösenordet måste vara minst {2} tecken långt", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nytt lösenord")]
        public string newPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Verifiera lösenordet")]
        [Compare("Lösenord", ErrorMessage =
            "Lösenordet och Verifiera lösenordet matchar inte")]
        public string confirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Användarnamn")]
        public string userName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string password { get; set; }

        [Display(Name = "Komma ihåg mig?")]
        public bool rememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Användarnamn")]
        public string userName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage =
            "Lösenordet {0} måste vara minst {2} tecken långt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Verifiera lösenordet")]
        [Compare("password", ErrorMessage =
            "Lösenordet och Verifiera lösenordet matchar inte")]
        public string confirmPassword { get; set; }

        [Required]
        public string email { get; set; }

        public ApplicationUser GetUser()
        {
            var user = new ApplicationUser()
            {
                UserName = this.userName,
                Email = this.email,
            };
            return user;
        }
    }

    public class EditUserViewModel
    {
        public EditUserViewModel() { }

        // Allow Initialization with an instance of ApplicationUser:
        public EditUserViewModel(ApplicationUser user)
        {
            this.userName = user.UserName;
            this.email = user.Email;
        }

        [Required]
        [Display(Name = "Användarnamn")]
        public string userName { get; set; }

        
        [Required]
        public string email { get; set; }
    }

    public class SelectUserRolesViewModel
    {
        public SelectUserRolesViewModel()
        {
            this.Roles = new List<SelectRoleEditorViewModel>();
        }


        // Enable initialization with an instance of ApplicationUser:
        public SelectUserRolesViewModel(ApplicationUser user)
            : this()
        {
            this.UserName = user.UserName;
            
            var Db = new ApplicationDbContext();

            // Add all available roles to the list of EditorViewModels:
            var allRoles = Db.Roles;
            foreach (var role in allRoles)
            {
                // An EditorViewModel will be used by Editor Template:
                var rvm = new SelectRoleEditorViewModel(role);
                this.Roles.Add(rvm);
            }

            // Set the Selected property to true for those roles for 
            // which the current user is a member:
            foreach (var userRole in user.Roles)
            {
                var checkUserRole =
                    this.Roles.Find(r => r.RoleName == userRole.RoleId);
                checkUserRole.Selected = true;
            }
        }

        public string UserName { get; set; }
       
        public List<SelectRoleEditorViewModel> Roles { get; set; }
    }

    public class SelectRoleEditorViewModel
    {
        public SelectRoleEditorViewModel() { }
        public SelectRoleEditorViewModel(IdentityRole role)
        {
            this.RoleName = role.Name;
        }

        public bool Selected { get; set; }

        [Required]
        public string RoleName { get; set; }
    }

    
}
