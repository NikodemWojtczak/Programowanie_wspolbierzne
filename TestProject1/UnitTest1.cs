namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Multiply m = new Multiply();

            Assert.AreEqual(m.mul(3,4),3*4);
        }
    }
}