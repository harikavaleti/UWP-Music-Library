using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPMusicLibrary.Model
{
    public class Advertisment
    {
        public double Rewards { get; set; }
        public string ImageFileName { get; set; }
        public string Hyperlink { get; set; }


        public Advertisment(string imageFileName, String hyperlink)
        {
            ImageFileName = imageFileName;
            Hyperlink = hyperlink;
            Rewards++;

        }
    }
}
