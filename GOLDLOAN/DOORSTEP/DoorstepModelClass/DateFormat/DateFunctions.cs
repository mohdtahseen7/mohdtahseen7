using DOORSTEP.DoorstepModel;
using Microsoft.EntityFrameworkCore;

namespace DOORSTEP.DoorstepModelClass.DateFormat
{
    public class DateFunctions
    {
        public static DateTime sysdate(DoorStepContext db)
        {
            var date = db.Duals.FromSqlRaw("Select sysdate from dual;").ToListAsync();
            var timeonly = date.Result.Select(x => x.SysDate).SingleOrDefault();
            return timeonly;
        }

        public static DateTime sysdatewithouttime(DoorStepContext db)
        {
            var dateTimeResult = db.Duals.FromSqlRaw("SELECT SYSDATE FROM DUAL;").ToListAsync();

            // Extract the date part from the first (and only) result
            var dateOnly = dateTimeResult.Result.Select(x => x.SysDate).SingleOrDefault();
            var date = dateOnly.Date;
            return (DateTime)date;
        }

    }
}
