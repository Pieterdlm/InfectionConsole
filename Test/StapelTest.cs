
public class Stapeltest{

    [Fact]
    public void legOpTest()
    {
        //arrange
        var testStapel = new Stapel<String>();
        string testBoek = "testBoek";
        string testBoek2 = "testBoek2";

        //act
        testStapel.LegOp(testBoek);
        testStapel.LegOp(testBoek2);

        //assert
        Assert.Equal(2, testStapel.getGrootteVanStapel());
    }

    [Fact]
    public void pakTest()
    {
        //arrange
        var testStapel = new Stapel<String>();
        string testBoek = "testBoek";
        string testBoek1 = "testBoek1";
        string testBoek2 = "testBoek2";

        //act
        testStapel.LegOp(testBoek);
        testStapel.LegOp(testBoek1);
        testStapel.LegOp(testBoek2);
        testStapel.pak();
        testStapel.pak();

        //assert
        Assert.Equal(1, testStapel.getGrootteVanStapel());
    }

    [Fact]
    public void pakGooitErrorBijLegeStapelTest(){

        //arrange
        var testStapel = new Stapel<String>();

        //act

        //assert
        Assert.Throws<InvalidOperationException>(() => testStapel.pak());
    }


    [Fact]
    public void bekijkTest(){

        //arrange
        var testStapel = new Stapel<String>();
        string testBoek = "testBoek";
        //act
        testStapel.LegOp(testBoek);
        //assert
        Assert.Equal(testBoek, testStapel.bekijk());
    }

    [Fact]
    public void bekijkGooitErrorBijLegeStapelTest(){

        //arrange
        var testStapel = new Stapel<String>();

        //act

        //assert
        Assert.Throws<InvalidOperationException>(() => testStapel.bekijk());
    }


}