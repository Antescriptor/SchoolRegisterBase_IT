using Es013.Models;
using System;
using System.Collections.Generic;

namespace Es013.Utils
{
	public static class StampaUtility
	{
		internal static void Alunno(Alunno? alunno)
		{
			if (alunno is not null)
			{
				Console.WriteLine($"\nMatricola:\t\t{alunno.Matricola}\nNome:\t\t\t{alunno.Nome}\nCognome:\t\t{alunno.Cognome}\nCodice Fiscale:\t\t{alunno.CodiceFiscale}\nData di nascita:\t{alunno.DataNascita}\nClasse:\t\t\t{alunno.Classe}\n");
			}
			else
			{
				Console.WriteLine("Nessun alunno.");
			}
		}
		internal static void ListaAlunni(List<Alunno>? lista)
		{
/*			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(lista))
			{
				string name = descriptor.Name;
				object value = descriptor.GetValue(lista);
				Console.WriteLine($"{name}={value}");
			}*/
			
			if (lista is not null)
			{
				foreach(Alunno a in lista)
				{
					Console.WriteLine($"\nMatricola:\t\t{a.Matricola}\nNome:\t\t\t{a.Nome}\nCognome:\t\t{a.Cognome}\nCodice Fiscale:\t\t{a.CodiceFiscale}\nData di nascita:\t{a.DataNascita}\nClasse:\t\t\t{a.Classe}\n");
				}
			}
			else
			{
				Console.WriteLine("\nNessun alunno.");
			}
		}
		internal static void ListaDocenti(List<Docente>? lista)
		{
            if (lista is not null && lista.Count > 0)
			{
				foreach (Docente d in lista) 
				{
					Console.WriteLine($"\nMatricola:\t\t{d.Matricola}\nNome:\t\t\t{d.Nome}\nCognome:\t\t{d.Cognome}\nCodice Fiscale:\t\t{d.CodiceFiscale}\nData di nascita:\t{d.DataNascita}\nMateria:\t\t{d.Materia}\n");
				}

			}
			else
			{
				Console.WriteLine("\nNessun docente.");
			}
		}
		internal static void Valutazione(Valutazione? valutazione)
		{
			if (valutazione is not null)
			{
				Console.WriteLine($"\nId:\t\t{valutazione.Id}\nDocente:\t{valutazione.Docente.Matricola}\nAlunno:\t\t{valutazione.Alunno.Matricola}\nData e ora:\t{valutazione.DataEOra}\nMateria:\t{valutazione.Materia}\nClasse:\t\t{valutazione.Classe}\nVoto:\t\t{valutazione.Voto}\n");
			}
			else
			{
				Console.WriteLine("\nNessuna valutazione.");
			}
		}
		internal static void ListaValutazioni(List<Valutazione>? lista)
		{
            if (lista is not null && lista.Count > 0)
			{
				foreach (Valutazione v in lista)
				{
					Console.WriteLine($"\nId:\t\t{v.Id}\nDocente:\t{v.Docente.Matricola}\nAlunno:\t\t{v.Alunno.Matricola}\nData e ora:\t{v.DataEOra}\nMateria:\t{v.Materia}\nClasse:\t\t{v.Classe}\nVoto:\t\t{v.Voto}\n");
				}

			}
			else
			{
				Console.WriteLine("\nNessuna valutazione.");
			}
		}
		internal static void ListaProiezioneValutazioni(List<ProiezioneValutazione>? lista, uint? anno = null)
		{
			if (lista is not null && lista.Count > 0 && anno is not null)
			{
				foreach(ProiezioneValutazione p in lista)
				{
					Console.WriteLine($"\nAnno:\t\t\t{p.DataEOra.Year}\nMateria:\t\t\t{p.Materia}\nMedia delle valutazioni:\t{p.MediaVoto}");
				}
			}
			else if (lista is not null && lista.Count > 0)
			{
				foreach (ProiezioneValutazione p in lista)
				{
					Console.WriteLine($"\nMateria:\t\t\t{p.Materia}\nMedia delle valutazioni:\t{p.MediaVoto}");
				}
			}
			else
			{
				Console.WriteLine("Nessuna proiezione delle valutazioni.");
			}
		}
	}
}