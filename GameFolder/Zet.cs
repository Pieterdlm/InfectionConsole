
public class Zet{
    public int vanRij {get; set;}
    public int vanKolom {get; set;}
    public int naarRij {get; set;}
    public int naarKolom {get; set;}

    public Zet(int vanRij, int vanKolom, int naarRij, int naarKolom){
        this.vanRij = vanRij;
        this.vanKolom = vanKolom;
        this.naarRij = naarRij;
        this.naarKolom = naarKolom;
    }

    public override string ToString(){
        return vanRij + "," + vanKolom + "," + naarRij + "," + naarKolom;
    }
}
    
    