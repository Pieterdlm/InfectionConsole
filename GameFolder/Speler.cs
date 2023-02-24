
public class Speler{

    public string naam;
    public Schaakstuk schaakstuk {get; set;}

    public Speler(string naam, Schaakstuk schaakstuk){
        this.naam = naam;
        this.schaakstuk = schaakstuk;
    }

    

    public virtual Zet doeZet(){
        
        string teMakenZet = Console.ReadLine();
        string[] coordinaten = teMakenZet.Split(',');

        if (teMakenZet == "undo"){
            return null;
        }

        if (coordinaten.Length != 4){
            Console.WriteLine("Voer een geldige zet in");
            return doeZet();
        }

        int vanRij = int.Parse(coordinaten[0]);
        int vanKolom = int.Parse(coordinaten[1]);
        int naarRij = int.Parse(coordinaten[2]);
        int naarKolom = int.Parse(coordinaten[3]);

        if (valideerZet(vanRij -1, vanKolom -1, naarRij -1, naarKolom -1) == false ){
            return doeZet();
        }
        else {
            return (new Zet(vanRij -1, vanKolom -1, naarRij -1, naarKolom -1));
        }
    }

    public bool valideerZet (int vanRij, int vanKolom, int naarRij, int naarKolom){
        if (vanRij > 6 || vanKolom > 6 || naarRij > 6 || naarKolom > 6){
            Console.WriteLine("Deze zet is niet mogelijk");
            return false;
        }
        if (vanRij < 0 || vanKolom < 0 || naarRij < 0 || naarKolom < 0){
            Console.WriteLine("Deze zet is niet mogelijk");
            return false;
        }
        if (vanRij == naarRij && vanKolom == naarKolom){
            Console.WriteLine("Deze zet is niet mogelijk");
            return false;
        }
        return true;
    }
}
