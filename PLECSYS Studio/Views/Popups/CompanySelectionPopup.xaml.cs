using CommunityToolkit.Maui.Views;
using PLECSYS_Studio.Services;
using PLECSYS_Studio.ViewModels;
using PLECSYS_Studio.Wrappers.Users;

namespace PLECSYS_Studio.Views.Popups
{
    public partial class CompanySelectionPopup : Popup
    {
        public CompanySelectionPopup(ICollection<CompanyOption> companies, SessionService sessionService)
        {
            InitializeComponent();
            BindingContext = new CompanySelectionViewModel(companies.ToList(), sessionService, this);
        }

    }
}