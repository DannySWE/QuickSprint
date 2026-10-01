using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrintPlannerDashboard.Domain.Entites
{
    public class ProjectTask
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = "Untitled";
        public bool InProgress { get; set; } = true;
        public bool Complete { get; set; } = false;
        [Column(TypeName = "decimal(18, 4)")]
        public decimal EstimatedHoursForCompletion { get; set; }
        public int CompletionPercentage { get; set; }

        public int BacklogItemId { get; set; }
    }
}
