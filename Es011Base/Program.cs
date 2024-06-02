using Es012Base.Services;

namespace Es012Base
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