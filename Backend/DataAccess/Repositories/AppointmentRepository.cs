using DataAccess.Context;
using Entities.Models;
using Entities.Repositories;

namespace DataAccess.Repositories;
internal sealed class AppointmentRepository : Repository<Appointment, AppDbContext>, IAppointmentRepository
{
    public AppointmentRepository(AppDbContext context) : base(context)
    {
    }
}
