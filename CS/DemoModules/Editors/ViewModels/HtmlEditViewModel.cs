using System.IO;
using System.Threading.Tasks;
using DevExpress.Maui.Mvvm;
using Microsoft.Maui.Storage;

namespace DemoCenter.Maui.DemoModules.Editors.ViewModels;

public class HtmlEditViewModel : DXObservableObject {
    public string Text { get => text; set => SetProperty(ref text, value); }
    public string FileName {
        get => fileName;
        set => SetProperty(ref fileName, value, changedCallback: async () => {
            await LoadData(FileName);
        });
    }

    async Task LoadData(string fileName) {
        using Stream stream = await FileSystem.Current.OpenAppPackageFileAsync(fileName);
        using StreamReader reader = new StreamReader(stream);
        string html = await reader.ReadToEndAsync();
        Text = html;
    }

    string text;
    string fileName;
}