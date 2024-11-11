using GOLDLOAN.ModelClass;
using Microsoft.EntityFrameworkCore;
using GOLDLOAN.ModelClass.DateFormat;
namespace GOLDLOAN.ModelClass.DateFormat
{
    public static class DateFunctions
    {

        public static DateTime sysdatewithtime(ModelContext db)
        {
            var date = db.Duals.FromSqlRaw("Select sysdate from dual;").ToListAsync();
            var timeonly = date.Result.Select(x => x.SysDate).SingleOrDefault();
            return timeonly;
        }


        public static DateTime sysdatewithouttime(ModelContext db)
        {
            var dateTimeResult = db.Duals.FromSqlRaw("SELECT SYSDATE FROM DUAL;").ToListAsync();

            // Extract the date part from the first (and only) result
            var dateOnly = dateTimeResult.Result.Select(x => x.SysDate).SingleOrDefault();
            var date = dateOnly.Date;
            return (DateTime)date;
        }




        //public static DateTime sysdate(CommonModelContext db1)
        //{
        //    var date = db1.Duals.FromSqlRaw("Select sysdate from dual;").ToListAsync();
        //    var timeonly = date.Result.Select(x => x.SysDate).SingleOrDefault();
        //    return timeonly;
        //}

        //        public static DateTime LastMonthDate(PNLModelContext db)
        //        {
        //            var firstDayMonth = new DateTime(sysdate(db).Year, sysdate(db).Month, 1);
        //            var lastDayMonth = firstDayMonth.AddMonths(-1);
        //            return lastDayMonth;
        //        }
        //        public static DateTime LastMonthDate1(PNLModelContext db)
        //        {
        //            var firstDayMonth = new DateTime(sysdate(db).Year, sysdate(db).Month, 1);
        //            var lastDayMonth = firstDayMonth.AddDays(-1);
        //            return firstDayMonth;
        //        }
        //        public static bool ValidateAmount(PNLModelContext db, decimal depositAmount, decimal Amount)
        //        {

        //            var maximumClosingAmount = db.GeneralParameters.Where(x => x.ParmtrId == 19 && x.ModuleId == 1 && x.FirmId == 2).Select(x => Convert.ToDecimal(x.ParmtrValue)).SingleOrDefault();
        //            if ((depositAmount + Amount) <= maximumClosingAmount)
        //            {
        //                return false;
        //            }
        //            return true;

        //        }

        //        public static double CalculateDueDate(PNLModelContext db, DateTime TransactionDate, string id)
        //        {

        //            if (id.Length > 14)
        //            {
        //                var rdmaster = db.PnlMsts.Where(x => x.DepositId == id).SingleOrDefault();
        //                if (rdmaster.InstallmentNumber == rdmaster.DepPeriod)
        //                {
        //                    return 0;
        //                }
        //                else
        //                {
        //                    var date = (sysdate(db).Year - TransactionDate.Year) * 12 + sysdate(db).Month - TransactionDate.Month + (DateFunctions.sysdate(db).Day >= TransactionDate.Day ? 0 : -1);

        //                    return date;
        //                }
        //            }/*((DateTime.Now.Year - depositDate.Year) * 12) + DateTime(sysdate(db).Now.Month)- depositDate.Month;*/
        //            else
        //            {
        //                var rdmaster = db.RdMasters.Where(x => x.CustId == id).SingleOrDefault();
        //                if (rdmaster.InstallmentNumber == rdmaster.DepPeriod)
        //                {
        //                    return 0;
        //                }
        //                else
        //                {
        //                    var date = (sysdate(db).Year - TransactionDate.Year) * 12 + sysdate(db).Month - TransactionDate.Month + (DateFunctions.sysdate(db).Day >= TransactionDate.Day ? 0 : -1);

        //                    return date;
        //                }
        //            }
        //        }


        //        public static double CalculateDueDate(PNLModelContext db, DateTime TranDate, DateTime ClosingDate)
        //        {


        //            var date = (ClosingDate.Year - TranDate.Year) * 12 + ClosingDate.Month - TranDate.Month + (ClosingDate.Day >= TranDate.Day ? 0 : -1);

        //            return date; /*((DateTime.Now.Year - depositDate.Year) * 12) + DateTime(sysdate(db).Now.Month)- depositDate.Month;*/
        //        }
        //        public static string GetFinancialYear(PNLModelContext db, DateTime TransactionDate)
        //        {

        //            int CurrentYear = TransactionDate.Year;
        //            int PreviousYear = (TransactionDate.Year - 1);
        //            int NextYear = (TransactionDate.Year + 1);
        //            string PreYear = PreviousYear.ToString();
        //            string NexYear = NextYear.ToString();
        //            string CurYear = CurrentYear.ToString();
        //            string FinYear = string.Empty;
        //            string FinDate = string.Empty;
        //            if (TransactionDate.Month > 3)
        //            {
        //                FinYear = CurYear + "-" + NexYear;
        //                FinDate = "1/4/" + NexYear;

        //            }
        //            else
        //            {
        //                FinYear = PreYear + "-" + CurYear;
        //                FinDate = "1/4/" + CurYear;
        //            }


        //            return FinDate;
        //        }



    }
}
