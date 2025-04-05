﻿using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IUtilityServices;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;



namespace CodingTracker.Common.UtilityServices
{
    public class UtilityService : IUtilityService
    {
        private readonly IApplicationLogger _appLogger;

        public UtilityService(IApplicationLogger appLogger)
        { 
            _appLogger = appLogger; 
        }
        public bool IsValidString(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        public int TryParseInt(string input)
        {
            if (!int.TryParse(input, out int result))
            {
                throw new FormatException($"Unable to parse '{input}' as an integer.");
            }
            return result;
        }

        public bool TryParseDate(string input, out DateTime result)
        {
            return DateTime.TryParseExact(input, "yy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }


        public double CalculatePercentage(double part, double total)
        {
            if (total == 0) return 0;
            return (part / total) * 100;
        }


        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public string ConvertDoubleToHHMM(double duration)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(duration);
            int totalHours = (int)timeSpan.TotalHours;
            int minutes = timeSpan.Minutes;
            return $"{totalHours} hours {minutes} minutes";
        }


    }
}