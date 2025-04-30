using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel;
using System.ServiceModel.Channels;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new NAVServer();
        // var result = await client.FindItemSerialAsync("WD74867558", "B0000013", "BAŞAK");
        // var result = await client.FindItemSerialAsync("H563Y89595", "B0000023", "BÜROTEKNİK");
        var result = await client.InsertWebBasket(DateTime.Now, "BAŞAK", "04TAS2554CI", 9, "NAVServer test açıklamasıdır.");
        Console.WriteLine(result.success + " " + result.errorMessage);

    }
}
