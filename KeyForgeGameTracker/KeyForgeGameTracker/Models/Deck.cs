using KeyForgeGameTracker.Areas.Identity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeyForgeGameTracker.Models
{
    public class Deck : KfgtTable
    {

        #region KeyForge Fields

        public Guid KeyForgeId { get; set; }
        public string Name { get; set; }
        public int Expansion { get; set; }
        public int PowerLevel { get; set; }
        public int Chains { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public string Notes { get; set; }

        #endregion KeyForge Fields


        #region GameTracker Fields

        public AppUser AppUser { get; set; }
        public string Alias { get; set; }
        public string MyNotes { get; set; }

        #endregion GameTracker Fields


        #region Foreign Keys

        public List<DeckHouse> DeckHouses { get; set; }
        public List<DeckCard> DeckCards { get; set; }

        #endregion Foreign Keys

        [NotMapped]
        public List<House> Houses { get; set; }

        [NotMapped]
        public List<Card> Cards { get; set; }
    }
}
