using System;
using System.Globalization;

namespace Es011Base.Utils
{
	internal static class ImmissioneUtility
	{
		internal static string Password(uint lunghezza)
		{
			string input;
			{
				do
				{
					Console.WriteLine($"Immettere password di {lunghezza} caratteri:");
					input = Console.ReadLine() ?? "";
				} while (input.Length != lunghezza);
				return input;
			}
		}
		internal static uint? NumeroNaturale(string nomeVariabile = "")
		{
			string? input;
			bool matricolaOIdNullo;
			bool verificaNumeroNaturale;
			uint numeroNaturale;
			do
			{
				do
				{
					matricolaOIdNullo = false;

					Console.WriteLine("Immettere {0}:", (nomeVariabile == "") ? "numero naturale" : nomeVariabile);
					input = Console.ReadLine();

					if (nomeVariabile != "matricola" && nomeVariabile != "id" && string.IsNullOrEmpty(input))
					{
						return null;
					}
					else if (string.IsNullOrEmpty(input))
					{
						matricolaOIdNullo = true;
					}

				} while (matricolaOIdNullo);

				verificaNumeroNaturale = uint.TryParse(input, out numeroNaturale);

			} while (!verificaNumeroNaturale);

			return numeroNaturale;
		}
		internal static string? Stringa(string nomeVariabile = "")
		{
			string? input;

			do
			{
				Console.WriteLine("Immettere {0}:", (nomeVariabile == "") ? "stringa" : nomeVariabile);
				input = Console.ReadLine();

				if (string.IsNullOrEmpty(input))
				{
					return null;
				}
			}
			while (input.Length < 1);

			return input;
		}
		internal static DateOnly? Data()
		{
			string? input;
			bool verificaData;
			CultureInfo localizzazione = new("it-IT");
			DateOnly data;

			do
			{
				Console.WriteLine("Immettere data nel formato \"gg/mm/aaaa\":");
				input = Console.ReadLine();

				if (string.IsNullOrEmpty(input))
				{
					return null;
				}

				verificaData = DateOnly.TryParse(input, localizzazione, out data);

			}
			while (!verificaData);

			return data;
		}
		internal static DateTime? DataEOra()
		{
			string? input;
			bool verificaDataEOra;
			CultureInfo localizzazione = new("it-IT");
			DateTimeStyles stile = DateTimeStyles.AssumeLocal;
			DateTime dataEOra;

			do
			{
				Console.WriteLine("Immettere data e ora nel formato \"gg/MM/aaaa hh:mm\":");
				input = Console.ReadLine();

				if (string.IsNullOrEmpty(input))
				{
					return null;
				}

				verificaDataEOra = DateTime.TryParse(input, localizzazione, stile, out dataEOra);
			}
			while (!verificaDataEOra);

			return dataEOra;
		}
		internal static decimal? NumeroRazionale(string nomeVariabile = "")
		{
			string? input;
			bool verificaNumeroRazionale;
			decimal numeroRazionale;

			do
			{
				Console.WriteLine("Immettere {0}:", (nomeVariabile == "") ? "numero naturale" : nomeVariabile);
				input = Console.ReadLine();

				if (string.IsNullOrEmpty(input))
				{
					return null;
				}

				verificaNumeroRazionale = decimal.TryParse(input, out numeroRazionale);

			} while (!verificaNumeroRazionale);

			return numeroRazionale;
		}
	}
}
