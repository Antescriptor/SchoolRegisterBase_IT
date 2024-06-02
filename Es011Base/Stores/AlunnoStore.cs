using System;
using System.Collections.Generic;
using System.Linq;
using Es013.Models;
using Es013.Stores.Interfaces;

namespace Es013.Stores
{
	public class AlunnoStore : IAlunnoStore
	{
		private readonly List<Alunno> alunni = new();
		public bool Inserisci(string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? classe = null)
		{
			Alunno nuovoAlunno = new(nome, cognome, codiceFiscale, dataNascita, classe);
			alunni?.Add(nuovoAlunno);
			return true;
		}
		public List<Alunno> Ottieni()
		{
			return alunni;
		}
		public Alunno? Ottieni(uint matricola)
		{
			return alunni?.FirstOrDefault(a => a.Matricola == matricola);
		}
		public bool Aggiorna(uint matricolaAlunnoDaAggiornare, string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? classe = null)
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
		public bool Cancella(uint? matricolaAlunnoDaCancellare)
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