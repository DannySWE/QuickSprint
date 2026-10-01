using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrintPlannerDashboard.Domain.Entites
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string PublicId { get; set; } = $"PROJ-{Guid.NewGuid():N}";
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string ProjectOwnerId { get; set; } = string.Empty;

        // Navigation Property: One project houses multiple specialized boards
        public virtual ICollection<ProjectBoard> Boards { get; set; } = new List<ProjectBoard>();
    }
}
