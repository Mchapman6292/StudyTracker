using System.ComponentModel.DataAnnotations;


namespace CodingTracker.Common.CodingSessions
{
    public class CodingSession
    {
        [Key]
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateOnly? EndDate { get; set; }
        public DateTime? EndTime { get; set; }
        public int? DurationSeconds { get; set; }
        public string DurationHHMM { get; set; } = string.Empty;
        public int? GoalMinutes { get; set; }   
        public bool? GoalReached { get; set; } = false;












    }
}
