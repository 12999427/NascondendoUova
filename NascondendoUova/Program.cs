using MyStackLib;
using MyQueueLib;
using System.Threading.Tasks;

namespace NascondendoUova;

class Program
{
    static async Task Main(string[] args)
    {
        Fabbrica fabbrica = new(11);
        Anselmo anselmo = fabbrica.anselmo;
        CancellationTokenSource cts = new();
        Task task_fabbrica = fabbrica.EseguiConigliProduttori(cts.Token);
        Task task_anselmo = anselmo.EseguiAnselmo(cts.Token);

        await task_anselmo;
        cts.Cancel();
        await task_fabbrica;
    }
}
