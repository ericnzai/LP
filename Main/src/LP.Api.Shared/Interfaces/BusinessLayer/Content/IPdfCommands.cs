using iTextSharp.text;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Content
{
    public interface IPdfCommands
    {
        Font CoverBlue { get; }
        Font TitleBlue { get; }
        Font ItalicGray { get; }
        Image FrontCoverImage { get; }
        Font CoverBlueSmall { get; }
        Font CoverGreySmall { get; }
        new Font GreySmall { get; }
        Font GreyMedium { get; }

        string Imagepath { set; }
        string FrontCoverImagePath { set; }
    }
}
