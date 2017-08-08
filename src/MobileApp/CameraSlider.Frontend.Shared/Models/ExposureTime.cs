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
            new ExposureTime("1/125", 8),
            new ExposureTime("1/60", 16),
            new ExposureTime("1/30", 33),
            new ExposureTime("1/15", 66),
            new ExposureTime("1/8", 125),
            new ExposureTime("1/4", 250),
            new ExposureTime("1/2", 500),
            new ExposureTime("1", 1000),
            new ExposureTime("2", 2000),
            new ExposureTime("3", 3000),
            new ExposureTime("4", 4000),
            new ExposureTime("5", 5000),
            new ExposureTime("6", 6000),
            new ExposureTime("7", 7000),
            new ExposureTime("8", 8000),
            new ExposureTime("9", 9000),
            new ExposureTime("10", 10000),
            new ExposureTime("15", 15000),
            new ExposureTime("20", 20000),
            new ExposureTime("25", 25000),
            new ExposureTime("30", 30000),
            new ExposureTime("40", 40000),
            new ExposureTime("50", 50000),
            new ExposureTime("1 min", 60000),
            new ExposureTime("1,5 min", 90000),
            new ExposureTime("2 min", 120000),
            new ExposureTime("2,5 min", 150000),
            new ExposureTime("3 min", 180000),
            new ExposureTime("3,5 min", 210000),
            new ExposureTime("4 min", 240000),
            new ExposureTime("4,5 min", 270000),
            new ExposureTime("5 min", 300000)
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