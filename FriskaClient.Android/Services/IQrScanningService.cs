using System.Threading.Tasks;
using ZXing.Mobile;
using Xamarin.Forms;
using FriskaClient.Services;
using Android.Nfc.Tech;

[assembly: Dependency(typeof(FriskaClient.Android.Services.QrScanningService))]

namespace FriskaClient.Android.Services
{
    public class QrScanningService : IQrScanningService
    {
        public async Task<string> ScanAsync()
        {
            var optionsDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Skanna QR Kod",
                BottomText = "",
            };

            var scanResult = await scanner.Scan(optionsCustom); if (scanResult != null) { return scanResult.Text; } else { return null; }
        }
    }
}