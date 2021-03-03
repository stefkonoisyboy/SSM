namespace StudentsSocialMedia.Web.ViewModels.Profiles
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class EditProfileInputModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Town")]
        public string TownId { get; set; }

        public IEnumerable<SelectListItem> Towns { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
