public class Schaakstuk{
    public string naam  { get; set; }

    public Schaakstuk(string naam){
        this.naam = naam;
    }

    public override string ToString(){
        return naam;
    }
}