using Es013.Models;
using System;
using System.Collections.Generic;

namespace Es013.Services.Interfaces
{
	public interface IAlunnoService
	{
		public void Menu(Alunno alunno);
		public List<Alunno>? Cerca(string? nome = null, string? cognome = null, string? codiceFiscale = null, uint? annoNascita = null, string? classe = null);
		public void RegistraAlunno();
		public Alunno? OttieniAlunno(uint matricola);
		public Alunno? MenuRicercaAlunno(string propositoRicerca, bool restituzioneNecessaria = true);
		public void MenuRegistroAnagraficoAlunni();
		public void InserisciAlunno(string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? classe = null);
	}
}
