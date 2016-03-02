using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCalendar
{
    class MainViewModel : INotifyPropertyChanged
    {
        private DateTime _currentMonth;
        
        public string CurrentMonth
        {
            get
            {
                return this._currentMonth.ToString(EasyCalendar.Resources.AppResources.TitleDateFormat);
            }
        }
        public DateTime CurrentMonthDate
        {
            get { return this._currentMonth; }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            this._currentMonth = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
            this.RaisePropertyChanged("CurrentMonth");
        }
        #region 移动月份
        public void MoveToLastMonth()
        {
            this._currentMonth = this._currentMonth.AddMonths(-1);
            this.RaisePropertyChanged("CurrentMonth");
        }
        public  void MoveToCurrentMonth()
        {
            this._currentMonth = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
            this.RaisePropertyChanged("CurrentMonth");
        }
        public void MoveToNextMonth()
        {
            this._currentMonth = this._currentMonth.AddMonths(1);
            this.RaisePropertyChanged("CurrentMonth");
        }
        #endregion
        private void RaisePropertyChanged(string propertyname)
        {
            if(this.PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }











    }
}
