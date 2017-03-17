using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Model.ViewModels.Common
{
    public class GroupInfoViewModel
    {
        public int GroupTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Culture { get; set; }
        public int? StatusId { get; set; }
        public string ImageUrl { get; set; }
        public int GroupId { get; set; }
        public bool IsPartiallyLive { get; set; }
        public int? NumberOfChaptersInModule { get; set; }
        public bool IsFavourite { get; set; }
        public bool HasNewContent { get; set; }
        public string FriendlyUrl { get; set; }
        public ChapterInfoViewModel CurrentChapterInfoViewModel { get; set; }
    }
}
