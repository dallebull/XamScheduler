using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FriskaClient
{
    public class CustomAppointment : CalendarInlineEvent, INotifyPropertyChanged
    {

        private int _Id;
        public int EventId
        {
            get { return _Id; }
            set { _Id = value; OnPropertyChanged("EventId"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
