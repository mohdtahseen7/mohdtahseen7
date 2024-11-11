using OtpNet;
using System.Text;

namespace DOORSTEP.DoorstepModelClass.DateFormat
{
    public class Computeotp
    {
        //public static string OTP(string userkey)
         public static int OTP()
        {

            //byte[] secretebyte = Encoding.ASCII.GetBytes(userkey);
            //Totp totp = new Totp(secretebyte, step: 30, totpSize: 6);

            //string pin = totp.ComputeTotp();
            //return pin;
            Random random = new Random();
            int pin = random.Next(100000, 999999);
            return pin;
        }
    }
}
