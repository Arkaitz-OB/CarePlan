using System.ComponentModel.DataAnnotations;

namespace CarePlan.Models
{
    public class Plan
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(450)]
        [Required]
        public string Title { get; set; }

        [MaxLength(450)]
        [Required]
        public string PatientName { get; set; }

        [MaxLength(450)]
        [Required]
        public string UserName { get; set; }

        [Required]
        public System.DateTime StartDate { get; set; }

        [Required]
        public System.DateTime TargetDate { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Reason { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Action { get; set; }

        public bool? Completed { get; set; }

        /// <summary>Required when Completed is true. Cannot be before StartDate.</summary>
        public System.DateTime? EndDate { get; set; }

        /// <summary>Required when Completed is true.</summary>
        [MaxLength(1000)]
        public string Outcome { get; set; }
    }

}