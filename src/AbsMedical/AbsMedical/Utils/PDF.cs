using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using AbsMedical.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace AbsMedical.Utils
{
    public static class PDF
    {
        public static bool CreatePDF(student student, DoctorServiceReference.Doctor doctor, absmedical absmedical)
        {
            try
            {
                string studentFullName = student.Firstname + student.Lastname;
                string fileName = "certificate-" + studentFullName + "-" + absmedical.VisitDate.ToString("dd-MM-yy") + ".pdf";
                string header = "Medical Certificate";

                //Text Doctor
                string txtDoctorIdentifier = doctor.Firstname + " " + doctor.Lastname + "\n";
                string txtDoctorAddress = doctor.Address + " - " + doctor.PostalCode + ", " + doctor.City + "\n";
                string txtDoctorContact = doctor.Email + " | " + doctor.Phone + "\n \n";

                //Text Student
                string txtStudentIdentifier = student.Firstname + " " + student.Lastname + "\n";
                string txtStudentAddress = student.Address + " - " + student.PostalCode + ", " + student.City + "\n";
                string txtStudentContact = student.Email + " | " + student.Phone + "\n \n";

                Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35); //properties of the document, see iTextSharp documentation
                PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream((fileName), FileMode.Create));
                doc.Open();

                //Intro paragraph
                Paragraph pdfHeader = new Paragraph(header, HeaderFont());
                pdfHeader.Alignment = Element.ALIGN_CENTER;
                doc.Add(pdfHeader);

                //Doctor paragraph
                Paragraph pDoctorTitle = new Paragraph("Doctor \n", TextBold());
                doc.Add(pDoctorTitle);
                Paragraph pDoctorIdentifier = new Paragraph(txtDoctorIdentifier, TextFont());
                doc.Add(pDoctorIdentifier);
                Paragraph pDoctorAddress = new Paragraph(txtDoctorAddress, TextFont());
                doc.Add(pDoctorAddress);
                Paragraph pDoctorContact = new Paragraph(txtDoctorContact, TextFont());
                doc.Add(pDoctorContact);

                //Student paragraph
                Paragraph pStudentTitle = new Paragraph("Student \n", TextBold());
                doc.Add(pStudentTitle);
                Paragraph pStudentIdentifier = new Paragraph(txtStudentIdentifier, TextFont());
                doc.Add(pStudentIdentifier);
                Paragraph pStudentAddress = new Paragraph(txtStudentAddress, TextFont());
                doc.Add(pStudentAddress);
                Paragraph pStudentContact = new Paragraph(txtStudentContact, TextFont());
                doc.Add(pStudentContact);

                //Motive
                Paragraph pMotiveTitle = new Paragraph("Motive: \n", TextUnderline());
                doc.Add(pMotiveTitle);
                Paragraph pMotiveText = new Paragraph(absmedical.Note, TextFont());
                pMotiveText.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(pMotiveText);

                doc.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private static Font HeaderFont()
        {
            return FontFactory.GetFont("Segoe UI", 18.0f, BaseColor.BLACK);
        }

        private static Font TitleFont()
        {
            return FontFactory.GetFont("Segoe UI", 15.0f, BaseColor.BLACK);
        }

        private static Font TextFont()
        {
            return FontFactory.GetFont("Segoe UI", 12.0f, BaseColor.BLACK);
        }


        private static Font TextUnderline()
        {
            return FontFactory.GetFont("Segoe UI", 15.0f, Font.UNDERLINE, BaseColor.BLACK);
        }

        private static Font TextBold()
        {
            return FontFactory.GetFont("Segoe UI", 15.0f, Font.BOLD, BaseColor.BLACK);
        }
    }
}
