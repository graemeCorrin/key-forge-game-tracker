
namespace KeyForgeGameTracker.Models
{
    public class DeckHouse
    {
        public int DeckId { get; set; }

        public Deck Deck { get; set; }
        public int HouseId { get; set; }

        public House House { get; set; }
    }
}
