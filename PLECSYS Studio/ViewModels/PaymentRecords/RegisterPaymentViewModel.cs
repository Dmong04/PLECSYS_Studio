using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PLECSYS_Studio.Data.Currencies;
using PLECSYS_Studio.Data.PaymentMethods;
using PLECSYS_Studio.Data.PaymentRecords;
using PLECSYS_Studio.Services;
using PLECSYS_Studio.Services.Payments;
using PLECSYS_Studio.Services.PaymentService;
using PLECSYS_Studio.Wrappers.Currencies;
using PLECSYS_Studio.Wrappers.PaymentMethods;
using PLECSYS_Studio.Wrappers.PaymentRecords;
using System.Collections.ObjectModel;
using System.Text.Json;
using static MongoDB.Driver.WriteConcern;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PLECSYS_Studio.ViewModels.Payments
{
    public partial class RegisterPaymentViewModel : ObservableObject
    {
        private readonly IPaymentRecordService _service;
        private readonly PaymentMethodData _paymentMethodData;
        private readonly CurrencyData _currencyData;
        private readonly SessionService _session;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private int invoiceConsecutive;

        [ObservableProperty]
        private string thirdPartyTransactionId = string.Empty;

        [ObservableProperty]
        private CurrencyResponse? selectedCurrency;

        [ObservableProperty]
        private PaymentMethodResponse? selectedPaymentMethod;

        [ObservableProperty]
        private bool isOtherMethod;

        [ObservableProperty]
        private string otherMethodDetail = string.Empty;

        [ObservableProperty]
        private string amount = string.Empty;

        [ObservableProperty]
        private DateTime paymentDate = DateTime.Today;

        [ObservableProperty]
        private string paymentDetail = string.Empty;

        public ObservableCollection<CurrencyResponse> Currencies { get; } = [];
        public ObservableCollection<PaymentMethodResponse> PaymentMethods { get; } = [];

        public RegisterPaymentViewModel(
            IPaymentRecordService service,
            PaymentMethodData paymentMethodData,
            CurrencyData currencyData,
            SessionService session)
        {
            _service = service;
            _paymentMethodData = paymentMethodData;
            _currencyData = currencyData;
            _session = session;
        }

        // Detecta si el método seleccionado es OTRO (código 99)
        partial void OnSelectedPaymentMethodChanged(PaymentMethodResponse? value)
            => IsOtherMethod = value?.PaymentMethodCode == 99;

        [RelayCommand]
        public async Task Load()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                InvoiceConsecutive = _session.CurrentInvoiceConsecutive;

                var methods = await _paymentMethodData.GetAllPaymentMethods();
                PaymentMethods.Clear();
                if (methods.Success && methods.Data is not null)
                    foreach (var m in methods.Data)
                        PaymentMethods.Add(m);

                var currencies = await _currencyData.GetAllCurrencies();
                Currencies.Clear();
                if (currencies.Success && currencies.Data is not null)
                    foreach (var c in currencies.Data)
                        Currencies.Add(c);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudieron cargar los datos: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task Save()
        {
            if (!decimal.TryParse(amount, out var parsedAmount) || parsedAmount <= 0)
            {
                await Shell.Current.DisplayAlert("Error", "Ingrese un monto válido.", "OK");
                return;
            }
            if (selectedCurrency is null)
            {
                await Shell.Current.DisplayAlert("Error", "Seleccione una moneda.", "OK");
                return;
            }
            if (selectedPaymentMethod is null)
            {
                await Shell.Current.DisplayAlert("Error", "Seleccione un método de pago.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(paymentDetail))
            {
                await Shell.Current.DisplayAlert("Error", "Ingrese el detalle del pago.", "OK");
                return;
            }
            if (IsOtherMethod && string.IsNullOrWhiteSpace(otherMethodDetail))
            {
                await Shell.Current.DisplayAlert("Error", "Especifique el detalle del método de pago.", "OK");
                return;
            }

            IsBusy = true;
            try
            {
                var request = new PaymentRecordRequest
                {
                    Source_id = _session.CurrentInvoiceId,
                    Currency_id = SelectedCurrency.CurrencyId,
                    Payment_method_id = SelectedPaymentMethod.PaymentMethodId,
                    Detail_payment_method = IsOtherMethod ? OtherMethodDetail : null,
                    Paid_amount = parsedAmount,
                    Payment_date = PaymentDate,
                    Payment_detail = PaymentDetail,
                    Third_party_transaction_id = string.IsNullOrWhiteSpace(ThirdPartyTransactionId)
                        ? Guid.NewGuid().ToString("N")[..8]  // genera uno si está vacío
                        : ThirdPartyTransactionId
                };

                var result = await _service.RegisterPaymentAsync(request);

                var json = JsonSerializer.Serialize(request);
                Console.WriteLine($"Request body: {json}");

                if (result is null || !result.Success)
                {
                    await Shell.Current.DisplayAlert("Error", result?.Message ?? "Error al registrar el pago.", "OK");
                    return;
                }

                await Shell.Current.DisplayAlert("Éxito", "Pago registrado correctamente.", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Ha ocurrido un problema: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task GoBack() => await Shell.Current.GoToAsync("..");
    }
}