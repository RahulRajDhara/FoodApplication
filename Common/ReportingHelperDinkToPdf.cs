using DinkToPdf;
using DinkToPdf.Contracts;
using System;

namespace Common
{
    public class ReportingHelperDinkToPdf
    {
        public void CreatePortraitA4PDF(IConverter _converter, string FinalPath, string html, string documenttitle, string PreparedBy = "")
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 4f, Bottom = 6f, Left = 4f, Right = 4f },
                DocumentTitle = documenttitle,
                Out = FinalPath,
                DPI = 300
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = html,
                WebSettings = { DefaultEncoding = "utf-8", LoadImages = true },
                HeaderSettings = { FontName = "Arial", FontSize = 1, Right = "", Line = false, Spacing = 0 },
                FooterSettings = { FontName = "Arial", FontSize = 6, Right = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"), Left = "Printed By : " + PreparedBy, Center = "Page [page] of [toPage]", Line = false, Spacing = 2.812 }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            _converter.Convert(pdf);

        }
        public void CreateLandscapeA4PDF(IConverter _converter, string FinalPath, string html, string documenttitle, string PreparedBy = "")
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Landscape,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 4f, Bottom = 6f, Left = 4f, Right = 4f },
                DocumentTitle = documenttitle,
                Out = FinalPath,
                DPI = 300
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = html,
                WebSettings = { DefaultEncoding = "utf-8", LoadImages = true },
                HeaderSettings = { FontName = "Arial", FontSize = 1, Right = "", Line = false, Spacing = 0 },
                FooterSettings = {FontName = "Arial", FontSize = 6,Right = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"), Left = "Printed By : " + PreparedBy, Center = "Page [page] of [toPage]", Line = false,Spacing = 2.812}
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            _converter.Convert(pdf);
        }

    }
}
