using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls;
#if PaidDemoModules
using SkiaSharp.Views.Maui.Controls.Hosting;
#endif
using CommunityToolkit.Maui;
using DevExpress.Maui;
using DevExpress.Maui.Core;


namespace DemoCenter.Maui {
    public static class MauiProgram {

        public static MauiApp CreateMauiApp() {
            ThemeManager.UseAndroidSystemColor = false;
            ThemeManager.ApplyThemeToSystemBars = true;
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseDevExpress(useLocalization: false)
                .UseDevExpressCharts()
                .UseDevExpressCollectionView()
                .UseDevExpressControls()
                .UseDevExpressDataGrid()
#if PaidDemoModules
                .UseDevExpressDataGridExport()
#endif
                .UseDevExpressEditors()
                .UseDevExpressGauges()
#if PaidDemoModules
                .UseDevExpressHtmlEditor()
                .UseDevExpressPdf()
#endif
                .UseDevExpressScheduler()
                .UseDevExpressTreeView()
                .UseMauiCommunityToolkit()
#if DEBUG

#endif
#if PaidDemoModules
        .UseSkiaSharp()
#endif
                .ConfigureMauiHandlers(handlers => {
                    handlers.AddHandler<Shell, CustomShellRenderer>();
                });
            return builder.Build();
        }
    }
}
