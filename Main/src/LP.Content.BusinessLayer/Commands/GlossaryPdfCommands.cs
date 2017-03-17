using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using iTextSharp.text.pdf.events;
using LP.Api.Shared.Interfaces.Api;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Content.BusinessLayer.PdfCreation;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Common.Translation;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.BusinessLayer.Commands
{
    public class GlossaryPdfCommands : IGlossaryPdfCommands
    {
        private readonly IGlossaryCommands _glossaryCommands;
        private readonly IPdfCommands _pdfCommands;
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IPdfComponents _pdfComponents;
        public GlossaryPdfCommands(IGlossaryCommands glossaryCommands, IPdfCommands pdfCommands, IConfigurationProvider configurationProvider, IPdfComponents pdfComponents)
        {
            _glossaryCommands = glossaryCommands;
            _pdfCommands = pdfCommands;
            _configurationProvider = configurationProvider;
            _pdfComponents = pdfComponents;
        }

        public async Task<GlossaryPDFResponseContract> GetWholeGlossaryPdfContent(string culture, List<TranslatedItem> translatedItems)
        {
            var glossaryItemsResponse = await _glossaryCommands.GetAllGlossaryItems(culture);

            var glossaryItems = glossaryItemsResponse.GlossaryItems.OrderBy(g => g.Title).ToList();

            var dataPdf = new DataPdf();
            dataPdf.GlossaryItems = glossaryItems;
            var glossaryTitle = translatedItems.FirstOrDefault(t => t.ResourceId == "ltGlossary.Text");
            dataPdf.GlossaryTitleStr = glossaryTitle != null ? glossaryTitle.TranslatedValue : "Glossary";

            var msgSorryNoResults = translatedItems.FirstOrDefault(t => t.ResourceId == "msgSorryNoResults");
            dataPdf.MsgSorryNoResultsStr = msgSorryNoResults != null ? msgSorryNoResults.TranslatedValue : "Sorry, there are no results for your search";

            return new GlossaryPDFResponseContract() { Content = CreatePdf(dataPdf) };
        }

        public async Task<GlossaryPDFResponseContract> GetFilteredGlossaryPdfContent(string culture, List<TranslatedItem> translatedItems, string filters, string sort)
        {
            var glossaryItemResponseContract = await _glossaryCommands.GetAllGlossaryItems(culture);

            var glossaryItems = FilterGlossaryItems(filters, sort, glossaryItemResponseContract.GlossaryItems);

            var dataPdf = new DataPdf();
            dataPdf.FilterMessage = BuildFilterMessage(filters, translatedItems);
            dataPdf.GlossaryItems = glossaryItems;
            var glossaryTitle = translatedItems.FirstOrDefault(t => t.ResourceId == "ltGlossary.Text");
            dataPdf.GlossaryTitleStr = glossaryTitle != null ? glossaryTitle.TranslatedValue : "Glossary";

            var msgSorryNoResults = translatedItems.FirstOrDefault(t => t.ResourceId == "msgSorryNoResults");
            dataPdf.MsgSorryNoResultsStr = msgSorryNoResults != null ? msgSorryNoResults.TranslatedValue : "Sorry, there are no results for your search";

            return new GlossaryPDFResponseContract() { Content = CreatePdf(dataPdf) };
        }

        public List<GlossaryItem> FilterGlossaryItems(string filters, string sort, List<GlossaryItem> glossaryItemsList)
        {
            var glossaryItems = glossaryItemsList;

            if (glossaryItems == null)
            {
                return new List<GlossaryItem>();
            }

            if (filters != null)
            {
                //var f = filters.Split('_');

                //if (f.Length > 0)
                //{
                //    if (!string.IsNullOrEmpty(f[0]))
                //    {
                glossaryItems =
                    glossaryItems.Where(g => g.Title.ToLower().Contains(filters.ToLower())).ToList();
                //}

                //if (f.Length > 1 && !string.IsNullOrEmpty(f[1]))
                //{
                //    foreach (var glossaryItem in glossaryItems.ToList())
                //    {
                //        var tr = glossaryItem.TrainingModules.Split(',');
                //        bool hasTrainingModule = false;

                //        for (int i = 0; i < tr.Length; i++)
                //        {
                //            for (int j = 1; j < f.Length; j++)
                //            {
                //                if (f[j] == tr[i])
                //                {
                //                    hasTrainingModule = true;
                //                    break;
                //                }
                //            }
                //        }

                //        if (!hasTrainingModule)
                //        {
                //            glossaryItems.Remove(glossaryItem);
                //        }
                //    }
                //}
                //}
            }

            if (sort == "desc")
            {
                glossaryItems = glossaryItems.OrderByDescending(g => g.Title).ToList();
            }
            else
            {
                glossaryItems = glossaryItems.OrderBy(g => g.Title).ToList();
            }

            return glossaryItems;
        }

        public string BuildFilterMessage(string filters, List<TranslatedItem> translatedItems)
        {
            var msgFollowingFiltersSelected = translatedItems.FirstOrDefault(t => t.ResourceId == "msgFollowingFiltersSelected");
            var msgFollowingFiltersSelectedStr = msgFollowingFiltersSelected != null ? msgFollowingFiltersSelected.TranslatedValue : "The following filters were selected: ";

            var msgFollowingFiltersSelectedForSearch = translatedItems.FirstOrDefault(t => t.ResourceId == "msgFollowingFiltersSelectedForSearch");
            var msgFollowingFiltersSelectedForSearchStr = msgFollowingFiltersSelectedForSearch != null ? msgFollowingFiltersSelectedForSearch.TranslatedValue : "for search";

            //var msgFollowingFiltersSelectedForTrainingModules = translatedItems.FirstOrDefault(t => t.ResourceId == "msgFollowingFiltersSelectedForTrainingModules");
            //var msgFollowingFiltersSelectedForTrainingModulesStr = msgFollowingFiltersSelectedForTrainingModules != null ? msgFollowingFiltersSelectedForTrainingModules.TranslatedValue : "for training modules";

            return BuildFilterMessage(filters, msgFollowingFiltersSelectedStr, msgFollowingFiltersSelectedForSearchStr);
        }

        public string BuildFilterMessage(string filters, string msgFollowingFiltersSelectedStr, string msgFollowingFiltersSelectedForSearchStr)
        {
            if (!string.IsNullOrEmpty(filters))
            {
                var resultStringBuilder = new StringBuilder();

                resultStringBuilder.Append(msgFollowingFiltersSelectedStr);

                //var filterArray = filters.Split('_');
                //if (filterArray.Length > 0)
                //{
                //if (!string.IsNullOrEmpty(filters))
                //    {
                resultStringBuilder.Append(string.Format("{0} - '{1}'; ",
                    msgFollowingFiltersSelectedForSearchStr, filters));
                //   }
                //}

                //if (filterArray.Length > 1 && !string.IsNullOrEmpty(filterArray[1]))
                //{
                //    resultStringBuilder.Append(string.Format("{0} - ", msgFollowingFiltersSelectedForTrainingModulesStr));
                //    for (int j = 1; j < filterArray.Length; j++)
                //    {
                //        if (j == filterArray.Length - 1)
                //        {
                //            resultStringBuilder.Append(string.Format("'{0}'", filterArray[j]));
                //        }
                //        else
                //        {
                //            resultStringBuilder.Append(string.Format("'{0}', ", filterArray[j]));
                //        }
                //    }
                //    resultStringBuilder.Append("; ");
                //}
                //else
                //{
                //    //if (string.IsNullOrEmpty(filterArray[0]))
                //    //{
                //        return null;
                //    //}
                //}

                return resultStringBuilder.ToString();
            }

            return null;
        }

        public string CreatePdf(DataPdf dataPdf)
        {
            byte[] content = null;
            var doc = new Document(PageSize.A4, 36, 36, 54, 56);
            using (var memStream = new MemoryStream())
            {
                try
                {
                    var writer = PdfWriter.GetInstance(doc, memStream);

                    doc.Open();

                    doc = WriteIntoPdfDocument(writer, doc, dataPdf);

                    doc.Close();

                    content = memStream.ToArray();
                }
                catch (Exception) { }
            }


            using (var ms2 = new MemoryStream())
            using (var reader1 = new PdfReader(content))
            {
                try
                {
                    using (var stamper = new PdfStamper(reader1, ms2))
                    {
                        int n = reader1.NumberOfPages;
                        for (int i = 1; i <= n; i++)
                        {
                            if (i > 1)
                            {
                                GetHeaderTable(i, n).WriteSelectedRows(
                                    0, -1, 9, 832, stamper.GetOverContent(i)
                                    );

                                var footer = GetFooterTable(i, n, doc.PageSize.Height);
                                footer.WriteSelectedRows(
                                    0, -1, 34, footer.TotalHeight + 30, stamper.GetOverContent(i)
                                    );
                            }
                        }
                    }
                    content = ms2.ToArray();
                }
                catch (Exception ex) { }
            }

            return Convert.ToBase64String(content);
        }

        public PdfPTable GetHeaderTable(int x, int y)
        {
            PdfPTable table = new PdfPTable(1);

            table.TotalWidth = 527;
            table.LockedWidth = true;
            table.DefaultCell.FixedHeight = 20;
            table.DefaultCell.Border = Rectangle.NO_BORDER;

            table.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            var phrase = new Phrase("EYLEA Learning Resource - Glossary", _pdfCommands.GreySmall);
            table.AddCell(phrase);
            return table;
        }

        public PdfPTable GetFooterTable(int x, int y, float size)
        {
            PdfPTable table = new PdfPTable(1);

            table.TotalWidth = 527;
            table.LockedWidth = true;
            table.DefaultCell.FixedHeight = 20;
            table.DefaultCell.Border = Rectangle.NO_BORDER;

            table.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            var phrase = new Phrase(string.Format("{0}", x), _pdfCommands.GreySmall);
            table.AddCell(phrase);
            return table;
        }

        public Document WriteIntoPdfDocument(PdfWriter writer, Document doc, DataPdf dataPdf)
        {
            if (dataPdf != null)
            {
                var bgTable = _pdfComponents.CreatePdfPTable(1, doc.PageSize.Width);

                var logo = _pdfCommands.FrontCoverImage;
                logo.ScaleToFit(doc.PageSize.Width, doc.PageSize.Height);

                var cell = new PdfPCell(logo) { HorizontalAlignment = Element.ALIGN_RIGHT, PaddingTop = -10, Border = 0 };

                bgTable.AddCell(cell);

                bgTable.WriteSelectedRows(0, -1, 0, (doc.PageSize.Height - 10), writer.DirectContent);

                var tableIndication = _pdfComponents.CreatePdfPTable(1, doc.PageSize.Width - 180);

                cell = new PdfPCell { Border = 0, PaddingBottom = 10f, PaddingLeft = 85f, PaddingTop = 200f };

                Paragraph para;

                var siteName = dataPdf.GlossaryTitleStr;
                para = new Paragraph(siteName, _pdfCommands.CoverBlue);

                for (var i = 0; i < 7; i++)
                {
                    para.Add(Environment.NewLine);
                }

                cell.AddElement(para);
                tableIndication.AddCell(cell);
                tableIndication.WriteSelectedRows(0, -1, 0, (doc.PageSize.Height - 250), writer.DirectContent);

                if (!string.IsNullOrEmpty(dataPdf.FilterMessage))
                {
                    var tableChapterTitle = _pdfComponents.CreatePdfPTable(2, doc.PageSize.Width - 180);
                    var widths = new[] { 1f, 4f };
                    tableChapterTitle.SetWidths(widths);

                    cell = new PdfPCell { Border = Rectangle.NO_BORDER };
                    para = new Paragraph(" ");
                    cell.AddElement(para);
                    tableChapterTitle.AddCell(cell);

                    para = new Paragraph(dataPdf.FilterMessage, _pdfCommands.GreyMedium);
                    cell = new PdfPCell { Border = 0 };
                    cell.AddElement(para);
                
                    tableChapterTitle.AddCell(cell);
                    tableChapterTitle.WriteSelectedRows(0, -1, 0, (doc.PageSize.Height - 500), writer.DirectContent);
                }

                doc.NewPage();

                if (dataPdf.GlossaryItems == null || dataPdf.GlossaryItems.Count == 0)
                {
                    var p = new Paragraph(dataPdf.MsgSorryNoResultsStr, _pdfCommands.ItalicGray);
                    p.Alignment = Element.ALIGN_CENTER;
                    doc.Add(p);

                    var line = new LineSeparator(2f, 100f, BaseColor.LIGHT_GRAY, Element.ALIGN_CENTER, -1);
                    doc.Add(new Chunk(line));
                }
                else
                {   
                    var line = new LineSeparator(2f, 100f, BaseColor.LIGHT_GRAY, Element.ALIGN_CENTER, -1);

                    foreach (var glossaryItem in dataPdf.GlossaryItems)
                    {
                        Paragraph paragraph = new Paragraph();
                        var reader = new StringReader("<p class='title-font'><b>" + HttpUtility.HtmlDecode(glossaryItem.Title) + "</b></p>");
                        var style = new StyleSheet();
                        style.LoadStyle("title-font", "color", "#005b81");
                        style.LoadStyle("title-font", "font-size", "18px");
                        style.LoadStyle("title-font", "font-family", "Arial");
                        ParseHtml(paragraph, reader, style);

                        reader = new StringReader("<p class='description-font'>" + HttpUtility.HtmlDecode(glossaryItem.Description) + "</p>");
                        style = new StyleSheet();
                        ParseHtml(paragraph, reader, style);

                        paragraph.Add(new Chunk(line));

                        paragraph.KeepTogether = true;

                        doc.Add(paragraph);
                    }
                }
            }
            return doc;
        }

        public static void ParseHtml(Paragraph para, TextReader reader, StyleSheet style)
        {
            var arrayList = HTMLWorker.ParseToList(reader, style);
            foreach (IElement t in arrayList)
            {
                para.Add(t);
            }
        }
    }
}
