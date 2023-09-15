using Helper.Application.Integrations;
using Helper.Core.Inquiry;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.Offer;
using Helper.Core.Utility;
using Microsoft.Extensions.Configuration;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using System.Drawing;

namespace Helper.Infrastructure.Integrations
{
    public class PdfGenerator : IPdfGenerator
    {
        private float pointerPosition = 100;
        private readonly IClockCustom _clock;
        private readonly IConfiguration _configuration;

        public PdfGenerator(IClockCustom clock, IConfiguration configuration) 
        {
            _clock = clock;
            _configuration = configuration;
        }

        private float MovePointer(float step)
        {
            pointerPosition += step;
            return pointerPosition;
        }

        private static double RectSize(XSize textSize, SizeF pageSize, double margin, XFont font)
        {
            return ((textSize.Width / (pageSize.Width - (margin * 2))) + 1) * font.Height;
        }

        public byte[] GenerateOffer(Inquiry Inquiry, Offer Offer) 
        {

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            var xfSansHeader = new XFont("Cambria", 16.0, XFontStyle.Bold);
            var xfSansBody = new XFont("Calibri", 12.0, XFontStyle.Regular);
            var xfSansBodyBold = new XFont("Calibri", 12.0, XFontStyle.Bold);

            var margin = 20.0F;
            var pageSize = new SizeF((float)gfx.PageSize.Width, (float)gfx.PageSize.Height);
            var createdAt = _clock.Now;


            gfx.DrawLine(XPens.Black, new XPoint(margin, pointerPosition), new XPoint(pageSize.Width - margin, pointerPosition));
            gfx.DrawString("Client:", xfSansBody, XBrushes.Black, new XPoint(margin, MovePointer(30)));
            gfx.DrawString("User", xfSansBody, XBrushes.Black, new XPoint(margin, MovePointer(xfSansBody.Height)));


            var rts = gfx.MeasureString($"Created at: {createdAt.Day}.{createdAt.Month}.{createdAt.Year}", xfSansBody);
            gfx.DrawString($"Created at: {createdAt.Day}.{createdAt.Month}.{createdAt.Year}", xfSansBody, XBrushes.Black, new XPoint(pageSize.Width - margin - (float)rts.Width, MovePointer(0)));
            rts = gfx.MeasureString($"Expires at at: {((DateTime)Offer.PaymentDate).Day}.{((DateTime)Offer.PaymentDate).Month}.{((DateTime)Offer.PaymentDate).Year}", xfSansBody);
            gfx.DrawString($"Expires at at: {((DateTime)Offer.PaymentDate).Day}.{((DateTime)Offer.PaymentDate).Month}.{((DateTime)Offer.PaymentDate).Year}", xfSansBody, XBrushes.Black, new XPoint(pageSize.Width - margin - (float)rts.Width, MovePointer(xfSansBody.Height)));

            gfx.DrawString("Users", xfSansBody, XBrushes.Black, new XPoint(margin, MovePointer(0)));
            gfx.DrawString($"email: {Inquiry.Author.Email.Value}", xfSansBody, XBrushes.Black, new XPoint(margin, MovePointer(xfSansBody.Height)));


            pointerPosition = pageSize.Height / 3;
            var cts = gfx.MeasureString($"Offer {Offer.Id.Value}", xfSansHeader);
            gfx.DrawString($"Offer {Offer.Id.Value}", xfSansHeader, XBrushes.Black,
                new XPoint(((pageSize.Width - (float)cts.Width) / 2), pageSize.Height / 3));


            XTextFormatter tf = new XTextFormatter(gfx);
            var tSize = gfx.MeasureString($"Clients inquiry:{Inquiry.Description.Value}", xfSansBody);
            var oldrectSize = RectSize(tSize, pageSize, margin, xfSansBody);
            XRect rect = new XRect(margin, MovePointer(xfSansBody.Height), pageSize.Width - (margin * 2), oldrectSize);
            gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb((int)(.100 * 255), 255, 255, 255)), rect);
            tf.DrawString($"Clients inquiry:", xfSansBody, XBrushes.Black, rect, XStringFormats.TopLeft);
            tf.DrawString($"Clients inquiry:{Inquiry.Description.Value}", xfSansBody, XBrushes.Black, rect, XStringFormats.TopLeft);

