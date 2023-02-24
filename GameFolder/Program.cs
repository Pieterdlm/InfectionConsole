
// Stapel<string> stapel = new Stapel<string>();

// stapel.LegOp("Steen der Wijzen");
// stapel.LegOp("Oorlogswinter");
// stapel.LegOp("Het Diner");


// Console.WriteLine("Het bovenste boek = " + stapel.bekijk());
// stapel.pak();
// Console.WriteLine("Het bovenste boek = " + stapel.bekijk());

// Speelbord speelbord = new Speelbord();

// speelbord.toonBeginBord();





// Spel spel = new Spel(new Speler("BaggySweater", new Schaakstuk(" B ")),
//                      new Speler("Hoodie", new Schaakstuk(" H ")));


Spel spel = new Spel(new Speler("BaggySweater", new Schaakstuk(" B ")),
                     new Computer("Hoodie(Computer)", new Schaakstuk(" H ")));

Boolean spelTenEinde = false;
    spel.startPosities();
    Console.WriteLine("*********************************************");
    Console.WriteLine("          Geef de zet op als volgt:");
    Console.WriteLine("Vanaf rij, vanaf kolom, naar rij, naar kolom");
    Console.WriteLine("             Voorbeeld: 5,1,5,2");
    Console.WriteLine("Typ undo om de laatste zet ongedaan te maken");
    Console.WriteLine("**********************************************");



while (!spelTenEinde){

    Console.WriteLine("==================================");
    spel.speelbord.toonBord(spel);
    spel.speel();

    if (spel.speelbord.geenZettenMeer(spel)){
        spel.speelbord.toonBord(spel);
        spel.toonScore();
        spel.toonWinnaar();
        spelTenEinde = true;
    }
}


// Spel is ten einde als:
// 1. er geen schaakstukken meer van een speler op het bord liggen
// 2. alle vakken op het bord zijn bezet
// 3. er geen zetten meer gedaan kunnen worden
