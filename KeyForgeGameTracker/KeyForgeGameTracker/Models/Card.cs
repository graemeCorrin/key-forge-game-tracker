using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeyForgeGameTracker.Models
{
    public class Card : KfgtTable
    {
        [Required]
        public Guid KeyForgeId { get; set; }
        public string Title { get; set; }
        public House House { get; set; }
        public string CardType { get; set; }
        public string FrontImage { get; set; }
        public string CardText { get; set; }
        public string Traits { get; set; }
        public int? Amber { get; set; }
        public string Power { get; set; }
        public string Armor { get; set; }
        public string Rarity { get; set; }
        public string FlavorText { get; set; }
        public string CardNumber { get; set; }
        public int? Expansion { get; set; }
        public bool IsMaverick { get; set; }
        public bool IsAnomaly { get; set; }


        public List<DeckCard> DeckCards { get; set; }
        
        [NotMapped]
        public List<Deck> Decks { get; set; }

    }
}
