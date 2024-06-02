using Es013.Models;
using Es013.Services.Interfaces;
using Es013.Stores;
using Es013.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Es013.Services
{
	public class AlunnoService : IAlunnoService
	{
		private readonly AlunnoStore _alunnoStore;
		private readonly ValutazioneService _valutazioneService;
		public AlunnoService(ValutazioneService valutazioneService)
		{
			_alunnoStore = new();
			_valutazioneService = valutazioneService;
		}
		public void Menu(Alunno alunno)
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

							List<ProiezioneValutazione>? mediaPerMateria = _valutazioneService.FiltraECalcolaMediaPerMateria(alunno, anno);
							StampaUtility.ListaProiezioneValutazioni(mediaPerMateria, anno);
							break;
						}
					//Media globale
					case 3:
						{
							Console.WriteLine("\nMedia complessiva");

							uint? anno = ImmissioneUtility.NumeroNaturale("anno");

							List<Valutazione>? valutazioniTrovate = _valutazioneService.Cerca(null, alunno, null, anno);

							if (valutazioniTrovate is null || valutazioniTrovate.Count < 1) break;

							decimal? mediaComplessiva = ValutazioneService.CalcolaMedia(valutazioniTrovate, alunno, anno);

							Console.WriteLine($"La media complessiva " + ((anno is not null) ? $"del {2024} è: " : "è: ") + mediaComplessiva);

							break;
						}
					default:
						break;
				}
			}
			while (!verificaNumeroNaturale || scelta != 0);
		}
		public List<Alunno>? Cerca(string? nome = null, string? cognome = null, string? codiceFiscale = null, uint? annoNascita = null, string? classe = null)
		{
			List<Alunno> alunni = _alunnoStore.Ottieni();
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
		public void RegistraAlunno()
		{
			string? nome, cognome, codiceFiscale, classe;
			DateOnly? dataNascita;

			nome = ImmissioneUtility.Stringa("nome");
			cognome = ImmissioneUtility.Stringa("cognome");
			codiceFiscale = ImmissioneUtility.Stringa("codice fiscale");
			dataNascita = ImmissioneUtility.Data();
			classe = ImmissioneUtility.Stringa("classe");

			_alunnoStore.Inserisci(nome, cognome, codiceFiscale, dataNascita, classe);
			Console.WriteLine("Dati anagrafici registrati");
		}
		public Alunno? OttieniAlunno(uint matricola)
		{
			Alunno? alunnoDaOttenere = _alunnoStore.Ottieni(matricola);
			if (alunnoDaOttenere is not null)
			{
				return alunnoDaOttenere;
			}
			else
			{
				return null;
			}
		}
		public Alunno? MenuRicercaAlunno(string propositoRicerca, bool restituzioneNecessaria = true)
		{
			string input;
			Alunno? alunnoOttenuto = null;

			Console.WriteLine($"\nCriteri di {propositoRicerca} dell'alunno\n\n1. Per matricola\t2. Altri criteri");
			input = Console.ReadLine() ?? "";
			uint.TryParse(input, out uint scelta);

			switch (scelta)
			{
				//Per matricola
				case 1:
					{
						Console.WriteLine($"\n{propositoRicerca} dell'alunno per matricola");
						uint? _matricola;
						uint matricola;

						_matricola = ImmissioneUtility.NumeroNaturale("matricola");
						if (_matricola is null) break;
						matricola = (uint)_matricola;

						alunnoOttenuto = _alunnoStore.Ottieni(matricola);
						StampaUtility.Alunno(alunnoOttenuto);

						break;
					}
				//Altri criteri
				case 2:
					{
						Console.WriteLine($"\n{propositoRicerca} dell'alunno secondo altri criteri");
						string? nome, cognome, codiceFiscale, classe;
						uint? annoNascita;


						nome = ImmissioneUtility.Stringa("nome");
						cognome = ImmissioneUtility.Stringa("cognome");
						codiceFiscale = ImmissioneUtility.Stringa("codice fiscale");
						annoNascita = ImmissioneUtility.NumeroNaturale("anno di nascita");
						classe = ImmissioneUtility.Stringa("classe");

						List<Alunno>? alunniTrovati = Cerca(nome, cognome, codiceFiscale, annoNascita, classe);

						StampaUtility.ListaAlunni(alunniTrovati);

						if (restituzioneNecessaria)
						{
							Console.WriteLine($"\nSelezionare uno degli alunni trovati ai fini della {propositoRicerca} dell'alunno");

							uint? _matricola;
							uint matricola;

							_matricola = ImmissioneUtility.NumeroNaturale("matricola");
							if (_matricola is null) break;
							matricola = (uint)_matricola;

							alunnoOttenuto = _alunnoStore.Ottieni(matricola);
							StampaUtility.Alunno(alunnoOttenuto);
						}
						break;
					}
				default:
					break;
			}

			return alunnoOttenuto;
		}
		public void MenuRegistroAnagraficoAlunni()
		{
			string input;
			bool verificaNumeroNaturale;
			uint scelta;

			do
			{
				Console.WriteLine("\nMenu docente -> Registro anagrafico alunni\n\n1. Inserimento\n2. Visualizzazione elenco degli alunni filtrato per anno e/o classe d'appartenenza\n3. Ricerca\n4. Aggiornamento\n5. Cancellazione\n\n0. Indietro");
				input = Console.ReadLine() ?? "";
				verificaNumeroNaturale = uint.TryParse(input, out scelta);

				switch (scelta)
				{
					//Registrazione alunno
					case 1:
						{
							RegistraAlunno();
							break;
						}
					//Visualizzazione elenco degli alunni
					case 2:
						{
							Console.WriteLine("Visualizzazione dei dati anagrafici degli alunni\nfiltrati per anno e/o classe d'appartenenza");

							uint? anno = ImmissioneUtility.NumeroNaturale("anno");
							string? classe = ImmissioneUtility.Stringa("classe");

							List<Alunno>? alunniFiltrati = Cerca(null, null, null, anno, classe);
							StampaUtility.ListaAlunni(alunniFiltrati);

							break;
						}
					//Ricerca alunno
					case 3:
						{
							Console.WriteLine("\nRicerca dei dati anagrafici di alunno");
							MenuRicercaAlunno("ricerca dei dati anagrafici", false);
							break;
						}
					//Aggiornamento alunno
					case 4:
						{
							Console.WriteLine("\nAggiornamento dati anagrafici di alunno");

							Alunno? alunnoDaAggiornare = MenuRicercaAlunno("modifica dei dati anagrafici");
							if (alunnoDaAggiornare is null) break;

							string? nome = ImmissioneUtility.Stringa("nome");
							string? cognome = ImmissioneUtility.Stringa("cognome");
							string? codiceFiscale = ImmissioneUtility.Stringa("codice fiscale");
							DateOnly? dataNascita = ImmissioneUtility.Data();
							string? classe = ImmissioneUtility.Stringa("classe");

							_alunnoStore.Aggiorna(alunnoDaAggiornare.Matricola, nome, cognome, codiceFiscale, dataNascita, classe);
							Console.WriteLine("Dati anagrafici dell'alunno selezionato aggiornati");
							break;
						}
					//Cancellazione alunno
					case 5:
						{
							Console.WriteLine("\nCancellazione dati anagrafici di alunno");

							Alunno? alunnoDaCancellare = MenuRicercaAlunno("cancellazione dei dati anagrafici");
							if (alunnoDaCancellare is null) break;

							_alunnoStore.Cancella(alunnoDaCancellare.Matricola);
							Console.WriteLine("Dati anagrafici dell'alunno selezionato cancellati");
							break;
						}
					default:
						break;
				}
			}
			while (!verificaNumeroNaturale || scelta != 0);
		}
		public void InserisciAlunno(string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? classe = null)
		{
			_alunnoStore.Inserisci(nome, cognome, codiceFiscale, dataNascita, classe);
			Console.WriteLine("Registrazione matricola alunno completata");
		}

	}
}