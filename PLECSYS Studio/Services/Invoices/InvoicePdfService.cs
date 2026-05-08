using Microsoft.Maui.Storage;
using PLECSYS_Studio.Data.Invoices;
using PLECSYS_Studio.Services;
using System.Net;

namespace PLECSYS_Studio.Services.InvoiceService
{
    public interface IInvoicePdfService
    {
        Task<string> DownloadInvoicePdfAsync(
            int consecutive,
            IProgress<double> progress,
            CancellationToken ct = default
         );
    }

    public class InvoicePdfService : IInvoicePdfService
    {
        private readonly InvoiceData _data;

        public InvoicePdfService(InvoiceData data)
        {
            _data = data;
        }

        public async Task<String> DownloadInvoicePdfAsync(
            int consecutive,
            IProgress<double> progress,
            CancellationToken ct = default
        )
        {
            var response = await _data.GetInvoicePdfAsync(consecutive, ct);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new HttpRequestException("PDF no encontrado(404).", null, HttpStatusCode.NotFound);
            }

            response.EnsureSuccessStatusCode();

            var total = response.Content.Headers.ContentLength ?? -1L;
            var canReport = total > 0 && progress is not null;

            var fileName = $"Invoice_{consecutive}.pdf";
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            await using var stream = await response.Content.ReadAsStreamAsync(ct);
            await using var fs = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None);

            var buffer = new byte[81920]; //80KB buffer
            long totalRead = 0;
            int read;

            progress?.Report(0);

            while ((read = await stream.ReadAsync(buffer.AsMemory(0, buffer.Length), ct)) > 0)
            {
                await fs.WriteAsync(buffer.AsMemory(0, read), ct);
                totalRead += read;

                if (canReport)
                {
                    double pct = (double)totalRead / total * 100;
                    progress?.Report(pct);
                }
            }

            progress?.Report(1);

            return filePath;
        }
    }
}