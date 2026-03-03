namespace NascondendoUova;

class Anselmo
{
    Fabbrica fabbrica;
    public Anselmo(Fabbrica f)
    {
        fabbrica = f;
    }

    public async Task EseguiAnselmo(CancellationToken ct)
    {
        try
        {
            while (true)
            {
                Uovo? uovo_prec = fabbrica.prato?[^1];

                Uovo uovoPrelevato = await fabbrica.PrelevaUovo();

                if (uovo_prec == null ||
                    uovoPrelevato.HaColore(uovo_prec.colore1) ||
                    uovoPrelevato.HaColore(uovo_prec.colore2))
                {
                    //lo usa
                    fabbrica.prato.Add(uovoPrelevato);
                }
                else
                {
                    //lo scarta
                    uovoPrelevato.DaRicolorare = true;
                    await fabbrica.CorreggiUovoEAccoda(uovoPrelevato);
                }
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Anselmo termianto");
        }
    }
}
