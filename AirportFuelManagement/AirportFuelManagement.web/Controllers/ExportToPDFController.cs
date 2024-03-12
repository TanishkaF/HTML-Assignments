using iText.Kernel.Pdf;
using System;
using System.IO;
using System.Web.Mvc;
using System.Configuration;
using AirportFuelManagement.UtilityLayer;
using iText.Layout;
using iText.Layout.Element;
using System.Collections.Generic;
using System.Linq;
using iText.Layout.Properties;

namespace AirportFuelManagement.Controllers
{
    public class ExportToPDFController : Controller
    {
        // GET: ExportToPDF
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExportAirportFuelConsumptionReportToPDF()
        {
            int pageSize = AirportBL.GetTotalAirportCount();
            var airportFuelReport = AirportBL.GetAllFuelSummary(AirportFuelConstants.SortExpression, AirportFuelConstants.SortDirection, AirportFuelConstants.PageIndex, pageSize);
            return ExportToPDF(airportFuelReport, "Airport_Summary");
        }

        public ActionResult ExportToPDF<T>(List<T> items, string fileName)
        {
            var pdfPath = ConfigurationManager.AppSettings["PdfExportFolderPath"];
            var pdfName = $"{fileName}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            var pdfFilePath = Path.Combine(pdfPath, pdfName);

            if (!Directory.Exists(pdfPath))
            {
                Directory.CreateDirectory(pdfPath);
            }

            using (var writer = new PdfWriter(pdfFilePath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);
                    var table = new iText.Layout.Element.Table(new UnitValue[] { UnitValue.CreatePercentValue(50), UnitValue.CreatePercentValue(50) }); 

                    // Add table headers
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Airport")));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Fuel Available")));

                    foreach (var item in items)
                    {
                        var properties = item.GetType().GetProperties();

                        // Get airport name and fuel available
                        var airportName = properties.FirstOrDefault(p => p.Name == "AirportName")?.GetValue(item)?.ToString() ?? "";
                        var fuelAvailable = properties.FirstOrDefault(p => p.Name == "FuelAvailable")?.GetValue(item)?.ToString() ?? "";

                        // Add airport name and fuel available as separate rows
                        table.AddCell(new Cell().Add(new Paragraph(airportName)));
                        table.AddCell(new Cell().Add(new Paragraph(fuelAvailable)));
                    }

                    document.Add(table);
                }
            }

            return File(pdfFilePath, "application/pdf", pdfName);
        }


        //public ActionResult ExportToPDF<T>(List<T> items, string fileName)
        //{
        //    var pdfPath = ConfigurationManager.AppSettings["PdfExportFolderPath"];
        //    var pdfName = $"{fileName}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
        //    var pdfFilePath = Path.Combine(pdfPath, pdfName);

        //    using (var writer = new PdfWriter(pdfFilePath))
        //    {
        //        using (var pdf = new PdfDocument(writer))
        //        {
        //            var document = new Document(pdf);
        //            var table = new iText.Layout.Element.Table(items.Count * 2); // Adjust the number of columns

        //            foreach (var item in items)
        //            {
        //                var properties = item.GetType().GetProperties();
        //                foreach (var property in properties)
        //                {
        //                    table.AddCell(new Cell().Add(new Paragraph(property.Name))); // Add property name as header
        //                    table.AddCell(new Cell().Add(new Paragraph(property.GetValue(item)?.ToString() ?? ""))); // Add property value
        //                }
        //            }

        //            document.Add(table);
        //        }
        //    }

        //    return File(pdfFilePath, "application/pdf", pdfName);
        //}


        //public ActionResult ExportToPDF<T>(List<T> items, string fileName)
        //{
        //    var pdfPath = ConfigurationManager.AppSettings["PdfExportFolderPath"];
        //    var pdfName = $"{fileName}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
        //    var pdfFilePath = Path.Combine(pdfPath, pdfName);

        //    using (var writer = new PdfWriter(pdfFilePath))
        //    {
        //        using (var pdf = new PdfDocument(writer))
        //        {
        //            var document = new Document(pdf);
        //            var table = new iText.Layout.Element.Table(2);

        //            foreach (var item in items)
        //            {
        //                var properties = item.GetType().GetProperties();
        //                foreach (var property in properties)
        //                {
        //                    table.AddCell(new Cell().Add(new Paragraph(property.GetValue(item)?.ToString() ?? "")));
        //                }
        //            }

        //            document.Add(table);
        //        }
        //    }

        //    return File(pdfFilePath, "application/pdf", pdfName);
        //}

        //public ActionResult ExportFuelConsumptionReportToPDF()
        //{
        //    var fuelConsumptionReport = AirportBL.GetFuelConsumptionReport();
        //    byte[] pdfBytes = GeneratePdfBytes(fuelConsumptionReport);
        //    string fileName = "Fuel_Consumption_Report";
        //    var pdfName = $"{fileName}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
        //    return File(pdfBytes, "application/pdf", pdfName);
        //}

        public ActionResult ExportFuelConsumptionReportToPDF()
        {
            var fuelConsumptionReport = AirportBL.GetFuelConsumptionReport();
            byte[] pdfBytes = GeneratePdfBytes(fuelConsumptionReport);
            string fileName = "Fuel_Consumption_Report";
            var pdfName = $"{fileName}_{DateTime.Now:yyyyMMddHHmmss}.pdf";

            var pdfFolderPath = ConfigurationManager.AppSettings["PdfExportFolderPath"];
            var pdfFilePath = Path.Combine(pdfFolderPath, pdfName);

            if (!Directory.Exists(pdfFolderPath))
            {
                Directory.CreateDirectory(pdfFolderPath);
            }

            System.IO.File.WriteAllBytes(pdfFilePath, pdfBytes);

            return File(pdfBytes, "application/pdf", pdfName);
        }


        private byte[] GeneratePdfBytes(List<AirportFuelManagement.ViewModel.AllViewModel.FuelConsumptionReportItem> report)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (PdfWriter writer = new PdfWriter(stream))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document document = new Document(pdf);

                        foreach (var item in report)
                        {
                            document.Add(new Paragraph($"Airport: {item.AirportName}"));
                            Table table = new Table(4);
                            table.AddHeaderCell("Date/time");
                            table.AddHeaderCell("Type");
                            table.AddHeaderCell("Fuel");
                            table.AddHeaderCell("Aircraft");

                            foreach (var transaction in item.Transactions)
                            {
                                table.AddCell(transaction.CreatedDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                                table.AddCell(transaction.Type);
                                table.AddCell(transaction.Fuel.ToString());
                                table.AddCell(transaction.Aircraft);
                            }

                            document.Add(table);
                            document.Add(new Paragraph($"Fuel Available: {item.FuelAvailable}"));
                           // document.Add(new LineSeparator());
                        }
                    }
                }

                return stream.ToArray();
            }
        }


    }
}