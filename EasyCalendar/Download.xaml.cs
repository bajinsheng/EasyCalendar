using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Browser;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
namespace EasyCalendar
{
    public partial class Download : PhoneApplicationPage
    {
        private string result;
        public Download()
        {
            InitializeComponent();
        }

        private void  Start_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += (sener, a) => { result = a.Result; };
            client.DownloadStringAsync(new Uri(this.Input.Text));
            Output.Text = result;
        }
       
       
           
        
    }
}
