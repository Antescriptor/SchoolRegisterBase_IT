using System;
using System.Collections.Generic;
using System.Linq;
using Es012Base.Models;

namespace Es012Base.Stores
{
	internal class DocenteStore
	{
		private readonly List<Docente> docenti = new();
		internal bool Inserisci(string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? materia = null)
		{
			Docente nuovoDocente = new(nome, cognome, codiceFiscale, dataNascita, materia);
			docenti?.Add(nuovoDocente);
			return true;
		}
		internal List<Docente>? Ottieni()
		{
			return docenti;
		}
		internal Docente? Ottieni(uint matricola)
		{
			return docenti?.FirstOrDefault(d => d.Matricola == matricola);
		}
		internal bool Aggiorna(uint matricolaDocenteDaAggiornare, string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? materia = null)
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
		internal bool Cancella(uint matricolaDocenteDaCancellare)
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