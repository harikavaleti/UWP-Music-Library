using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPMusicLibrary.Model
{
    public enum MusicCategory
    {
        Albums,
        RecentPlayList,
        Favourite,
        RegionalMusic

    }
   
    public class Music
    {
      
        public string FileName { get; set; }
        public string SongName { get; set; }
        public string ImageFile { get; set; }
        public MusicCategory Category { get; set; }


        public Music(string filename, string imagefile, string songname)
        {
            FileName = filename;
            
            if(imagefile == null)
            {
                ImageFile = $"/Assets/Icons/Regional Music.jfif";
            }
            else
            {
                ImageFile = imagefile;
            }

            SongName = songname;

        }
        

       

    }

}


