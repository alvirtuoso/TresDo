using System;
namespace ThreeDo.DbContext
{
    public class AppOptions
    {
        public AppOptions()
        {
            // Set default value.
            DefaultConnection = "";
        }

        public string DefaultConnection { get; set; }
    }
}
