using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IO;
using System.Windows.Forms;

namespace ImagineTrailvan
{
    class PdfCreator
    {
        //http://www.pdfsharp.net/wiki/Graphics-sample.ashx#Shapes_32 for the sample used
        //consider sending an arrayList here with the datagridview details, then it can be made dynamic?
        //dataTables sending 1) inventory details for order, 2)Supplier details for this order
        //also, consider sending suggested  FILENAME in method as well (filename: tipe of doc, and number. eg> Order Number IT-17-EE-002, or> Invoice Number IT17-12345
        //the final total will have to be calculated here, with all the VAT details as well.

        //A4 dimensions are *'width x height = 595 x 842 pt'. 72pts ==1inch, 1inch==25.4mm
        public PdfCreator()
        {
        }//end of public PdfCreator()
        private void DrawHeader(PdfPage pdfpage, XGraphics graph)
        {//this draws the entire header.

            XPen pen = new XPen(Color.Black, 0.8);
            XPen boldpen = new XPen(Color.Black, 1.0);
            XImage image = XImage.FromFile(@"C:\Users\Carolien\Documents\GitHub\NewbyProgrammer\ImagineTrailvan\Imagine Logo.png");
            XFont fontHead = new XFont("Sans-serif", 6, XFontStyle.Regular);
            XFont fontBoldHead = new XFont("Sans-serif", 6, XFontStyle.Bold);
            XFont fontBoldTableHead = new XFont("Sans-serif", 8, XFontStyle.Bold);
            XFont fontTitle = new XFont("Sans-serif", 12, XFontStyle.Bold);

            Rectangle recDate = new Rectangle(375, 80, 185, 14);
            Rectangle recFileName = new Rectangle(375, 108, 185, 14);
            Rectangle recBorder = new Rectangle(25, 25, 545, 792);

            graph.DrawRectangle(pen, recDate);
            graph.DrawRectangle(pen, recFileName);
            graph.DrawRectangle(pen, recBorder);
            recBorder.Inflate(-2, -2);
            graph.DrawRectangle(pen, recBorder);

            graph.DrawImage(image, 170, 60, 200, 60);//here I insert the image and specify the size.

            graph.DrawString("ORDER DETAILS", fontTitle, XBrushes.Black, new XRect(200, 170, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("REG. No: 2006/032270/07", fontHead, XBrushes.Black, new XRect(40, 48, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("VAT REG. No: 4150234369", fontHead, XBrushes.Black, new XRect(40, 56, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("PO Box 25238", fontHead, XBrushes.Black, new XRect(40, 76, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("MONUMENTPARK  0105", fontHead, XBrushes.Black, new XRect(40, 84, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("DATE: " + DateTime.Now.ToString(), fontHead, XBrushes.Black, new XRect(380, 84, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("PRETORIA", fontHead, XBrushes.Black, new XRect(40, 92, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("TEL : 27 (012)  349  2636", fontHead, XBrushes.Black, new XRect(40, 112, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("FAX : 27 (012)  349 2625", fontHead, XBrushes.Black, new XRect(40, 120, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("E MAIL: gideon@imagine-trailvan.co.za", fontHead, XBrushes.Black, new XRect(40, 128, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
        }//end of public void DrawHeader(PdfPage pdfpage, XGraphics graph, string title)

        private void DrawTotal(PdfPage pdfpage, XGraphics graph, double total)
        {
            //this method makes it possible to draw the total multiple times for when more than one page is used- dynamic
            XFont fontFoot = new XFont("Sans-serif", 8, XFontStyle.Regular);
            graph.DrawString("R " + total, fontFoot, XBrushes.Black, new XRect(500, 685, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
        }//end of private void DrawTotal(PdfPage pdfpage, XGraphics graph, double total)

        #region Fixed Stock Order PDF
        //this is Orders' umbrella method
        public void CreateOrderPDF(DataTable dtInvValues, DataTable dtSupValues, string fileName)
        {
            //this will have to work as the main class for the calling of methods
            //create a pdf file
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = fileName;
            PdfPage pdfpage = pdf.AddPage();

            XGraphics graph = XGraphics.FromPdfPage(pdfpage);

            #region Fonts
            XFont font = new XFont("Arial", 8, XFontStyle.Regular);
            XFont fontHead = new XFont("Sans-serif", 6, XFontStyle.Regular);
            XFont fontBoldHead = new XFont("Sans-serif", 6, XFontStyle.Bold);
            XFont fontBoldTableHead = new XFont("Sans-serif", 8, XFontStyle.Bold);
            XFont fontFoot = new XFont("Sans-serif", 8, XFontStyle.Regular);
            XFont fontDocType = new XFont("Sans-serif", 12, XFontStyle.Bold);
            #endregion

            graph.DrawString("ORDER NO.: " + fileName, fontHead, XBrushes.Black, new XRect(380, 112, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);

            //get header
            DrawHeader(pdfpage, graph);

            //get supplier details
            DrawSupDetails(pdfpage, graph, dtSupValues);

            //get the delivery details
            DrawDelivery(pdfpage, graph);

            //get the table headings
            DrawOrderTableHead(pdfpage, graph);

            //get footer
            DrawOrderFooter(pdfpage, graph);

            //get the order details and the totals
            double tot = DrawStockOrderDetails(pdf, pdfpage, graph, dtInvValues, dtSupValues, fileName);
            DrawTotal(pdfpage, graph, tot);

            //save the pdf....
            //this line now creates the path, and adds the extention to the file
            fileName = @"C:\Users\Carolien\Desktop\" + fileName + ".pdf"; //create the pdf's name, and adding the pdf extention-otherwise program doesn't know how to open it.
            pdf.Save(fileName);
            Process.Start(fileName);
        }//end of public void CreateOrderPDF(DataTable dtInvValues, DataTable dtSupValues, string fileName, string title)
        private void DrawSupDetails(PdfPage pdfpage, XGraphics graph, DataTable dtSupValues)
        {
            //this gets all supplier details needed and allocates them to fixed points on page(s)
            XFont fontHead = new XFont("Sans-serif", 6, XFontStyle.Regular);
            XFont fontBoldHead = new XFont("Sans-serif", 6, XFontStyle.Bold);
            XFont fontFoot = new XFont("Sans-serif", 8, XFontStyle.Regular);

            string payTerm = "";
            
            payTerm = dtSupValues.Rows[0][12].ToString();

            #region SupplierDetails
            if (dtSupValues.Rows[0][3].ToString()!="")
            {
                graph.DrawString("ATTENTION " + dtSupValues.Rows[0][3].ToString(), fontHead, XBrushes.Black, new XRect(380, 48, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            }//end of if (dtSupValues.Rows[0][3].ToString()!="")
            else
            {
                graph.DrawString("TO WHOM IT MAY CONCERN", fontHead, XBrushes.Black, new XRect(380, 48, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            }//end of else of if (dtSupValues.Rows[0][3].ToString()!="")
            graph.DrawString("TO: " + dtSupValues.Rows[0][1].ToString(), fontBoldHead, XBrushes.Black, new XRect(40, 158, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("TEL: " + dtSupValues.Rows[0][4].ToString(), fontHead, XBrushes.Black, new XRect(40, 166, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("CELL: " + dtSupValues.Rows[0][5].ToString(), fontHead, XBrushes.Black, new XRect(40, 174, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("EMAIL: " + dtSupValues.Rows[0][6].ToString(), fontHead, XBrushes.Black, new XRect(40, 182, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString(payTerm.ToUpper(), fontFoot, XBrushes.Black, new XRect(130, 685, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            #endregion
        }//end of private void DrawSupDetails()
        private void DrawOrderTableHead(PdfPage pdfpage, XGraphics graph)
        {
            XFont fontBoldTableHead = new XFont("Sans-serif", 8, XFontStyle.Bold);

            #region TableHeadings
            graph.DrawString("PART CODE", fontBoldTableHead, XBrushes.Black, new XRect(60, 220, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("ITEM", fontBoldTableHead, XBrushes.Black, new XRect(140, 220, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("QUANTITY", fontBoldTableHead, XBrushes.Black, new XRect(345, 220, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("PRICE/UNIT", fontBoldTableHead, XBrushes.Black, new XRect(410, 220, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("TOTAL", fontBoldTableHead, XBrushes.Black, new XRect(490, 220, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //  graph.DrawString(itemCount.ToString(), fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            #endregion
        }//end of private void DrawOrderTableHead(PdfPage pdfpage, XGraphics graph)
        private double DrawStockOrderDetails(PdfDocument pdf, PdfPage pdfpage, XGraphics graph, DataTable dtInvValues, DataTable dtSupValues, string fileName)
        {
            //this gets and orders the details gotten for the order: the items
            int ypoint = 0;
            int itemCount = 1;
            double total = 0.00;
            int lineCount = 0;

            XFont font = new XFont("Arial", 8, XFontStyle.Regular);
            XFont fontHead = new XFont("Sans-serif", 6, XFontStyle.Regular);

            #region TableValues
            ypoint = 240;
            for (int i = 0; i < dtInvValues.Rows.Count; i++)
            {//INCLUDE LENGTH?
                if (lineCount < 29)
                {
                    graph.DrawString(itemCount.ToString(), fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][1].ToString(), font, XBrushes.Black, new XRect(60, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][2].ToString(), font, XBrushes.Black, new XRect(140, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][3].ToString(), font, XBrushes.Black, new XRect(355, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][4].ToString(), font, XBrushes.Black, new XRect(410, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString((int.Parse(dtInvValues.Rows[i][3].ToString()) * double.Parse(dtInvValues.Rows[i][4].ToString())).ToString(), font, XBrushes.Black, new XRect(490, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);

                   // graph.DrawString(dtInvValues.Rows[i][5].ToString(), font, XBrushes.Black, new XRect(490, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    //  graph.DrawString(dtInvValues.Rows[i][6].ToString(), fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    ypoint = ypoint + 15;
                    itemCount += 1;
                    lineCount += 1;
                    total += (int.Parse(dtInvValues.Rows[i][3].ToString()) * double.Parse(dtInvValues.Rows[i][4].ToString()));
                }
                else
                {
                    pdfpage = pdf.AddPage();
                    graph = XGraphics.FromPdfPage(pdfpage);

                    //gets the filename and places it on the next page
                    graph.DrawString("ORDER NO.: " + fileName, fontHead, XBrushes.Black, new XRect(380, 112, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);

                    //get supplier info
                    DrawSupDetails(pdfpage, graph, dtSupValues);

                    //get the delivery details
                    DrawDelivery(pdfpage, graph);

                    //get header
                    DrawHeader(pdfpage, graph);

                    //get table headings
                    DrawOrderTableHead(pdfpage, graph);

                    //get footer
                    DrawOrderFooter(pdfpage, graph);

                    lineCount = 1;
                    ypoint = 240;

                    //do the next page
                    graph.DrawString(itemCount.ToString(), fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][1].ToString(), font, XBrushes.Black, new XRect(60, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][2].ToString(), font, XBrushes.Black, new XRect(140, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][3].ToString(), font, XBrushes.Black, new XRect(355, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][4].ToString(), font, XBrushes.Black, new XRect(410, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString((int.Parse(dtInvValues.Rows[i][3].ToString()) * double.Parse(dtInvValues.Rows[i][4].ToString())).ToString(), font, XBrushes.Black, new XRect(490, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);

                    //    graph.DrawString(dtInvValues.Rows[i][5].ToString(), font, XBrushes.Black, new XRect(490, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    //  graph.DrawString(dtInvValues.Rows[i][6].ToString(), fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    ypoint = ypoint + 15;
                    itemCount += 1;
                    total += (int.Parse(dtInvValues.Rows[i][3].ToString()) * double.Parse(dtInvValues.Rows[i][4].ToString()));
                }
            }//end of for (int i = 0; i < tableValues.Rows.Count; i++)
            //send the total to the last page
            DrawTotal(pdfpage, graph, total);
            #endregion

            return total;// this magic line makes it possible to get the total on the first page too!
        }//end of private void DrawStockOrderDetails(PdfPage pdfpage, XGraphics graph, DataTable dtInvValues)
        private void DrawOrderFooter(PdfPage pdfpage, XGraphics graph)
        {//this draws the entire footer.
            XFont fontFoot = new XFont("Sans-serif", 8, XFontStyle.Regular);
            XPen pen = new XPen(Color.Black, 0.8);
            XPen dashpen = new XPen(Color.Black, 0.8);
            dashpen.DashStyle= XDashStyle.Dot;
           
            Rectangle recTotal = new Rectangle(487, 680, 60, 18);
            Rectangle recPayment = new Rectangle(35, 680, 450, 18);

            graph.DrawRectangle(pen, recTotal);
            recTotal.Inflate(-1, -1);                       //inflating the rectangle and drawing it once again, creates the double line border effect
            graph.DrawRectangle(pen, recTotal);
            graph.DrawRectangle(pen, recPayment);
            recPayment.Inflate(-1, -1);
            graph.DrawRectangle(pen, recPayment);

            graph.DrawLine(pen, 380, 770, 520, 770); //signature line
            graph.DrawLine(pen, 40, 770, 300, 770); //SPECIAL ARRANGEMENTS line
            graph.DrawLine(pen, 40, 790, 300, 790); //SPECIAL ARRANGEMENTS line
            graph.DrawLine(dashpen, 110, 723, 300, 723);//account name line
            graph.DrawLine(dashpen, 75, 733, 160, 733);//bank name line
            graph.DrawLine(dashpen, 220, 733, 300, 733);//account no line
            graph.DrawLine(dashpen, 75, 743, 160, 743);//branch line
            graph.DrawLine(dashpen, 220, 743, 300, 743);//bank code line

            graph.DrawString("Terms of payment:", fontFoot, XBrushes.Black, new XRect(40, 685, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("Total: ", fontFoot, XBrushes.Black, new XRect(450, 685, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("YOUR BANKING DETAILS FOR ELECTRONIC TRANSFER:", fontFoot, XBrushes.Black, new XRect(40, 705, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("ACCOUNT NAME: ", fontFoot, XBrushes.Black, new XRect(40, 715, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("BANK: ", fontFoot, XBrushes.Black, new XRect(40, 725, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("ACC NO.: ", fontFoot, XBrushes.Black, new XRect(170, 725, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("BRANCH: ", fontFoot, XBrushes.Black, new XRect(40, 735, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("BANK CODE: ", fontFoot, XBrushes.Black, new XRect(170, 735, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("SPECIAL ARRANGEMENTS: ", fontFoot, XBrushes.Black, new XRect(40, 745, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("Authorized signature ", fontFoot, XBrushes.Black, new XRect(415, 775, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
        }//end of private void DrawOrderFooter(PdfPage pdfpage, XGraphics graph, string payTerm,double total)
        private void DrawDelivery(PdfPage pdfpage, XGraphics graph)
        {
            XPen pen = new XPen(Color.Black, 0.8);
            XFont fontHead = new XFont("Sans-serif", 6, XFontStyle.Regular);

            Rectangle recDelivery = new Rectangle(375, 135, 185, 58);
            graph.DrawRectangle(pen, recDelivery);

            graph.DrawString("DELIVERY INSTRUCTIONS", fontHead, XBrushes.Black, new XRect(380, 138, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("TO: ", fontHead, XBrushes.Black, new XRect(380, 148, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("new adress: ", fontHead, XBrushes.Black, new XRect(440, 148, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("UNIT 38 ", fontHead, XBrushes.Black, new XRect(440, 156, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("SCIENTIA TECHNOPARK(next to CSIR) ", fontHead, XBrushes.Black, new XRect(440, 164, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("PRETORIA ", fontHead, XBrushes.Black, new XRect(440, 172, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("DELIVERY DATE: ", fontHead, XBrushes.Black, new XRect(380, 180, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("ASAP ", fontHead, XBrushes.Black, new XRect(440, 180, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
        }//end of private void DrawDelivery(PdfPage pdfpage, XGraphics graph)
        #endregion
 
        public void CreateRepInvPDF(DataTable dtInvValues, DataTable dtClientDetails, string fileName, string title)
        {
            //this will have to work as the main class for the calling of methods

            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = fileName;
            PdfPage pdfpage = pdf.AddPage();



            //save the pdf....
            //this line now creates the path, and adds the extention to the file
            fileName = @"C:\Users\Carolien\Desktop\" + fileName + ".pdf"; //create the pdf's name, and adding the pdf extention-otherwise program doesn't know how to open it.
            pdf.Save(fileName);
            Process.Start(fileName);
        }//end of public void CreateRepInvPDF(DataTable dtInvValues, DataTable dtClientDetails, string fileName, string title)
        public void CreateFibreOrderPDF(DataTable dtFibreValues, DataTable dtSupValues, string fileName, string title)
        {
            //this will have to work as the main class for the calling of methods

            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = fileName;
            PdfPage pdfpage = pdf.AddPage();



            //save the pdf....
            //this line now creates the path, and adds the extention to the file
            fileName = @"C:\Users\Carolien\Desktop\" + fileName + ".pdf"; //create the pdf's name, and adding the pdf extention-otherwise program doesn't know how to open it.
            pdf.Save(fileName);
            Process.Start(fileName);
        }//end of public void CreateFibreOrderPDF(DataTable dtFibreValues, DataTable dtSupValues, string fileName, string title)
        private void DrawRepInvFooter(PdfPage pdfpage, XGraphics graph, string payTerm, double total)
        {
        }//end of private void DrawRepInvFooter(PdfPage pdfpage, XGraphics graph,string payTerm,double total)
        private void DrawFibreFooter(PdfPage pdfpage, XGraphics graph, double total)
        {
        }//end of private void DrawFibreFooter(PdfPage pdfpage, XGraphics graph, double total)

        //my original
        public void PDForderForm(DataTable dtInvValues, DataTable dtSupValues, string fileName)
        {//the original method-not dynamic
           
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Inventory Test PDF";
            PdfPage pdfpage = pdf.AddPage();

            XGraphics graph = XGraphics.FromPdfPage(pdfpage);
            XFont font = new XFont("Arial", 8, XFontStyle.Regular);
            XFont fontHead = new XFont("Sans-serif", 6, XFontStyle.Regular);
            XFont fontBoldHead = new XFont("Sans-serif", 6, XFontStyle.Bold);
            XFont fontBoldTableHead = new XFont("Sans-serif", 8, XFontStyle.Bold);
            XFont fontFoot = new XFont("Sans-serif", 8, XFontStyle.Regular);
            XFont fontDocType = new XFont("Sans-serif", 12, XFontStyle.Bold);

            XPen pen = new XPen(Color.Black, 0.8);
            XPen boldpen = new XPen(Color.Black, 1.0);
            
            int ypoint = 0;
            int itemCount = 1;
            double total = 0;
            int lineCount = 0;

            #region Rectangles and Graphics
            Rectangle recDate = new Rectangle(375, 80, 185, 14);
            Rectangle recFileName = new Rectangle(375, 108, 185, 14);
            Rectangle recDelivery = new Rectangle(375, 135, 185, 58);
            Rectangle recTotal = new Rectangle(487, 680, 60, 18);
            Rectangle recPayment = new Rectangle(35, 680, 450, 18);
            Rectangle recBorder = new Rectangle(25, 25, 545, 792);

            graph.DrawRectangle(pen, recDate);
            graph.DrawRectangle(pen, recFileName);
            graph.DrawRectangle(pen, recDelivery);
            graph.DrawRectangle(pen, recTotal);
            recTotal.Inflate(-1, -1);                       //inflating the rectangle and drawing it once again, creates the double line border effect
            graph.DrawRectangle(pen, recTotal);
            graph.DrawRectangle(pen, recPayment);
            recPayment.Inflate(-1, -1);
            graph.DrawRectangle(pen, recPayment);
            graph.DrawRectangle(pen, recBorder);
            recBorder.Inflate(-2, -2);
            graph.DrawRectangle(pen, recBorder);

            XImage image = XImage.FromFile(@"C:\Users\Carolien\Documents\GitHub\NewbyProgrammer\ImagineTrailvan\Imagine Logo.png");
            graph.DrawImage(image, 170, 60, 200, 60);//here I insert the image and specify the size.

            graph.DrawLine(pen, 380, 750, 520, 750); //signature line
            #endregion
            
            #region Header
            ypoint = 40;
            graph.DrawString("ORDER DETAILS", fontDocType, XBrushes.Black, new XRect(200, 170, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString(dtSupValues.Rows[0][3].ToString(), fontHead, XBrushes.Black, new XRect(380, 40, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 8;
            graph.DrawString("REG. No: 2006/032270/07", fontHead, XBrushes.Black, new XRect(40, 48, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 8;
            graph.DrawString("VAT REG. No: 4150234369", fontHead, XBrushes.Black, new XRect(40, 56, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 20;
            graph.DrawString("PO Box 25238", fontHead, XBrushes.Black, new XRect(40, 76, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 8;
            graph.DrawString("MONUMENTPARK  0105", fontHead, XBrushes.Black, new XRect(40, 84, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("DATE: "+DateTime.Now.ToString(), fontHead, XBrushes.Black, new XRect(380, 84, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 8;
            graph.DrawString("PRETORIA", fontHead, XBrushes.Black, new XRect(40, 92, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 20;
            graph.DrawString("TEL : 27 (012)  349  2636", fontHead, XBrushes.Black, new XRect(40, 112, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("ORDER NO.: "+fileName, fontHead, XBrushes.Black, new XRect(380, 112, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 8;
            graph.DrawString("FAX : 27 (012)  349 2625", fontHead, XBrushes.Black, new XRect(40, 120, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 8;
            graph.DrawString("E MAIL: gideon@imagine-trailvan.co.za", fontHead, XBrushes.Black, new XRect(40, 128, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 10;
            graph.DrawString("DELIVERY INSTRUCTIONS", fontHead, XBrushes.Black, new XRect(380, 138, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 10;
            graph.DrawString("TO: " + dtSupValues.Rows[0][1].ToString(), fontBoldHead, XBrushes.Black, new XRect(40, 158, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("TO: ", fontHead, XBrushes.Black, new XRect(380, 148, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("new adress: ", fontHead, XBrushes.Black, new XRect(440, 148, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 8;
            graph.DrawString("TEL: " + dtSupValues.Rows[0][4].ToString(), fontHead, XBrushes.Black, new XRect(40, 166, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("UNIT 38 ", fontHead, XBrushes.Black, new XRect(440, 156, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 8;
            graph.DrawString("CELL: " + dtSupValues.Rows[0][5].ToString(), fontHead, XBrushes.Black, new XRect(40, 174, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("SCIENTIA TECHNOPARK(next to CSIR) ", fontHead, XBrushes.Black, new XRect(440, 164, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 8;
            graph.DrawString("EMAIL: " + dtSupValues.Rows[0][6].ToString(), fontHead, XBrushes.Black, new XRect(40, 182, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("PRETORIA ", fontHead, XBrushes.Black, new XRect(440, 172, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 8;
            graph.DrawString("DELIVERY DATE: ", fontHead, XBrushes.Black, new XRect(380, 180, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("ASAP ", fontHead, XBrushes.Black, new XRect(440, 180, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            #endregion

            ypoint = ypoint + 60;

            #region TableHeadings
            graph.DrawString("PART CODE", fontBoldTableHead, XBrushes.Black, new XRect(60, 220, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("ITEM", fontBoldTableHead, XBrushes.Black, new XRect(140, 220, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("QUANTITY", fontBoldTableHead, XBrushes.Black, new XRect(345, 220, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("PRICE/UNIT", fontBoldTableHead, XBrushes.Black, new XRect(410, 220, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("TOTAL", fontBoldTableHead, XBrushes.Black, new XRect(490, 220, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            //  graph.DrawString(itemCount.ToString(), fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            #endregion
            ypoint = ypoint + 20;
            ypoint += 380;
            #region Footer
            ypoint = 685;
            graph.DrawString("Terms of payment:", fontFoot, XBrushes.Black, new XRect(40, 685, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("Total: ", fontFoot, XBrushes.Black, new XRect(450, 685, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("R " + total, fontFoot, XBrushes.Black, new XRect(500, 685, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 20;
            graph.DrawString("YOUR BANKING DETAILS FOR ELECTRONIC TRANSFER:", fontFoot, XBrushes.Black, new XRect(40, 705, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 10;
            graph.DrawString("ACCOUNT NAME: ", fontFoot, XBrushes.Black, new XRect(40, 715, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 10;
            graph.DrawString("BANK: ", fontFoot, XBrushes.Black, new XRect(40, 725, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("ACC NO.: ", fontFoot, XBrushes.Black, new XRect(170, 725, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 10;
            graph.DrawString("BRANCH: ", fontFoot, XBrushes.Black, new XRect(40, 735, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("BANK CODE: ", fontFoot, XBrushes.Black, new XRect(170, 735, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 10;
            graph.DrawString("SPECIAL ARRANGEMENTS: ", fontFoot, XBrushes.Black, new XRect(40, 745, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            ypoint = ypoint + 10;
            graph.DrawString("Authorized signature ", fontFoot, XBrushes.Black, new XRect(400, 755, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
            #endregion

            #region TableValues
            ypoint = 240;
            for (int i = 0; i < dtInvValues.Rows.Count; i++)
            {//INCLUDE LENGTH?
                if (lineCount < 28)
                {
                    graph.DrawString(itemCount.ToString(), fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][1].ToString(), font, XBrushes.Black, new XRect(60, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][2].ToString(), font, XBrushes.Black, new XRect(140, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][3].ToString(), font, XBrushes.Black, new XRect(345, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][4].ToString(), font, XBrushes.Black, new XRect(410, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][5].ToString(), font, XBrushes.Black, new XRect(490, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    //  graph.DrawString(dtInvValues.Rows[i][6].ToString(), fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    ypoint = ypoint + 15;
                    itemCount += 1;
                    lineCount += 1;
                }
                else
                {
                    lineCount = 0;
                    ypoint = 260;
                    pdfpage = pdf.AddPage();
                    graph = XGraphics.FromPdfPage(pdfpage);
                    graph.DrawString(itemCount.ToString(), fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][1].ToString(), font, XBrushes.Black, new XRect(60, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][2].ToString(), font, XBrushes.Black, new XRect(140, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][3].ToString(), font, XBrushes.Black, new XRect(345, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][4].ToString(), font, XBrushes.Black, new XRect(410, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(dtInvValues.Rows[i][5].ToString(), font, XBrushes.Black, new XRect(490, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    //  graph.DrawString(dtInvValues.Rows[i][6].ToString(), fontHead, XBrushes.Black, new XRect(40, ypoint, pdfpage.Width.Point, pdfpage.Height.Point), XStringFormats.TopLeft);
                    ypoint = ypoint + 15;
                    itemCount += 1;
                }
                
            }//end of for (int i = 0; i < tableValues.Rows.Count; i++)
            #endregion
           
            //this line now creates the path, and adds the extention to the file
            fileName = @"C:\Users\Carolien\Desktop\" + fileName + ".pdf"; //create the pdf's name, and adding the pdf extention-otherwise program doesn't know how to open it.
            //string pdfFilename = "InventoryListTest.pdf";
           // pdf.Save(fileName);//somehow, this little line shoud be able to create a file path
            pdf.Save(fileName);
            Process.Start(fileName);
        }//end of public void PDForderForm()


        //create a method here for orders to suppliers

        //create a method here for invoices to clients for stock sold

        //create a method here for quotations to clients on a van

        //create a method here for invoices to clients on a van

        //create a method here for proforma to clients on a van

        //create a method here for custom invoices/ orders with only a header and footer


    }//end of class PdfCreator
}//end of namespace ImagineTrailvan
