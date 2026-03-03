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
                Uovo? uovo_prec = fabbrica.prato.Count == 0 ? null : fabbrica.prato[^1];

                Uovo uovoPrelevato = await fabbrica.PrelevaUovo();

                if (uovo_prec == null ||
                    uovoPrelevato.HaColore(uovo_prec.colore1) ||
                    uovoPrelevato.HaColore(uovo_prec.colore2))
                {
                    Console.WriteLine($"Anselmo ha preso un {uovoPrelevato} giusto e lo aggiunge al prato (precedente: {uovo_prec}). Lunghezza prato: " + (fabbrica.prato.Count+1));
                    //lo usa
                    fabbrica.prato.Add(uovoPrelevato);

                    if (fabbrica.prato.Count >= fabbrica.LimiteUovaProdotte)
                    {
                        break;
                    }
                }
                else
                {
                    //lo scarta
                    Console.WriteLine($"Anselmo fa cooreggere {uovoPrelevato}, che è sbagliato (precedente: {uovo_prec}). Lunghezza prato: " + fabbrica.prato.Count);
                    uovoPrelevato.DaRicolorare = true;
                    _ = fabbrica.CorreggiUovoEAccoda(uovoPrelevato);
                }
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Anselmo termianto");
        }
    }
}
