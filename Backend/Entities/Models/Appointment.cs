namespace Entities.Models;
public sealed class Appointment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid DoctorId { get; set; }
    public AppUser? Doctor { get; set; }
    public Guid PatientId { get; set; }
    public AppUser? Patient { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string EpicrisisReport { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsItFinished { get; set; }
}
