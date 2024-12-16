using ASPNETCoreIdentityDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ASPNETCoreIdentityDemo.Models.InvoiceModel;

namespace ASPNETCoreIdentityDemo.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult NonSecureMethod()
        {
            return View();
        }

        [Authorize]
        public IActionResult SecureMethod()
        {
            return View();
        }

        //Generate Invoice PDF using iText
        public IActionResult GenerateInvoicePDF()
        {
            // Sample invoice data
            // Here, we have hardcoded the data,
            // In Real-time you will get the data from the database
            var invoice = new Invoice
            {
                InvoiceNumber = "INV-DOTNET-1001",
                Date = DateTime.Now,
                CustomerName = "Pranaya Rout",
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { ItemName = "Item 1", Quantity = 2, UnitPrice = 15.0m },
                    new InvoiceItem { ItemName = "Item 2", Quantity = 3, UnitPrice = 10.0m },
                    new InvoiceItem { ItemName = "Item 3", Quantity = 1, UnitPrice = 35.0m }
                },
                PaymentMode = "COD"
            };

            //Set the Total Amount
            invoice.TotalAmount = invoice.Items.Sum(x => x.TotalPrice);

            //Create an Instance of PDFService
            PDFService pdfService = new PDFService();

            //Call the GeneratePDF method passing the Invoice Data
            var pdfFile = pdfService.GeneratePDF(invoice);

            //Return the PDF File
            return File(pdfFile, "application/pdf", "Invoice.pdf");
        }


        public IActionResult DownloadPDF()
        {
            return View();
        }
    }
}