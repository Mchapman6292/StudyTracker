

namespace CodingTracker.Common.CodingSessions
{
    public class CodingSession
    {
        // Add session topic & project?

        public int? SessionId { get; set; } = null;
        public int UserId { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateOnly? EndDate { get; set; }
        public DateTime? EndTime { get; set; }
        public double? DurationSeconds { get; set; }
        public string DurationHHMM { get; set; } = string.Empty;
        public bool? GoalSet { get; set; }
        public double? GoalSeconds { get; set; }
        public bool? GoalReached { get; set; }
        public string? StudyProject { get; set; }
        public string? StudyNotes { get; set; } 












    }
}
