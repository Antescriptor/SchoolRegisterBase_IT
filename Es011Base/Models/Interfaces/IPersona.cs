using System;

namespace Es013.Models.Interfaces
{
    public interface IPersona
    {
		static uint ContatoreMatricola { get; private set; }
        uint Matricola { get; }
        string? Nome { get; set; }
        string? Cognome { get; set; }
        string? CodiceFiscale { get; set; }
        DateOnly? DataNascita { get; set; }

        bool ControllaPassword(string password);
    }
}
