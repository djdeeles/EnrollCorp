namespace EnrollKurumsal.Kutuphaneler
{
    public static class getCurrentPath
    {
        public static string setCurrent { get; set; }

        public static string currentPath
        {
            get { return setCurrent; }
        }
    }

    public class MenuPath
    {
        public MenuPath()
        {
            SetPath = SetMenuPath;
        }

        public string SetMenuPath { get; set; }
        public static string SetPath { get; set; }

        public static string CurrentMenuPath
        {
            get { return SetPath; }
        }
    }
}