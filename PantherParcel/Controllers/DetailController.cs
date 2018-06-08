using System;
using Microsoft.AspNetCore.Mvc;

namespace PantherParcel.Controllers {

using PantherParcel.Models;


[Route("api/[controller]")]
public class DetailController {
        
        public DetailController() {
            
        }

        [HttpPost]
        protected string ParcelEnquiry([FromBody]ParcelEnquiry parcelEnquiry) {

            long parcelEnquiryId = dataManager.SaveEnquiry(parcelEnquiry);

            for (int i = 0; i < parcelEnquiry.AllParcels.Count(); i++) {

                dataManager.InsertParcels(parcelEnquiry.AllParcels[i], parcelEnquiryId);

            }

            // Send Email

            StringBuilder emailBody = new StringBuilder();  

            emailBody.Append(parcelEnquiry.fromCountry);
            emailBody.Append("<br />");
            emailBody.Append(parcelEnquiry.fromAddress);
            emailBody.Append("<br />");
            emailBody.Append(parcelEnquiry.toCountry);
            emailBody.Append("<br />");
            emailBody.Append(parcelEnquiry.toAddress);
            emailBody.Append("<br />");
            emailBody.Append(parcelEnquiry.contactNumber);
            emailBody.Append("<br />");
            emailBody.Append(parcelEnquiry.parcelDescription);
            emailBody.Append("<br />");
            emailBody.Append(parcelEnquiry.parcelQuantity);


            for (int q = 0; q < parcelEnquiry.AllParcels.Count(); q++) {

                ParcelDetails parcela = parcelEnquiry.AllParcels[q];

                emailBody.AppendFormat("Height:{0}", parcela.height);
                emailBody.AppendFormat("Length:{0}", parcela.length);
                emailBody.AppendFormat("Width:{0}", parcela.width);
                emailBody.AppendFormat("Weight:{0}", parcela.weight);
                emailBody.Append("<br />");
                emailBody.Append("<br />");
                emailBody.Append("<br />");

            }

            MailMessage mailMessage = new MailMessage("chaywa4@gmail.com", "chaywa4@gmail.com");
            mailMessage.Subject = "Booking";
            mailMessage.Body = emailBody.ToString();
            mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(emailBody.ToString(), new System.Net.Mime.ContentType("text/html")));

            var client = new SmtpClient("smtp.gmail.com", 587);

            client.Credentials = new System.Net.NetworkCredential() {

                //Credentials = new NetworkCredential("chaywa4@gmail.com", "Mad2behere"),
                //EnableSsl = true
                UserName = "chaywa4@gmail.com",
                Password = "Mad2behere"
            };

            try {

                client.EnableSsl = true;
                client.Send(mailMessage);

            } catch (Exception e) {

                string error = e.Message;
            }

            return JsonConvert.SerializeObject(parcelEnquiry.contactNumber);


        } // End Method



    }
}
