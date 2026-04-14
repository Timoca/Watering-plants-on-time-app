using System;

namespace PlantsWateringApp.Models;

public sealed class ReminderState
{
    public DateTime? LastCompletedUtc { get; set; }
}