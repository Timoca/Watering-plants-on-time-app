using PlantsWateringApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PlantsWateringApp.Forms
{
    public partial class SettingsForm : Form
    {
        private readonly NumericUpDown intervalDaysNumericUpDown;
        private readonly Button saveButton;
        private readonly Button cancelButton;

        public int IntervalDays => (int)intervalDaysNumericUpDown.Value;

        public SettingsForm(ReminderSettings reminderSettings)
        {
            Text = "Instellingen";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(320, 160);
            TopMost = true;

            Label intervalLabel = new()
            {
                Text = "Herinnering om de hoeveel dagen:",
                AutoSize = true,
                Left = 20,
                Top = 20
            };

            intervalDaysNumericUpDown = new NumericUpDown
            {
                Left = 20,
                Top = 50,
                Width = 100,
                Minimum = 1,
                Maximum = 365,
                Value = Math.Clamp(reminderSettings.IntervalDays, 1, 365)
            };

            saveButton = new Button
            {
                Text = "Opslaan",
                Left = 40,
                Top = 100,
                Width = 100,
                DialogResult = DialogResult.OK
            };

            cancelButton = new Button
            {
                Text = "Annuleren",
                Left = 160,
                Top = 100,
                Width = 100,
                DialogResult = DialogResult.Cancel
            };

            Controls.Add(intervalLabel);
            Controls.Add(intervalDaysNumericUpDown);
            Controls.Add(saveButton);
            Controls.Add(cancelButton);

            AcceptButton = saveButton;
            CancelButton = cancelButton;
        }
    }
}
