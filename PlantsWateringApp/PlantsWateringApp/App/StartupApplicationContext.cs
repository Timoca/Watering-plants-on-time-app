using PlantsWateringApp.Forms;
using PlantsWateringApp.Models;
using PlantsWateringApp.Services;

namespace PlantsWateringApp.App
{
    public sealed class StartupApplicationContext : ApplicationContext
    {
        private readonly ReminderStateService reminderStateService;
        private ReminderSettings reminderSettings;
        private ReminderForm? reminderForm;

        public StartupApplicationContext()
        {
            reminderStateService = new ReminderStateService();
            reminderSettings = reminderStateService.LoadSettings();

            EnsureAutoStart();

            if (ShouldShowReminder())
            {
                ShowReminderForm();
            }
            else
            {
                ExitThread();
            }
        }

        private void EnsureAutoStart()
        {
            AutoStartService.SetAutoStart(
                applicationName: "PlantsWateringApp",
                enabled: true);
        }

        private bool ShouldShowReminder()
        {
            ReminderState reminderState = reminderStateService.LoadState();

            if (reminderState.LastCompletedUtc is null)
            {
                return true;
            }

            DateOnly lastCompletedDate = DateOnly.FromDateTime(reminderState.LastCompletedUtc.Value.ToLocalTime());
            DateOnly nextReminderDate = lastCompletedDate.AddDays(reminderSettings.IntervalDays);
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            return today >= nextReminderDate;
        }

        private void ShowReminderForm()
        {
            reminderForm = new ReminderForm(reminderSettings);
            reminderForm.FormClosed += ReminderForm_FormClosed;
            reminderForm.DoneClicked += ReminderForm_DoneClicked;
            reminderForm.NotYetClicked += ReminderForm_NotYetClicked;
            reminderForm.SettingsClicked += ReminderForm_SettingsClicked;
            reminderForm.Show();
        }

        private void ReminderForm_DoneClicked(object? sender, EventArgs e)
        {
            ReminderState reminderState = reminderStateService.LoadState();
            reminderState.LastCompletedUtc = DateTime.UtcNow;
            reminderStateService.SaveState(reminderState);

            ExitThread();
        }

        private void ReminderForm_NotYetClicked(object? sender, EventArgs e)
        {
            ExitThread();
        }

        private void ReminderForm_SettingsClicked(object? sender, EventArgs e)
        {
            using SettingsForm settingsForm = new(reminderSettings);

            if (settingsForm.ShowDialog(reminderForm) == DialogResult.OK)
            {
                reminderSettings.IntervalDays = settingsForm.IntervalDays;
                reminderStateService.SaveSettings(reminderSettings);

                reminderForm?.Close();
                ShowReminderForm();
            }
        }

        private void ReminderForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            ExitThread();
        }
    }
}
