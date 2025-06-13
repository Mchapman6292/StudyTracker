using Guna.UI2.Material.Animation;
using Guna.UI2.WinForms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace CodingTracker.View.Forms.Services.SharedFormServices.CustomGradientButtons
{
    /// <summary>
    /// Important design points:
    /// - EnableHoverRipple property allows ripple animation on mouse hover instead of just click events.
    /// To trigger the ripple effect on hover we override OnPaint to draw custom ripple circles using AnimationManager progress values, with the ripple origin calculated from the button's inner rectangle center rather than click coordinates.
    /// _isRealClick flag differentiates between mouseEnter hover events and actual user button presses,preventing hover animations from triggering unintended click actions.
    /// GetInnerRectangle() confines ripple to a safe rectangular area that avoids rounded corners
    /// Other approaches caused the CustomEdges to highlight or the ripple effect to  extend beyond the button's visual boundaries, creating unwanted effects.
    ///

    public class CustomGradientButton : Guna2GradientButton
    {
        private bool _enableHoverRipple = false;
        private bool _isHoverRippleActive = false;
        private bool _isRealClick = false;

        [Browsable(true)]
        [DefaultValue(false)]
        [Description("Enable ripple animation on hover instead of just click")]
        public bool EnableHoverRipple
        {
            get => _enableHoverRipple;
            set => _enableHoverRipple = value;
        }

        public CustomGradientButton()
        {
            this.MouseEnter += OnCustomMouseEnter;
            this.MouseLeave += OnCustomMouseLeave;
            
            // Seems to be responsible for running one animation at a time, if not set ripple effect bugs out.
            AnimationManager.Singular = true;
        }

        private void OnCustomMouseEnter(object sender, EventArgs e)
        {
            if (EnableHoverRipple && this.Animated)
            {
                _isHoverRippleActive = true;
                TriggerRippleAnimation();
            }
        }

        private void OnCustomMouseLeave(object sender, EventArgs e)
        {
            if (EnableHoverRipple && this.Animated)
            {
                _isHoverRippleActive = false;
                StopRippleAnimation();
            }
        }

        public void TriggerRippleAnimation()
        {
            Point centerPoint = new Point(this.Width / 2, this.Height / 2);
            AnimationManager?.StartNewAnimation(AnimationDirection.In, centerPoint);
        }

        public void StopRippleAnimation()
        {
            Point centerPoint = new Point(this.Width / 2, this.Height / 2);
            AnimationManager?.StartNewAnimation(AnimationDirection.Out, centerPoint);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (EnableHoverRipple && _isHoverRippleActive)
            {
                DrawCustomHoverRipple(e.Graphics);
            }
        }

        private void DrawCustomHoverRipple(Graphics g)
        {
            if (!this.Enabled || AnimationManager == null) return;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            for (int i = 0; i < AnimationManager.GetAnimationCount(); i++)
            {
                double progress = AnimationManager.GetProgress(i);

                float centerX = this.Width / 2;
                float centerY = this.Height / 2;

                /// Need to calculate the distance from the center to the furthest point so we use LERP.
                //// result = start + (end - start) * t
               
                
                Color rippleColor = Color.FromArgb(
                    (int)Math.Max(1, 101 - progress * 100),
                    Color.White);

                // Pythagorean theorem: a^2 + b^2 = c^2
                float maxDistance = (float)Math.Sqrt((this.Width / 2.0) * (this.Width / 2.0) +(this.Height / 2.0) * (this.Height / 2.0));
                float rippleRadius = (float)progress * maxDistance;

                /// SetClip sets drawing to only be done within a certain area, keeping as might be needed.
                /*
                var originalClip = g.Clip;
                g.SetClip();
                */

                var originalClip = g.Clip;
                try
                {
                    using (var brush = new SolidBrush(rippleColor))
                    {
                        float rippleDiameter = rippleRadius * 2;
                        RectangleF rippleRect = new RectangleF(
                            centerX - rippleRadius,
                            centerY - rippleRadius,
                            rippleDiameter,
                            rippleDiameter);

                        g.FillEllipse(brush, rippleRect);
                    }
                }
                finally
                {
                    g.Clip = originalClip;
                }

            }
        }

        public Size CalculateButtonEdges()
        {
            return  this.GetPreferredSize(this.Size);
        }

        /// <summary>
        /// Calculates a rectanglular area within the button to apply the ripple effect to.
        /// Other approaches caused the CustomEdges to highlight or the ripple effect to  extend beyond the button's visual boundaries, creating unwanted effects.
        /// 
        private RectangleF GetInnerRectangle()
        {
            return RectangleF.Empty;
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isRealClick = true;
                _isHoverRippleActive = false; 
            }
            base.OnMouseDown(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (_isRealClick)
            {
                base.OnClick(e);
                _isRealClick = false;
            }
            else if (EnableHoverRipple)
            {
                return;
            }
            else
            {
                base.OnClick(e);
            }
        }


    }
}