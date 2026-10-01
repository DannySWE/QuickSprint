using SprintPlannerDashboard.Application.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrintPlannerDashboard.Domain.Entites
{
    public class Sprint
    {
        [Key]
        public int Id { get; set; }
        public string PublicId { get; set; } = $"SPR-{Guid.NewGuid():N}"; // Using :N formats the GUID without hyphens, making it a clean block of characters, like PROJ-8f3b2a1ac5d64e7f8a9b0c1d2e3f4a5b

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Goal { get; set; } = string.Empty;

        [Required]
        public SprintStatus Status { get; set; } = SprintStatus.Future;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CapacityStoryPoints { get; set; }

        public int CommittedStoryPoints { get; set; }

        public int CompletedStoryPoints { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Foreign Key to the parent Project Board
        public int ProjectBoardId { get; set; }

        // Navigation Property: One sprint contains many backlog items
        public virtual ICollection<BacklogItem> BacklogItems { get; set; } = new List<BacklogItem>();
    }
}
