using Es013.Models;
using System;
using System.Collections.Generic;

namespace Es013.Stores.Interfaces
{
	internal interface IValutazioneStore
	{
		public bool Inserisci(Docente docente, Alunno alunno, DateTime? dataEOra = null, string? materia = null, string? classe = null, decimal? voto = null);
		public List<Valutazione> Ottieni();
		public Valutazione? Ottieni(uint id);
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
		public bool Cancella(uint id);
	}
}