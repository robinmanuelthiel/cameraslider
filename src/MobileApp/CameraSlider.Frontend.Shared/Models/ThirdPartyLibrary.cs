using System;

namespace CameraSlider.Frontend.Shared.Models
{
    public class ThirdPartyLibrary
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Url { get; set; }
        public Platform Platform { get; set; }

        public ThirdPartyLibrary(string name, string author, string url, Platform platform)
        {
            this.Name = name;
            this.Author = author;
            this.Url = url;
            this.Platform = platform;
        }
    }

    public enum Platform
    {
        Xamarin
    }
}
