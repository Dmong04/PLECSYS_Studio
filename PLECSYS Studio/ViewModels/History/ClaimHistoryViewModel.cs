using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Models;
using PLECSYS_Studio.Services.History;
using PLECSYS_Studio.Wrappers.History;
using System.Collections.ObjectModel;

namespace PLECSYS_Studio.ViewModels.History
{
    public partial class ClaimHistoryViewModel : ObservableObject
    {
        private readonly IInvoiceHistoryService _service;

        public ObservableCollection<InvoiceHistoryResponse> Claims { get; set; } = new();

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private string _message = string.Empty;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public ClaimHistoryViewModel(IInvoiceHistoryService service)
        {
            _service = service;
        }

        public async Task LoadClaims(int invoiceId)
        {
            try
            {
                IsLoading = true;
                Message = string.Empty;
                Claims.Clear();

                var response = await _service.GetClaimHistory(invoiceId);

                if (response.Success && response.Data != null)
                {
                    foreach (var item in response.Data)
                    {
                        Claims.Add(item);
                    }
                }
                else
                {
                    Message = response.Message ?? "Sin datos";
                }
            }
            catch (Exception ex)
            {
                Message = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}