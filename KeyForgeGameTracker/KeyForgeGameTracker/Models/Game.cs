using KeyForgeGameTracker.Areas.Identity.Models;
using System;

namespace KeyForgeGameTracker.Models
{
    public class Game : KfgtTable
    {
        public DateTime GameDate { get; set; }
        public string Comments { get; set; }
        public bool Swap { get; set; }

        public AppUser WinningPlayer { get; set; }
        public AppUser LosingPlayer { get; set; }
        public Deck WinningDeck { get; set; }
        public Deck LosingDeck { get; set; }
    }
}
