using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.Storage;


using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Prayers_Suwar
{
    public class Quran // class Quran contains info about number and name of surah
    {
        public int number;
        public string name;

        public Quran(int number, string name)
        {
            this.number = number;
            this.name = name;
        }

        public int Number
        {
            get {return number;}
            set {number = value;}
        }
        public string Name
        {
            get {return name;}
            set {name = value;}
        }
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public string AppStatus = String.Empty;
        public string SavedData = String.Empty;
        List<Quran> surah = new List<Quran>(); // create a list of Quran class

        Random rnd = new Random();

        public MainPage()
        {
            this.InitializeComponent();
            Application.Current.Suspending += new SuspendingEventHandler(App_Suspending);
            Application.Current.Resuming += new EventHandler<object>(App_Resuming);
            //this.NavigationCacheMode = NavigationCacheMode.Required;

            surah.Add(new Quran(2, "Al-Baqara"));
            surah.Add(new Quran(55, "Al-Rahman"));
            surah.Add(new Quran(87, "Al-A'ala"));
            surah.Add(new Quran(59, "Al-Teen"));
            surah.Add(new Quran(97, "Al-Qadr"));
            surah.Add(new Quran(98, "Al-Bayinah"));
            surah.Add(new Quran(99, "Al-Zalzala"));
            surah.Add(new Quran(100, "Al-Aadiyat"));
            surah.Add(new Quran(101, "Al-Qari'ah"));
            surah.Add(new Quran(102, "Al-Takathur"));
            surah.Add(new Quran(103, "Al-Asr"));
            surah.Add(new Quran(104, "Al-Humazah"));
            surah.Add(new Quran(105, "Al-Fil"));
            surah.Add(new Quran(106, "Quraysh"));
            surah.Add(new Quran(107, "Al-Ma'un"));
            surah.Add(new Quran(108, "Al-Kawthar"));
            surah.Add(new Quran(109, "Al-Kafiroon"));
            surah.Add(new Quran(110, "Al-Nasr"));
            surah.Add(new Quran(111, "Al-Masad"));
            surah.Add(new Quran(112, "Al-Ekhlas"));
            surah.Add(new Quran(113, "Al-Falaq"));
            surah.Add(new Quran(114, "Al-Nas"));
        }
        private async void App_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            await SaveAppData();
            deferral.Complete();
        }
        async private Task SaveAppData()
        {
            await Task.Factory.StartNew(() => ApplicationData.Current.LocalSettings.Values["data"] = DateTime.Now.ToString());
            return;
        }
        private async void App_Resuming(object sender, object e)
        {
            AppStatus = "Resuming from suspension.";
            SavedData = ApplicationData.Current.LocalSettings.Values["data"].ToString();
            ((Window.Current.Content as Frame).Content as MainPage).Refresh();
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void randomize_Click(object sender, RoutedEventArgs e)
        {
            Faridha.Items.Clear();
            Nawafil.Items.Clear();

            // calculate suwar for prayers
            salawat();
        }


        private void salawat()
        {
            int frand = 0;
            Quran tmp = new Quran(0, "");
            for (int i = 0; i < 22; i++)  // there are 22 rakaa for fara'ed and nawafil
            {
                if (i % 2 == 0) // first rakaas
                {
                    ////rnd.Next uses incluseive lower bound and exclusive upper bound
                    frand = rnd.Next(i, surah.Count-1); // exclude surat Al-Nas
                }
                else // second rakaas
                {
                    frand = rnd.Next(i, surah.Count); // include surat Al-Nas
                }
                tmp = surah[i];
                surah[i] = surah[frand];
                surah[frand] = tmp;
            }

            for (int i = 1; i < 22; i=i+2)
            {
                // surah in second rakaa must has lower number than first
                if (surah[i].number < surah[i - 1].number) // assuring the order
                {
                    tmp = surah[i];
                    surah[i] = surah[i - 1];
                    surah[i - 1] = tmp;
                }
            }

            for (int i = 0; i < 22; i++)
            {
                switch (i)
                {
                    case (0):
                        Faridha.Items.Add(" Fajr:\n\t1- " + surah[i].name);
                        break;
                    case (2):
                        Faridha.Items.Add(" Duhr:\n\t1-" + surah[i].name);
                        break;
                    case (4):
                        Faridha.Items.Add(" Asr:\n\t1-" + surah[i].name);
                        break;
                    case (6):
                        Faridha.Items.Add(" Magrib:\n\t1-" + surah[i].name);;
                        break;
                    case (8):
                        Faridha.Items.Add(" Isha:\n\t1-" + surah[i].name);;
                        break;
                    case (10):
                        Nawafil.Items.Add(" Fajr:\n\t1-" + surah[i].name);
                        break;
                    case (12):
                        Nawafil.Items.Add(" Duhr:\n    BEFORE\n\t1-" + surah[i].name);
                        break;
                    case (11):  case (13): case (15): case (17): case (19): case (21):
                        Nawafil.Items.Add("\t2- " + surah[i].name);
                        break;
                    case (14): 
                        Nawafil.Items.Add("\n\t1- " + surah[i].name);
                        break;
                    case (16):
                        Nawafil.Items.Add("    AFTER\n\t1- " + surah[i].name);
                        break;
                    case (18):
                        Nawafil.Items.Add(" Magrib:\n\t1-" + surah[i].name);
                        break;
                    case (20):
                        Nawafil.Items.Add(" Isha:\n\t1-" + surah[i].name); ;
                        break;
                    default:
                        Faridha.Items.Add("\t2- " + surah[i].name);
                        break;
                }
            }
        }

        private void Faridha_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Nawafil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
