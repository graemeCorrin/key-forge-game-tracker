﻿using KeyForgeGameTracker.Areas.Identity.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeyForgeGameTracker.Models
{
    public class Game : KfgtTable
    {
        [Required]
        [Display(Name = "Game Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime GameDate { get; set; }

        public string Comments { get; set; }

        public bool Swap { get; set; }


        [Required]
        [Display(Name = "Winning Player")]
        public int? WinningPlayerId { get; set; }

        [ForeignKey("WinningPlayerId")]
        public AppUser WinningPlayer { get; set; }


        [Required]
        [Display(Name = "Losing Player")]
        public int? LosingPlayerId { get; set; }

        [ForeignKey("LosingPlayerId")]
        public AppUser LosingPlayer { get; set; }


        [Required]
        [Display(Name = "Winning Deck")]
        public int? WinningDeckId { get; set; }

        [ForeignKey("WinningDeckId")]
        public Deck WinningDeck { get; set; }


        [Required]
        [Display(Name = "Losing Deck")]
        public int? LosingDeckId { get; set; }

        [ForeignKey("LosingDeckId")]
        public Deck LosingDeck { get; set; }

        
    }
}
