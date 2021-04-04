using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace FlightSimulator2.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.g
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase,INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        public ICommand SelectedAttributesCommand { get; set; }
        public MainViewModel()
        {
            SelectedAttributesCommand = new RelayCommand(SelectedAttributeFunc);
            string path = @"C:\Users\noa83\Downloads\reg_flight.csv";
            ParseXml(@"C:\Users\noa83\Downloads\playback_small (2).xml");
            ParseCsv(path);


            //DataContext = this;

            this.Title = "Example 2";
            this.Points = new List<DataPoint>
                              {
                                  new DataPoint(0, 4),
                                  new DataPoint(10, 13),
                                  new DataPoint(20, 15),
                                  new DataPoint(30, 16),
                                  new DataPoint(40, 12),
                                  new DataPoint(50, 12)
                              };
        }
        public int SelectedAttributeIndex { get; set; }
        void SelectedAttributeFunc()
        {
            var selection = SelectedAttributeIndex;
           // var name = Attributes[selection];

            DataBase dataBase = new DataBase(@"C:\Users\noa83\Downloads\reg_flight.csv");

            var test = dataBase.getColumnAtIndex(selection);
        }

        public string[] Attributes { get; set; }
        public void ParseXml(string path)
        {
            Settings d = new Settings(path);

            Attributes = d.attributes;
        }


        public void ParseCsv(string path)
        {
            // this is the path on my computer. i dont know how to define a path that will be accurate on every computer
            DataBase dataBase = new DataBase(path);



            int currentRowIndex = 0; // bind to "currentRow" to jump to another frame
            int lastRowIndex = dataBase.numberOfRows;
            int timeToSleep = 100; // bind to "timeToSleep" to change the speed

            while (currentRowIndex != lastRowIndex)
            {
                var row = Encoding.ASCII.GetBytes(dataBase.getRowAtIndex(currentRowIndex) + "\n");

                string test = dataBase.getRowAtIndex(currentRowIndex);

                currentRowIndex++;
                string[] spl = test.Split(',');
            }
        }

        public string Title { get; private set; }

        public IList<DataPoint> Points { get; private set; }


        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}