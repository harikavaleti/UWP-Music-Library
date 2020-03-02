using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using static System.Net.Mime.MediaTypeNames;
using Windows.Media.Core;
using Windows.Media.Playback;



namespace UWPMusicLibrary.Model
{
    
    public static class MusicManager
    {
        private static string extension { get; set; }
        private static string imagefile { get; set; }

        public static List<Music> GetMusic()
        {
            var music = new List<Music>();

            // Get the path to the app's Assets folder.
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Assets\Music\Albums";

            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    string extension = Path.GetExtension(file);

                    if (extension == ".png" || extension == ".jfif")
                    {
                        imagefile = file;
                    }
                }

                foreach (string file in files)
                {
                    string extension = Path.GetExtension(file);

                    if (!(extension == ".png" || extension == ".jfif"))
                    {
                        string musicname = Path.GetFileNameWithoutExtension(file);

                        music.Add(new Music(file, imagefile, musicname));
                    }
                }
            }
            return music;

        }


        public static List<Music> GetFavourite()
        {
            var favmusic = new List<Music>();

            // Get the path to the app's Assets folder.
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Assets\Music\Favourite";

            string[] files = Directory.GetFiles(path);

            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    string extension = Path.GetExtension(file);

                    if (extension == ".png" || extension == ".jfif")
                    {
                        imagefile = file;
                    }
                }

                foreach (string file in files)
                {
                    string extension = Path.GetExtension(file);

                    if (!(extension == ".png" || extension == ".jfif"))
                    {
                        string musicname = Path.GetFileNameWithoutExtension(file);

                        favmusic.Add(new Music(file, imagefile, musicname));
                    }
                }
                
            }
            return favmusic;
        }
                //Get all advertisment images........................................................................
                private static List<Advertisment> ImagesForAdvertisment()
        {

            List<Advertisment> advertisments = new List<Advertisment>();


            Advertisment advertisment = new Advertisment($"Assets/Advertisment/Gaana.png", $"https://www.google.com/");
            advertisments.Add(advertisment);

            Advertisment advertisment1 = new Advertisment($"Assets/Advertisment/jango.png", $"https://www.kalacademy.org/");
            advertisments.Add(advertisment1);

            Advertisment advertisment5 = new Advertisment($"Assets/Advertisment/social.jpg", $"https://www.youtube.com/");
            advertisments.Add(advertisment5);

            Advertisment advertisment2 = new Advertisment($"Assets/Advertisment/youtube.png", $"https://www.gaana.com/");
            advertisments.Add(advertisment2);

            Advertisment advertisment3 = new Advertisment($"Assets/Advertisment/spotify.png", $"https://www.spotify.com/us/");
            advertisments.Add(advertisment3);

            Advertisment advertisment4 = new Advertisment($"Assets/Advertisment/savan.png", $"https://www.kalacademy.org/");
            advertisments.Add(advertisment4);


            return advertisments;
        }

        //Get all abumns only.....................................................................
        private static List<Music> GetAllAlbums()
        {
            List<Music> allAlbumsList = new List<Music>();
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Assets\Music\Albums";
            //Reading all sub folders from the main folder
            string[] directories = Directory.GetDirectories(path);

            for (int i = 0; i < directories.Length; i++)
            {
                //getting all the file path name 
                string albumFileFullPathName = directories[i];
                // getting folder name from the path
                string folderName = Path.GetFileName(albumFileFullPathName);
                string imageFile = $"Assets/Folder.png";
                //creating object for music
                Music albumObj = new Music(albumFileFullPathName, imageFile, folderName);
                //adding all the music to the allalbumslist
                allAlbumsList.Add(albumObj);
            }
            //returning allalbums list
            return allAlbumsList;
        }

        private static List<Music> GetAllAlbumsinRegional()
        {
            List<Music> allRegionalSongs = new List<Music>();
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Assets\Music\Regional Music";
            string[] directories3 = Directory.GetDirectories(path);
            for (int i = 0; i < directories3.Length; i++)
            {
                string allRegional = directories3[i];
                string folderName1 = Path.GetFileName(allRegional);
                string imageFile1 = $"Assets/Folder.png";

                Music regionalAlbum = new Music(allRegional, imageFile1, folderName1);
                allRegionalSongs.Add(regionalAlbum);



            }

            return allRegionalSongs;
        }

        //creating another method for getting all music in albums

        private static List<Music> GetAllMusicFilesInAlbum(string userSelectedAlbumName)
        {
            //creating list of music
            List<Music> allFilesInUserSelectedAlbum = new List<Music>();

            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Assets\Music\Albums";

            //Reading the sub folders from the albums folder
            string[] directories1 = Directory.GetDirectories(path);

            for (int i = 0; i < directories1.Length; i++)
            {
                //getting all the filepath name
                string albumNameFullPath = directories1[i];
                //getting foldername from the path
                string albumName = Path.GetFileName(albumNameFullPath);


                //if user selected the album name that should be equal to album name
                if (albumName == userSelectedAlbumName)

                {
                    //get the full path of files
                    string[] files1 = Directory.GetFiles(albumNameFullPath);

                    if (files1.Length > 0)
                    {
                        for (int j = 0; j < files1.Length; j++)
                        {
                            //get extension of that file
                            string extension1 = Path.GetExtension(files1[j]);

                            //if the extension is .png or .jfif
                            if (extension1 == ".png" || extension1 == ".jfif")
                            {
                                //give that file to that image
                                imagefile = files1[j];
                            }
                            //if that file is .mp3 
                            if (extension1 == ".mp3")
                            {
                                //give that file without extension
                                string musicname = Path.GetFileNameWithoutExtension(files1[j]);
                                //creating object for the music
                                Music musicFile = new Music(files1[j], imagefile, musicname);
                                //adding all the music file to allFilesinuserselectedalbum
                                allFilesInUserSelectedAlbum.Add(musicFile);
                            }
                        }
                    }
                }

            }

            return allFilesInUserSelectedAlbum;
           
        }
        
        //creating another folder for regional music
        private static List<Music> GetAllMusicFilesinRegionalMusic(string userselectedalbuminregionalmusic)
        {
            //creating the music list
            List<Music> allAlbumsInRegionalAlbums = new List<Music>();
            //getting the full folder path 
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            //getting the path of that specific folder
            string path = root + @"\Assets\Music\Regional Music";
            //get the subfolder path from that full path
            string[] directories2 = Directory.GetDirectories(path);

            for (int i = 0; i < directories2.Length; i++)
            {
                string allFoldersInRegionalAlums = directories2[i];
                string regioanlAlbum = Path.GetFileName(allFoldersInRegionalAlums);
                {
                    if (regioanlAlbum == userselectedalbuminregionalmusic)
                    {
                        string[] files2 = Directory.GetFiles(allFoldersInRegionalAlums);
                        if (files2.Length > 0)
                        {
                            for (int j = 0; j < files2.Length; j++)
                            {
                                string extension3 = Path.GetExtension(files2[j]);
                                if (extension3 == ".png" || extension3 == ".jfif")
                                {
                                    imagefile = files2[j];
                                }
                                if (extension3 == ".mp3")
                                {
                                    string regionalMusicName = Path.GetFileNameWithoutExtension(files2[j]);
                                    Music allRegionalMusic = new Music(files2[j], imagefile, regionalMusicName);
                                    allAlbumsInRegionalAlbums.Add(allRegionalMusic);

                                }
                            }
                        }
                    }


                }
            }



            return allAlbumsInRegionalAlbums;
           
        }

       /* public async static List<Music> GetAllRecentMusic()
        {
            //creating list of music
            List<Music> allFilesUserSelected = new List<Music>();
            string musicFileName = @"\" + music.SongName + @".txt";

            StorageFolder Folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await Folder.GetFileAsync(musicFileName);

            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Assets\Music\Albums";

            //Reading the sub folders from the albums folder
            string[] dirs = Directory.GetDirectories(path);

            for (int i = 0; i < dirs.Length; i++)
            {
                //getting all the filepath name
                string albumNameFullPath = dirs[i];

                {
                    //get the full path of files
                    string[] files = Directory.GetFiles(albumNameFullPath);

                    if (files.Length > 0)
                    {
                        for (int j = 0; j < files.Length; j++)
                        {
                            string musicpath = files[j];
                            string musicname = Path.GetFileName(musicpath);


                            //get extension of that file
                            string ext = Path.GetExtension(musicpath);

                            //if the extension is .png or .jfif
                            if ((ext == ".png" || ext == ".jfif") && (musicname == userSelectedMusic))
                            {
                                //give that file to that image
                                imagefile = files[j];
                            }
                            //if that file is .mp3 
                            if (ext == ".mp3")
                            {
                                //give that file without extension
                                string songname = Path.GetFileNameWithoutExtension(files[j]);
                                //creating object for the music
                                string destinationfolder = root + @"\Assets\User\" + $"{LoginPage.UserName}" + @"\Recent Playlist\";

                                Music musicFile = new Music(files[j], imagefile, songname);

                                //adding all the music file to allFilesinuserselectedalbum
                                allFilesUserSelected.Add(new Music(files[j], imagefile, songname));
                            }
                        }
                    }
                }

            }

            return allFilesUserSelected;

        }
        */
        public static void GetAllMusic(ObservableCollection<Music> music)
        {
            var allmusic = GetMusic();
            music.Clear();
            allmusic.ForEach(m => music.Add(m));
        }

       
        public static void GetAllAlbumNames(ObservableCollection<Music> uiAlbumsList, MusicCategory category)
        {
            if (category == MusicCategory.Albums)
            {
                List<Music> allAlbums = GetAllAlbums();

                uiAlbumsList.Clear(); //Clear UI album List
                //Add all albums items to uiAlbumList
                allAlbums.ForEach(m => uiAlbumsList.Add(m));

            }
            else if (category == MusicCategory.RegionalMusic)
            {
                List<Music> allregionalfolders = GetAllAlbumsinRegional();
                uiAlbumsList.Clear();
                allregionalfolders.ForEach(r => uiAlbumsList.Add(r));

            }
            else if(category == MusicCategory.Favourite)
            {
              List<Music> allfavs = GetFavourite();
                uiAlbumsList.Clear();
                allfavs.ForEach(x => uiAlbumsList.Add(x));
            }
            else if(category == MusicCategory.RecentPlayList)
            {
                


            }
          


        }


        public static void DisplayAllAdds(ObservableCollection<Advertisment> adds)
        {

            List<Advertisment> allAddvertisment = ImagesForAdvertisment();
            adds.Clear();
            allAddvertisment.ForEach(n => adds.Add(n));

        }




            public static void GetAlbumFiles(ObservableCollection<Music> uiFolderList, string userSelectedAlbumName, string userSElectedRegionalAlbumName)
        {
            //Albums: we are calling GetAllMusicFilesInAlbum method to get all mp3 files in userSelectedAlbumName
            List<Music> allFilesInAlbum = GetAllMusicFilesInAlbum(userSelectedAlbumName);
            List<Music> allFilesInregionalalbum = GetAllMusicFilesinRegionalMusic(userSElectedRegionalAlbumName);
            List<Music> favs = GetFavourite();
            uiFolderList.Clear();

            allFilesInAlbum.ForEach(f => uiFolderList.Add(f));
            allFilesInregionalalbum.ForEach(a => uiFolderList.Add(a));
        }

    }
}
