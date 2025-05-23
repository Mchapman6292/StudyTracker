

using System.ComponentModel.DataAnnotations;

namespace CodingTracker.Common.CodingSessions
{
    public class CodingSession
    {
        // Add session topic & project?

        public int? SessionId { get; set; } = null;
        public int UserId { get; set; }

        public DateOnly? StartDateLocal { get; set; }
        public DateTime? StartTimeLocal { get; set; }
        public DateOnly? EndDateLocal { get; set; }
        public DateTime? EndTimeLocal { get; set; }
        public int? DurationSeconds { get; set; }
        public string DurationHHMM { get; set; } = string.Empty;
        public bool? GoalSet { get; set; }
        public int? GoalSeconds { get; set; }
        public bool? GoalReached { get; set; }
        public string StudyProject { get; set; } = string.Empty;
        public string StudyNotes { get; set; } = string.Empty ;
        
        public int? SessionStarRating { get; set; }









}
}
