using KeyForgeGameTracker.Models;
using System.Collections.Generic;

namespace KeyForgeGameTracker.ViewModels
{
    public class DeckIndexViewModel
    {
        public IEnumerable<Deck> Decks { get; set; }
        public IEnumerable<House> Houses { get; set; }
        public IEnumerable<Card> Cards { get; set; }
    }
}
