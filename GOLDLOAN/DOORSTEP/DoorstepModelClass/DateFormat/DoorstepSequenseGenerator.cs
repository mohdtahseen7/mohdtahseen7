using DOORSTEP.DoorstepModel;
using System.Text;

namespace DOORSTEP.DoorstepModelClass.DateFormat
{
    public class DoorstepSequenseGenerator
    {
        //new customer id generate
        public static int Customer_id(DoorStepContext db, byte pfirmid, short pbranchid, byte pmoduleid, int pkeyid)
        {
            var data1 = db.KeyMasters.FirstOrDefault(x => x.FirmId == pfirmid && x.BranchId == pbranchid && x.ModuleId == pmoduleid && x.KeyId == pkeyid);
            int result = 0;
            if (data1 != null)
            {
                if (int.TryParse(data1.Value, out int parsedValue))
                {

                    result = parsedValue + 1;
                    data1.Value = result.ToString();
                }
                else
                {

                    result = 0;
                }
            }

            //int result=(int)(++data!.Value);
            return result;




        }

        //request id generate
        public static string request_id()
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string digits = "0123456789";
            Random random = new Random();

            StringBuilder sequence = new StringBuilder(10);

            for (int i = 0; i < 5; i++)
            {
                sequence.Append(alphabets[random.Next(alphabets.Length)]);
                sequence.Append(digits[random.Next(digits.Length)]);
            }

            return sequence.ToString();
        }

        public static string Otp_trans_id()
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string digits = "0123456789";
            Random random = new Random();

            StringBuilder sequence = new StringBuilder(10);

            for (int i = 0; i < 5; i++)
            {
                sequence.Append(alphabets[random.Next(alphabets.Length)]);
                sequence.Append(digits[random.Next(digits.Length)]);
            }

            return sequence.ToString();
        }
        //security code generation
        public static string Security_code()
        {
            Random rnd = new Random();

            string validChars = DateTime.Now.Ticks.ToString();
            string pin = "";
            for (int i = 0; i < 6; i++)
            {
                pin += validChars[rnd.Next(0, validChars.Length)];
            }
            return pin;
        }



    }
}
