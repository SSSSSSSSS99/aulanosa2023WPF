using AulaNosaApp.DTO;
using AulaNosaApp.Util;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Win32;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.UI;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Xml.Linq;

namespace AulaNosaApp.Servicios
{
    public class EmpresaApi
    {
        static RestClient cliente;
        static RestRequest request;
        public static List<EmpresasDTO> ListarEmpresas()
        {
            cliente = new RestClient(Constantes.client);
            request = new RestRequest("/api/empresa", Method.Get);
            var response = cliente.Execute<List<EmpresasDTO>>(request);
            var apiResponse = response.Data;
            return apiResponse;
        }

        public static void exportarPDF(List<EmpresasDTO> empresaLista) {
            try
            {
                String fichero = "";
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.CheckFileExists = false;
                openFileDialog.Filter = "PDF (*.pdf)|*.pdf";
                if (openFileDialog.ShowDialog() == true)
                    fichero = openFileDialog.FileName;
                else
                    return;
                PdfWriter writer = new PdfWriter(fichero);

                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);
                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                iText.Layout.Element.Paragraph header = new iText.Layout.Element.Paragraph("INFORME EMPRESA - EMPRESAS DISPONIBLES").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(20);
                document.Add(header);
                LineSeparator ls = new LineSeparator(new SolidLine());
                document.Add(ls);
                iText.Layout.Element.Paragraph subheader = new iText.Layout.Element.Paragraph("").SetFontSize(10);
                document.Add(subheader);

                iText.Layout.Element.Table table = new iText.Layout.Element.Table(4);

                table.AddCell(new iText.Layout.Element.Paragraph("EMPRESA").SetFont(bold).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                table.AddCell(new iText.Layout.Element.Paragraph("CIF").SetFont(bold).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                table.AddCell(new iText.Layout.Element.Paragraph("REPRESENTANTE").SetFont(bold).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                table.AddCell(new iText.Layout.Element.Paragraph("CONTACTO").SetFont(bold).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
 

                foreach (EmpresasDTO empresa in empresaLista)
                {
                    table.AddCell(new iText.Layout.Element.Paragraph(empresa.nombre).SetFont(font).SetBackgroundColor(ColorConstants.WHITE).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    table.AddCell(new iText.Layout.Element.Paragraph(empresa.cif).SetFont(font).SetBackgroundColor(ColorConstants.WHITE).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    table.AddCell(new iText.Layout.Element.Paragraph(empresa.representante).SetFont(font).SetBackgroundColor(ColorConstants.WHITE).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    table.AddCell(new iText.Layout.Element.Paragraph(empresa.contacto).SetFont(font).SetBackgroundColor(ColorConstants.WHITE).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                }

                document.Add(table);
                document.Close();

                MessageBox.Show("Informe generado correctamente.", "Informe generado", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch {
                MessageBox.Show("No se ha podido crear el fichero. Verifique que no esté en uso.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
