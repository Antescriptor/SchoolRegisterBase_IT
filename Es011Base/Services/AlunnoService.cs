using Es011Base.Models;
using Es011Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Es011Base.Services
{
	internal class AlunnoService
	{
		private List<Alunno> alunni = new();
		private ValutazioneService _valutazioneService;
		internal AlunnoService(ValutazioneService valutazioneService)
		{
			_valutazioneService = valutazioneService;
		}
		internal void Inserisci(string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? classe = null)
		{
			Alunno nuovoAlunno = new(nome, cognome, codiceFiscale, dataNascita, classe);
			alunni?.Add(nuovoAlunno);
		}
		internal Alunno? Ottieni(uint matricola)
		{
			return alunni?.FirstOrDefault(a => a.Matricola == matricola);
		}
		internal void Aggiorna(Alunno? alunnoDaAggiornare, string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? classe = null)
		{
			if (alunnoDaAggiornare is not null)
			{
				if (nome is not null) alunnoDaAggiornare.Nome = nome;
				if (cognome is not null) alunnoDaAggiornare.Cognome = cognome;
				if (codiceFiscale is not null) alunnoDaAggiornare.CodiceFiscale = codiceFiscale;
				if (dataNascita is not null) alunnoDaAggiornare.DataNascita = dataNascita;
				if (classe is not null) alunnoDaAggiornare.Classe = classe;
			}
		}
		internal void Cancella(Alunno? alunnoDaCancellare)
		{
			if (alunnoDaCancellare is not null)
			{
				alunni.Remove(alunnoDaCancellare);
			}
		}
		internal List<Alunno>? Cerca(string? nome = null, string? cognome = null, string? codiceFiscale = null, uint? annoNascita = null, string? classe = null)
		{
			if (alunni is not null && alunni.Count > 0)
			{
				return alunni.Where
					(
						a =>
						((a.Nome is not null && nome is not null) ? a.Nome.ToLower().Trim() == nome.ToLower().Trim() : true) &&
						((a.Cognome is not null && cognome is not null) ? a.Cognome.ToLower().Trim() == cognome.ToLower().Trim() : true) &&
						((a.CodiceFiscale is not null && codiceFiscale is not null) ? a.CodiceFiscale.ToLower().Trim() == codiceFiscale.ToLower().Trim() : true) &&
						((a.DataNascita is not null && annoNascita is not null && annoNascita > 0 && annoNascita < 10000)
							? ((DateOnly)(a.DataNascita)).Year == annoNascita : true) &&
						((a.Classe is not null && classe is not null) ? a.Classe.ToLower().Trim() == classe.ToLower().Trim() : true)
					)?.ToList();

			}
			else
			{
				return null;
			}
		}
		internal void Menu(Alunno alunno)
		{
			string input;
			bool verificaNumeroNaturale;
			uint scelta;
			do
			{
				Console.WriteLine("\nMenu alunno\n\n1. Valutazioni\n2. Media per materia\n3. Media complessiva\n\n0. Disconnessione");
				input = Console.ReadLine() ?? "";
				verificaNumeroNaturale = uint.TryParse(input, out scelta);

				switch (scelta)
				{
					//Visualizzazione valutazioni
					case 1:
						{
							Console.WriteLine("\nImmettere dati di valutazione per la visualizzazione:");

							DateOnly? data = ImmissioneUtility.Data();
							uint? anno = ImmissioneUtility.NumeroNaturale("anno");
							string? materia = ImmissioneUtility.Stringa("materia");
							string? classe = ImmissioneUtility.Stringa("classe");
							decimal? voto = ImmissioneUtility.NumeroRazionale("voto");

							List<Valutazione>? valutazioniTrovate = _valutazioneService.Cerca(null, alunno, data, anno, materia, classe, voto);

							if (valutazioniTrovate is null || valutazioniTrovate.Count < 1) break;

							StampaUtility.ListaValutazioni(valutazioniTrovate);

							break;
						}
					//Media per materia
					case 2:
						{
							Console.WriteLine("\nMedia per materia");

							uint? anno = ImmissioneUtility.NumeroNaturale("anno");

							List<Valutazione>? valutazioniTrovate = _valutazioneService.Cerca(null, alunno, null, anno);
							if (valutazioniTrovate is null || valutazioniTrovate.Count < 1) break;

							List<ProiezioneValutazione>? mediaPerMateria = _valutazioneService.CalcolaMediaPerMateria(alunno, anno);
							StampaUtility.ListaProiezioneValutazioni(mediaPerMateria);
							break;
						}
					//Media globale
					case 3:
						{
							Console.WriteLine("\nMedia complessiva");

							uint? anno = ImmissioneUtility.NumeroNaturale("anno");

							List<Valutazione>? valutazioniTrovate = _valutazioneService.Cerca(null, alunno, null, anno);

							if (valutazioniTrovate is null || valutazioniTrovate.Count < 1) break;

							decimal? mediaComplessiva = _valutazioneService.CalcolaMedia(valutazioniTrovate, alunno, anno);

							Console.WriteLine($"La media complessiva " + ((anno is not null) ? $"del {2024} è: " : "è: ") + mediaComplessiva);

							break;
						}
					default:
						break;
				}
			}
			while (!verificaNumeroNaturale || scelta != 0);
		}
	}
}