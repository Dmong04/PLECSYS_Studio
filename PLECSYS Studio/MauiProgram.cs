using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PLECSYS_Studio.Data.Companies;
using PLECSYS_Studio.Data.Currencies;
using PLECSYS_Studio.Data.GPS;
using PLECSYS_Studio.Data.History;
using PLECSYS_Studio.Data.Invoices;
using PLECSYS_Studio.Data.PaymentMethods;
using PLECSYS_Studio.Data.PaymentRecords;
using PLECSYS_Studio.Data.Products;
using PLECSYS_Studio.Data.SaleOrderDetails;
using PLECSYS_Studio.Data.SaleOrders;
using PLECSYS_Studio.Data.Users;
using PLECSYS_Studio.Handlers;
using PLECSYS_Studio.Handlers.GPS;
using PLECSYS_Studio.Services;
using PLECSYS_Studio.Services.Currencies;
using PLECSYS_Studio.Services.GPS;
using PLECSYS_Studio.Services.History;
using PLECSYS_Studio.Services.Invoices;
using PLECSYS_Studio.Services.InvoiceService;
using PLECSYS_Studio.Services.PaymentMethods;
using PLECSYS_Studio.Services.Payments;
using PLECSYS_Studio.Services.PaymentService;
using PLECSYS_Studio.Services.Products;
using PLECSYS_Studio.Services.SaleOrderDetails;
using PLECSYS_Studio.Services.SaleOrders;
using PLECSYS_Studio.Services.Users;
using PLECSYS_Studio.ViewModels;
using PLECSYS_Studio.ViewModels.GPS;
using PLECSYS_Studio.ViewModels.History;
using PLECSYS_Studio.ViewModels.Invoices;
using PLECSYS_Studio.ViewModels.Invoices.Filters;
using PLECSYS_Studio.ViewModels.Payments;
using PLECSYS_Studio.ViewModels.SaleOrders;
using PLECSYS_Studio.ViewModels.SaleOrders.Options;
using PLECSYS_Studio.ViewModels.SmartFlow;
using PLECSYS_Studio.Views.History;
using PLECSYS_Studio.Views.Payments;
using PLECSYS_Studio.Views.SmartFlow;

namespace PLECSYS_Studio
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiMaps()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // https context
            builder.Services.AddHttpClient();

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddHttpClient("PLECSYS", client =>
            {
                //client.BaseAddress = new Uri("https://mobiledev.plecsys-studio-bi.net/api/v1/plecsys/");
                client.BaseAddress = new Uri("https://10.0.2.2:7158/api/v1/plecsys/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
             {
                 ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
             });


            builder.Services.AddHttpClient("PLECSYS_API", client =>
            {
                client.BaseAddress = new Uri("https://10.0.2.2:7158/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });

            // Handlers de datos
            builder.Services.AddSingleton<InvoiceData>();
            builder.Services.AddSingleton<CompanyService>();
            builder.Services.AddSingleton<UserData>();
            builder.Services.AddSingleton<GPSData>();
            builder.Services.AddSingleton<ProductData>();
            builder.Services.AddSingleton<SaleOrderData>();
            builder.Services.AddSingleton<SaleOrderDetailData>();
            builder.Services.AddScoped<LocationData>();
            builder.Services.AddTransient<TrackingConfigHandler>();
            builder.Services.AddScoped<InvoiceHistoryData>();
            builder.Services.AddScoped<PaymentMethodData>();
            builder.Services.AddScoped<CurrencyData>();
            builder.Services.AddScoped<PaymentRecordData>();
            // Handlers de datos

            // handlers
            builder.Services.AddScoped<LoginHandler>();
            // handlers

            // Servicios
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<ISaleOrderService, SaleOrderService>();
            builder.Services.AddSingleton<ISaleOrderDetailService, SaleOrderDetailService>();
            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddSingleton<ITrackingConfigService, TrackingConfigService>();
            builder.Services.AddSingleton<LocationTrackingService>();
            builder.Services.AddSingleton<SessionService>();
            builder.Services.AddScoped<IInvoiceHistoryService, InvoiceHistoryService>();
            builder.Services.AddScoped<IInvoicePdfService, InvoicePdfService>();
            builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            builder.Services.AddScoped<ICurrencyService, CurrencyService>();
            builder.Services.AddScoped<IPaymentRecordService, PaymentRecordService>();
            // Servicios

            // ViewModels
            builder.Services.AddScoped<HomePageViewModel>();
            builder.Services.AddScoped<InvoicesViewModel>();
            builder.Services.AddScoped<ClientFilterViewModel>();
            builder.Services.AddScoped<DateFilterViewModel>();
            builder.Services.AddScoped<CurrencyFilterViewModel>();
            builder.Services.AddScoped<MapViewModel>();
            builder.Services.AddScoped<ShellViewModel>();
            builder.Services.AddScoped<SaleOrderViewModel>();
            builder.Services.AddScoped<SaleOrderDetailViewModel>();
            builder.Services.AddScoped<ProductViewModel>();
            builder.Services.AddScoped<ClientViewModel>();
            builder.Services.AddScoped<SignUpViewModel>();
            builder.Services.AddScoped<LocationMapViewModel>();
            builder.Services.AddTransient<TrackingConfigViewModel>();
            builder.Services.AddTransient<CompanySelectionViewModel>();
            builder.Services.AddTransient<SmartFlowPage>();
            builder.Services.AddTransient<SmartFlowViewModel>();
            builder.Services.AddScoped<InvoiceHistoryViewModel>();
            builder.Services.AddScoped<InvoiceHistoryPage>();
            builder.Services.AddScoped<RegisterPaymentViewModel>();
            builder.Services.AddScoped<RegisterPaymentPage>();
            // ViewModels

            // Shells
            builder.Services.AddSingleton<LoginShell>();
            builder.Services.AddSingleton<AppShell>();
            // Shells
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            // Rutas
            Routing.RegisterRoute(nameof(Views.Payments.RegisterPaymentPage), typeof(Views.Payments.RegisterPaymentPage));
            Routing.RegisterRoute(nameof(Views.Claims.RegisterClaimPage), typeof(Views.Claims.RegisterClaimPage));
            Routing.RegisterRoute(nameof(Views.History.InvoiceHistoryPage), typeof(Views.History.InvoiceHistoryPage));
            // Rutas

            return builder.Build();
        }
    }
}
