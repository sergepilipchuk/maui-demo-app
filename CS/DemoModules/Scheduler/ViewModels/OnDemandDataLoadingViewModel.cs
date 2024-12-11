using System.Linq;
using System.Threading.Tasks;
using DevExpress.Maui.Scheduler;
using Microsoft.EntityFrameworkCore;
using DevExpress.Maui.Mvvm;
using CreateSourceObjectEventArgs = DevExpress.Maui.Scheduler.CreateSourceObjectEventArgs;
using ImageSource = Microsoft.Maui.Controls.ImageSource;
using Command = Microsoft.Maui.Controls.Command;
using System;


namespace DemoCenter.Maui.ViewModels;

public class OnDemandDataLoadingViewModel : DXObservableObject {
    const int millisecondsDelay = 2000;

    ImageSource delayImageSource;
    ImageSource noDelayImageSource;
    ImageSource delayButtonImageSource;
    bool isBusy;
    bool delay;

    public OnDemandDataLoadingViewModel() {
        Init();
        ToggleDelayCommand = new Command(OnToggleDelay);
        UpdateImageSource();
    }

    public bool IsBusy {
        get => isBusy;
        set => SetProperty(ref isBusy, value);
    }
    public bool Delay {
        get => delay;
        set => SetProperty(ref delay, value, (oldValue, newValue) => UpdateImageSource());
    }
    public ImageSource DelayButtonImageSource {
        get => delayButtonImageSource;
        set => SetProperty(ref delayButtonImageSource, value);
    }
    public Command ToggleDelayCommand { get; }

    public event EventHandler OnDataReady;

    public void CreateSourceObject(CreateSourceObjectEventArgs args) {
        if (args.ItemType == ItemType.AppointmentItem)
            args.Instance = new AppointmentEntity();
    }
    public void FetchAppointments(FetchDataEventArgs args) {
        if (IsBusy)
            return;
        args.AsyncResult = GetAppointments(args);
    }
    async void Init() {
        IsBusy = true;
        await Task.Run(() => {
            DBInitializer.Init();
        });
        IsBusy = false;
        OnDataReady?.Invoke(this, EventArgs.Empty);
    }
    async Task<object[]> GetAppointments(FetchDataEventArgs args) {
        if (Delay)
            await Task.Delay(millisecondsDelay);
        using (var dbContext = new SchedulingContext())
            return await dbContext
                .AppointmentEntities
                .Where(args.GetFetchExpression<AppointmentEntity>())
                .ToArrayAsync();
    }
    void OnToggleDelay() {
        Delay = !Delay;
    }
    void UpdateImageSource() {
        if (Delay)
            DelayButtonImageSource = delayImageSource ??= ImageSource.FromFile("scheduler_load_delay");
        else
            DelayButtonImageSource = noDelayImageSource ??= ImageSource.FromFile("scheduler_load_nodelay");
    }
}