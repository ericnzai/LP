using System;
using System.IO;
using System.Linq;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;

namespace LP.Content.BusinessLayer.PdfCreation
{
    public class PdfContent : IPdfContent
    {
        public string GetPdfFileContent(string filename)
        {
            using (var fs = new FileStream(filename, FileMode.Open))
            {
                byte[] buffer = new byte[fs.Length];

                int readBytes = fs.Read(buffer, 0, buffer.Count());

                return readBytes != fs.Length ? null : Convert.ToBase64String(buffer);
            }
            return null;
        }
    }
}
