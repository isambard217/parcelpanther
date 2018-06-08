using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PantherParcel.Database;
using PantherParcel.Models;
using PantherParcel.Models.Local;
using System.Net.Mail;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace PantherParcel.Controllers {

  
    public class HomeController : BaseController {
        
        // Inject below part to any controller that needs to work with database
        private readonly ConnectionStrings _connectionStrings;
        private DataManager dataManager;

        public HomeController(IOptions<ConnectionStrings> options) : base(options.Value.DefaultConnection) {

            this._connectionStrings = options.Value;
            dataManager = new DataManager(_connectionStrings.DefaultConnection);
        }


        public IActionResult Index() {
            
            return View();
        }

        public IActionResult About() {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact() {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error() {
            
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public string ParcelEnquiry([FromBody]ParcelEnquiry parcelEnquiry) {

                long parcelEnquiryId = dataManager.SaveEnquiry(parcelEnquiry);

                for (int i = 0; i < parcelEnquiry.AllParcels.Count(); i ++) {

                    dataManager.InsertParcels(parcelEnquiry.AllParcels[i], parcelEnquiryId);

                }


            // Send Email

            StringBuilder emailBody = new StringBuilder();

            emailBody.Append("<strong><p>From Country</p></strong>");
                emailBody.Append(parcelEnquiry.fromCountry);             emailBody.Append("<br />");
            emailBody.Append("<br />");
            emailBody.Append("<strong><p>From Address</p></strong>");                 emailBody.Append(parcelEnquiry.fromAddress);             emailBody.Append("<br />");
            emailBody.Append("<br />");
            emailBody.Append("<strong><p>To Country</p></strong>");                 emailBody.Append(parcelEnquiry.toCountry);             emailBody.Append("<br />");
            emailBody.Append("<br />");
            emailBody.Append("<strong><p>To Address</p></strong>");                 emailBody.Append(parcelEnquiry.toAddress);               emailBody.Append("<br />");
            emailBody.Append("<br />");
            emailBody.Append("<strong><p>Contact Number</p></strong>");                 emailBody.Append(parcelEnquiry.contactNumber);               emailBody.Append("<br />");
            emailBody.Append("<br />");
            emailBody.Append("<strong><p>ParcelDescription</p></strong>");                 emailBody.Append(parcelEnquiry.parcelDescription);               emailBody.Append("<br />");
            emailBody.Append("<br />");
            emailBody.Append("<strong><p> Parcel Quantity </p></strong>");                 emailBody.Append(parcelEnquiry.parcelQuantity);   

            emailBody.Append("<br />");
            emailBody.Append("<br />");
            emailBody.Append("<strong><p> individal parcels </p></strong>");

            for (int q = 0; q < parcelEnquiry.AllParcels.Count(); q++) {

                ParcelDetails parcela = parcelEnquiry.AllParcels[q];

                emailBody.AppendFormat("Height:{0} ", parcela.height);
                emailBody.AppendFormat("Length:{0} ", parcela.length);
                emailBody.AppendFormat("Width:{0} ", parcela.width);
                emailBody.AppendFormat("Weight:{0} ", parcela.weight);
                emailBody.Append("<br />");
                emailBody.Append("<br />");
                emailBody.Append("<br />");

            }

            MailMessage mailMessage = new MailMessage("chaywa4@gmail.com","chaywa4@gmail.com");
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

            } catch (Exception e ) {

                string error = e.Message;
            }

           // client.Send("chaywa4@gmail.com", "iwa4@student.le.ac.uk", "Booking", emailBody.ToString());              // End Send Email

            //return View("~/Views/Home/Booking.cshtml", new ParcelDetails());
            return JsonConvert.SerializeObject(parcelEnquiry.contactNumber);
                               
        }


        public IActionResult Booking() {
            
                return View();

        }

    }
}
