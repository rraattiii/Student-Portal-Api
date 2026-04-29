using System.ComponentModel.DataAnnotations;

namespace Student_portal.Web.Models.Entities
{
    public class Student
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        [StringLength(30)]
        public string Phone { get; set; } = string.Empty;

        [Display(Name = "Active Subscription")]
        public bool Subscription { get; set; }
    }
}
