using System;
using System.Collections.Generic;

namespace PickerDemo.Popup
{
    public class MultiPickerModel
    {
       
            public List<JsonModel> DataModel { get; set; }
            public string Header { get; set; }

            public string LblCategoryHeader { get; set; }
            public List<object> Category { get; set; }
        public string LblSubCategoryHeader { get; set; }
        public List<object> SubCategory { get; set; }

    }
    public class JsonModel
    {
        public int id { get; set; }
        public string value { get; set; }
        public string category { get; set; }
    }
}
