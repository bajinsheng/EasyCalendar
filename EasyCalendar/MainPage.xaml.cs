using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Scheduler;
using EasyCalendar.Resources;

namespace EasyCalendar
{
    public partial class MainPage : PhoneApplicationPage
    {
        private MainViewModel _mainViewModel;

        public MainPage()
        {
            InitializeComponent();
            InitializeApplicationBar();
            this._mainViewModel = new MainViewModel();
            this.DataContext = _mainViewModel;
        }

        private void InitializeApplicationBar()
        {
            this.ApplicationBar.Buttons.Clear();

            ApplicationBarIconButton btnLastMonth = new ApplicationBarIconButton();
            btnLastMonth.Text = AppResources.LastMonth;
            btnLastMonth.IconUri = new Uri("Assets/Icons/Dark/back.png", UriKind.Relative);
            btnLastMonth.Click += btnLastMonth_Click;
            this.ApplicationBar.Buttons.Add(btnLastMonth);

            ApplicationBarIconButton btnCurrentMonth = new ApplicationBarIconButton();
            btnCurrentMonth.Text = AppResources.CurrentMonth;
            btnCurrentMonth.IconUri = new Uri("Assets/Icons/Dark/favs.png", UriKind.Relative);
            btnCurrentMonth.Click += btnCurrentMonth_Click;
            this.ApplicationBar.Buttons.Add(btnCurrentMonth);

            ApplicationBarIconButton btnNextMonth = new ApplicationBarIconButton();
            btnNextMonth.Text = AppResources.NextMonth;
            btnNextMonth.IconUri = new Uri("Assets/Icons/Dark/next.png", UriKind.Relative);
            btnNextMonth.Click += btnNextMonth_Click;
            this.ApplicationBar.Buttons.Add(btnNextMonth);

            ApplicationBarMenuItem Download = new ApplicationBarMenuItem();
            Download.Text = "........";
            Download.Click += Download_Click;


            if (ScheduledActionService.Find("task") != null)
            {
                ScheduledActionService.Remove("task");
            }
            PeriodicTask task = new PeriodicTask("task");
            task.Description = "Refreash the icon";
            ScheduledActionService.Add(task);
        }

        void Download_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Download.xaml", UriKind.Relative));
        }

        void btnNextMonth_Click(object sender, EventArgs e)
        {
            this._mainViewModel.MoveToNextMonth();
            this.ShowCalendar();
        }

        void btnCurrentMonth_Click(object sender, EventArgs e)
        {
            this._mainViewModel.MoveToCurrentMonth();
            this.ShowCalendar();
        }

        void btnLastMonth_Click(object sender, EventArgs e)
        {
            this._mainViewModel.MoveToLastMonth();
            this.ShowCalendar();
        }

        public void ShowCalendar()
        {
            this.ClearCalendar();
            Int32 start = 7 + (Int32)this._mainViewModel.CurrentMonthDate.DayOfWeek;
            Int32 end = this._mainViewModel.CurrentMonthDate.AddMonths(1).AddDays(-1).Day + start;
            if (end < 6 * 7 - 4)
            {
                start += 7;
                end += 7;
            }
            for (Int32 i = start, day = 1; i < end; i++, day++)
            {
                TextBlock block = this.GridViewAll.Children[i] as TextBlock;
                block.Style = this.GetStyle("CalendarDateStyle");
                block.Text = day.ToString();
            }

            for (Int32 i = start - 1, day = this._mainViewModel.CurrentMonthDate.AddDays(-1).Day; i >= 7; i--, day--)
            {
                TextBlock block = this.GridViewAll.Children[i] as TextBlock;
                block.Style = this.GetStyle("CalendarHypoDateStyle");
                block.Text = day.ToString();
            }

            for (Int32 i = end, day = 1; i < 7 * 7; i++, day++)
            {
                TextBlock block = this.GridViewAll.Children[i] as TextBlock;
                block.Style = this.GetStyle("CalendarHypoDateStyle");
                block.Text = day.ToString();
            }
        }
        
        private Style GetStyle(string StyleName)
        {
            return Application.Current.Resources[StyleName] as Style;
        }
        private void ClearCalendar()
        {
            for(Int32 i=1;i<7;i++)
            {
                for(Int32 j=0;j<7;j++)
                {
                    TextBlock temp = this.GridViewAll.Children[i * 7 + j] as TextBlock;
                    temp.Text = "";
                }
            }
        }

     
       
 
    }
}