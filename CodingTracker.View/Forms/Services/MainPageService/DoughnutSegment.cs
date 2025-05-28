using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View.Forms.Services.MainPageService.DoughnutSegments
{
    public class DoughnutSegment
    {
        public float StartAngle { get; set; }
        public float SweepAngle { get; set; }
        public Color Color1 { get; set; }
        public Color Color2 { get; set; }
        public string Label { get; set; }
        public int Value { get; set; }
    }
}
