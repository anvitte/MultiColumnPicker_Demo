using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PickerDemo.Popup;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace PickerDemo
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public void OpenPopup_Tapped(object obj, EventArgs e)
        {
            List<JsonModel> arrayModel;
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("PickerDemo.data.json");
            string text = "";
            // text = File.ReadAllText("data.json");
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var rootobject = JsonConvert.DeserializeObject<List<JsonModel>>(json);
                arrayModel = rootobject;



            }
         

            MultiPickerModel objModel = new MultiPickerModel();
            objModel.DataModel = arrayModel;
            objModel.Header = "Multi Picker for city";
            objModel.LblCategoryHeader = "Country";
            objModel.LblSubCategoryHeader = "City";

            MessagingCenter.Subscribe<MyMessage>(this, "PopUpData", (value) =>
            {
                string receivedData = value.Myvalue;
                txtLabel.Text = receivedData;
            });

            Navigation.PushPopupAsync(new MultiPicker(objModel));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }

    internal class MyMessage
    {
        public string Myvalue { get; set; }
    }
}
