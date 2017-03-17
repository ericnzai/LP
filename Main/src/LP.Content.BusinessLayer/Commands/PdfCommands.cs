using System;
using System.IO;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using LP.Api.Shared.Interfaces.Api;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;

namespace LP.Content.BusinessLayer.Commands
{
    public class PdfCommands : IPdfCommands
    {
        private IConfigurationProvider _configurationProvider;
        private readonly HttpServerUtility _httpServerUtility;
        private Image _frontCoverImage, _backCoverImage, _correctImage, _bulletImage;
        public string Imagepath { get; set; }
        public string FrontCoverImagePath { get; set; }
        
        public PdfCommands(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;

            Imagepath = _configurationProvider.FrontEndWebUrl + "Images";

            FrontCoverImagePath = Imagepath + "/FrontCover.png";
        }
        
        public Font TitleBlue
        {
            get
            {
                var blue = new BaseColor(0, 91, 129);
                var font = FontFactory.GetFont("Arial", 34, Font.NORMAL, blue);

                return font;
            }
        }

        public Font ItalicGray
        {
            get
            {
                var font = FontFactory.GetFont("Arial", 10, Font.ITALIC, BaseColor.LIGHT_GRAY);

                return font;
            }
        }

        public Image FrontCoverImage
        {
            get
            {
                if (_frontCoverImage == null)
                {
                    _frontCoverImage =
                        Image.GetInstance(FrontCoverImagePath);
                }
                return _frontCoverImage;
            }
        }

        public Font CoverBlueSmall
        {
            get
            {
                string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
                BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                var blue = new BaseColor(0, 73, 118);
                var font = new Font(bf, 18, Font.BOLD, blue);
                return font;
            }
        }

        public Font CoverGreySmall
        {
            get
            {
                string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
                BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                var grey = new BaseColor(88, 88, 90);
                var font = new Font(bf, 18, Font.NORMAL, grey);
                return font;
            }
        }

        public Font CoverBlue
        {
            get
            {
                string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
                BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                var blue = new BaseColor(0, 73, 118);
                var font = new Font(bf, 24, Font.BOLD, blue);
                return font;
            }
        }

        public Font GreySmall
        {
            get
            {
                string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
                BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                var grey = new BaseColor(168, 168, 168);
                var font = new Font(bf, 10, Font.NORMAL, grey);
                return font;
            }
        }

        public Font GreyMedium
        {
            get
            {
                string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
                BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                var grey = new BaseColor(88, 88, 90);
                var font = new Font(bf, 14, Font.NORMAL, grey);
                return font;
            }
        }
    }
}
