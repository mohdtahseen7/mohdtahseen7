using System.Globalization;

namespace GOLDLOAN.ModelClass.DateFormat;

public class ConvertDateFormat
{

   public static string convertDateFormat(string dateString,string desiredFormat)
    {
        string[] supportedFormats = { "yyyy-MM-dd", "MM/dd/yyyy", "yyyy-MM-ddTHH:mm:ss","dd/M/yyyy","dd-MM-yyyy"};
        foreach (string format in supportedFormats)
        {
            if(DateTime.TryParseExact(dateString,format,CultureInfo.InvariantCulture,DateTimeStyles.None,out var parsedDate)) 
            {
                return parsedDate.ToString(desiredFormat);
            }
        }

        throw new Exception("Unsupported date format");
    }
}
