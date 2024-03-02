public class Multiply()
{
  public  float mul(float x, float y)
    {
        return x * y;
    }
}


class TheApp()
{
    static void Main()
    {
        Multiply m = new Multiply();
        Console.WriteLine(m.mul(3, 4));
    }
}