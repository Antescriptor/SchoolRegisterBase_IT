using Es011Base.Utils;
using Es011Base.Models;
using System;

namespace Es011Base.Services
{
	internal class RegistroService
	{
		private ValutazioneService _valutazioneService;
		private AlunnoService _alunnoService;
		private DocenteService _docenteService;
		internal RegistroService()
		{
			_valutazioneService = new ValutazioneService();
			_alunnoService = new AlunnoService(_valutazioneService);
			_docenteService = new(_alunnoService, _valutazioneService);
		}
		internal void Menu()
		{
			bool verificaNumeroNaturale;
			uint scelta;
			do
			{
				Console.WriteLine("Registro scolastico\n\nAccesso\n1. Docente\t2. Alunno\n\nRegistrazione\n3. Docente\t4. Matricola alunno\n\n0. Uscita");
				string input = Console.ReadLine() ?? "";
				verificaNumeroNaturale = uint.TryParse(input, out scelta);

				switch (scelta)
				{
					//Accesso: Docente
					case 1:
						{
							Console.WriteLine("Accesso docente");

							uint? _matricola = ImmissioneUtility.NumeroNaturale("matricola");
							uint matricola;
							if (_matricola is null) break;

							matricola = (uint)(_matricola);
							string password = ImmissioneUtility.Password(8);
							Docente? docente = _docenteService.Ottieni(matricola);
							if (docente is not null && docente.ControllaPassword(password))
							{
								_docenteService.Menu(docente);
							}
							else
							{
								Console.WriteLine("\nCredenziali errate.");
							}
						}
						break;
					//Accesso: Alunno
					case 2:
						{
							Console.WriteLine("Accesso alunno");
							uint? _matricola = ImmissioneUtility.NumeroNaturale("matricola");
							uint matricola;
							if (_matricola is null) break;

							matricola = (uint)(_matricola);
							string password = ImmissioneUtility.Password(8);
							Alunno? alunno = _alunnoService.Ottieni(matricola);
							if (alunno is not null && alunno.ControllaPassword(password))
							{
								_alunnoService.Menu(alunno);
							}
							else
							{
								Console.WriteLine("\nCredenziali errate.");
							}
						}
						break;
					//Registrazione: Docente
					case 3:
						{
							Console.WriteLine("Registrazione docente");

							string? nome, cognome, codiceFiscale, materia;
							DateOnly? dataNascita;

							nome = ImmissioneUtility.Stringa("nome");
							cognome = ImmissioneUtility.Stringa("cognome");
							codiceFiscale = ImmissioneUtility.Stringa("codice fiscale");
							dataNascita = ImmissioneUtility.Data();
							materia = ImmissioneUtility.Stringa("materia");

							_docenteService.Inserisci(nome, cognome, codiceFiscale, dataNascita, materia);

							Console.WriteLine("Registrazione completata");

							break;
						}
					//Registrazione: Matricola alunno
					case 4:
						{
							Console.WriteLine("Registrazione matricola alunno");

							/*
							string? nome, cognome, codiceFiscale, classe;
							DateOnly? dataNascita;

							nome = ImmissioneUtility.Stringa("nome");
							cognome = ImmissioneUtility.Stringa("cognome");
							codiceFiscale = ImmissioneUtility.Stringa("codice fiscale");
							dataNascita = ImmissioneUtility.Data();
							classe = ImmissioneUtility.Stringa("classe");
							*/

							_alunnoService.Inserisci();

							Console.WriteLine("Registrazione matricola alunno completata");

							break;
						}
					case 0:
					default:
						break;
				}
			}
			while (!verificaNumeroNaturale || scelta != 0);
		}
	}
}
