using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Windows.Navigation;


namespace RSSReader
{
    public partial class PhonePage1 : PhoneApplicationPage
    {
        List<SyndicationItem> list;
        WebClient wc1;
        WebClient wc2;
        WebClient wc3;
        WebClient wc4;
        int count = 0;
        public String DescriptionUri(String html)
        {
            string CurrentDescriptionFileName = "Loading Feed" + count + ".htm";
            IsoStore.SaveToIsoStore(
            CurrentDescriptionFileName,
            new System.Text.UTF8Encoding().GetBytes(html));
            count++;
            return CurrentDescriptionFileName;
        }
        public PhonePage1()
        {
            InitializeComponent();
            this.Loaded +=new RoutedEventHandler(PhonePage1_Loaded);
        }
        private static Random random = new Random();

        public static List<E> ShuffleList<E>(List<E> ilist)
        {
            List<E> list = ilist;
            if (list.Count > 1)
            {
                for (int c = 0; c < list.Count; c++)
                {
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        E tmp = list[i];
                        int randomIndex = random.Next(i + 1);

                        //Swap elements
                        list[i] = list[randomIndex];
                        list[randomIndex] = tmp;
                    }
                }
                
            }
            return list;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            list = new List<SyndicationItem>();
            wc1 = new WebClient();
            wc2 = new WebClient();
            wc3 = new WebClient();
            wc4 = new WebClient();
            wc1.OpenReadCompleted +=new OpenReadCompletedEventHandler(wc_OpenReadCompleted);
            wc2.OpenReadCompleted += new OpenReadCompletedEventHandler(wc_OpenReadCompleted);
            wc3.OpenReadCompleted += new OpenReadCompletedEventHandler(wc_OpenReadCompleted);
            wc4.OpenReadCompleted += new OpenReadCompletedEventHandler(wc_OpenReadCompleted);base.OnNavigatedTo(e);
            OpenFeed(wc1,NavigationContext.QueryString["source0"]);
            OpenFeed(wc2, NavigationContext.QueryString["source1"]);
            OpenFeed(wc3, NavigationContext.QueryString["source2"]);
            OpenFeed(wc4, NavigationContext.QueryString["source3"]); 
            
        }
        private void OpenFeed(WebClient _client, String uri)
        {
            try
            {
                if (uri != null)
                {
                    _client.OpenReadAsync(new Uri(uri));
                }
            }
            catch (Exception) { } //ignores various null exceptions from empty links
        }
        void PhonePage1_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            SyndicationFeed feed;
            try
            {
                using (XmlReader reader = XmlReader.Create(e.Result))
                {
                    try
                    {
                        feed = SyndicationFeed.Load(reader);
                        list.AddRange(feed.Items.ToList());
                        list = ShuffleList(list);
                        foreach (SyndicationItem item in list)
                        {
                            item.Summary = new TextSyndicationContent(DescriptionUri(item.Summary.Text));
                        }
                        text.ItemsSource = list;
                    }
                    catch (Exception)
                    { }
                }
            }
            catch (WebException) { MessageBox.Show("Connection Failed. Press OK to try again."); OnBackKeyPress(new System.ComponentModel.CancelEventArgs());}
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            bool cant_backup = false;
            try
            {
                JournalEntry entry = NavigationService.BackStack.ElementAt(1);
            }
            catch(Exception){
                cant_backup = true;
            }
            if (cant_backup) { }
            else { NavigationService.RemoveBackEntry(); }

            base.OnBackKeyPress(e);            
        }

    }
}
