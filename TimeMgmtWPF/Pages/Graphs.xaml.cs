using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using TimeMgmtLib;
using TimeMgmtLib.Models;

namespace TimeMgmtWPF.Pages
{
    /// <summary>
    /// Interaction logic for Graphs.xaml
    /// </summary>
    public partial class Graphs : Page
    {
        public Graphs()
        {
            InitializeComponent();
            InitializeWorktimeStartEndChart();
            InitializeFlexibleHours();
        }

        // Properties for WorktimeStartEndChart
        public SeriesCollection SeriesCollectionWorktimeStartEnd { get; set; }
        public string[] LabelsWorktimeStartEnd { get; set; }
        public Func<double, string> YFormatterWorktimeStartEnd { get; set; }

        // Properties for FlexibleHours
        public SeriesCollection SeriesCollectionFlexibleHours { get; set; }
        public string[] LabelsFlexibleHours { get; set; }
        public Func<double, string> YFormatterFlexibleHours { get; set; }

        private void InitializeWorktimeStartEndChart()
        {
            TimeMgmtFactory fac = TimeMgmtFactory.Instance;
            ITimestampHandler tshandler = fac.Resolve<ITimestampHandler>();
            
            SeriesCollectionWorktimeStartEnd = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Check in",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 } //Values dazua
                },
                new LineSeries
                {
                    Title = "Check out",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 } //Values do a dazua
                }
            };

            //Defining Labels of Graph
            LabelsWorktimeStartEnd = new[] { "Jan", "Feb", "Mar", "Apr", "May" }; //Dua de tog dazua du hundling
            YFormatterWorktimeStartEnd = value => value.ToString("C");
            DataContext = this;
        }
        private void InitializeFlexibleHours()
        {
            //Here comes second Graph
        }
    }
}
