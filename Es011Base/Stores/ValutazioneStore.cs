using Es013.Models;
using Es013.Stores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Es013.Stores
{
	public class ValutazioneStore : IValutazioneStore
	{
		private readonly List<Valutazione> valutazioni = new();
		public bool Inserisci(Docente docente, Alunno alunno, DateTime? dataEOra = null, string? materia = null, string? classe = null, decimal? voto = null)
		{
			Valutazione valutazioneDaAggiungere = new(docente, alunno, dataEOra, materia, classe, voto);
			valutazioni.Add(valutazioneDaAggiungere);
			return true;
		}
		public List<Valutazione> Ottieni()
		{
			return valutazioni;
		}
		public Valutazione? Ottieni(uint id)
		{
			return valutazioni.FirstOrDefault(v => v.Id == id);
		}
		public static bool Aggiorna(Valutazione? valutazioneDaAggiornare, DateTime? data = null, decimal? voto = null)
		{
			if (valutazioneDaAggiornare is not null)
			{
				if (voto is not null) valutazioneDaAggiornare.Voto = voto;
				if (data is not null) valutazioneDaAggiornare.DataEOra = (DateTime)(data);
				return true;
			}
			else
			{
				return false;
			}
		}
		public bool Cancella(uint id)
		{
			Valutazione? valutazioneDaCancellare = valutazioni.FirstOrDefault(v => v.Id == id);
			if (valutazioneDaCancellare is not null)
			{
				valutazioni.Remove(valutazioneDaCancellare);
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}