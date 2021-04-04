using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;
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
        // this class counts the time that passed since the beginning of the video simulator
        //private Timer timer;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        public ICommand SelectedAttributesCommand { get; set; }
       static string path = @"C:\Users\noa83\Downloads\reg_flight.csv";
        //int currentRowIndex = 0; // bind to "currentRow" to jump to another frame

        
        public MainViewModel()
        {
            SelectedAttributesCommand = new RelayCommand(SelectedAttributeFunc);
           
            ParseXml(@"C:\Users\noa83\Downloads\playback_small (2).xml");
            ParseCsv(path);

            //lastRowIndex = dataBase.numberOfRows;
            //timer = new Timer(100);
            //timer.Elapsed += Timer_Elapsed;
            //timer.AutoReset = true;
            //timer.Enabled = true;


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


        DataBase dataBase = new DataBase(path);
        //int lastRowIndex;
        //private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    if (currentRowIndex < lastRowIndex)
        //    {
        //        var row = Encoding.ASCII.GetBytes(dataBase.getRowAtIndex(currentRowIndex) + "\n");
        //        Console.Write(dataBase.getRowAtIndex(currentRowIndex));
        //        currentRowIndex++;
        //    }          
        //}

        public int SelectedAttributeIndex { get; set; }
        void SelectedAttributeFunc()
        {
            var selection = SelectedAttributeIndex;
            var name = Attributes[selection];
            Title1 = name;

            //DataBase dataBase = new DataBase(@"C:\Users\noa83\Downloads\reg_flight.csv");

            //var test = dataBase.getColumnAtIndex(selection);
            var data = dataBase.data;
            var dataPoints = new ObservableCollection<DataPoint>();


            for (int i = 0; i < dataBase.numberOfRows; i++)
            {
                dataPoints.Add(new DataPoint(i,double.Parse(data[i][selection])));
              //  dataPoints.Add(new DataPoint(i, r.Next(70)));
            }

            Points_Test = dataPoints;
        }
        Random r = new Random();
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
            //int timeToSleep = 100; // bind to "timeToSleep" to change the speed

            while (currentRowIndex != lastRowIndex)
            {
                var row = Encoding.ASCII.GetBytes(dataBase.getRowAtIndex(currentRowIndex) + "\n");

                string test = dataBase.getRowAtIndex(currentRowIndex);

                currentRowIndex++;
                string[] spl = test.Split(',');
            }
        }

        public string Title { get; private set; }

        private string title1;
        public string Title1
        {
            get { return title1; }
            set
            {
                title1 = value;
                NotifyPropertyChanged("Title1");
            }
        }

        public IList<DataPoint> Points { get; private set; }


        private IList<DataPoint> points_Test;
        public IList<DataPoint> Points_Test
        {
            get
            {
                return points_Test;

            }
            set
            {
                points_Test = value;
                NotifyPropertyChanged("Points_Test");
            }
        }


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