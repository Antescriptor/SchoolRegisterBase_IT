using System;

namespace Es013.Models
{
	public class Alunno : Persona
	{
		internal Alunno(string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? classe = null)
			: base(nome, cognome, codiceFiscale, dataNascita)
		{
			Classe = classe;
		}
		internal string? Classe { get; set; }
	}
}