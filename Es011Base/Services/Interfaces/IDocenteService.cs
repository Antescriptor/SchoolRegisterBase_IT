using Es013.Models;
using System;

namespace Es013.Services.Interfaces
{
	public interface IDocenteService
	{
		public void Menu(Docente docente);
		public void MenuRegistroValutazioni(Docente docente);
		public Valutazione? MenuRicercaValutazione(Docente docente, string propositoRicerca, bool restituzioneNecessaria = true);
		public Alunno? MenuRicercaAlunno(string propositoRicerca, bool restituzioneNecessaria = true);
		public void MenuRegistroAnagraficoDocenti(); //Da implementare
		public void InserisciDocente(string? nome, string? cognome, string? codiceFiscale, DateOnly? dataNascita, string? materia);
		public Docente? OttieniDocente(uint matricola);
	}
}