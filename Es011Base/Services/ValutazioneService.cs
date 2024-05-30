using Es011Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Es011Base.Services
{
	internal class ValutazioneService
	{
		private List <Valutazione> valutazioni = new();

		internal void Inserisci(Docente docente, Alunno alunno, DateTime? dataEOra = null, string? materia = null, string? classe = null, decimal? voto = null)
		{
			Valutazione valutazioneDaAggiungere = new(docente, alunno, dataEOra, materia, classe, voto);
			valutazioni.Add(valutazioneDaAggiungere);
		}
		internal Valutazione? Ottieni(uint idValutazione)
		{
			return valutazioni.FirstOrDefault(v => v.Id == idValutazione);
		}
		internal static void Aggiorna(Valutazione? valutazioneDaAggiornare, DateTime? data = null, decimal? voto = null)
		{
			if (valutazioneDaAggiornare is not null)
			{
				if (voto is not null) valutazioneDaAggiornare.Voto = voto;
				if (data is not null) valutazioneDaAggiornare.DataEOra = (DateTime)(data);
			}
		}
		internal void Cancella(Valutazione? valutazioneDaCancellare)
		{
			if (valutazioneDaCancellare is not null)
			{
				valutazioni.Remove(valutazioneDaCancellare);
			}
		}
		internal List<Valutazione>? Cerca(Docente? docente = null, Alunno? alunno = null, DateOnly? data = null, uint? anno = null, string? materia = null, string? classe = null, decimal? voto = null)
		{
			if (valutazioni is not null && valutazioni.Count > 0)
			{
				return valutazioni
					.Where
					(
						v =>
						((docente is not null) ? v.Docente == docente : true) &&
						((alunno is not null) ? v.Alunno == alunno : true) &&
						((data is not null) ? DateOnly.FromDateTime(v.DataEOra) == data : true) &&
						((anno is not null && anno > 0 && anno < 10000) ? (v.DataEOra).Year == anno : true) &&
						((v.Materia is not null && materia is not null) ? v.Materia.ToLower().Trim() == materia.ToLower().Trim() : true) &&
						((v.Classe is not null && classe is not null) ? v.Classe.ToLower().Trim() == classe.ToLower().Trim() : true) &&
						((v.Voto is not null && voto is not null) ? Math.Round((decimal)v.Voto) == Math.Round((decimal)voto) : true)
					)
					.OrderBy(v => v.DataEOra)
					.ThenBy(v => v.Materia)
					?.ToList();
			}
			else
			{
				return null;
			}
		}
		internal List<Valutazione>? FiltraPerDocenteAnnoMateriaClasse(Docente docente, uint? anno = null, string? materia = null, string? classe = null)
		{
			return valutazioni
					.Where(v =>
						(v.Docente == docente) &&
						((anno is not null) ? v.DataEOra.Year == anno : true) &&
						((materia is not null && v.Materia is not null) ? v.Materia.ToLower().Trim() == materia.ToLower().Trim() : true) &&
						((classe is not null && v.Classe is not null) ? v.Classe.ToLower().Trim() == classe.ToLower().Trim() : true))
					.OrderBy(v => v.DataEOra)
					.ThenBy(v => v.Classe).ToList();
		}
		internal List<ProiezioneValutazione>? CalcolaMediaPerMateria(Alunno? alunno = null, uint? anno = null)
		{
			if (valutazioni is not null && valutazioni.Count > 0)
			{
				/* Tipo di restituzione decimale
				return valutazioni
					.Where
					(
						v =>
						(anno is not null ? v.DataEOra.Year == anno : true) &&
						v.Voto.HasValue
					)
					.OrderByDescending(v => v.DataEOra)
					.GroupBy(v => v.Materia)
					.Select(gruppo => gruppo.Average(v => v.Voto.GetValueOrDefault()))
					?.ToList();
				*/

				return valutazioni
					.Where(v =>
						(alunno is not null ? alunno == v.Alunno : true) &&
						(anno is not null ? v.DataEOra.Year == anno : true) &&
						v.Materia is not null &&
						v.Voto.HasValue)
					.OrderByDescending(v => v.DataEOra)
					.GroupBy(v => v.Materia)
					.Select(gruppo => new ProiezioneValutazione(
						gruppo.First().DataEOra,
						gruppo.Key,
						gruppo.Average(v => v.Voto.GetValueOrDefault())))
					?.ToList();
			}
			else
			{
				return null;
			}
		}
		internal decimal? CalcolaMedia(List<Valutazione> valutazioni, Alunno? alunno = null, uint? anno = null)
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
	}
}