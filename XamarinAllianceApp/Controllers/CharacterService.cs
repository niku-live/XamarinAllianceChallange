using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
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
        private MobileServiceClient client;

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public CharacterService()
        {
            client = new MobileServiceClient(GetUrl());
        }

        private string GetUrl()
        {
            //my service test:
            //string mobileServiceClientUrl = "https://eb59d72a-0ee0-4-231-b9ee.azurewebsites.net";
            //local:
            //string mobileServiceClientUrl = "http://localhost:51537/";
            //challange:
            string mobileServiceClientUrl = "http://xamarinalliancebackend.azurewebsites.net";
            //secure challenge:
            //string mobileServiceClientUrl = "https://xamarinalliancesecurebackend.azurewebsites.net";
            return mobileServiceClientUrl;
        }

        public async Task<string> GetToken()
        {            
            var token = await CurrentClient.InvokeApiAsync("/api/StorageToken/CreateToken");
            return token.ToString();
        }

        public async Task<byte[]> DownloadPicture(string token)
        {
            string storageAccountName = "xamarinalliance";
            StorageCredentials credentials = new StorageCredentials(token);
            CloudStorageAccount account = new CloudStorageAccount(credentials, storageAccountName, null, true);
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("images");
            var blob = container.GetBlobReference("XAMARIN-Alliance-logo.png");
            MemoryStream stream = new MemoryStream();
            await blob.DownloadToStreamAsync(stream);
            return stream.ToArray();
        }

        public async Task<string> GetDiploma()
        {
            
            var guid = await CurrentClient.InvokeApiAsync("/api/XamarinAlliance/ReceiveCredit");
            return guid.ToString();
        }


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
            var table = CurrentClient.GetTable<Character>();            
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

        private static CharacterService defaultInstance = new CharacterService();
        public static CharacterService DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }
    }
}
