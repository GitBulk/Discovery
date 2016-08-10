using Novacode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HowToDocX
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void btnSample1_Click(object sender, EventArgs e)
        {
            string fileName = @"D:\sample1.docx";
            var doc = DocX.Create(fileName);
            doc.InsertParagraph("this is my first paragraph");
            doc.Save();
            MessageBox.Show("Done");
        }

        private void btnFormatText_Click(object sender, EventArgs e)
        {
            string fileName = @"D:\sample2.docx";
            var doc = DocX.Create(fileName);
            string headlineText = "Constitution of the United States";
            string paraOne = "We the People of the United States, in Order to form a more perfect Union, "
                + "establish Justice, insure domestic Tranquility, provide for the common defence, "
                + "promote the general Welfare, and secure the Blessings of Liberty to ourselves "
                + "and our Posterity, do ordain and establish this Constitution for the United States of America.";

            // A formatting object for our headline:
            var headLineFormat = new Formatting();
            headLineFormat.FontFamily = new System.Drawing.FontFamily("Arial Black");
            headLineFormat.Bold = true;
            headLineFormat.FontColor = Color.Blue;
            headLineFormat.Size = 18D;
            headLineFormat.Position = 12;

            // A formatting object for our normal paragraph text:
            var paraFormat = new Formatting();
            paraFormat.FontFamily = new System.Drawing.FontFamily("Calibri");
            paraFormat.Size = 10D;

            doc.InsertParagraph(headlineText, false, headLineFormat);
            doc.InsertParagraph(paraOne, false, paraFormat);
            doc.Save();
            MessageBox.Show("Done");
        }

        private void btnCreateTemplate_Click(object sender, EventArgs e)
        {
            // Adjust the path so suit your machine:
            string fileName = @"D:\template_letter.docx";

            // Set up our paragraph contents:
            string headerText = "Rejection Letter";
            string letterBodyText = DateTime.Now.ToShortDateString();
            string paraTwo = ""
                + "Dear %APPLICANT%" + Environment.NewLine + Environment.NewLine
                + "I am writing to thank you for your resume. Unfortunately, your skills and "
                + "experience do not match our needs at the present time. We will keep your "
                + "resume in our circular file for future reference. Don't call us, we'll call you. "
                + Environment.NewLine + Environment.NewLine
                + "Sincerely, "
                + Environment.NewLine + Environment.NewLine
                + "Jim Smith, Corporate Hiring Manager";

            // Title Formatting:
            var titleFormat = new Formatting();
            titleFormat.FontFamily = new System.Drawing.FontFamily("Arial Black");
            titleFormat.Size = 18D;
            titleFormat.Position = 12;

            // Body Formatting
            var paraFormat = new Formatting();
            paraFormat.FontFamily = new System.Drawing.FontFamily("Calibri");
            paraFormat.Size = 10D;
            titleFormat.Position = 12;

            // Create the document in memory:
            var doc = DocX.Create(fileName);

            // Insert each prargraph, with appropriate spacing and alignment:
            Paragraph title = doc.InsertParagraph(headerText, false, titleFormat);
            title.Alignment = Alignment.center;

            doc.InsertParagraph(Environment.NewLine);
            Paragraph letterBody = doc.InsertParagraph(letterBodyText, false, paraFormat);
            letterBody.Alignment = Alignment.both;

            doc.InsertParagraph(Environment.NewLine);
            doc.InsertParagraph(paraTwo, false, paraFormat);

            //return doc;
            doc.Save();
            MessageBox.Show("Done");
        }

        private void btnReplaceText_Click(object sender, EventArgs e)
        {
            // Adjust the path so suit your machine:
            string fileName = @"D:\template_letter.docx";
            DocX letter = DocX.Load(fileName);
            string searchValue = "%APPLICANT%";
            string newValue = "Toàn Trần";
            letter.ReplaceText(searchValue, newValue);
            letter.SaveAs(@"D:\result_letter.docx");
            MessageBox.Show("Done");
        }
    }
}
