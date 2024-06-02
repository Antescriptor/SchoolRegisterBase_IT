using System;
using System.Collections.Generic;
using System.Linq;
using Es012Base.Models;

namespace Es012Base.Stores
{
	internal class AlunnoStore
	{
		private readonly List<Alunno> alunni = new();
		internal bool Inserisci(string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? classe = null)
		{
			Alunno nuovoAlunno = new(nome, cognome, codiceFiscale, dataNascita, classe);
			alunni?.Add(nuovoAlunno);
			return true;
		}
		internal List<Alunno> Ottieni()
		{
			return alunni;
		}
		internal Alunno? Ottieni(uint matricola)
		{
			return alunni?.FirstOrDefault(a => a.Matricola == matricola);
		}
		internal bool Aggiorna(uint matricolaAlunnoDaAggiornare, string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? classe = null)
		{
			Alunno? alunnoDaAggiornare = alunni.FirstOrDefault(a => a.Matricola == matricolaAlunnoDaAggiornare);
			if (alunnoDaAggiornare is not null)
			{
				if (nome is not null) alunnoDaAggiornare.Nome = nome;
				if (cognome is not null) alunnoDaAggiornare.Cognome = cognome;
				if (codiceFiscale is not null) alunnoDaAggiornare.CodiceFiscale = codiceFiscale;
				if (dataNascita is not null) alunnoDaAggiornare.DataNascita = dataNascita;
				if (classe is not null) alunnoDaAggiornare.Classe = classe;
			}
			return true;
		}
		internal bool Cancella(uint? matricolaAlunnoDaCancellare)
		{
			Alunno? alunnoDaCancellare = alunni.FirstOrDefault(a => a.Matricola == matricolaAlunnoDaCancellare);
			if (alunnoDaCancellare is not null)
			{
				alunni.Remove(alunnoDaCancellare);
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}