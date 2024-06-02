using System;

namespace Es012Base.Models
{
	internal class Docente : Persona
	{
		internal Docente(string? nome = null, string? cognome = null, string? codiceFiscale = null, DateOnly? dataNascita = null, string? materia = null)
			: base(nome, cognome, codiceFiscale, dataNascita)
		{
			Materia = materia;
		}
		internal string? Materia { get; set; }

	}
}