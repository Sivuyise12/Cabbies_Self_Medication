using ImfuyoRanch.Models;
using Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ImfuyoRanch.Services
{
    public class EmailSender
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static void sendOrderTrackEmail(string id)
        {

            Order order = db.Orders.Find(id);
            var customers = db.Customers.Where(x => x.Email == order.Email).FirstOrDefault();
            var mailTo = new List<MailAddress>();

            mailTo.Add(new MailAddress(order.Email, customers.FirstName));
            var body = $"Hello {customers.FirstName}, <br/><br/> " +
                $"Thank you.<br/> We have Received your Order. This email Confirms your Order and will be received upon every change that happens to your order. If you have any further enquiries feel free to contact us.";
            ImfuyoRanch.Services.Email_Service emailService = new ImfuyoRanch.Services.Email_Service();
            emailService.SendEmail(new EmailContent()
            {
                mailTo = mailTo,
                mailCc = new List<MailAddress>(),
                mailSubject = $"Order Progress( {order.status} )  Reference: {order.Order_ID}",
                mailBody = body,
                mailFooter = "<br/> Many Thanks, <br/> <b>Imfuyo Ranch</b>",
                mailPriority = MailPriority.High,
                mailAttachments = new List<Attachment>()
            });
        } 
        public static void sendDriverEmail(AssignEmployee assignEmployee)
        {

            var employe = db.Employees.Where(x => x.EmployeeId == assignEmployee.EmployeeId).FirstOrDefault();
            var mailTo = new List<MailAddress>();

            mailTo.Add(new MailAddress(employe.EmployeeEmail, employe.EmployeeName));
            var body = $"Hello {employe.EmployeeName}, <br/><br/> " +
                $"Please note you have a delivery scheduled for today please come and collect for delivery.";
            ImfuyoRanch.Services.Email_Service emailService = new ImfuyoRanch.Services.Email_Service();
            emailService.SendEmail(new EmailContent()
            {
                mailTo = mailTo,
                mailCc = new List<MailAddress>(),
                mailSubject = $"Order Number( {assignEmployee.Order_ID} )",
                mailBody = body,
                mailFooter = "<br/> Many Thanks, <br/> <b>Imfuyo Ranch</b>",
                mailPriority = MailPriority.High,
                mailAttachments = new List<Attachment>()
            });
        }
    }
}