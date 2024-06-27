namespace LibLesson;

public class DeckOfCards
{
    public static void Main()
    {
        Deck deck = new Deck();

        Console.WriteLine("Full deck:");
        deck.DisplayDeck();

        Player player = new Player();

        player.DrawCards(deck, 4);
        Console.WriteLine("\nAfter 4 cards:");
        player.DisplayHand();

        player.DrawCards(deck, 2);
        Console.WriteLine("\nAfter more 2 cards:");
        player.DisplayHand();

        player.DrawCards(deck, 2);
        Console.WriteLine("\nTry to take one more 2 cards (there are 6 cards):");
        player.DisplayHand();
    }
}

public enum Suit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}

public enum Rank
{
    Six = 6,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

public class Card
{
    public Suit Suit { get; set; }
    public Rank Rank { get; set; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override string ToString()
    {
        return $"{Suit}:{Rank}";
    }
}

public class Deck
{
    private Card[] cards;
    private int currentCardIndex;

    public Deck()
    {
        cards = new Card[36];
        CreateDeck();
        ShuffleDeck();
        currentCardIndex = 0;
    }

    private void CreateDeck()
    {
        int index = 0;
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            for (int rank = 6; rank <= 14; rank++)
            {
                cards[index++] = new Card(suit, (Rank)rank);
            }
        }
    }

    public void ShuffleDeck()
    {
        Random random = new Random();
        for (int i = cards.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }

    public void DisplayDeck()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            Console.WriteLine(cards[i]);
        }
    }

    public Card DrawCard()
    {
        if (currentCardIndex < cards.Length)
        {
            return cards[currentCardIndex++];
        }

        return null;
    }
}

public class Player
{
    private Card[] hand;
    private int handCount;

    public Player()
    {
        hand = new Card[12]; 
        handCount = 0;
    }

    public void DrawCards(Deck deck, int numberOfCards)
    {
        if (handCount < 6)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                if (handCount < hand.Length)
                {
                    Card card = deck.DrawCard();
                    if (card != null)
                    {
                        hand[handCount++] = card;
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Player already has 6 or more cards and cannot draw more.");
        }
    }

    public void DisplayHand()
    {
        for (int i = 0; i < handCount; i++)
        {
            Console.WriteLine(hand[i]);
        }
    }
}