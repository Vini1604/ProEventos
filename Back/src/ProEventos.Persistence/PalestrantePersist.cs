using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexts;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext _context;
        public PalestrantePersist(ProEventosContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> palestrantes = _context.Palestrante.Include(p => p.RedesSociais);
           if (includeEventos)
           {
                palestrantes.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
           }

           palestrantes.OrderBy(p => p.Id);

           return await palestrantes.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> palestrantes = _context.Palestrante.Include(p => p.RedesSociais);
           if (includeEventos)
           {
                palestrantes.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
           }

           palestrantes.OrderBy(p => p.Id).Where(p => p.Nome.ToUpper().Contains(nome.ToUpper()));

           return await palestrantes.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestrantesByIdAsyc(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> palestrantes = _context.Palestrante.Include(p => p.RedesSociais);
           if (includeEventos)
           {
                palestrantes.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
           }

           palestrantes.OrderBy(p => p.Id);

           return await palestrantes.FirstOrDefaultAsync(p => p.Id == palestranteId);
        }

    }
}