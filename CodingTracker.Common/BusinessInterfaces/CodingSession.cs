

namespace CodingTracker.Common.CodingSessions
{
    public class CodingSession
    {
        public int? SessionId { get; set; } = null;
        public int UserId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateOnly? EndDate { get; set; }
        public DateTime? EndTime { get; set; }
        public int? DurationSeconds { get; set; }
        public string DurationHHMM { get; set; } = string.Empty;
        public bool? GoalSet { get; set; }
        public int? GoalSeconds { get; set; }   
        public bool? GoalReached { get; set; } 












    }
}
