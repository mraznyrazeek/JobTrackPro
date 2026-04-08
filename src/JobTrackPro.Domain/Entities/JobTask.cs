using JobTrackPro.Domain.Common;

namespace JobTrackPro.Domain.Entities;

public class JobTask : BaseEntity
{
    public int JobApplicationId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }

    public JobApplication? JobApplication { get; set; }
}