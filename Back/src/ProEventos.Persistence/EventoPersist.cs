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
    public class EventoPersist : IEventoPersist
    {
        private readonly ProEventosContext _context;
        public EventoPersist(ProEventosContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
           IQueryable<Evento> eventos = _context.Evento.Include(e => e.Lotes).Include(e => e.RedesSociais);
           if (includePalestrantes)
           {
                eventos.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
           }

           eventos.OrderBy(e => e.EventoId);

           return await eventos.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> eventos = _context.Evento.Include(e => e.Lotes).Include(e => e.RedesSociais);
           if (includePalestrantes)
           {
                eventos.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
           }

           eventos.OrderBy(e => e.EventoId).Where(e => e.Tema.ToUpper().Contains(tema.ToUpper()));

           return await eventos.ToArrayAsync();
        }

        public async Task<Evento> GetEventosByIdAsyc(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> eventos = _context.Evento.Include(e => e.Lotes).Include(e => e.RedesSociais);
           if (includePalestrantes)
           {
                eventos.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
           }

           eventos.OrderBy(e => e.EventoId);

           return await eventos.FirstOrDefaultAsync(e => e.EventoId == eventoId);
        }
    }
}