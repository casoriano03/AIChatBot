using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;

namespace AIChatBot
{
    internal class PokemonPlugin
    {
        [KernelFunction("get_pokemons")]
        [Description("Gets all pokemons registered in the database")]
        [return: Description("List of pokemons")]
        public async Task<List<Pokemon>> GetPokemons(Kernel kernel)
        {
            using HttpClient client = new HttpClient();

            string url = "https://localhost:7252/api/Pokemon/GetAllPokemons"; // Example API
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                List<Pokemon> pokemons = JsonSerializer.Deserialize<List<Pokemon>>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Pokemon>();

                return pokemons;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                return new List<Pokemon>();
            }
        }
    }
}
