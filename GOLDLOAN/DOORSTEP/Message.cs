﻿using MACOM.Contracts.Alerts;
using MACOM.Integrations;

namespace DOORSTEP
{
    public class Message
    {
        public void SendSms(string Subject, int firmid, int branchid, string documentid, int moduleid, string customerid, string sender, string to, string message)
        {
            string _clientID = Guid.NewGuid().ToString();

            // Console.WriteLine("Client Ready");

            NotificationClient _notificationClient = new NotificationClient(_clientID, "10.192.5.44:19092");

            int _OTPNo = new Random().Next(100000, 999999);

            NotificationRequest _notificationRequest = new NotificationRequest();


            _notificationRequest.From = "MABEN RD";

            _notificationRequest.MessageId = _OTPNo;//Unique No

            _notificationRequest.MessageType = "SMS";

            _notificationRequest.MessageDate = DateTime.Now;

            _notificationRequest.Status = "0";

            _notificationRequest.Subject = Subject; //"New SD Created";

            _notificationRequest.DocumentDetail.FirmId = (byte)firmid;

            _notificationRequest.DocumentDetail.BranchId = (byte)branchid;

            _notificationRequest.DocumentDetail.DocumentId = documentid;

            _notificationRequest.DocumentDetail.ModuleId = 3;

            _notificationRequest.DocumentDetail.CustomerId = customerid;

            NotificationMessage _SMS = new NotificationMessage();




            _SMS.sender = sender;

            //_SMS.to = "9846626720";

            // _SMS.to = to;
            _SMS.to = "9633102738";  //Sreya

            _SMS.message = String.Format(message, _OTPNo);

            // "Dear MABEN customer, Your OTP for KYC submition is  {0}  .Do not share this OTP with anyone- MABEN NIDHI LIMITED",


            _notificationRequest.SmsBatch = new NotificationMessages();

            _notificationRequest.SmsBatch.sms.Add(_SMS);

            string transactionMessage = _notificationRequest.ToString();

            //Console.WriteLine("Send Message {0} ", transactionMessage);



            _notificationClient.PostMessage(transactionMessage);
        }


    }
}
