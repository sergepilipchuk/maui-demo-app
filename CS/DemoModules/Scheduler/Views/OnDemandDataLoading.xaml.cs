using DemoCenter.Maui.Services;
using DemoCenter.Maui.ViewModels;

using DevExpress.Maui.Scheduler;
using Microsoft.Maui.Controls;

namespace DemoCenter.Maui.Views {
    public partial class OnDemandDataLoading : Demo.DemoPage {
        bool inNavigation;

        public OnDemandDataLoading() {
            InitializeComponent();
            dayView.DataStorage.DataSource.FetchAppointments += (s, e) => ViewModel.FetchAppointments(e);
            dayView.DataStorage.DataSource.CreateSourceObject += (s, e) => ViewModel.CreateSourceObject(e);
            ViewModel.OnDataReady += (s, e) => dayView.DataStorage.RefreshData();
        }

        OnDemandDataLoadingViewModel ViewModel => (OnDemandDataLoadingViewModel)BindingContext;

        protected override void OnAppearing() {
            base.OnAppearing();
            this.inNavigation = false;
        }

        async void DayView_OnTap(object sender, SchedulerGestureEventArgs e) {
            if (this.inNavigation)
                return;
            Page appointmentPage = storage.CreateAppointmentPageOnTap(e, true);
            if (appointmentPage != null) {
                inNavigation = true;
                await DemoNavigationService.NavigateToPage(appointmentPage);
            }
        }
    }
}
