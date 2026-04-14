namespace PlantsWateringApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new App.StartupApplicationContext());
        }
    }
}