            tSize = gfx.MeasureString($"Feasibility note:{Inquiry.FeasibilityNote.Value}", xfSansBody);
            var rectSize = RectSize(tSize, pageSize, margin, xfSansBody);
            rect = new XRect(margin, MovePointer((float)oldrectSize), pageSize.Width - (margin * 2), rectSize);
            gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb((int)(.100 * 255), 255, 215, 255)), rect);
            tf.DrawString($"Feasibility note:", xfSansBody, XBrushes.Black, rect, XStringFormats.TopLeft);
            tf.DrawString($"Feasibility note:{Inquiry.FeasibilityNote.Value}", xfSansBody, XBrushes.Black, rect, XStringFormats.TopLeft);
            oldrectSize = rectSize;

            gfx.DrawString($"Selected solution: {Inquiry.SolutionDecision.Value}", xfSansBody, XBrushes.Black, new XPoint(margin, MovePointer((float)oldrectSize + xfSansBody.Height)));
            gfx.DrawString($"Selected solution:", xfSansBody, XBrushes.Black, new XPoint(margin, MovePointer(0)));
            gfx.DrawString($"Realisation start: {Inquiry.RequestedCompletionDate.Start.Day}.{Inquiry.RequestedCompletionDate.Start.Month}.{Inquiry.RequestedCompletionDate.Start.Year}", xfSansBody, XBrushes.Black, new XPoint(margin, MovePointer(xfSansBody.Height)));
            if (Inquiry.SolutionDecision.Value.Equals(Variants.consulting))
            {
                gfx.DrawString($"Realisation end: {((DateTime)Inquiry.RequestedCompletionDate.End).Day}.{((DateTime)Inquiry.RequestedCompletionDate.End).Month}.{((DateTime)Inquiry.RequestedCompletionDate.End).Year}", xfSansBody, XBrushes.Black, new XPoint(margin, MovePointer(xfSansBody.Height)));
            }
            else
            {
                gfx.DrawString($"Realisation end: {Inquiry.RequestedCompletionDate.Start.Day}.{Inquiry.RequestedCompletionDate.Start.Month}.{Inquiry.RequestedCompletionDate.Start.Year}", xfSansBody, XBrushes.Black, new XPoint(margin, MovePointer(xfSansBody.Height)));
            }

            rts = gfx.MeasureString($"Estimated netto price: {Offer.Price}", xfSansBody);
            gfx.DrawString($"Estimated netto price: {Offer.Price}", xfSansBody, XBrushes.Black, new XPoint(pageSize.Width - margin - (float)rts.Width, MovePointer(0)));
            rts = gfx.MeasureString($"Total: {Offer.Price.Value}", xfSansBodyBold);
            gfx.DrawString($"Total: {Offer.Price.Value}", xfSansBodyBold, XBrushes.Black, new XPoint(pageSize.Width - margin - (float)rts.Width, MovePointer(xfSansBody.Height)));

            Footer(_configuration.GetValue<string>("app:name"), " ", _configuration.GetValue<string>("projectMail:adress"));

            void Footer(string textLeft, string textCenter, string textRight)
            {
                gfx.DrawLine(XPens.Black, new XPoint(margin, pageSize.Height - 50),
                    new XPoint(pageSize.Width - margin, pageSize.Height - 50));

                gfx.DrawString(textLeft, xfSansBody, XBrushes.Black,
                    new XPoint(margin, pageSize.Height - (xfSansBody.Height / 2) - margin));


                var fcts = gfx.MeasureString(textCenter, xfSansBody);
                gfx.DrawString(textCenter, xfSansBody, XBrushes.Black,
                    new XPoint(((pageSize.Width - (float)fcts.Width) / 2), pageSize.Height - (xfSansBody.Height / 2) - margin));

                var frts = gfx.MeasureString(textRight, xfSansBody);
                gfx.DrawString(textRight, xfSansBody, XBrushes.Black,
                    new XPoint(pageSize.Width - margin - (float)frts.Width, pageSize.Height - (xfSansBody.Height / 2) - margin));
            }

            string filename = $"Offer {Offer.Id.Value}.pdf";
            //document.Save(filename);
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
