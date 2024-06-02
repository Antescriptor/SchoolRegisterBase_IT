using System;

namespace Es013.Models.Interfaces
{
	public interface IValutazione
	{
		public static uint ContatoreId { get; set; }
		public uint Id { get; }
		public Docente Docente { get; }
		public Alunno Alunno { get; }
		public DateTime DataEOra { get; set; }
		public string? Materia { get; set; }
		public string? Classe { get; set; }
		public decimal? Voto { get; set; }
	}
}
