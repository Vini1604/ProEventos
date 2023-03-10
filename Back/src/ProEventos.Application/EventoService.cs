using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Application.Contracts;
using ProEventos.Domain;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            _geralPersist = geralPersist;
            _eventoPersist = eventoPersist;
        }

        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _geralPersist.Add<Evento>(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventosByIdAsyc(model.EventoId, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Falha ao adicionar o elemento: " + ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventosByIdAsyc(eventoId, false);

                if(evento == null) throw new Exception ("Evento nao encontrado");

                _geralPersist.Delete<Evento>(evento);

                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao deletar o evento: " + ex.Message);
            }
            
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
                if(eventos == null) return null;

                return eventos;    
            }
            catch (Exception ex)
            {
                
                throw new Exception("Falha ao buscar todos os eventos: " + ex.Message);
            }
            
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);

                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Falha ao buscar eventos com o tema \"{tema}\": {ex.Message}");
            }
        }

        public async Task<Evento> GetEventosByIdAsyc(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersist.GetEventosByIdAsyc(eventoId, includePalestrantes);
            
                if(evento == null) return null;

                return evento;

            }
            catch (Exception ex)
            {
                
                throw new Exception($"Falha ao buscar um evento com o ID {eventoId}: {ex.Message}");
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersist.GetEventosByIdAsyc(eventoId, false);
                if(evento == null) return null;

                model.EventoId = evento.EventoId;

                _geralPersist.Update(model);

                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventosByIdAsyc(model.EventoId, false);
                }

                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Falha ao atualizar o evento: " + ex.Message);
            }
        }
    }
}