using CommunityToolkit.Maui.Views;

namespace PLECSYS_Studio.Views.Popups
{
    public partial class InvoiceHistoryDetailPopup : Popup
    {
        public string ActionText { get; set; } = string.Empty;
        public string DateText { get; set; } = string.Empty;
        public string DescriptionText { get; set; } = string.Empty;

        public InvoiceHistoryDetailPopup()
        {
            InitializeComponent();
            this.Opened += InvoiceHistoryDetailPopup_Opened;
        }

        private void InvoiceHistoryDetailPopup_Opened(object? sender, EventArgs e)
        {
            ActionLabel.Text = $"Acción: {ActionText}";
            DateLabel.Text = $"Fecha: {DateText}";
            DescriptionLabel.Text = $"Descripción: {DescriptionText}";
        }

        private void OnCloseClicked(object? sender, EventArgs e) => CloseAsync();
    }
}