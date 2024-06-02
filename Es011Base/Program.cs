using Es013.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Es013
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//1. Istanziazione di un contenitore per l'iniezione delle dipendenze
			ServiceCollection serviceCollection = new();

			//2. Aggiunta dei servizi al contenitore di iniezione delle dipendenze con durata di vita
			//limitata all'ambito della richiesta del cliente, cioè all'ambito della connessione
			serviceCollection.AddScoped<ValutazioneService>();
			serviceCollection.AddScoped<AlunnoService>();
			serviceCollection.AddScoped<DocenteService>();
			serviceCollection.AddScoped<RegistroService>();

			//3. Costruzione del fornitore dei servizi a partire dal contenitore di iniezione delle dipendenze
			ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

			//4. Ottenimento del registroService dal fornitore dei servizi
			RegistroService? registroService = serviceProvider.GetService<RegistroService>();

			//5. Chiamata al metodo Menu del registroService se questo esiste
			registroService?.Menu();
		}
	}
}