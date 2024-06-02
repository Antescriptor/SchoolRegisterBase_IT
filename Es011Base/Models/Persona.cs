using Es012Base.Utils;
using System;

namespace Es012Base.Models
{
	internal abstract class Persona
	{
		internal Persona(string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null)
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
		internal static uint ContatoreMatricola { get; set; } = 0;
		internal uint Matricola { get; }
		internal string? Nome { get; set; }
		internal string? Cognome { get; set; }
		internal string? CodiceFiscale { get; set; }
		internal DateOnly? DataNascita { get; set; }

		private readonly string _password;
		internal bool ControllaPassword(string password)
		{
			return password == _password ? true : false;
		}
	}
}
