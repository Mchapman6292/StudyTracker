using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.LoggingInterfaces;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;



namespace CodingTracker.Common.Utilities
{
    public class UtilityService : IUtilityService
    {
        private readonly IApplicationLogger _appLogger;

        public UtilityService(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
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

        public string ConvertDoubleToHHMMString(double duration)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(duration);
            int totalHours = (int)timeSpan.TotalHours;
            int minutes = timeSpan.Minutes;
            return $"{totalHours} hours {minutes} minutes";
        }


        public string ConvertDurationSecondsToHHMMStringWithSpace(int durationSeconds)
        {
            TimeSpan duration = TimeSpan.FromSeconds(durationSeconds);

            return $"{(int)duration.TotalHours}:{duration.Minutes:D2}";

        }





        public int ConvertHHMMStringToSeconds(string input)
        {
            int hours = int.Parse(input.Substring(0, 2));
            int minutes = int.Parse(input.Substring(2, 2));
            var result = hours * 3600 + minutes * 60;

            _appLogger.Debug($"Result of {nameof(ConvertDoubleToHHMMString)}: {result}.");
            return result;
        }







    }
}