using System;

namespace Es013.Models
{
	public class ProiezioneValutazione
	{
		public ProiezioneValutazione(DateTime dataEOra, string? materia = null, decimal? mediaVoto = null) 
		{ 
			DataEOra = dataEOra;
			Materia = materia;
			MediaVoto = mediaVoto;
		}
		public DateTime DataEOra {  get; set; }
		public string? Materia { get; set; }
		public decimal? MediaVoto { get; set; }
	}
}
