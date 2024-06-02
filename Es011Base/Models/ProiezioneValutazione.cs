using System;

namespace Es012Base.Models
{
	internal class ProiezioneValutazione
	{
		internal ProiezioneValutazione(DateTime dataEOra, string? materia = null, decimal? mediaVoto = null) 
		{ 
			DataEOra = dataEOra;
			Materia = materia;
			MediaVoto = mediaVoto;
		}
		internal DateTime DataEOra {  get; set; }
		internal string? Materia { get; set; }
		internal decimal? MediaVoto { get; set; }
	}
}
