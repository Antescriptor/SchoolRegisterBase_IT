using Es013.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Es013.Services.Interfaces
{
	public interface IValutazioneService
	{
		public static decimal? CalcolaMedia(List<Valutazione> valutazioni, Alunno? alunno = null, uint? anno = null)
		{
			if (valutazioni is not null && valutazioni.Count > 0)
			{
				return valutazioni
					.Where
					(
						v => v.Voto.HasValue &&
						((alunno is not null) ? v.Alunno == alunno : true) &&
						((anno is not null) ? v.DataEOra.Year == anno : true)
					)
					.Average(v => v.Voto.GetValueOrDefault());
			}
			else
			{
				return null;
			}
		}
		public List<Valutazione>? Cerca(Docente? docente = null, Alunno? alunno = null, DateOnly? data = null, uint? anno = null, string? materia = null, string? classe = null, decimal? voto = null);
		public List<Valutazione>? FiltraPerDocenteAnnoMateriaClasse(Docente docente, uint? anno = null, string? materia = null, string? classe = null);
		public List<ProiezioneValutazione>? FiltraECalcolaMediaPerMateria(Alunno? alunno = null, uint? anno = null);
		public void InserisciValutazione(Docente docente, Alunno alunno);
		public Valutazione? OttieniValutazione(uint id);
		public void CancellaValutazione(uint id);
	}
}
