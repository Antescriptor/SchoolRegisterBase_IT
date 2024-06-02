using System;
using System.Collections.Generic;
using System.Linq;
using Es013.Models;
using Es013.Stores.Interfaces;

namespace Es013.Stores
{
	public class DocenteStore : IDocenteStore
	{
		private readonly List<Docente> docenti = new();
		public bool Inserisci(string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? materia = null)
		{
			Docente nuovoDocente = new(nome, cognome, codiceFiscale, dataNascita, materia);
			docenti?.Add(nuovoDocente);
			return true;
		}
		public List<Docente>? Ottieni()
		{
			return docenti;
		}
		public Docente? Ottieni(uint matricola)
		{
			return docenti?.FirstOrDefault(d => d.Matricola == matricola);
		}
		public bool Aggiorna(uint matricolaDocenteDaAggiornare, string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? materia = null)
		{
			Docente? docenteDaAggiornare = docenti.FirstOrDefault(d => d.Matricola == matricolaDocenteDaAggiornare);
			if (docenteDaAggiornare is not null)
			{
				if (nome is not null) docenteDaAggiornare.Nome = nome;
				if (cognome is not null) docenteDaAggiornare.Cognome = cognome;
				if (codiceFiscale is not null) docenteDaAggiornare.CodiceFiscale = codiceFiscale;
				if (dataNascita is not null) docenteDaAggiornare.DataNascita = dataNascita;
				if (materia is not null) docenteDaAggiornare.Materia = materia;

				return true;
			}
			else
			{
				return false;
			}
		}
		public bool Cancella(uint matricolaDocenteDaCancellare)
		{
			Docente? docenteDaCancellare = docenti.FirstOrDefault(d => d.Matricola == matricolaDocenteDaCancellare);
			if (docenteDaCancellare is not null)
			{
				docenti.Remove(docenteDaCancellare);
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}