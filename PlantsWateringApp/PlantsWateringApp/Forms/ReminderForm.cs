using PlantsWateringApp.Models;

namespace PlantsWateringApp
{
    public partial class ReminderForm : Form
    {
        public event EventHandler? DoneClicked;
        public event EventHandler? NotYetClicked;
        public event EventHandler? SettingsClicked;

        public ReminderForm(ReminderSettings reminderSettings)
        {
            Text = "Planten water geven";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(460, 210);
            TopMost = true;

            Label messageLabel = new()
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = $"De planten moeten water krijgen.\r\n\r\nHerinnering elke {reminderSettings.IntervalDays} dagen.",
                Dock = DockStyle.Top,
                Height = 110
            };

            Button doneButton = new()
            {
                Text = "Gedaan",
                Width = 110,
                Height = 35,
                Left = 40,
                Top = 130
            };
            doneButton.Click += (_, _) =>
            {
                DoneClicked?.Invoke(this, EventArgs.Empty);
                Close();
            };

            Button notYetButton = new()
            {
                Text = "Nog niet",
                Width = 110,
                Height = 35,
                Left = 170,
                Top = 130
            };
            notYetButton.Click += (_, _) =>
            {
                NotYetClicked?.Invoke(this, EventArgs.Empty);
                Close();
            };

            Button settingsButton = new()
            {
                Text = "Instellingen",
                Width = 110,
                Height = 35,
                Left = 300,
                Top = 130
            };
            settingsButton.Click += (_, _) =>
            {
                SettingsClicked?.Invoke(this, EventArgs.Empty);
            };

            Controls.Add(messageLabel);
            Controls.Add(doneButton);
            Controls.Add(notYetButton);
            Controls.Add(settingsButton);
        }
    }
}
