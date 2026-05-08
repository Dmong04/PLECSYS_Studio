using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using PLECSYS_Studio.ViewModels.GPS;
using System.ComponentModel;

namespace PLECSYS_Studio.Views.GPS;

public partial class MapPage : ContentPage
{
    private readonly LocationMapViewModel _viewModel;

    public MapPage(LocationMapViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var location = await Geolocation.Default.GetLastKnownLocationAsync();

        if (location != null)
        {
            var mapSpan = MapSpan.FromCenterAndRadius(
                new Location(location.Latitude, location.Longitude),
                Distance.FromKilometers(1));

            UserMap.MoveToRegion(mapSpan);
        }
    }
}