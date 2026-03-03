using System.Drawing;
using MyQueueLib;

namespace NascondendoUova;

public enum Colore
{
    Verde, Magenta, Azzurro, Giallo, Arancione, Rosa, Viola
};

class Uovo
{
    private static int Cont;
    public int Id {get; private set;}
    public Colore colore1 {get; private set;}
    public Colore colore2 {get; private set;}
    public bool DaRicolorare;
    public Uovo(Colore c1, Colore c2)
    {
        colore1 = c1;
        colore2 = c2;
        DaRicolorare = false;
        Id = Cont++;
    }

    public Uovo()
    {
        colore1 = RandomColore();
        colore2 = RandomColore();
        DaRicolorare = false;
        Id = Cont++;
    }

    private Colore RandomColore()
    {
        return (Colore) Random.Shared.Next(0, Enum.GetValues(typeof(Colore)).Length);
    }

    public bool HaColore(Colore c)
    {
        if (colore1 == c || colore2 == c)
            return true;
        
        return false;
    }

    public void CambiaColoreDiversiDaPrec()
    {
        Colore c1 = colore1;
        Colore c2 = colore2;

        do 
        {
            colore1 = RandomColore();
            colore2 = RandomColore();
        } while (HaColore(c1) || HaColore(c2));
    }

    public override string ToString()
    {
        return $"[Uovo {Id} con colori: {colore1.ToString()} {colore2.ToString()}]";
    }
}
