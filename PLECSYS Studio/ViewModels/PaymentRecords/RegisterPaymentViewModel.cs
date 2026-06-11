using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Services.Currencies;
using PLECSYS_Studio.Services.PaymentMethods;
using PLECSYS_Studio.Services.PaymentService;
using PLECSYS_Studio.Wrappers.Currencies;
using PLECSYS_Studio.Wrappers.PaymentMethods;
using PLECSYS_Studio.Wrappers.PaymentRecords;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace PLECSYS_Studio.ViewModels.PaymentRecords
{
    public partial class RegisterPaymentViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ICurrencyService _currencyService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IPaymentRecordService _paymentRecordService;

        [ObservableProperty] private int invoiceId;
        [ObservableProperty] private int invoiceConsecutive;
        [ObservableProperty] private string? paymentConsecutive;
        [ObservableProperty] private CurrencyResponse? selectedCurrency;
        [ObservableProperty] private PaymentMethodResponse? selectedPaymentMethod;
        [ObservableProperty] private string? paymentMethodDetail;
        [ObservableProperty] private decimal? paymentAmount;
        [ObservableProperty] private DateTime? paymentDate;
        [ObservableProperty] private string? paymentDetail;
        [ObservableProperty] private decimal pendingBalance;
        [ObservableProperty] private string? thirdPartyTransactionId;
        [ObservableProperty] private bool isBusy;
        [ObservableProperty] private bool isOtherMethod;

        public ObservableCollection<CurrencyResponse> Currencies { get; } = [];
        public ObservableCollection<PaymentMethodResponse> PaymentMethods { get; } = [];

        public RegisterPaymentViewModel(
            ICurrencyService currencyService,
            IPaymentMethodService paymentMethodService,
            IPaymentRecordService paymentRecordService)
        {
            _currencyService = currencyService;
            _paymentMethodService = paymentMethodService;
            _paymentRecordService = paymentRecordService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("invoiceId", out var invoiceId))
                InvoiceId = Convert.ToInt32(invoiceId);

            if (query.TryGetValue("invoiceConsecutive", out var consecutive))
            {
                InvoiceConsecutive = Convert.ToInt32(consecutive);
                PaymentConsecutive = InvoiceConsecutive.ToString();
            }

            if (query.TryGetValue("pendingBalance", out var balance))
                PendingBalance = Convert.ToDecimal(balance);
        }

        public async Task InitializeAsync()
        {
            await LoadCurrencies();
            await LoadPaymentMethods();
        }

        [RelayCommand]
        public async Task LoadCurrencies()
        {
            try
            {
                IsBusy = true;
                var response = await _currencyService.GetCurrencies();
                Currencies.Clear();

                if (response.Data?.Count is not 0)
                    foreach (var currency in response.Data) Currencies.Add(currency);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Hubo un error al selecionar la moneda de pago: {ex.Message}", "Ok");
                return;
            }
            finally { IsBusy = false; }
        }

        [RelayCommand]
        public async Task LoadPaymentMethods()
        {
            try
            {
                IsBusy = true;
                var response = await _paymentMethodService.GetPaymentMethods();
                PaymentMethods.Clear();

                if (response.Data is not null)
                    foreach (var method in response.Data)
                        PaymentMethods.Add(method);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error",
                    $"Hubo un error al cargar los métodos de pago: {ex.Message}", "Ok");
                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task RegisterPayment()
        {
            try
            {
                IsBusy = true;
                var request = new PaymentRecordRequest()
                {
                    Source_id = InvoiceId,
                    Currency_id = SelectedCurrency?.CurrencyId ?? 0,
                    Payment_method_id = SelectedPaymentMethod?.PaymentMethodId ?? 0,
                    Detail_payment_method = PaymentMethodDetail,
                    Paid_amount = PaymentAmount ?? 0,
                    Payment_date = PaymentDate ?? DateTime.Now,
                    Payment_detail = PaymentDetail,
                    Third_party_transaction_id = ThirdPartyTransactionId ?? string.Empty
                };
                var response = await _paymentRecordService.RegisterPaymentAsync(request);

                var json = JsonSerializer.Serialize(request);
                Console.WriteLine($"Request body: {json}");

                if (!response.Success)
                {
                    await Shell.Current.DisplayAlert("Error al registrar pago", response.Message, "Aceptar");
                    return;
                }

                await Shell.Current.DisplayAlert("Pago registrado", response.Message, "Aceptar");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error",
                    $"Hubo un error al Registrar pago: {ex.Message}", "Ok");
                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

        partial void OnSelectedPaymentMethodChanged(PaymentMethodResponse? value)
        {
            IsOtherMethod = value?.PaymentMethodCode == 99;
        }

        // GoBack
        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}