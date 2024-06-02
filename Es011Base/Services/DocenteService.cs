using System;
using System.Collections.Generic;
using Es013.Models;
using Es013.Services.Interfaces;
using Es013.Stores;
using Es013.Utils;

namespace Es013.Services
{
	public class DocenteService : IDocenteService
	{
		private readonly DocenteStore _docenteStore;
		private readonly AlunnoService _alunnoService;
		private readonly ValutazioneService _valutazioneService;

		public DocenteService(AlunnoService alunnoService, ValutazioneService valutazioneService)
		{
			_docenteStore = new();

			_valutazioneService = valutazioneService;
			_alunnoService = alunnoService;
		}
		public void Menu(Docente docente)
		{
			string input;
			bool verificaNumeroNaturale;
			uint scelta;

			do
			{
				Console.WriteLine("\nMenu docente\n\n1. Gestione del registro anagrafico degli alunni\n2. Gestione del registro delle valutazioni\n3. Visualizzazione del registro anagrafico dei docenti\n\n0. Disconnessione");
				input = Console.ReadLine() ?? "";
				verificaNumeroNaturale = uint.TryParse(input, out scelta);

				switch (scelta)
				{
					case 1:
						Console.WriteLine("\nRegistro anagrafico degli alunni");
						_alunnoService.MenuRegistroAnagraficoAlunni();
						break;
					case 2:
						Console.WriteLine("\nRegistro delle valutazioni");
						MenuRegistroValutazioni(docente);
						break;
					case 3:
						Console.WriteLine("\nVisualizzazione del registro anagrafico dei docenti");
						StampaUtility.ListaDocenti(_docenteStore.Ottieni());
						break;
					default:
						break;
				}

			}
			while (!verificaNumeroNaturale || scelta != 0);

		}
		public void MenuRegistroValutazioni(Docente docente)
		{
			string input;
			bool verificaNumeroNaturale;
			uint scelta;

			do
			{
				Console.WriteLine("\nMenu docente -> Registro valutazioni\n\n1. Inserimento\n2. Ricerca valutazioni di alunno\n3. Aggiornamento\n4. Cancellazione\n5. Visualizzazione valutazioni filtrate per anno, materia e/o classe\n6. Visualizzazione media di alunno\n\n0. Indietro");
				input = Console.ReadLine() ?? "";
				verificaNumeroNaturale = uint.TryParse(input, out scelta);


				switch (scelta)
				{
					//Inserimento valutazione
					case 1:
						{
							Console.WriteLine("\nInserimento valutazione");
							uint? _matricola;
							uint matricola;

							_matricola = ImmissioneUtility.NumeroNaturale("matricola");
							if (_matricola is null) return;
							matricola = (uint)_matricola;

							Alunno? alunnoOttenuto = _alunnoService.OttieniAlunno(matricola);
							if (alunnoOttenuto is null) return;
							_valutazioneService.InserisciValutazione(docente, alunnoOttenuto);

							break;
						}
					//Ricerca valutazione
					case 2:
						{
							Console.WriteLine("\nRicerca valutazioni di alunno");
							MenuRicercaValutazione(docente, "ricerca", false);
							break;
						}
					//Aggiornamento valutazione
					case 3:
						{
							Console.WriteLine("\nAggiornamento valutazione");
							Valutazione? valutazioneDaAggiornare = MenuRicercaValutazione(docente, "modifica");
							if (valutazioneDaAggiornare is null) break;

							Console.WriteLine("\nImmettere data e ora e/o voto da modificare");

							DateTime? dataEOra = ImmissioneUtility.DataEOra();
							Decimal? voto = ImmissioneUtility.NumeroRazionale("voto");

							ValutazioneStore.Aggiorna(valutazioneDaAggiornare, dataEOra, voto);
							Console.WriteLine("Valutazione aggiornata");

							break;
						}
					//Cancellazione valutazione
					case 4:
						{
							Console.WriteLine("\nCancellazione valutazione");
							Valutazione? valutazioneDaCancellare = MenuRicercaValutazione(docente, "cancellazione");
							if (valutazioneDaCancellare is null) break;

							_valutazioneService.CancellaValutazione(valutazioneDaCancellare.Id);
							Console.WriteLine("Valutazione cancellata");

							break;
						}
					//Visualizzazione valutazioni filtrate per anno, materia e/o classe
					case 5:
						{
							Console.WriteLine("\nVisualizzazione valutazioni filtrate per anno, materia e/o classe\nInserire dati di valutazione ai fini del filtro");

							uint? anno = ImmissioneUtility.NumeroNaturale("anno");
							string? materia = ImmissioneUtility.Stringa("materia");
							string? classe = ImmissioneUtility.Stringa("classe");

							List<Valutazione>? valutazioniFiltrate = _valutazioneService.FiltraPerDocenteAnnoMateriaClasse(docente, anno, materia, classe);

							Console.WriteLine("Valutazioni filtrate");
							StampaUtility.ListaValutazioni(valutazioniFiltrate);

							break;
						}
					//Visualizzazione media di alunno
					case 6:
						{
							Console.WriteLine("\nVisualizzazione della media delle valutazioni di un alunno");

							Alunno? alunnoOttenuto = MenuRicercaAlunno("ricerca");

							if (alunnoOttenuto is null) break;

							Console.WriteLine("\nCriteri di filtraggio delle valutazioni dell'alunno selezionato");

							DateOnly? data = ImmissioneUtility.Data();
							uint? anno = ImmissioneUtility.NumeroNaturale("anno");
							string? materia = ImmissioneUtility.Stringa("materia");
							string? classe = ImmissioneUtility.Stringa("classe");

							List<Valutazione>? valutazioniTrovate = _valutazioneService.Cerca(docente, alunnoOttenuto, data, anno, materia, classe);

							if (valutazioniTrovate is null || valutazioniTrovate.Count < 1) break;

							decimal? mediaAlunno = ValutazioneService.CalcolaMedia(valutazioniTrovate);
							Console.WriteLine($"La media delle valutazioni di {alunnoOttenuto.Nome} {alunnoOttenuto.Cognome} (matricola n. {alunnoOttenuto.Matricola})\nsecondo i criteri impostati è di {mediaAlunno}");
							break;
						}
					default:
						break;
				}
			}
			while (!verificaNumeroNaturale || scelta != 0);
		}
		public Valutazione? MenuRicercaValutazione(Docente docente, string propositoRicerca, bool restituzioneNecessaria = true)
		{
			string input;
			Valutazione? valutazioneOttenuta = null;
			Alunno? alunnoOttenuto;

			uint? _idValutazione;
			uint idValutazione;

			uint? _matricolaAlunno;
			uint matricolaAlunno;

			DateOnly? data;
			uint? anno;
			Decimal? voto;
			string? materia, classe;

				Console.WriteLine($"\nCriteri di {propositoRicerca} di valutazione\n\n1. Per Id\t2. Altri criteri");
				input = Console.ReadLine() ?? "";
				uint.TryParse(input, out uint scelta);

				switch (scelta)
				{
					//Ricerca per id
					case 1:
						Console.WriteLine($"\n{propositoRicerca} della valutazione per ID");
						_idValutazione = ImmissioneUtility.NumeroNaturale("id");
						if (_idValutazione is null) break;
						idValutazione = (uint)_idValutazione;

						valutazioneOttenuta = _valutazioneService.OttieniValutazione(idValutazione);
						StampaUtility.Valutazione(valutazioneOttenuta);

						break;
					// Ricerca per altri criteri
					case 2:
						Console.WriteLine($"\n{propositoRicerca} della valutazione secondo altri criteri.\nInserire dati alunno");

						_matricolaAlunno = ImmissioneUtility.NumeroNaturale("matricola");
						if (_matricolaAlunno is null) break;
						matricolaAlunno = (uint)_matricolaAlunno;

						alunnoOttenuto = _alunnoService.OttieniAlunno(matricolaAlunno);
						if (alunnoOttenuto is null) break;

						Console.WriteLine($"\nInserire dati valutazione per la {propositoRicerca}");

						data = ImmissioneUtility.Data();
						anno = ImmissioneUtility.NumeroNaturale("anno");
						materia = ImmissioneUtility.Stringa("materia");
						classe = ImmissioneUtility.Stringa("classe");
						voto = ImmissioneUtility.NumeroRazionale("voto");

						List<Valutazione>? valutazioniTrovate = _valutazioneService.Cerca(docente, alunnoOttenuto, data, anno, materia, classe, voto);
						StampaUtility.ListaValutazioni(valutazioniTrovate);

						if (restituzioneNecessaria)
						{
							Console.WriteLine($"\nSelezionare una delle valutazioni ottenute ai fini della {propositoRicerca}");

							_idValutazione = ImmissioneUtility.NumeroNaturale("id");
							if (_idValutazione is null) break;
							idValutazione = (uint)_idValutazione;

							valutazioneOttenuta = _valutazioneService.OttieniValutazione(idValutazione);
							StampaUtility.Valutazione(valutazioneOttenuta);
						}

						break;
					default:
						break;

				}
			return valutazioneOttenuta;
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

						alunnoOttenuto = _alunnoService.OttieniAlunno(matricola);
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

						List<Alunno>? alunniTrovati = _alunnoService.Cerca(nome, cognome, codiceFiscale, annoNascita, classe);

						StampaUtility.ListaAlunni(alunniTrovati);

						if (restituzioneNecessaria)
						{
							Console.WriteLine($"\nSelezionare uno degli alunni trovati ai fini della {propositoRicerca} dell'alunno");

							uint? _matricola;
							uint matricola;

							_matricola = ImmissioneUtility.NumeroNaturale("matricola");
							if (_matricola is null) break;
							matricola = (uint)_matricola;

							alunnoOttenuto = _alunnoService.OttieniAlunno(matricola);
							StampaUtility.Alunno(alunnoOttenuto);
						}
						break;
					}
				default:
					break;
			}

			return alunnoOttenuto;
		}
		public void MenuRegistroAnagraficoDocenti() //Da implementare
		{
			Console.WriteLine("Menu docente -> Registro anagrafico docenti\n1. ");
		}
		public void InserisciDocente(string? nome, string? cognome, string? codiceFiscale, DateOnly? dataNascita, string? materia)
		{
			_docenteStore.Inserisci(nome, cognome, codiceFiscale, dataNascita, materia);
			Console.WriteLine("Registrazione completata");
		}
		public Docente? OttieniDocente(uint matricola)
		{
			return _docenteStore.Ottieni(matricola);
		}
	}
}