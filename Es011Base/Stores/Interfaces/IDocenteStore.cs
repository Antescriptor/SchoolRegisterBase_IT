using Es013.Models;
using System;
using System.Collections.Generic;

namespace Es013.Stores.Interfaces
{
	internal interface IDocenteStore
	{
		public bool Inserisci(string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? materia = null);
		public List<Docente>? Ottieni();
		public Docente? Ottieni(uint matricola);
		public bool Aggiorna(uint matricolaDocenteDaAggiornare, string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? materia = null);
		public bool Cancella(uint matricolaDocenteDaCancellare);
	}
}
