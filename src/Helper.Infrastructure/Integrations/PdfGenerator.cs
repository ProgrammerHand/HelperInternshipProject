using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using Helper.Application.Integrations;
using MigraDoc.Rendering;

namespace Helper.Infrastructure.Integrations
{
    public class PdfGenerator : IPdfGenerator
    {
        public byte[] GeneratePdf(string name) 
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
            XStringFormat format = new XStringFormat();
            format.Alignment = XStringAlignment.Center;
            gfx.DrawString("Hello There!", font, XBrushes.Black,
            new XRect(0, 0, page.Width, page.Height),
            format);
            string filename = name + ".pdf";

            byte[] bin = null;
            using (var stream = new MemoryStream())
            {
                document.Save(stream, false);
                bin = stream.ToArray();
            }
            return bin;
        }
    }
}
