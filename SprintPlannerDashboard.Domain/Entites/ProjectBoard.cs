using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrintPlannerDashboard.Domain.Entites
{
    public class ProjectBoard
    {
        [Key]
        public int Id { get; set; }
        public string PublicId { get; set; } = $"PROJBOARD-{Guid.NewGuid():N}";
        public string Name { get; set; } = string.Empty; // e.g., "Core API Team Board"
        public int ProjectId { get; set; }

        // Columns configured for this board (e.g., ["To Do", "In Progress", "QA", "Done"])
        public List<string> Columns { get; set; } = new List<string>();

        // Navigation Properties
        public Project Project { get; set; }

        // 1. One Board manages many Sprints over time
        public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();

        // 2. Contains the Product Backlog items belonging to this board's scope
        public virtual ICollection<BacklogItem> ProductBacklog { get; set; } = new List<BacklogItem>();
    }
}
