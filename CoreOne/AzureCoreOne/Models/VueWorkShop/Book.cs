namespace AzureCoreOne.Models.VueWorkShop
{
    public class Rootobject
    {
        public string gitbook { get; set; }
        public string title { get; set; }
        public string[] plugins { get; set; }
        public Pluginsconfig pluginsConfig { get; set; }
        public ThemeDefault themedefault { get; set; }
    }
}
