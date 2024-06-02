using Es012Base.Utils;
using Es012Base.Models;
using System;

namespace Es012Base.Services
{
	public class RegistroService
	{
		private DocenteService _docenteService;
		private AlunnoService _alunnoService;
		public RegistroService(DocenteService docenteService, AlunnoService alunnoService)
		{
			_docenteService = docenteService;
			_alunnoService = alunnoService;
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
							Docente? docente = _docenteService.OttieniDocente(matricola);
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
							Alunno? alunno = _alunnoService.OttieniAlunno(matricola);
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

							_docenteService.InserisciDocente(nome, cognome, codiceFiscale, dataNascita, materia);

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

							_alunnoService.InserisciAlunno();

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