public class Spel{
    public Speelbord speelbord {get; set;}
    public Speler speler1;
    public Speler speler2;
    public Speler legeplek;
    public Speler huidigeSpeler;
    Stapel<Speelbord> stapelVanSpeelborden = new Stapel<Speelbord>();
    const int GROOTTEVANVELD = 7;


    public Spel(Speler speler1, Speler speler2){
        this.speelbord = new Speelbord();
        this.speler1 = speler1;
        this.speler2 = speler2;
        this.legeplek = new Speler("Legeplek", new Schaakstuk(" - "));
        this.huidigeSpeler = speler1;
    }


    public void speel(){
        bool spelKlaar = false;

        while (!spelKlaar){
            spelKlaar = true;
            toonScore();

            if (huidigeSpeler == speler1){
                huidigeSpeler = huidigeSpelerDoetZet(speler1);
            }
            else {
                huidigeSpeler = huidigeSpelerDoetZet(speler2);
            }
        }
    }
    

    public Speler huidigeSpelerDoetZet(Speler speler){
        var gemaakteZet = new Zet(0, 0, 0, 0);

        if (!(speler.GetType() == typeof(Computer))){
            Console.WriteLine( "* " + huidigeSpeler.naam + " Is aan de beurt *");
             gemaakteZet = speler.doeZet();
        }
        else {
            Computer computer = (Computer) speler;
            //Sleep voor 1 seconde om een delay er in te brengen
            Thread.Sleep(1000);
            gemaakteZet = computer.computerMaaktZet(this);
        }

        if (gemaakteZet == null){
                Console.WriteLine("Je hebt undo ingevoerd");

                Speelbord terugTeZettenSpeelbord = new Speelbord();

                if (stapelVanSpeelborden.getGrootteVanStapel() == 0){
                    Console.WriteLine("Je bent al bij de eerste zet ");
                    return huidigeSpeler;
                }

                for (int i  = 0; i < 2; i++){
                    terugTeZettenSpeelbord = stapelVanSpeelborden.pak();
                }

                

                for (int i = 0; i < GROOTTEVANVELD; i++){
                    for (int j = 0; j < GROOTTEVANVELD; j++)
                    {
                        speelbord.speelbord[i, j] = terugTeZettenSpeelbord.speelbord[i,j];
                    }
                }

                return huidigeSpeler;
        }

        Schaakstuk schaakstuk = speelbord.toonSchaakstukOpBord(gemaakteZet.vanRij, gemaakteZet.vanKolom);

        bool ligtNaastHuidigeZet = checkOpNaastGelegen(gemaakteZet);
        bool stapVan2 = checkOpStapVan2(gemaakteZet);

        if (schaakstuk != speler.schaakstuk){
            
            Console.WriteLine("Dit is niet jouw schaakstuk");
            return huidigeSpeler;
        }

        if (ligtNaastHuidigeZet){
            maakZetVan1Stap(speler.schaakstuk, gemaakteZet);
        }
        else if (stapVan2){
            maakZetVan2Stappen(speler.schaakstuk, gemaakteZet);
        }
        else if (!ligtNaastHuidigeZet && !stapVan2){
            Console.WriteLine("Stap mag niet verder dan 2 stappen zijn");
            huidigeSpelerDoetZet(speler);
        }
        


        if (speler == speler1){
                return speler2;
            }
            else {
                return speler1;
            }

    }

    public void maakZetVan1Stap(Schaakstuk schaakstuk, Zet gemaakteZet){
        voegBordToeAanStapel();
        speelbord.doeZetOpBord(schaakstuk , gemaakteZet);
        List<Zet> lijstVanNaastGelegenTegenstanders = speelbord.toonAlleSchaakstukkenNaastGemaakteZet(schaakstuk, gemaakteZet);
        
        foreach (Zet zet in lijstVanNaastGelegenTegenstanders){
            speelbord.doeZetOpBord(schaakstuk, zet);
        }
    }

