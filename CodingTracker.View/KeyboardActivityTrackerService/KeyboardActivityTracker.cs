using CodingTracker.View.TimerDisplayService.StopWatchTimerServices;
using Gma.System.MouseKeyHook;
using CodingTracker.View.KeyboardActivityTrackerService;

namespace CodingTracker.View.KeyboardActivityTrackers
{


    public interface IKeyboardActivityTracker
    {

    }

    public class KeyboardActivityTracker : IDisposable
    {
        private IKeyboardMouseEvents _keyboardMouseEvents;
        private IStopWatchTimerService _stopWatchTimerService;

        private Queue<DateTime> keyPressTimestamps = new Queue<DateTime>();
        private double lastKeystrokeTimeMs;

        private float currentIntensity = 0.0f;
        private float intensityBuildUpRate = IntensityValues.MediumBuildUp;
        private float intensityDecayRate = IntensityValues.MediumDecay;
        private double rapidTypingThreshold = 200.0; // 200 milliseconds.
        private float MultiplierCap = 5.0f;

        public float ActivityLevel => currentIntensity;
        public EventHandler<float> ActivityChanged;

        public KeyboardActivityTracker(IStopWatchTimerService stopWatchTimerService)
        {
            _stopWatchTimerService = stopWatchTimerService;
            lastKeystrokeTimeMs = _stopWatchTimerService.ReturnElapsedMilliseconds();
            Subscribe();
        }


        private void Subscribe()
        {
            _keyboardMouseEvents = Hook.GlobalEvents();
            _keyboardMouseEvents.KeyPress += GlobalHookKeyPress;
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            CalculateElapsedTimeSinceLastKeyPress();
        }

        private void CalculateElapsedTimeSinceLastKeyPress()
        {
            double currentTimeMs = _stopWatchTimerService.ReturnElapsedMilliseconds();
            double intervalMs = currentTimeMs - lastKeystrokeTimeMs;
            lastKeystrokeTimeMs = currentTimeMs;
        }

        private float CalculateTypingMultiplier(double intervalMs)
        {
            float boost = intensityBuildUpRate;
            float multiplier = (float)(Math.Min(MultiplierCap, rapidTypingThreshold / Math.Max(10.0, intervalMs)));
            boost *= multiplier;
            return multiplier;

        }


        public float CalculateIntensityBuildUp(double intervalMs)
        {
            float boost = intensityBuildUpRate;

            if(rapidTypingThreshold > intervalMs)
            {
               boost = CalculateTypingMultiplier(intervalMs);
            }
        }





        private void UpdateDecay()
        {
            if (currentIntensity > 0)
            {
                currentIntensity = Math.Max(0.0f, currentIntensity - intensityDecayRate);

                if(currentIntensity > 0.01f || currentIntensity == 0)
                {
                    ActivityChanged.Invoke(this, currentIntensity);
                }
            }
        }

        private int CalculateKeyStrokesLastMinute(DateTime dateTimeNow)
        {

        }

    }

}