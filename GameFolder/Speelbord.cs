public class Speelbord{

    public Schaakstuk[,] speelbord {get; set;}
    const int GROOTTEVANVELD = 7;

    public Speelbord(){
        this.speelbord = new Schaakstuk[7, 7];

        for (int i = 0; i < GROOTTEVANVELD; i++){
            for (int j = 0; j < GROOTTEVANVELD; j++){
                speelbord[i, j] = new Schaakstuk (" - ");
            }
        }
    }


    public void toonBord(Spel spel){
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.Write("    ");
        for (int x = 1; x < GROOTTEVANVELD + 1; x++){
            Console.Write("  " + x + " ");
        }
        Console.Write(" ");
        Console.ResetColor();
        Console.WriteLine();

        for (int i = 0; i < GROOTTEVANVELD; i++){
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write("   " + (i + 1) + " ");
            Console.ResetColor();
            for (int j = 0; j < GROOTTEVANVELD; j++){
                if (speelbord[i, j] == spel.speler1.schaakstuk){
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                }
                else if (speelbord[i, j] == spel.speler2.schaakstuk){
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                }
                else if (speelbord[i, j].naam == " - "){
                    Console.BackgroundColor = ConsoleColor.Black;

                }
                Console.Write(speelbord[i, j] + " ");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }


    public void doeZetOpBord (Schaakstuk schaakstuk, Zet zet){
        Schaakstuk gemaakteZet = speelbord[zet.naarRij, zet.naarKolom];
        speelbord[zet.naarRij, zet.naarKolom] = schaakstuk;
    }

    public void resetOpBord(Schaakstuk schaakstuk, Zet zet){
        speelbord[zet.vanRij, zet.vanKolom] = new Schaakstuk(" - ");
    }
    

    public Schaakstuk toonSchaakstukOpBord(int rij, int kolom){
        Schaakstuk schaakstuk = speelbord[rij, kolom];
        return schaakstuk;
    }

    public List<Zet> toonAlleSchaakstukkenNaastGemaakteZet(Schaakstuk schaakstuk, Zet zet){
        List<Zet> lijstVanZetten = new List<Zet>();
        int kolomTeChecken = zet.naarKolom - 1;
        int rijTeChecken = zet.naarRij -1;
        
        for (int i = rijTeChecken; i < (rijTeChecken + 3); i++){
            for (int j = kolomTeChecken; j <(kolomTeChecken + 3); j++){
                if ((i >= 0) && (i < GROOTTEVANVELD) && (j >= 0) && (j < GROOTTEVANVELD)){
                    if (speelbord[i,j] != schaakstuk && speelbord[i,j].naam != (" - ") ){
                        lijstVanZetten.Add(new Zet(0, 0, i, j));
                    }
                }
            }
        }    

        return lijstVanZetten;
    }


    public bool geenZettenMeer(Spel spel){
        int x = 0;
        int y = 0;
        int z = 0;
        for (int i = 0; i < GROOTTEVANVELD; i++){
            for (int j = 0; j < GROOTTEVANVELD; j++){
                if (speelbord[i, j].naam == " - "){
                   x++;
                }
                else if (speelbord[i, j] == spel.speler1.schaakstuk){
                   y++;
                }
                else if (speelbord[i, j] == spel.speler2.schaakstuk){
                   z++;
                }
            }
        }

        if (x == 0 || y == 0 || z == 0){
            return true;
        }
        else {
            return false;
        }
    }

    public override string ToString(){
        string s = "";
        for (int i = 0; i < GROOTTEVANVELD; i++){
            for (int j = 0; j < GROOTTEVANVELD; j++){
                s += this.speelbord[i, j].naam;
            }
            s += "\r \n";
        }
        return s; 
    }
}

