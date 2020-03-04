using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPMusicLibrary.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPMusicLibrary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Music> music;
        private List<MenuItems> menuItems;
        public string imagefile { get; set; }
        public string musicname { get; set; }
        public string search_term { get; set; }
        public object StorageFile { get; private set; }
        public bool isAlbums { get; set;}
        public bool isRegional { get; set; }

        private ObservableCollection<Advertisment> advertisments;


        public MainPage()
        {
            this.InitializeComponent();
            if (LoginPage.Islogin == false)
            {
                Loaded += MainPage_Loaded;
            }
            advertisments = new ObservableCollection<Advertisment>();
            MusicManager.DisplayAllAdds(advertisments);
            music = new ObservableCollection<Music>();
            MusicManager.GetAllMusic(music);



            menuItems = new List<MenuItems>();
            //Load Pane
            menuItems.Add(new MenuItems { Category = MusicCategory.Albums, IconFile = "Assets/Icons/Albums.png" });
            menuItems.Add(new MenuItems { Category = MusicCategory.Favourite, IconFile = "Assets/Icons/Favourite.png" });
            menuItems.Add(new MenuItems { Category = MusicCategory.RecentPlayList, IconFile = "Assets/Icons/Recent Playlist.png" });
            menuItems.Add(new MenuItems { Category = MusicCategory.RegionalMusic, IconFile = "Assets/Icons/Regional.png" });
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));

        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MusicGridView.ItemsSource = MusicManager.GetMusic().ToList();
            CategoryTextBlock.Text = "All Songs";
            MenuItemsListView.SelectedItem = null;
            BackButton.Visibility = Visibility.Collapsed;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
            CategoryTextBlock.Text = "All Songs";
            MusicGridView.ItemsSource = MusicManager.GetMusic().ToList();

        }

        

        private void MenuItemsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItems = (MenuItems)e.ClickedItem;
            CategoryTextBlock.Text = menuItems.Category.ToString();
      
            {
                if (menuItems.Category == MusicCategory.RecentPlayList)
                {
                   
                    music.Clear();
                    recentPlaylist().Clear();
                    MusicGridView.ItemsSource = recentPlaylist().ToList();
                    isAlbums = false;
                    isRegional = false;
                }
                else if ((menuItems.Category == MusicCategory.Favourite))
                {
                    music.Clear();
                    MusicManager.GetFavourite().Clear();
                    MusicGridView.ItemsSource = MusicManager.GetFavourite().ToList();
                    isAlbums = false;
                    isRegional = false;
                }

                else if (menuItems.Category == MusicCategory.Albums)
                {
                    isAlbums = true;
                    music.Clear();
                    MusicManager.GetFavourite().Clear();
                    recentPlaylist().Clear();

                    MusicGridView.ItemsSource = MusicManager.GetAllAlbums();
                }
                else if (menuItems.Category == MusicCategory.RegionalMusic)
                {
                    isRegional = true;
                    music.Clear();
                    MusicManager.GetFavourite().Clear();
                    recentPlaylist().Clear();
                    MusicGridView.ItemsSource = MusicManager.GetAllAlbumsinRegional();
                }
            }
  
           BackButton.Visibility = Visibility.Visible;
        }

        private void MusicGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Music userClickedItem = (Music)e.ClickedItem;
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string destinationfolder = root + @"\Assets\User\" + $"{LoginPage.UserName}" + @"\RecentPlaylist";
            Userplaylist(userClickedItem);     
            string extension = Path.GetExtension(userClickedItem.FileName);
            if (extension == ".mp3")
            {
                MyMediaElement.Source = new Uri(this.BaseUri, userClickedItem.FileName);
                NowPlaying.Text = userClickedItem.SongName;
                Symbol pico = Symbol.Pause;
                SymbolIcon spico = new SymbolIcon(pico);
                Play.Icon = spico;
                Symbol xx = Symbol.Like;
                SymbolIcon spico1 = new SymbolIcon(xx);

            }
            else 
            {
                if(isAlbums == true)
                {
                    MusicGridView.ItemsSource = MusicManager.GetAllMusicFilesInAlbum(userClickedItem.SongName);
                    isAlbums = false;
                }
                
                if(isRegional == true)
                {
                    MusicGridView.ItemsSource = MusicManager.GetAllMusicFilesinRegionalMusic(userClickedItem.SongName);
                    isRegional = false;
                }
              
                CategoryTextBlock.Text = userClickedItem.SongName;

                NowPlaying.Text = userClickedItem.SongName;

            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {

            if (MyMediaElement.CurrentState == MediaElementState.Playing)
            {
                Symbol pico = Symbol.Play;
                SymbolIcon spico = new SymbolIcon(pico);
                Play.Icon = spico;
                MyMediaElement.Pause();
            }
            else
            {
                Symbol pico = Symbol.Pause;
                SymbolIcon spico = new SymbolIcon(pico);
                Play.Icon = spico;
                MyMediaElement.Play();
            }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {

            MyMediaElement.Position += TimeSpan.FromSeconds(10);
        }

        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            var rand = new Random();
            var randomList = (MusicManager.GetMusic()).OrderBy(x => rand.Next()).ToList();
        }

        private void RepeatButton_Click_1(object sender, RoutedEventArgs e)
        {
            Symbol repico = Symbol.RepeatOne;
            SymbolIcon srepico = new SymbolIcon(repico);
            repButton.Icon = srepico;
            repButton.IsChecked = true;
            MyMediaElement.Position = TimeSpan.Zero;
            MyMediaElement.Play();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            MyMediaElement.Stop();
            Symbol pico = Symbol.Play;
            SymbolIcon spico = new SymbolIcon(pico);
            Play.Icon = spico;
        }

        private void BackButton_MediaClick(object sender, RoutedEventArgs e)
        {
            MyMediaElement.Position -= TimeSpan.FromMinutes(1);
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            MyMediaElement.Position -= TimeSpan.FromMinutes(1);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            //this.music.SelectedIndex = music.SelectedIndex + 1; 
            //MyMediaElement.Play();
            MyMediaElement.Position += TimeSpan.FromSeconds(10);
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {

            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (sender.Text.Length > 1)
                {
                    /*sender.ItemsSource = this.GetSuggestion(sender.Text);*/
                    txtAutoSuggestBox.ItemsSource = this.GetSuggestion(sender.Text);
                }
                else
                {
                    /* sender.ItemsSource = new string[] {"No Results..."};*/
                    txtAutoSuggestBox.ItemsSource = new string[] { "No Results" };
                }
            }
        }


        private string[] GetSuggestion(string text)
        {
            
            string[] result = null;
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Assets\Music\Albums";
            string[] suggestions = Directory.GetFiles(path, "*.mp3", SearchOption.AllDirectories).Select(file => Path.GetFileNameWithoutExtension(file)).ToArray();

            result = suggestions.Where(x => x.Contains(text, StringComparison.OrdinalIgnoreCase)).ToArray();
            return result;

        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)

        {
            if (args.ChosenSuggestion != null)
            {

                search_term = args.QueryText;
                var results = this.GetSuggestion(search_term);
                txtAutoSuggestBox.ItemsSource = results;
                txtAutoSuggestBox.IsSuggestionListOpen = true;

            }

            MusicGridView.ItemsSource = GetSearchMusic(search_term).ToList();
            CategoryTextBlock.Text = "Search Results";
            NowPlaying.Text = search_term;

        }

        private List<Music> GetSearchMusic(string search_term)
        {
            var searchresult = new List<Music>();
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Assets\Music\Albums";
            string[] searchterm_result = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            if (searchterm_result.Length > 0)
            {
                foreach (string File in searchterm_result)
                {
                    string extension = Path.GetExtension(File);
                    string searchfile = File;
                    string searchfilename = Path.GetFileNameWithoutExtension(searchfile);
                    if (extension == ".png" || extension == ".jfif")
                    {
                        imagefile = File;
                    }
                    if (extension == ".mp3" && (search_term == searchfilename))
                    {
                        searchresult.Add(new Music(searchfile, imagefile, searchfilename));
                    }

                }
            }

            return searchresult;

        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)

        {

            txtAutoSuggestBox.Text = args.SelectedItem as string;

        }


       
        private async void Userplaylist(Music music)
        {
            string musicFileName = @"\" + music.SongName + @".txt";
            //StorageFolder f = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder Folder = ApplicationData.Current.LocalFolder;
            StorageFile File = await Folder.CreateFileAsync(musicFileName, CreationCollisionOption.ReplaceExisting);
            var lines = await FileIO.ReadLinesAsync(File);
            await FileIO.WriteTextAsync(File, $"{music.FileName}" + Environment.NewLine);
            await FileIO.AppendTextAsync(File, $"{music.SongName}" + Environment.NewLine);
            await FileIO.AppendTextAsync(File, $"{music.ImageFile}");

            
        }
        private List<Music>  getrecents()
        {
            List<Music> recentplayList = new List<Music>();
            //string musicFileName = @"\" + music.SongName + @".txt";
            //StorageFolder f = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder Folder = ApplicationData.Current.LocalFolder;
            //IReadOnlyList<StorageFile> fileList = await Folder.GetFilesAsync();
            String dir = Folder.Path;
                //Directory.GetCurrentDirectory();
                if (Directory.Exists(dir))
                {
                    String[] fileList = Directory.GetFiles(dir);

                    foreach (string file in fileList)
                    {
                        // IList<string> fileData = await FileIO.ReadLinesAsync(file);
                        string[] fileData = System.IO.File.ReadAllLines(file);
                     if (fileData.Length > 0)
                     {
                        string musicFilename = fileData[0];
                        string song = fileData[1];
                        string image = fileData[2];
                        recentplayList.Add(new Music(musicFilename,image,song));
                     }
                    }
                }
                
            return recentplayList;
        }
        private List<Music> recentPlaylist()
        {
           return  getrecents();
           /* var recentplayList = new List<Music>();
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
                    string musicFilename = Path.GetFileNameWithoutExtension(file);
                    if ((extension == ".mp3" && musicname == musicFilename))
                    {

                        recentplayList.Add(new Music(file, imagefile, musicFilename));
                    }

                }
            }
         return recentplayList;

           */

            
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LogOut));

        }


        private void UserProfile_Click(object sender, RoutedEventArgs e)
        {

            if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }


        }

        private void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
        }
    }
}
