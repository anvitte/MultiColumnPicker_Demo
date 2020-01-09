using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Rg.Plugins.Popup.Contracts;
namespace PickerDemo.Popup
{
    public partial class MultiPicker : PopupPage
    {
        MultiPickerModel multiPickerModel = new MultiPickerModel();
        string subcategory = string.Empty;
        public MultiPicker(MultiPickerModel objModel)
        {
            InitializeComponent();
            
            LoadData(objModel);
            CloseWhenBackgroundIsClicked = false;
        }

        private void LoadData(MultiPickerModel objModel)
        {
            multiPickerModel = objModel;
            lblHeader.Text = objModel.Header;
            lblCategoryHeader.Text = objModel.LblCategoryHeader;
            lblSubCategoryHeader.Text = objModel.LblSubCategoryHeader;
            lstCategory.ItemsSource = GetAllCategory(objModel);
            lstCategory.SelectedItem = objModel.DataModel[0].category;
            lstCategory.ItemSelected += LstCategory_ItemSelected;
            lstSubCategory.ItemsSource = GetAllSubCategory(objModel.DataModel[0].category,objModel.DataModel);
            lstSubCategory.ItemSelected += LstSubCategory_ItemSelected;
            lstSubCategory.ItemSelected -= LstSubCategory_UnSelected;
        }

        private void LstSubCategory_UnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            subcategory = string.Empty;
        }

        private void LstSubCategory_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            subcategory = e.SelectedItem as string;
        }

        private void LstCategory_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        
           var selectedItem= e.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedItem))
            {
                lstSubCategory.ItemsSource = GetAllSubCategory(selectedItem, multiPickerModel.DataModel);
            }
            if (!String.IsNullOrEmpty(subcategory))
            {
                subcategory = string.Empty;
            }
        }

        private IEnumerable GetAllSubCategory(string category, List<JsonModel> objModel)
        {
            List<string> lstObjDefaultSubcategory = new List<string>();

            foreach (var item in objModel)
            {
                if (item.category == category)
                    lstObjDefaultSubcategory.Add(item.value);
            }
            return lstObjDefaultSubcategory;
        }

        private List<string> GetAllCategory(MultiPickerModel objModel)
        {
            List<string> lstobjCategory = new List<string>();
            foreach (var item in objModel.DataModel.Select(m=>m.category).Distinct())
            {

                lstobjCategory.Add(item);
            }
            return lstobjCategory;
        }
        public  void Ok_Clicked(object obj,EventArgs e)
        {
            if (!string.IsNullOrEmpty(subcategory))
            {
                MessagingCenter.Send(new MyMessage() { Myvalue = subcategory }, "PopUpData");
                Navigation.PopPopupAsync();

            }
            else
            {
                DisplayAlert("Select", "Please select the sub category/ Location", "ok");
            }
           
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}
