using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrintPlannerDashboard.Domain.Entites
{
    public class BacklogItem
    {
        [Key]
        public int Id { get; set; }
        public string PublicId { get; set; } = $"BACKLOG-{Guid.NewGuid():N}";
        [Required]
        [StringLength(100)]
        public string Title { get; set; }          // e.g., "Add PayPal Checkout"
        [Required]
        [StringLength(500)]
        public string Description { get; set; }    // User story format
        public int StoryPoints { get; set; }       // e.g., 5 points

        // Parent relationship
        public int SprintId { get; set; }
        public virtual Sprint Sprint { get; set; }
        public virtual ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
