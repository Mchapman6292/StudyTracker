using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTracker.Common.Entities.CodingSessionEntities
{
    public class CodingSessionEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; } // Default value indicating not set. 

        [ForeignKey("UserCredentials")]

        public int UserId { get; set; }

        public DateOnly StartDateUTC { get; set; }
        public DateTime StartTimeUTC { get; set; }
        public DateOnly EndDateUTC { get; set; }
        public DateTime EndTimeUTC { get; set; }
        public int DurationSeconds { get; set; }

        public string DurationHHMM { get; set; }

        public bool GoalSet { get; set; }
        public int GoalSeconds { get; set; }

        public bool GoalReached { get; set; }

        public string StudyProject { get; set; } 
        public string StudyNotes { get; set; }



        // These hold the local time for start and end times, used mainly for the DataGridViewManager class. 
        [NotMapped]
        public DateOnly StartDateLocal => DateOnly.FromDateTime(DateTime.SpecifyKind(StartDateUTC.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc).ToLocalTime());

        [NotMapped]
        public DateTime StartTimeLocal => StartTimeUTC.ToLocalTime();

        [NotMapped]
        public DateOnly EndDateLocal => DateOnly.FromDateTime(DateTime.SpecifyKind(EndDateUTC.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc).ToLocalTime());

        [NotMapped]
        public DateTime EndTimeLocal => EndTimeUTC.ToLocalTime();


    }
}
