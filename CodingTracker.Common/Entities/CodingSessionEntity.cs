﻿using System.ComponentModel.DataAnnotations;
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

        public DateOnly StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateOnly EndDate { get; set; }
        public DateTime EndTime { get; set; }
        public double DurationSeconds { get; set; }

        public string DurationHHMM { get; set; }

        public bool GoalSet {  get; set; }
        public int GoalMinutes { get; set; }

        public bool GoalReached { get; set; }
    }
}
