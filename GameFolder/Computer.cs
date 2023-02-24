public class Computer : Speler
{
    public Computer(string naam, Schaakstuk schaakstuk) : base(naam, schaakstuk)
    {
        this.naam = naam;
        this.schaakstuk = schaakstuk;
    }

    //Vooruit denken / Backtracking?
    public Zet computerMaaktZet(Spel spel){
        //print het spelbord zoals de computer het nu ziet
        //Console.WriteLine(spel.speelbord);
        
        //huidige posities ophalen van bord
        var huidigePosities = mijnHuidigePosities(spel);

        //Alle mogelijke zetten ophalen
        var alleMogelijkeZetten = checkAlleMogelijkeZetten(spel, huidigePosities);
        
        //De beste zet uitzoeken
        var besteZet = maakBesteZet(alleMogelijkeZetten, spel);

        //Random zet maken
        // var teMakenZet = maakRandomZet(alleMogelijkeZetten);
        // return teMakenZet;

        if (besteZet == null){
            var teMakenZet = maakRandomZet(alleMogelijkeZetten);
            return teMakenZet;
        }
        else {
            return besteZet;
        }
        
    }

    public Zet maakBesteZet(List<Zet> alleMogelijkeZetten, Spel spel){
        //check of er een zet is zodat de computer kan winnen.

        //Check voor de zet die de meeste van de tegenstander infecteert
        var lijstMetTeInfecteren = zettenDieInfecteren(alleMogelijkeZetten, spel);
        
        int hoogsteTeller = 0;
        Zet besteZet = null;

        foreach (Zet zet in lijstMetTeInfecteren){
            int kolomTeChecken = zet.naarKolom - 1;
            int rijTeChecken = zet.naarRij -1;
            int teller = 0;

            //Console.WriteLine("Deze zet infecteert de tegenstander: " + zet);
            //Check welke zet de meeste van de tegenstander infecteert
            for (int i = rijTeChecken ; i <  (rijTeChecken + 3); i++){
                for (int j = kolomTeChecken; j < (kolomTeChecken + 3); j++){
                    if ((i >= 0) && (i <= 6) && (j >= 0) && (j <= 6)){
                        if (spel.speelbord.speelbord[i,j] == spel.speler1.schaakstuk){
                            teller++;
                        }
                    }
                }
            }
            //Console.WriteLine("Deze zet infecteert de tegenstander: " + teller + " keer");

            if (teller > hoogsteTeller){
                hoogsteTeller = teller;
                besteZet = zet;
            }

        }
            //Console.WriteLine("Deze zet infecteert de tegenstander het meest: " + besteZet);

        return besteZet;
    }

    public List<Zet> zettenDieInfecteren(List<Zet> alleMogelijkeZetten, Spel spel){
        List<Zet> lijstMetBesteZetten = new List<Zet>();

        foreach (Zet zet in alleMogelijkeZetten){
            
            int kolomTeChecken = zet.naarKolom - 1;
            int rijTeChecken = zet.naarRij -1;


            for (int i = rijTeChecken; i < (rijTeChecken + 3); i++){
                for (int j = kolomTeChecken; j <(kolomTeChecken + 3); j++){
                    if ((i >= 0) && (i <= 6) && (j >= 0) && (j <= 6)){
                        if (spel.speelbord.speelbord[i,j] != schaakstuk && spel.speelbord.speelbord[i,j] != spel.legeplek.schaakstuk){
                            lijstMetBesteZetten.Add(zet);
                        }
                    }
                }
            }
        }
        return lijstMetBesteZetten;
    }


    public Zet maakRandomZet(List<Zet> alleMogelijkeZetten){
        var random = new Random();
        var randomIndex = random.Next(alleMogelijkeZetten.Count);
        var teMakenZet = alleMogelijkeZetten[randomIndex];

        return teMakenZet;
    }

    public List<Zet> checkAlleMogelijkeZetten(Spel spel, List<Zet> huidigePosities){
        List<Zet> lijstVanZetten = new List<Zet>();
        foreach (Zet zet in huidigePosities){
            //Console.WriteLine("Dit is een van mijn posities : " + zet);

            int kolomTeChecken = zet.vanKolom - 1;
            int rijTeChecken = zet.vanRij -1;

            for (int i = rijTeChecken; i < (rijTeChecken + 3); i++){
                for (int j = kolomTeChecken; j <(kolomTeChecken + 3); j++){
                    if ((i >= 0) && (i <= 6) && (j >= 0) && (j <= 6)){
                        if (spel.speelbord.speelbord[i,j] != schaakstuk){
                            lijstVanZetten.Add(new Zet(zet.vanRij, zet.vanKolom, i, j));
                        }
                    }
                }
            }

            for (int i = rijTeChecken; i < (rijTeChecken + 4); i++){
                for (int j = kolomTeChecken; j <(kolomTeChecken + 4); j++){
                    if ((i >= 0) && (i <= 6) && (j >= 0) && (j <= 6)){
                        if (spel.speelbord.speelbord[i,j] != schaakstuk){
                            lijstVanZetten.Add(new Zet(zet.vanRij, zet.vanKolom, i, j));
                        }
                    }
                }
            }
        }

        //print voor duidelijkheid
        foreach (Zet zet in lijstVanZetten){
            //Console.WriteLine("Dit is een van de mogelijke zetten : " + zet);
        }

        return lijstVanZetten;
    }


    public List<Zet> mijnHuidigePosities(Spel spel){
        List<Zet> positiesVanMijnSchaakstukken = new List<Zet>();

        for (int i = 0; i < spel.speelbord.speelbord.GetLength(0); i++){
            for (int j = 0; j < spel.speelbord.speelbord.GetLength(1); j++){
                if (schaakstuk == spel.speelbord.speelbord[i, j]){
                    positiesVanMijnSchaakstukken.Add(new Zet(i, j, 0, 0));
                }
            }
        }

        return positiesVanMijnSchaakstukken;
    }



}



// foreach Schaakstuk dat hetzelfde is als het schaakstuk van de computer bekijken of er een leeg vakje naast ligt of naar toe kan springen
// Bekijk alleen de schaakstukken waar dit voor geldt
// Als er een schaakstuk is dat dit kan, kijk of er een schaakstuk van de tegenstander naast ligt of naar toe kan springen
// Tel hoeveel stukken er overgenomen kunnnen worden
// Kies het schaakstuk met de meeste stukken die overgenomen kunnen worden

// voor elke mogelijke zet kijken of er een schaakstuk van de tegenstander naast ligt.
// Als dit zo is, tel hoeveel er overgenomen kunnen worden.
// Kies de zet met de meeste stukken die overgenomen kunnen worden.