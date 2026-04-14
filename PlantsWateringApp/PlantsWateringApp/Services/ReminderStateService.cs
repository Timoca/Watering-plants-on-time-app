using PlantsWateringApp.Models;
using System.Text.Json;

namespace PlantsWateringApp.Services
{
    public sealed class ReminderStateService
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            WriteIndented = true
        };

        private readonly string appFolderPath;
        private readonly string stateFilePath;
        private readonly string settingsFilePath;

        public ReminderStateService()
        {
            appFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PlantsWateringApp");

            stateFilePath = Path.Combine(appFolderPath, "state.json");
            settingsFilePath = Path.Combine(appFolderPath, "settings.json");

            Directory.CreateDirectory(appFolderPath);
        }

        public ReminderState LoadState()
        {
            if (!File.Exists(stateFilePath))
            {
                return new ReminderState();
            }

            string json = File.ReadAllText(stateFilePath);
            ReminderState? state = JsonSerializer.Deserialize<ReminderState>(json);
            return state ?? new ReminderState();
        }

        public void SaveState(ReminderState reminderState)
        {
            string json = JsonSerializer.Serialize(reminderState, JsonSerializerOptions);
            File.WriteAllText(stateFilePath, json);
        }

        public ReminderSettings LoadSettings()
        {
            if (!File.Exists(settingsFilePath))
            {
                ReminderSettings defaultSettings = new();
                SaveSettings(defaultSettings);
                return defaultSettings;
            }

            string json = File.ReadAllText(settingsFilePath);
            ReminderSettings? settings = JsonSerializer.Deserialize<ReminderSettings>(json);
            return settings ?? new ReminderSettings();
        }

        public void SaveSettings(ReminderSettings reminderSettings)
        {
            string json = JsonSerializer.Serialize(reminderSettings, JsonSerializerOptions);
            File.WriteAllText(settingsFilePath, json);
        }
    }
}
