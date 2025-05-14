using CodingTracker.Common.BusinessInterfaces;
using CodingTracker.Common.IInputValidationResults;
using CodingTracker.Common.IInputValidators;
using CodingTracker.Common.LoggingInterfaces;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CodingTracker.Business.InputValidators
{
    public class InputValidator : IInputValidator
    {
        private readonly IApplicationLogger _appLogger;
        private readonly IInputValidationResult _validResult;

        public InputValidator(IApplicationLogger appLogger, IInputValidationResult validResult)
        {
            _appLogger = appLogger;
            _validResult = validResult;
        }

        public InputValidationResult ValidateUsername(string username)
        {
            var result = new InputValidationResult();
            Stopwatch stopwatch = Stopwatch.StartNew();

            _appLogger.Debug($"Starting {nameof(ValidateUsername)}.");

            if (string.IsNullOrWhiteSpace(username))
            {
                result.AddErrorMessage("Username cannot be empty or whitespace.");
            }

            if (username.Length > 15)
            {
                result.AddErrorMessage("Username must be 15 characters or less.");
            }

            if (!string.IsNullOrWhiteSpace(username) && !char.IsUpper(username[0]))
            {
                result.AddErrorMessage("Username must begin with a capital letter.");
            }

            stopwatch.Stop();
            _appLogger.Info($"Username validation completed. Duration: {stopwatch.ElapsedMilliseconds}ms.");

            return result;
        }

        public InputValidationResult ValidatePassword(string password)
        {
            var result = new InputValidationResult();
            Stopwatch stopwatch = Stopwatch.StartNew();

            _appLogger.Debug($"Starting {nameof(ValidatePassword)}.");

            if (string.IsNullOrWhiteSpace(password))
            {
                result.AddErrorMessage("PasswordHash cannot be empty or whitespace.");
            }

            if (password.Length > 15)
            {
                result.AddErrorMessage("PasswordHash must be 15 characters or less.");
            }

            if (!password.Any(char.IsUpper))
            {
                result.AddErrorMessage("PasswordHash must contain at least one uppercase letter.");
            }

            if (!new Regex("[^a-zA-Z0-9]").IsMatch(password))
            {
                result.AddErrorMessage("PasswordHash must contain at least one special character.");
            }

            stopwatch.Stop();
            _appLogger.Info($"PasswordHash validation completed. Duration: {stopwatch.ElapsedMilliseconds}ms.");

            return result;
        }

        public bool CheckDateInput(string input, out DateTime result)
        {
            return DateTime.TryParseExact(input, "yy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }

        public bool CheckTimeInput(string input, out DateTime result)
        {
            return DateTime.TryParseExact(input, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }

        public bool IsValidTimeFormatHHMM(string input)
        {
            _appLogger.Info($"Starting {nameof(IsValidTimeFormatHHMM)} for {input}.");

            if (string.IsNullOrWhiteSpace(input))
            {
                _appLogger.Error($"Input for {IsValidTimeFormatHHMM} is null or empty. Input: {input}.");
                return false;
            }

            string pattern = @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";

            bool isValid = Regex.IsMatch(input, pattern);
            if (isValid)
            {
                _appLogger.Info($"Input {input} is a valid HH:MM format.");
            }
            else
            {
                _appLogger.Error($"Input {input} is not a valid HH:MM format.");
            }

            return isValid;
        }

        public bool TryParseTime(string input, out TimeSpan timeSpan)
        {
            timeSpan = default;

            if (IsValidTimeFormatHHMM(input))
            {
                string[] parts = input.Split(':');
                if (int.TryParse(parts[0], out int hours) && int.TryParse(parts[1], out int minutes))
                {
                    timeSpan = new TimeSpan(hours, minutes, 0);
                    return true;
                }
            }

            return false;
        }

        public DateTime GetValidDateFromUser()
        {
            while (true)
            {
                Console.WriteLine("Enter enter date start time (yy-MM-dd):");
                string userInput = Console.ReadLine();

                if (CheckDateInput(userInput, out DateTime result))
                {
                    return result;
                }

                Console.WriteLine("Invalid date format please enter in format yy-MM-dd");
            }
        }

        public DateTime GetValidTimeFromUser()
        {
            while (true)
            {
                string userInput = Console.ReadLine();

                if (CheckTimeInput(userInput, out DateTime result))
                {
                    return result;
                }

                Console.WriteLine("Invalid time format. Please try again.");
            }
        }

        public bool ValidateTimeFormat(string time)
        {
            if (time.Length != 4 || !int.TryParse(time, out int _))
                return false;

            int hours = int.Parse(time.Substring(0, 2));
            int minutes = int.Parse(time.Substring(2, 2));

            if (hours > 23 || minutes > 59)
                return false;

            return true;
        }
    }
}