using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WCFClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Web servisine bağlantı için binding ve endpoint oluşturuyoruz
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://192.168.100.5:9047/DTRKYOCERA/WS/Bilgitas/Codeunit/Integration");

            // WCF Proxy istemcisini oluşturuyoruz
            var client = new IntegrationService.IntegrationSoapClient(binding, endpoint);

            // Parametreleri belirliyoruz
            string _data = "03KYFS2020D";

            try
            {
                // Web servis metodunu çağırıyoruz
                var result = await client.TestGirisAsync("_pValue", _data);

                // Sonucu ekrana yazdırıyoruz
                Console.WriteLine("Response from NAV Web Service: ");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                // Hata durumunda exception'ı yakalayıp yazdırıyoruz
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Bağlantıyı kapatıyoruz
            client.Close();
        }
    }
}
