using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeyForgeGameTracker.Models
{
    public class House : KfgtTable
    {
        public string KeyForgeId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public List<DeckHouse> DeckHouses { get; set; }
        
        [NotMapped]
        public List<Deck> Decks { get; set; }
    }
}
