using System;
using System.Collections.Generic;
using System.Text;

namespace WallPaper_Project
{
    public class ApiResponse
    {
        public bool success { get; set; }
        public List<wallpaper> wallpapers { get; set; }
        public string total_match { get; set; }
    }

    public class wallpaper
    {
        public string id { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string file_type { get; set; }
        public string file_size { get; set; }
        public string url_image { get; set; }
        public string url_thumb { get; set; }
        public string url_page { get; set; }
    }
}
