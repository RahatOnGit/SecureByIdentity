using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout;
using static ASPNETCoreIdentityDemo.Models.InvoiceModel;

namespace ASPNETCoreIdentityDemo.Models
{
    public class PDFService
    {
        public byte[] GeneratePDF(Invoice invoice)
        {
            //Define your memory stream which will temporarily hold the PDF
            using (MemoryStream stream = new MemoryStream())
            {
                //Initialize PDF writer
                PdfWriter writer = new PdfWriter(stream);
                //Initialize PDF document
                PdfDocument pdf = new PdfDocument(writer);
                // Initialize document
                Document document = new Document(pdf);
                // Add content to the document
                // Header
                document.Add(new Paragraph("Invoice")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20));
                // Invoice data
                document.Add(new Paragraph($"Invoice Number: {invoice.InvoiceNumber}"));
                document.Add(new Paragraph($"Date: {invoice.Date.ToShortDateString()}"));
                document.Add(new Paragraph($"Customer Name: {invoice.CustomerName}"));
                document.Add(new Paragraph($"Payment Mode: {invoice.PaymentMode}"));
                // Table for invoice items
                Table table = new Table(new float[] { 3, 1, 1, 1 });
                table.SetWidth(UnitValue.CreatePercentValue(100));
                table.AddHeaderCell("Iten Name");
                table.AddHeaderCell("Quantity");
                table.AddHeaderCell("Unit Price");
                table.AddHeaderCell("Total");
                foreach (var item in invoice.Items)
                {
                    table.AddCell(new Cell().Add(new Paragraph(item.ItemName)));
                    table.AddCell(new Cell().Add(new Paragraph(item.Quantity.ToString())));
                    table.AddCell(new Cell().Add(new Paragraph(item.UnitPrice.ToString("C"))));
                    table.AddCell(new Cell().Add(new Paragraph(item.TotalPrice.ToString("C"))));
                }
                //Add the Table to the PDF Document
                document.Add(table);
                // Total Amount
                document.Add(new Paragraph($"Total Amount: {invoice.TotalAmount.ToString("C")}")
                    .SetTextAlignment(TextAlignment.RIGHT));
                // Close the Document
                document.Close();
                return stream.ToArray();
            }
        }
    }
}
