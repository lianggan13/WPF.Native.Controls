using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFCommon.MVVMFoundation;
using WPFItemsControl.Model;

namespace WPFItemsControl.ViewModel
{
    public class SelectorVM : NotifyPropertyChanged
    {
        private int _instrumentValue = 0;

        public int InstrumentValue
        {
            get { return _instrumentValue; }
            set { _instrumentValue = value; OnPropertyChanged(); }
        }

        private int _itemCount;

        public int ItemCount
        {
            get { return _itemCount; }
            set { _itemCount = value; OnPropertyChanged(); }
        }

        public ObservableCollection<CourseSeriesModel> CourseSelections { get; set; } = new ObservableCollection<CourseSeriesModel>();

        public ObservableCollection<CourseSeriesModel> CourseSeriesList { get; set; } = new ObservableCollection<CourseSeriesModel>();


        Random random = new Random();
        bool taskSwitch = true;
        List<Task> taskList = new List<Task>();
        public SelectorVM()
        {
            this.RefreshInstrumentValue();

            this.InitCourseSeries();
        }

        private void InitCourseSeries()
        {
            int count = 3;
            for (int i = 0; i < count; i++)
            {
                CourseSeriesModel csm = new CourseSeriesModel()
                {
                    IsShowSkeleton = true,
                };
                this.CourseSeriesList.Add(csm);
            };

            Task.Run(new Action(async () =>
            {
                var cList = new List<CourseSeriesModel>(); // LocalDataAccess.GetInstance().GetCoursePlayRecord();
                for (int i = 0; i < count; i++)
                {
                    CourseSeriesModel csm = new CourseSeriesModel()
                    {
                        CourseName = $"{nameof(CourseSeriesModel.CourseName)}_{i}",
                        SeriesColection = new LiveCharts.SeriesCollection(),
                        SeriesList = new ObservableCollection<SeriesModel>(),
                    };
                    cList.Add(csm);
                }

                await Task.Delay(4000);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.CourseSelections.Clear();
                    this.CourseSeriesList.Clear();

                    this.CourseSelections.Add(new CourseSeriesModel() { CourseName = "All" });


                    this.ItemCount = cList.Max(c => c.SeriesList.Count);
                    foreach (var csm in cList)
                    {
                        csm.SeriesColection.Add(new PieSeries
                        {
                            Title = "PieSeriesTitle1",
                            Values = new ChartValues<ObservableValue> { new ObservableValue(3) },
                            DataLabels = false
                        });
                        csm.SeriesColection.Add(new PieSeries
                        {
                            Title = "PieSeriesTitle2",
                            Values = new ChartValues<ObservableValue> { new ObservableValue(2) },
                            DataLabels = false
                        });
                        csm.SeriesColection.Add(new PieSeries
                        {
                            Title = "PieSeriesTitle3",
                            Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                            DataLabels = false
                        });
                        foreach (var s in csm.SeriesColection)
                        {
                            csm.SeriesList.Add(new SeriesModel
                            {
                                SeriesName = s.Title,
                                CurrentValue = (decimal)(s.Values[0] as ObservableValue).Value,
                                IsGrowing = true,
                                ChangeRate = 30,
                            });
                        }

                        this.CourseSelections.Add(new CourseSeriesModel() { CourseName = csm.CourseName });
                        this.CourseSeriesList.Add(csm);
                    }
                });
            }));


        }


        private void RefreshInstrumentValue()
        {
            var task = Task.Factory.StartNew(new Action(async () =>
            {
                while (taskSwitch)
                {
                    InstrumentValue = random.Next(Math.Max(this.InstrumentValue - 5, -10), Math.Min(this.InstrumentValue + 5, 90));

                    await Task.Delay(1000);
                }
            }));
            taskList.Add(task);
        }

        public void Dispose()
        {
            try
            {
                taskSwitch = false;
                Task.WaitAll(this.taskList.ToArray());
            }
            catch { }
        }
    }
}
