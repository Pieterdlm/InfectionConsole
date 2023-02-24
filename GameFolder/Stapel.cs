public class Stapel <B>{
    private int grootteVanStapel;
    private Boek<B> bovensteBoek;
    

    public Stapel (){
        grootteVanStapel = 0;
        bovensteBoek = null;
    }


        private class Boek<B>{
            public B dataVanGeneric;
            public Boek<B> volgendeBoek;

            public Boek(B dataVanGeneric, Boek<B> volgendeBoek){
                this.dataVanGeneric = dataVanGeneric;
                this.volgendeBoek = volgendeBoek;
            }
        }

    public int getGrootteVanStapel(){
        return grootteVanStapel;
    }    


    //leg bovenop\
    public void LegOp(B Boek){
        
        Boek<B> boek = new Boek<B>(Boek, bovensteBoek);
        bovensteBoek = boek;
        grootteVanStapel += 1;
        // Console.WriteLine("Boek is bovenop gelegd");
    }
    
    //pak bovenste
    public B pak(){
        if (grootteVanStapel == 0){
            throw new InvalidOperationException("Je probeert de bovenste te pakken maar de Stapel is leeg");
        }

        B boek = bovensteBoek.dataVanGeneric;
        bovensteBoek = bovensteBoek.volgendeBoek;

        grootteVanStapel -= 1;
        // Console.WriteLine("Het bovenste element wordt gepakt");
        return boek;
    }

    //bekijk bovenste
    public B bekijk (){
        if (grootteVanStapel == 0){
            throw new InvalidOperationException("Je probeert de bovenste te bekijken maar de Stapel is leeg");
        }
        
            B boek = bovensteBoek.dataVanGeneric;
            return boek;
    }

    public void printStapel() {
        if (grootteVanStapel == 0) {
            Console.WriteLine("De stapel is leeg.");
            return;
        }
        Console.WriteLine("Elementen in de stapel:");

        Boek<B> currentBoek = bovensteBoek;
        while (currentBoek != null) {
            Console.WriteLine(currentBoek.dataVanGeneric);
            currentBoek = currentBoek.volgendeBoek;
        }
    }

}