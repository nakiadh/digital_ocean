namespace tests;
using services;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        //Arrange
        Class1 cl = new Class1();

        //Act
        int z = cl.TheAdder(0, 1);

        //Assert
        Assert.Equal(z, 1);
    }
}