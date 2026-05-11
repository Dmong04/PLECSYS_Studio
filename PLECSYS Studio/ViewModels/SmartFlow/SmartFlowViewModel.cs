using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Services;
using PLECSYS_Studio.Wrappers.Users;
using System.Collections.ObjectModel;

namespace PLECSYS_Studio.ViewModels.SmartFlow
{
    public partial class SmartFlowViewModel : ObservableObject
    {
        private readonly SessionService _session;
        private List<SmartFlowOption> _allProcesses = [];

        [ObservableProperty]
        private string searchText = string.Empty;

        public ObservableCollection<SmartFlowOption> Processes { get; } = [];

        public SmartFlowViewModel(SessionService session)
        {
            _session = session;
            LoadProcesses();
        }

        private void LoadProcesses()
        {
            _allProcesses = _session.GetLinkedProcesses();
            ApplyFilter();
        }

        partial void OnSearchTextChanged(string value) => ApplyFilter();

        private void ApplyFilter()
        {
            Processes.Clear();

            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? _allProcesses
                : _allProcesses.Where(p =>
                    p.smartflow_name?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true);

            foreach (var process in filtered)
                Processes.Add(process);
        }

        [RelayCommand]
        public async Task SelectProcess(SmartFlowOption process)
        {
            await Shell.Current.DisplayAlert("Proceso seleccionado", process.smartflow_name, "OK");
        }
    }
}