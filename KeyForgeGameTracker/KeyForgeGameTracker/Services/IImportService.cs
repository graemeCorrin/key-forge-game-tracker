using KeyForgeGameTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyForgeGameTracker.Services
{
    public interface IImportService
    {
        Task ImportDeckAsync(Deck deck);
    }
}
