using JobTrackPro.Domain.Common;
using JobTrackPro.Domain.Enums;

namespace JobTrackPro.Domain.Entities;

public class Interview : BaseEntity
{
    public int JobApplicationId { get; set; }
    public string RoundName { get; set; } = string.Empty;
    public DateTime InterviewDate { get; set; }
    public InterviewMode InterviewMode { get; set; }
    public string? InterviewType { get; set; }
    public string? InterviewerName { get; set; }
    public string? InterviewerEmail { get; set; }
    public string? Notes { get; set; }
    public string? Outcome { get; set; }

    public JobApplication? JobApplication { get; set; }
}
