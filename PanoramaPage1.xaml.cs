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
using System.Windows.Navigation;

namespace RSSReader
{
    public partial class PanoramaPage1 : PhoneApplicationPage
    {
        static bool reset = true;
        static bool update = false;
        static String[,] links = new String[4,4];
        public PanoramaPage1()
        {
            InitializeComponent();
            if(reset)
            {
            links[0, 0] = "http://rss.cnn.com/rss/edition_world.rss";

            links[0, 1] = "http://www.guardian.co.uk/world/rss";

            links[0, 2] = "http://www.thedailymail.net/?rss=news/";

            links[0, 3] = "http://news.yahoo.com/rss/";

            links[1, 0] = "http://rss.cnn.com/rss/edition_sport.rss";

            links[1, 1] = "http://www.guardian.co.uk/sport/rss";

            links[1, 2] = "http://www.thedailymail.net/?rss=sports/";

            links[1, 3] = "http://feeds.feedburner.com/SportcoukNewsRssFeed";

            links[2, 0] = "http://www.thedailymail.net/?rss=opinion/";

            links[2, 1] = "http://www.foxnews.com/about/rss";

            links[2, 2] = "http://feeds.nytimes.com/nyt/rss/Opinion";

            links[3, 0] = "http://rss.cnn.com/rss/edition_technology.rss";

            links[3, 1] = "http://www.guardian.co.uk/technology/rss";

            links[3, 2] = "http://www.microsoft.com/presspass/rss/RSSFeed.aspx?ContentType=articleContentType&Tags=&VideoContentType=&FeedType=0";
        }
            reset = false;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            update = false;
            try
            {
                String category = NavigationContext.QueryString["category"];
                if (category == "world")
                {
                    OpenFeeds(0);
                }
                if (category == "sport")
                {
                    OpenFeeds(1);
                }
                if (category == "opinion")
                {
                    OpenFeeds(2);
                }
                if (category == "technology")
                {
                    OpenFeeds(3);
                }
            } catch (KeyNotFoundException) {
                Fill(N1, 0, 0);
                Fill(N2, 0, 1);
                Fill(N3, 0, 2);
                Fill(N4, 0, 3);
                Fill(S1, 1, 0);
                Fill(S2, 1, 1);
                Fill(S3, 1, 2);
                Fill(S4, 1, 3);
                Fill(T1, 3, 0);
                Fill(T2, 3, 1);
                Fill(T3, 3, 2);
                Fill(T4, 3, 3);
                Fill(O1, 2, 0);
                Fill(O2, 2, 1);
                Fill(O3, 2, 2);
                Fill(O4, 2, 3);
                update = true;
            }
         }
        private void Fill(TextBox box, int category, int feed)
        {
            try
            {
                box.Text = links[category, feed];
            }
            catch (ArgumentNullException) { }
            
        }
        private void Update(TextBox box, int category, int feed)
        {
            try
            {
                links[category, feed] = box.Text;
            }
            catch (ArgumentNullException) { }
        }
            private void OpenFeeds(int source)
            {
                NavigationService.Navigate(new Uri("/PhonePage1.xaml?source0=" + links[source,0] + "&source1=" + links[source,1] + "&source2=" + links[source,2] + "&source3=" + links[source,3],UriKind.Relative));
            }

            private void UpdateAll()
            {
                if (update)
                {
                    Update(N1, 0, 0);
                    Update(N2, 0, 1);
                    Update(N3, 0, 2);
                    Update(N4, 0, 3);
                    Update(S1, 1, 0);
                    Update(S2, 1, 1);
                    Update(S3, 1, 2);
                    Update(S4, 1, 3);
                    Update(T1, 3, 0);
                    Update(T2, 3, 1);
                    Update(T3, 3, 2);
                    Update(T4, 3, 3);
                    Update(O1, 2, 0);
                    Update(O2, 2, 1);
                    Update(O3, 2, 2);
                    Update(O4, 2, 3);
                }
            }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
            {
                UpdateAll();
                base.OnNavigatedFrom(e);
            }
            protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
            {
                UpdateAll();
                base.OnBackKeyPress(e);
            }

            private void button1_Click(object sender, RoutedEventArgs e)
            {
                if(MessageBox.Show("Are you sure you want to return to default settings?", "Restore", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                reset = true;
            }
        }
    }

