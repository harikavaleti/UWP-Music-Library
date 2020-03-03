using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPMusicLibrary.Model
{
    public enum RegionalCategory
    {

        English,
        Hindi,
        Korea,
        Spanish,
        Punjabi,
        Telugu
    }
    public class Album
    {
        public string ImageFile { get; set; }
        public string AlbumName { get; set; }
        public List<string> AllFilesInAlbum { get; set; }
        public string FileName { get; set; }
        public RegionalCategory RegionalCategory { get; set; }


        public Album(string albumName, string imagefile)
        {
            AlbumName = albumName;
            if (imagefile == null)
            {
                ImageFile = $"Assets/Folder.png";
            }
            else
            {
                ImageFile = imagefile;
            }


        }
    }
}