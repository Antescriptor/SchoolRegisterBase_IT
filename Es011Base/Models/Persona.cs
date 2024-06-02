using Es013.Models.Interfaces;
using Es013.Utils;
using System;

namespace Es013.Models
{
    public abstract class Persona : IPersona
	{
		public Persona(string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null)
		{
			ContatoreMatricola++;
			Matricola = ContatoreMatricola;
			Console.WriteLine($"Segnarsi la matricola:\t{Matricola}");
			_password = GenerazioneUtility.Password(8);
			Nome = nome;
			Cognome = cognome;
			CodiceFiscale = codiceFiscale;
			DataNascita = dataNascita;
		}
		public static uint ContatoreMatricola { get; private set; } = 0;
		public uint Matricola { get; }
		public string? Nome { get; set; }
		public string? Cognome { get; set; }
		public string? CodiceFiscale { get; set; }
		public DateOnly? DataNascita { get; set; }

		private readonly string _password;
		public bool ControllaPassword(string password)
		{
			return password == _password ? true : false;
		}
	}
}
