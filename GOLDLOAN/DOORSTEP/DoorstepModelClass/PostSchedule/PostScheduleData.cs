namespace DOORSTEP.DoorstepModelClass.PostSchedule
{
    public class PostScheduleData:BaseData
    {
        public string Customerid { get; set; }
        public decimal Growssweight { get; set; }
        public decimal Amount { get; set; }
        public int Pincode { get; set; }
        public string Addressone { get; set; }
        public string Addresstwo { get; set; }
        public string Scheduledate { get; set; }
        public string Scheduletime { get; set; }
        public int Branchid { get; set; }
    }
}