using System;
using System.Collections.Generic;
using System.Linq;
using Es011Base.Models;
using Es011Base.Utils;

namespace Es011Base.Services
{
	internal class DocenteService
	{
		private List<Docente> docenti = new();
		private AlunnoService _alunnoService;
		private ValutazioneService _valutazioneService;
		internal DocenteService(AlunnoService alunnoService, ValutazioneService valutazioneService)
		{
			_alunnoService = alunnoService;
			_valutazioneService = valutazioneService;
		}
		internal void Inserisci(string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? materia = null)
		{
			Docente nuovoDocente = new(nome, cognome, codiceFiscale, dataNascita, materia);
			docenti?.Add(nuovoDocente);
		}
		internal Docente? Ottieni(uint matricola)
		{
			return docenti?.FirstOrDefault(d => d.Matricola == matricola);
		}
		internal void Aggiorna(Docente docenteDaAggiornare, string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? materia = null) 
		{
			if (docenteDaAggiornare is not null)
			{
				if (nome is not null) docenteDaAggiornare.Nome = nome;
				if (cognome is not null) docenteDaAggiornare.Cognome = cognome;
				if (codiceFiscale is not null) docenteDaAggiornare.CodiceFiscale = codiceFiscale;
				if (dataNascita is not null) docenteDaAggiornare.DataNascita = dataNascita;
				if (materia is not null) docenteDaAggiornare.Materia = materia;
			}
		}
		internal void Cancella(Docente? docenteDaCancellare)
		{
			if (docenteDaCancellare is not null)
			{
				docenti.Remove(docenteDaCancellare);
			}
		}
		internal void Menu(Docente docente)
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
						MenuRegistroAnagrafico();
						break;
					case 2:
						Console.WriteLine("\nRegistro delle valutazioni");
						MenuRegistroValutazioni(docente);
						break;
					case 3:
						Console.WriteLine("\nVisualizzazione del registro anagrafico dei docenti");
						StampaUtility.ListaDocenti(docenti);
						break;
					default:
						break;
				}

			}
			while (!verificaNumeroNaturale || scelta != 0);

		}
		internal void MenuRegistroAnagrafico()
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
					//Inserimento alunno
					case 1:
						{
							Console.WriteLine("\nInserimento dati anagrafici di alunno");

							string? nome, cognome, codiceFiscale, classe;
							DateOnly? dataNascita;

							nome = ImmissioneUtility.Stringa("nome");
							cognome = ImmissioneUtility.Stringa("cognome");
							codiceFiscale = ImmissioneUtility.Stringa("codice fiscale");
							dataNascita = ImmissioneUtility.Data();
							classe = ImmissioneUtility.Stringa("classe");

							_alunnoService.Inserisci(nome, cognome, codiceFiscale, dataNascita, classe);

							Console.WriteLine("Dati anagrafici registrati");

							break;
						}
					//Visualizzazione elenco degli alunni
					case 2:
						{
							Console.WriteLine("Visualizzazione dei dati anagrafici degli alunni\nfiltrati per anno e/o classe d'appartenenza");

							uint? anno = ImmissioneUtility.NumeroNaturale("anno");
							string? classe = ImmissioneUtility.Stringa("classe");

							List<Alunno>? alunniFiltrati = _alunnoService.Cerca(null, null, null, anno, classe);
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

							_alunnoService.Aggiorna(alunnoDaAggiornare, nome, cognome, codiceFiscale, dataNascita, classe);
							Console.WriteLine("Dati anagrafici dell'alunno selezionato aggiornati");
							break;
						}
					//Cancellazione alunno
					case 5:
						{
							Console.WriteLine("\nCancellazione dati anagrafici di alunno");

							Alunno? alunnoDaCancellare = MenuRicercaAlunno("cancellazione dei dati anagrafici");
							if (alunnoDaCancellare is null) break;

							_alunnoService.Cancella(alunnoDaCancellare);
							Console.WriteLine("Dati anagrafici dell'alunno selezionato cancellati");
							break;
						}
					default:
						break;
				}
			}
			while (!verificaNumeroNaturale || scelta != 0);
		}
		internal void MenuRegistroValutazioni(Docente docente)
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
							if (_matricola is null) break;
							matricola = (uint)_matricola;

							Alunno? alunnoOttenuto = _alunnoService.Ottieni(matricola);
							if (alunnoOttenuto is null) break;

							DateTime? dataEOra = ImmissioneUtility.DataEOra();

							string? materia = docente.Materia;
							if (docente.Materia is null)
							{
								materia = ImmissioneUtility.Stringa("materia");
							}

							string? classe = alunnoOttenuto.Classe;
							if (alunnoOttenuto.Classe is null)
							{
								classe = ImmissioneUtility.Stringa("classe");
							}

							decimal? voto = ImmissioneUtility.NumeroRazionale("voto");

							_valutazioneService.Inserisci(docente, alunnoOttenuto, dataEOra, materia, classe, voto);

							Console.WriteLine("Valutazione inserita");

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

							ValutazioneService.Aggiorna(valutazioneDaAggiornare, dataEOra, voto);
							Console.WriteLine("Valutazione aggiornata");

							break;
						}
					//Cancellazione valutazione
					case 4:
						{
							Console.WriteLine("\nCancellazione valutazione");
							Valutazione? valutazioneDaCancellare = MenuRicercaValutazione(docente, "cancellazione");
							if (valutazioneDaCancellare is null) break;

							_valutazioneService.Cancella(valutazioneDaCancellare);
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

							decimal? mediaAlunno = _valutazioneService.CalcolaMedia(valutazioniTrovate);
							Console.WriteLine($"La media delle valutazioni di {alunnoOttenuto.Nome} {alunnoOttenuto.Cognome} (n. di matricola: {alunnoOttenuto.Matricola})\nsecondo i criteri impostati è: {mediaAlunno}");
							break;
						}
					default:
						break;
				}
			}
			while (!verificaNumeroNaturale || scelta != 0);
		}
		internal Valutazione? MenuRicercaValutazione(Docente docente, string propositoRicerca, bool restituzioneNecessaria = true)
		{
			string input;
			uint scelta;
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
				uint.TryParse(input, out scelta);

				switch (scelta)
				{
					//Ricerca per id
					case 1:
						Console.WriteLine($"\n{propositoRicerca} della valutazione per ID");
						_idValutazione = ImmissioneUtility.NumeroNaturale("id");
						if (_idValutazione is null) break;
						idValutazione = (uint)_idValutazione;

						valutazioneOttenuta = _valutazioneService.Ottieni(idValutazione);
						StampaUtility.Valutazione(valutazioneOttenuta);

						break;
					// Ricerca per altri criteri
					case 2:
						Console.WriteLine($"\n{propositoRicerca} della valutazione secondo altri criteri.\nInserire dati alunno");

						_matricolaAlunno = ImmissioneUtility.NumeroNaturale("matricola");
						if (_matricolaAlunno is null) break;
						matricolaAlunno = (uint)_matricolaAlunno;

						alunnoOttenuto = _alunnoService.Ottieni(matricolaAlunno);
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

							valutazioneOttenuta = _valutazioneService.Ottieni(idValutazione);
							StampaUtility.Valutazione(valutazioneOttenuta);
						}

						break;
					default:
						break;

				}
			return valutazioneOttenuta;
		}
		internal Alunno? MenuRicercaAlunno(string propositoRicerca, bool restituzioneNecessaria = true)
		{
			string input;
			uint scelta;
			Alunno? alunnoOttenuto = null;

			Console.WriteLine($"\nCriteri di {propositoRicerca} dell'alunno\n\n1. Per matricola\t2. Altri criteri");
			input = Console.ReadLine() ?? "";
			uint.TryParse(input, out scelta);

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

						alunnoOttenuto = _alunnoService.Ottieni(matricola);
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

							alunnoOttenuto = _alunnoService.Ottieni(matricola);
							StampaUtility.Alunno(alunnoOttenuto);
						}
						break;
					}
				default:
					break;
			}

			return alunnoOttenuto;
		}
	}
}