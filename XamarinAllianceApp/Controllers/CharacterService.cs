using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using XamarinAllianceApp.Helpers;
using XamarinAllianceApp.Models;

namespace XamarinAllianceApp.Controllers
{
    public class CharacterService
    {
        /// <summary>
        /// Get the list of characters
        /// </summary>
        /// <returns>ObservableCollection of Character objects</returns>
        public async Task<ObservableCollection<Character>> GetCharactersAsync()
        {            
            return new ObservableCollection<Character>(await ReadCharacters());
        }


        private async Task<IEnumerable<Character>> ReadCharacters()
        {
            //return await ReadCharactersFromFile();
            return await ReadCharactersFromService();
        }

        private async Task<IEnumerable<Character>> ReadCharactersFromService()
        {
            //my service test:
            string mobileServiceClientUrl = "http://eb59d72a-0ee0-4-231-b9ee.azurewebsites.net";
            //local:
            //string mobileServiceClientUrl = "http://localhost:51537/";
            //challange:
            //string mobileServiceClientUrl = "http://xamarinalliancebackend.azurewebsites.net";
            MobileServiceClient client = new MobileServiceClient(mobileServiceClientUrl);
            var table = client.GetTable<Character>();            
            return await table.ToListAsync();
        }

        /// <summary>
        /// Get the list of characters from an embedded JSON file, including their child entities.
        /// </summary>
        /// <returns>Array of Character objects</returns>
        private async Task<IEnumerable<Character>> ReadCharactersFromFile()
        {
            var assembly = typeof(CharacterService).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(Constants.CharactersFilename);
            string text;

            using (var reader = new System.IO.StreamReader(stream))
            {
                text = await reader.ReadToEndAsync();
            }

            var characters = JsonConvert.DeserializeObject<Character[]>(text);
            return characters;
        }
    }
}
