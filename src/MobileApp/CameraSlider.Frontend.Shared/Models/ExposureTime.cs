using System;
using System.Collections.Generic;

namespace CameraSlider.Frontend.Shared.Models
{
    public class ExposureTime
    {
        public static List<ExposureTime> Times = new List<ExposureTime>
        {
            new ExposureTime("1/1000", 1),
            new ExposureTime("1/500", 2),
            new ExposureTime("1/250", 4),
            new ExposureTime("1/60", 16),
        };

        public string Name { get; set; }
        public int Milliseconds { get; set; }

        public ExposureTime(string name, int milliseconds)
        {
            this.Name = name;
            this.Milliseconds = milliseconds;
        }
    }
}