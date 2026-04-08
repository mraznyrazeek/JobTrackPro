using JobTrackPro.Domain.Common;
using JobTrackPro.Domain.Enums;

namespace JobTrackPro.Domain.Entities;

public class JobApplication : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string? Location { get; set; }
    public WorkMode WorkMode { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public string? Source { get; set; }
    public string? ApplicationUrl { get; set; }
    public string? JobDescription { get; set; }
    public DateTime DateApplied { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Saved;
    public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;
    public string? Notes { get; set; }

    public ApplicationUser? User { get; set; }
    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
    public ICollection<JobTask> Tasks { get; set; } = new List<JobTask>();
}

