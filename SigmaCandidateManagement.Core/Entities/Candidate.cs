using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaCandidateManagement.Core.Entities
{
    public class Candidate
    {
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Phone]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string PhoneNumber { get; set; }

        [Key]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; } // Primary Key

        [StringLength(50, ErrorMessage = "Call time interval cannot exceed 50 characters.")]
        public string CallTimeInterval { get; set; }

        [Url(ErrorMessage = "Invalid LinkedIn profile URL.")]
        [StringLength(200, ErrorMessage = "LinkedIn profile URL cannot exceed 200 characters.")]
        public string LinkedInProfile { get; set; }

        [Url(ErrorMessage = "Invalid GitHub profile URL.")]
        [StringLength(200, ErrorMessage = "GitHub profile URL cannot exceed 200 characters.")]
        public string GitHubProfile { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        public string Comments { get; set; }
    }
}
