using System.Collections.Generic;

namespace M3uParser
{
    public class PlayItem
    {
        public PlayItem()
        {
            
        }

        public PlayItem(string url, double duration)
        {
            Url = url;
            Duration = duration;
        }

        public Dictionary<string, string> ExtentionData { get; set; } = new Dictionary<string, string>();
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public double Duration { get; set; }
    }
}
