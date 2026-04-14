using Microsoft.Win32;
using System.Diagnostics;

namespace PlantsWateringApp.Services
{
    public static class AutoStartService
    {
        public static void SetAutoStart(string applicationName, bool enabled)
        {
            using RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", writable: true);

            if (registryKey is null)
            {
                return;
            }

            if (enabled)
            {
                string executablePath = Process.GetCurrentProcess().MainModule?.FileName ?? Environment.ProcessPath ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(executablePath))
                {
                    registryKey.SetValue(applicationName, $"\"{executablePath}\"");
                }
            }
            else
            {
                registryKey.DeleteValue(applicationName, false);
            }
        }
    }
}
