using CodingTracker.View.TimerDisplayService.StopWatchTimerServices;
using Gma.System.MouseKeyHook;

namespace CodingTracker.View.KeyboardActivityTrackerService.KeyboardActivityTrackers
{


    public interface IKeyboardActivityTracker
    {
        float CurrentIntensity { get; }
        event EventHandler<float> ActivityChanged;
        void UpdateDecay();
    }

    public class KeyboardActivityTracker : IKeyboardActivityTracker, IDisposable
    {
        public float CurrentIntensity { get; private set; } = 0.0f;

        private const float SlowBuildUp = 0.05f;
        private const float MediumBuildUp = 0.08f;
        private const float FastBuildUp = 0.12f;

        private const float SlowDecay = 0.02f;
        private const float MediumDecay = 0.04f;
        private const float FastDecay = 0.06f;

        public event EventHandler<float> ActivityChanged; // event = only containing class can trigger event. 

        private IKeyboardMouseEvents _keyboardMouseEvents;
        private IStopWatchTimerService _stopWatchTimerService;

        private Queue<DateTime> keyPressTimestamps = new Queue<DateTime>();
        private double lastKeystrokeTimeMs;


        private float intensityBuildUpRate = MediumBuildUp;
        private float intensityDecayRate = MediumDecay;
        private double rapidTypingThreshold = 200.0; // 200 milliseconds.
        private float MultiplierCap = 5.0f;


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

            double currentTimeMs = _stopWatchTimerService.ReturnElapsedMilliseconds();
            double intervalMs = currentTimeMs - lastKeystrokeTimeMs;

            // First calculate the interval & then update the lastKeyStrokeTime.
            lastKeystrokeTimeMs = currentTimeMs;

            // Calculate the interval & then update CurrentIntensity.
            CalculateAndUpdateICurrentIntensity(intervalMs);


            ActivityChanged?.Invoke(this, CurrentIntensity);
        }



        private float CalculateTypingMultiplier(double intervalMs)
        {
            return (float)(Math.Min(MultiplierCap, rapidTypingThreshold / Math.Max(10.0, intervalMs)));
        }

        private void CalculateAndUpdateICurrentIntensity(double intervalMs)
        {
            float intensityValue = CalculateIntensityIncremental(intervalMs);
            float finalIntensityValue = Math.Min(1.0f, CurrentIntensity + intensityValue);
            UpdateCurrentIntensity(finalIntensityValue);
        }

        // Should only be called by CalculateAndUpdateICurrentIntensity
        private float CalculateIntensityIncremental(double intervalMs)
        {
            float intensityIncremental = intensityBuildUpRate;
            if (rapidTypingThreshold > intervalMs)
            {
                float multiplier = CalculateTypingMultiplier(intervalMs);
                intensityIncremental *= multiplier;
            }
            return intensityIncremental;
        }

        private void UpdateCurrentIntensity(float currentIntensity)
        {
            CurrentIntensity = currentIntensity;
        }



        private void UpdateIntensityBuildUp(float intensityValue)
        {
            intensityBuildUpRate = intensityValue;
        }



        // Decay is updated continuously and is called from the animationTimer 
        public void UpdateDecay()
        {
            if (CurrentIntensity > 0)
            {
                CurrentIntensity = Math.Max(0.0f, CurrentIntensity - intensityDecayRate);

                if (CurrentIntensity > 0.01f || CurrentIntensity == 0)
                {
                    ActivityChanged?.Invoke(this, CurrentIntensity);
                }
            }
        }


        public void Dispose()
        {
            if (_keyboardMouseEvents != null)
            {
                _keyboardMouseEvents.KeyPress -= GlobalHookKeyPress;
                _keyboardMouseEvents.Dispose();
                _keyboardMouseEvents = null;
            }
        }

    }

}