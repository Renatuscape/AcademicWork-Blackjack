namespace AW_Blackjack.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Card_EnableBlackJackRules_ReturnValue()
        {
            //Arrange
            Card cardQueen = new(Suit.Clubs, Value.Queen);
            cardQueen.blackJackRules = true;
            Card cardJack = new(Suit.Clubs, Value.Jack);
            cardJack.blackJackRules = true;
            int expected = 20;

            //Act
            int actual = cardQueen + cardJack;
            Console.WriteLine("The sum of both cards is " + actual);

            int sumActual = cardQueen + 10;
            Console.WriteLine("The sum of the Queen + 10 is " + actual);


            //Assert
            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(sumActual, Is.EqualTo(expected));
        }


        [Test]
        public void Card_DisableBlackJackRules_ReturnValue()
        {
            //Arrange
            Card cardQueen = new(Suit.Clubs, Value.Queen);
            Card cardJack = new(Suit.Clubs, Value.Jack);
            int expected = 23;

            //Act
            int actual = cardQueen + cardJack;
            Console.WriteLine("The sum of both cards is " + actual);

            int sumActual = cardQueen + 11;
            Console.WriteLine("The sum of the Queen + 11 is " + actual);


            //Assert
            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(sumActual, Is.EqualTo(expected));
        }
    }
}