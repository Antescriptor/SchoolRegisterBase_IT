using System;

namespace Es013.Models
{
	public class Docente : Persona
	{
		public Docente(string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? materia = null)
			: base(nome, cognome, codiceFiscale, dataNascita)
		{
			Materia = materia;
		}
		internal string? Materia { get; set; }

	}
}