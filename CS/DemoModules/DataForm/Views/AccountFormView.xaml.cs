using System;
using DemoCenter.Maui.Demo;
using DemoCenter.Maui.DemoModules.DataForm.ViewModels;
using DevExpress.Maui.Core;

namespace DemoCenter.Maui.Views {
    public partial class DataFormAccountFormView : DemoPage {
        public DataFormAccountFormView() {
            InitializeComponent();
            BindingContext = new AccountFormViewModel();
            ON.OrientationChanged(this, (x) => {
                ((AccountFormViewModel)x.BindingContext).Rotate(dataForm, ON.Orientation(true, false));
            });
        }

        void OnOrientationChanged(object sender, EventArgs e) {
            ((AccountFormViewModel)this.BindingContext).Rotate(dataForm, ON.Orientation(true, false));
        }

        void SubmitOnClicked(object sender, EventArgs e) {
            if (dataForm.Validate()) {
                dataForm.Commit();
                DisplayAlert("Success", "Your account has been created successfully", "OK");
            }
        }
    }
}
