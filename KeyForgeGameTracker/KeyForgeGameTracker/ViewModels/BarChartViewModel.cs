using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyForgeGameTracker.ViewModels
{

    public class DataSet
    {
        public string Label { get; set; }
        public List<int> Data { get; set; }
        public List<string> BackgroundColor { get; set; }
        public List<string> BorderColor { get; set; }
        public int BorderWidth { get; set; }
    }

    public class BarChartViewModel
    {

        public List<string> Labels { get; set; }

        public List<DataSet> DataSets { get; set; }

    }
}
