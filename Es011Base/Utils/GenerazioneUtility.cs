﻿using System;

namespace Es012Base.Utils
{
	public class GenerazioneUtility
	{
		internal static string Password(int lunghezza)
		{
			Random gen = new();
			string alfabeto = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@&*$%#";
			string[] passwordArray = new string[lunghezza];
			int j;

			for (int i = 0; i < lunghezza; i++)
			{
				j = gen.Next(0, alfabeto.Length);
				passwordArray[i] = alfabeto[j].ToString();
			}

			string password = string.Join("", passwordArray);

			Console.WriteLine($"Segnarsi la password:\t{password}");
			return password;
		}
	}
}