    public void maakZetVan2Stappen(Schaakstuk schaakstuk, Zet gemaakteZet){
        voegBordToeAanStapel();
        speelbord.doeZetOpBord(schaakstuk, gemaakteZet);
        speelbord.resetOpBord(schaakstuk, gemaakteZet);
        List<Zet> lijstVanNaastGelegenTegenstanders = speelbord.toonAlleSchaakstukkenNaastGemaakteZet(schaakstuk, gemaakteZet);
        
        foreach (Zet zet in lijstVanNaastGelegenTegenstanders){
            speelbord.doeZetOpBord(schaakstuk, zet);
        }
    }

    public void voegBordToeAanStapel(){
            Speelbord vorigeSpeelbord =  new Speelbord();
            for (int i = 0; i < GROOTTEVANVELD; i++){
                for (int j = 0; j < GROOTTEVANVELD; j++)
                {
                    vorigeSpeelbord.speelbord[i, j ] = speelbord.speelbord[i,j];
                }
            }
            
            stapelVanSpeelborden.LegOp(vorigeSpeelbord);
            // Console.WriteLine("=========Stapel=========");
            // stapelVanSpeelborden.printStapel();
            // Console.WriteLine("=========Stapel=========");
        
    }

    public bool checkOpNaastGelegen(Zet zet){
        if ((zet.vanRij - zet.naarRij <= 1 && zet.vanKolom - zet.naarKolom <= 1) && (zet.naarRij - zet.vanRij <= 1 && zet.naarKolom - zet.vanKolom <= 1)){
            return true;
        }
        return false;
    }

    public bool checkOpStapVan2(Zet zet){
        if (zet.vanRij - zet.naarRij == 2 || zet.vanKolom - zet.naarKolom == 2 || zet.naarRij - zet.vanRij == 2 || zet.naarKolom - zet.vanKolom == 2){
            return true;
        }
        return false;
    }


    public void toonScore(){
            int scoreSpeler1 = 0;
            int scoreSpeler2 = 0;

            foreach (Schaakstuk schaakstuk in speelbord.speelbord){
                if (schaakstuk == speler1.schaakstuk){
                    scoreSpeler1++;
                }
                else if (schaakstuk == speler2.schaakstuk){
                    scoreSpeler2++;
                }
            }
            Console.WriteLine("==================================");
            Console.WriteLine("Score speler " + speler1.naam + " : " +  scoreSpeler1);
            Console.WriteLine("Score speler " + speler2.naam + " : " +  scoreSpeler2);
            Console.WriteLine("==================================");
    }

    public void toonWinnaar(){
        int scoreSpeler1 = 0;
        int scoreSpeler2 = 0;

        foreach (Schaakstuk schaakstuk in speelbord.speelbord){
            if (schaakstuk == speler1.schaakstuk){
                scoreSpeler1++;
            }
            else if (schaakstuk == speler2.schaakstuk){
                scoreSpeler2++;
            }
        }
        if (scoreSpeler1 > scoreSpeler2){
            Console.WriteLine("De winnaar is " + speler1.naam);
        }
        else if (scoreSpeler2 > scoreSpeler1){
            Console.WriteLine("De winnaar is " + speler2.naam);
        }
        else {
            Console.WriteLine("Het is gelijkspel");
        }
    }


    public void startPosities(){
        int[,] initieleStartPosities = {
            {0, 5}, {0, 6}, {1, 5}, {1, 6},{5, 0}, {5, 1}, {6, 0}, {6, 1}
        };

        Console.WriteLine(initieleStartPosities.Length);

        for (int i = 0; i < initieleStartPosities.Length / 2; i++){
            if (initieleStartPosities[i, 0] > 2) {
                
                speelbord.doeZetOpBord(speler2.schaakstuk, new Zet (0, 0, initieleStartPosities[i, 0], initieleStartPosities[i, 1]));
            }
            else {
                
                speelbord.doeZetOpBord(speler1.schaakstuk, new Zet (0, 0, initieleStartPosities[i, 0], initieleStartPosities[i, 1]));
            }
        }
    }

}