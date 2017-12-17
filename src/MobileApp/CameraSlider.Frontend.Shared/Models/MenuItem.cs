using System;
namespace CameraSlider.Frontend.Shared.Models
{
    public class MenuItem
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string IconUrl { get; set; }

        public MenuItem(string title, string subtitle, string iconUrl)
        {
            this.Title = title;
            this.Subtitle = subtitle;
            this.IconUrl = iconUrl;
        }
    }
}
