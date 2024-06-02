using System;

namespace Es012Base.Models
{
	internal class Valutazione
	{
		internal Valutazione(Docente docente, Alunno alunno, DateTime? dataEOra = null, string? materia = null, string? classe = null, decimal? voto = null)
		{
			ContatoreId++;
			Id = ContatoreId;
			Docente = docente;
			Alunno = alunno;
			Materia = materia;
			Classe = classe;
			Voto = voto;

			if (dataEOra is null)
			{
				DataEOra = DateTime.Now;
			}
			else
			{
				DataEOra = (DateTime)(dataEOra);
			}
		}
		internal static uint ContatoreId { get; set; } = 0;
		internal uint Id { get; }
		internal Docente Docente { get; }
		internal Alunno Alunno { get; }
		internal DateTime DataEOra { get; set; }
		internal string? Materia { get; set; }
		internal string? Classe { get; set; }
		internal decimal? Voto { get; set; }
	}
}
