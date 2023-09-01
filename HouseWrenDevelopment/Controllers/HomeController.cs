using HouseWrenDevelopment.Models;
using HouseWrenDevelopment.Models.Contact;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HouseWrenDevelopment.Controllers
{
    public class HomeController : Controller
    {
        private EmailAddress FromAndToEmailAddress;
        private IEmailService EmailService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(EmailAddress _fromAddress,
            IEmailService _emailService, ILogger<HomeController> logger)
        {
            FromAndToEmailAddress = _fromAddress;
            EmailService = _emailService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(ContactForm model)
        { 
            if (ModelState.IsValid)
            {
                EmailMessage msgToSend = new EmailMessage
                {
                    FromAddresses = new List<EmailAddress> { FromAndToEmailAddress },
                    ToAddresses = new List<EmailAddress> { FromAndToEmailAddress },
                    Content = $"New message from:\n" +
                    $"Name: {model.Name}, " +
                        $"Email: {model.Email} \nMessage: {model.Message}",
                    Subject = "HouseWrenDev Contact - " + model.Subject
                };

                EmailService.Send(msgToSend);
                return RedirectToAction("Contact");
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}