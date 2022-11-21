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

            Task.Run(new Action(() =>
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    this.CourseSelections.Clear();
                    //this.CourseSeriesList.Clear();
                    this.CourseSelections.Add(new CourseSeriesModel() { CourseName = "All" });

                    foreach (var (i, c) in CourseSeriesList.Select((c, i) => (i, c)))
                    {
                        c.CourseName = $"{nameof(CourseSeriesModel.CourseName)}_{i}";
                        c.SeriesColection = new SeriesCollection();
                        c.SeriesList = new ObservableCollection<SeriesModel>();

                        await Task.Delay(TimeSpan.FromSeconds(2)).ConfigureAwait(true);

                        c.SeriesColection.Add(new PieSeries
                        {
                            Title = "PieSeriesTitle1",
                            Values = new ChartValues<ObservableValue> { new ObservableValue(3) },
                            DataLabels = false
                        });
                        c.SeriesColection.Add(new PieSeries
                        {
                            Title = "PieSeriesTitle2",
                            Values = new ChartValues<ObservableValue> { new ObservableValue(2) },
                            DataLabels = false
                        });
                        c.SeriesColection.Add(new PieSeries
                        {
                            Title = "PieSeriesTitle3",
                            Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                            DataLabels = false
                        });
                        foreach (var s in c.SeriesColection)
                        {
                            c.SeriesList.Add(new SeriesModel
                            {
                                SeriesName = s.Title,
                                CurrentValue = (decimal)(s.Values[0] as ObservableValue).Value,
                                IsGrowing = true,
                                ChangeRate = 30,
                            });
                        }

                        this.CourseSelections.Add(new CourseSeriesModel() { CourseName = c.CourseName });

                        c.IsShowSkeleton = false;


                    }

                    this.ItemCount = CourseSeriesList.Max(c => c.SeriesList.Count);
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
