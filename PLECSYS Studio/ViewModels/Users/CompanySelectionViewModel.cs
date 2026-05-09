using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Services;
using PLECSYS_Studio.Wrappers.Users;

namespace PLECSYS_Studio.ViewModels
{
    public partial class CompanySelectionViewModel : ObservableObject
    {
        private readonly SessionService _sessionService;
        private readonly Popup _popup;

        public ICollection<CompanyOption> Companies { get; }

        public CompanySelectionViewModel(
            ICollection<CompanyOption> companies,
            SessionService sessionService,
            Popup popup)
        {
            Companies = companies;
            _sessionService = sessionService;
            _popup = popup;
        }

        [RelayCommand]
        public async Task SelectCompany(CompanyOption company)
        {
            _sessionService.SaveSelectedCompany(company.company_id, company.company_name);
            await Task.Delay(100);
            await _popup.CloseAsync();
        }
    }
}