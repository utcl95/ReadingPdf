using System;
using System.IO;
using System.Text;

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;



namespace PdfInformesEscalafonarios
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin...");

            ShowPdf("abc");
            
            Console.WriteLine("End...");
            Console.ReadKey();
        }

        static void ShowPdf(String pathDirectory)
        {
            String pdfFile = "D:\\UGEL05\\Repositorio_Input\\A1.pdf";
            int largo = 0;
            PdfReader pdfReader = new PdfReader(pdfFile);

            ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
            String texto = PdfTextExtractor.GetTextFromPage(pdfReader, 1, strategy);
            texto = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(texto)));

            Console.WriteLine(texto);

            // Generado por el Legix
            if (texto.Contains("Versión del Sistema:"))
            {
                // dni
                int posDni = texto.IndexOf("D.N.I.:");
                String dni = texto.Substring(posDni + 8, 8);
                
                // usuario
                int posUser = texto.IndexOf("Usuario del Sistema:");
                int posFin = texto.IndexOf("Pag. N");
                largo = posFin - 1 - posUser - 21;                
                String user = texto.Substring(posUser + 21, largo);
                
                // fecha y hora
                int posFecha = texto.IndexOf("Fecha y Hora de Impresión:");
                int posFFin = texto.IndexOf("Versión del Sistema:");
                largo = posFFin - posFecha - 28;
                String fecha = texto.Substring(posFecha + 27, largo);

                // Informe escalafonario
                int posNum = texto.IndexOf("ESCALAFONARIO N");
                int posFNum = texto.IndexOf("-UGEL N");
                largo = posFNum - posNum - 6;
                String informe = texto.Substring(posNum + 17, largo);              

                // Numero de hojas
                int posHojas = texto.IndexOf("Pag. N");
                String hojas = texto.Substring(posHojas);
                int posFHojas = hojas.IndexOf(" de");
                String numHojas = hojas.Substring(posFHojas + 3);

                // Expediente
                int posExp = texto.IndexOf("MPT2018");
                int posFExp = texto.IndexOf("Estado Civil");
                largo = posFExp - 1 - posExp;
                String expediente = texto.Substring(posExp, largo);

                String line = dni + "\t" + user + "\t" + fecha + "\t" + informe + "\t" + numHojas + "\t" + expediente;
                Console.WriteLine(line);
            }
        }
    }
}
