using Es013.Models;
using Es013.Services.Interfaces;
using Es013.Stores;
using Es013.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Es013.Services
{
	public class ValutazioneService : IValutazioneService
	{
		private readonly ValutazioneStore _valutazioneStore;
		public ValutazioneService(ValutazioneStore valutazioneStore)
		{
			_valutazioneStore = valutazioneStore;
		}
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
		public List<Valutazione>? Cerca(Docente? docente = null, Alunno? alunno = null, DateOnly? data = null, uint? anno = null, string? materia = null, string? classe = null, decimal? voto = null)
		{
			List<Valutazione> valutazioni = _valutazioneStore.Ottieni();
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
		public List<Valutazione>? FiltraPerDocenteAnnoMateriaClasse(Docente docente, uint? anno = null, string? materia = null, string? classe = null)
		{
			List<Valutazione> valutazioni = _valutazioneStore.Ottieni();
			if (valutazioni.Count > 0)
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
			else
			{
				return null;
			}
		}
		public List<ProiezioneValutazione>? FiltraECalcolaMediaPerMateria(Alunno? alunno = null, uint? anno = null)
		{
			List<Valutazione> valutazioni = _valutazioneStore.Ottieni();
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
			if (valutazioni.Count > 0)
			{
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
		public void InserisciValutazione(Docente docente, Alunno alunno)
		{
			DateTime? dataEOra = ImmissioneUtility.DataEOra();

			string? materia = docente.Materia;
			if (docente.Materia is null)
			{
				materia = ImmissioneUtility.Stringa("materia");
			}

			string? classe = alunno.Classe;
			if (alunno.Classe is null)
			{
				classe = ImmissioneUtility.Stringa("classe");
			}

			decimal? voto = ImmissioneUtility.NumeroRazionale("voto");
			_valutazioneStore.Inserisci(docente, alunno, dataEOra, materia, classe, voto);
		}
		public Valutazione? OttieniValutazione(uint id)
		{
			return _valutazioneStore.Ottieni(id);
		}
		public void CancellaValutazione(uint id)
		{
			_valutazioneStore.Cancella(id);
		}
	}
}