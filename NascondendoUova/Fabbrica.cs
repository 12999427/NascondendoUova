using MyQueueLib;

namespace NascondendoUova;

class Fabbrica
{
    private MyQueue<Uovo> codaUova;
    public Anselmo anselmo {get; private set;}
    private SemaphoreSlim M_codaUova;
    private SemaphoreSlim S_codaUova;
    public List<Uovo> prato {get; private set;}
    public Fabbrica()
    {
        codaUova = new();
        M_codaUova = new(1, 1);
        S_codaUova = new(0);
        prato = new();
        anselmo = new(this);
    }

    public async Task<Uovo> PrelevaUovo()
    {
        await S_codaUova.WaitAsync();
        await M_codaUova.WaitAsync();
        Uovo u = codaUova.Dequeue();
        M_codaUova.Release();
        return u;
    }

    public async Task AggiungiUovo(Uovo u)
    {
        await M_codaUova.WaitAsync();
        codaUova.Enqueue(u);
        M_codaUova.Release();
        S_codaUova.Release();
    }

    public async Task CorreggiUovoEAccoda(Uovo u)
    {
        u.CambiaColoreDiversiDaPrec();
        u.DaRicolorare = false;

        await AggiungiUovo(u);
    }

    public async Task EseguiConigliProduttori(CancellationToken ct)
    {
        try
        {
            while (true)
            {
                await Task.Delay(1000);
                Uovo uovoCostruito = new();
                await AggiungiUovo(uovoCostruito);
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Processo fabbrica cancellato");
        }
    }
}
