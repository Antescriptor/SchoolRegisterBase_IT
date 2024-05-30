using Es011Base.Services;

namespace Es011Base
{
	internal class Program
	{
		static void Main(string[] args)
		{
			RegistroService registro = new();
			registro.Menu();
		}
	}
}