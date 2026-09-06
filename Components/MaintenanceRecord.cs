namespace AircraftIndustry.Components;

/// <summary>
/// запись о проведенном техническом обслуживании летательного аппарата
/// </summary>
public class MaintenanceRecord
{
    public DateTime Date { get; }
    public string Description { get; }
    public decimal Cost { get; } // 128 bit decimal number
    public int FlightHoursAtService { get; }

    public MaintenanceRecord(DateTime date, string description, decimal cost, int flightHoursAtService)
    {
        Date = date;
        Description = description;
        Cost = cost;
        FlightHoursAtService = flightHoursAtService;
    }

    public override string ToString() =>
        $"{Date:dd.MM.yyyy} - {Description} (at service {FlightHoursAtService} h, {Cost:N2}$)";
}