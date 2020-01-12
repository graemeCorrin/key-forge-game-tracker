﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KeyForgeGameTracker.Models;
using KeyForgeGameTracker.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using KeyForgeGameTracker.Services;

namespace KeyForgeGameTracker.Controllers
{
    public class DecksController : Controller
    {
        private readonly KeyForgeContext _context;
        private readonly IImportService _importService;

        public DecksController(KeyForgeContext context, IImportService importService)
        {
            _context = context;
            _importService = importService;
        }

        // GET: Decks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Deck.ToListAsync());
        }

        // GET: Decks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deck = await _context.Deck
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deck == null)
            {
                return NotFound();
            }

            return View(deck);
        }

        // GET: Decks/Import
        public IActionResult Import()
        {
            return View();
        }

        // POST: Decks/Import
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import([Bind("Id,KeyForgeId,Alias,MyNotes")] Deck deck)
        {
            if (ModelState.IsValid)
            {

                var existing = _context.Deck.Where(d => d.KeyForgeId == deck.KeyForgeId).Any();
                if (existing)
                {
                    return View(deck);
                }
                else
                {
                    _context.Add(deck);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(deck);
        }

        // GET: Decks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deck = await _context.Deck.FindAsync(id);
            if (deck == null)
            {
                return NotFound();
            }
            return View(deck);
        }

        // POST: Decks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KeyForgeId,Name,Expansion,PowerLevel,Chains,Wins,Loses,Notes,Alias,MyNotes,Id,CreatedDate,UpdatedDate,UpdatedBy,CreatedBy")] Deck deck)
        {
            if (id != deck.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deck);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeckExists(deck.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(deck);
        }

        // GET: Decks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deck = await _context.Deck
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deck == null)
            {
                return NotFound();
            }

            return View(deck);
        }

        // POST: Decks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deck = await _context.Deck.FindAsync(id);
            _context.Deck.Remove(deck);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeckExists(int id)
        {
            return _context.Deck.Any(e => e.Id == id);
        }

        //private int ParseInt(string s)
        //{
        //    Int32.TryParse(s)
        //}
    }
}