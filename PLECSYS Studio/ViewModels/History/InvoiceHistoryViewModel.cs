using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Services;
using PLECSYS_Studio.Services.History;
using PLECSYS_Studio.Wrappers.History;
using System.Collections.ObjectModel;

namespace PLECSYS_Studio.ViewModels.History
{
    public partial class InvoiceHistoryViewModel : ObservableObject
    {
        private readonly IInvoiceHistoryService _service;
        private readonly SessionService _session;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string statusMessage = string.Empty;

        public ObservableCollection<InvoiceHistoryResponse> Items { get; } = [];
        public bool HasNoItems => Items.Count == 0;

        public InvoiceHistoryViewModel(IInvoiceHistoryService service, SessionService session)
        {
            _service = service;
            _session = session;
        }

        [RelayCommand]
        public async Task Load()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                var request = new FindHistoryRequest
                {
                    Email = _session.GetEmail(),
                    CompanyId = _session.GetCompanyId()
                };

                var result = await _service.GetInvoicesHistorybyUseraAndCompanyId(request);

                Items.Clear();

                if (!result.Success || result.Data is null)
                {
                    StatusMessage = result.Message ?? "No se encontraron registros.";
                    return;
                }

                foreach (var item in result.Data)
                    Items.Add(item);

                StatusMessage = string.Empty;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al cargar historial: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged(nameof(HasNoItems));
            }
        }

        [RelayCommand]
        public async Task GoBack() => await Shell.Current.GoToAsync("..");
    }
}