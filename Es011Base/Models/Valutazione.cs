using Es013.Models.Interfaces;
using System;

namespace Es013.Models
{
	public class Valutazione : IValutazione
	{
		public Valutazione(Docente docente, Alunno alunno, DateTime? dataEOra = null, string? materia = null, string? classe = null, decimal? voto = null)
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
		public static uint ContatoreId { get; set; } = 0;
		public uint Id { get; }
		public Docente Docente { get; }
		public Alunno Alunno { get; }
		public DateTime DataEOra { get; set; }
		public string? Materia { get; set; }
		public string? Classe { get; set; }
		public decimal? Voto { get; set; }
	}
}