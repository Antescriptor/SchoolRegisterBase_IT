using Es013.Models;
using System;
using System.Collections.Generic;

namespace Es013.Stores.Interfaces
{
	internal interface IAlunnoStore
	{
		public bool Inserisci(string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? classe = null);
		public List<Alunno> Ottieni();
		public Alunno? Ottieni(uint matricola);
		public bool Aggiorna(uint matricolaAlunnoDaAggiornare, string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? classe = null);
		public bool Cancella(uint? matricolaAlunnoDaCancellare);
	}
}