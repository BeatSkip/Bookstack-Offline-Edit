using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using BookStackApiClient;
using VsCodeStack.Configuration;

namespace VsCodeStack.BookStack
{
    internal class StackReader
    {
        private readonly BookStackClient _client;
        private readonly Config _config;

        internal StackReader(Config config)
        {
            _config = config;
            _client = new BookStackClient(new Uri(config.ApiAddress), config.ApiToken, config.ApiSecret);
        }


        public async Task<ShelfItem[]> GetShelves()
        {
           var rcv = await _client.ListShelvesAsync();

            if(rcv == null)
            {
                return new ShelfItem[0];
            }
            var x = rcv.data[0];
            
            return rcv.data;
        }

        public async Task<ShelfContentBook[]> ReadShelf(long id)
        {
            var opts = new ListingOptions();
            
            var rcv = await _client.ReadShelfAsync(id);

            if(rcv == null)
            {
                return new ShelfContentBook[0];
            }

            return rcv.books;
        }

        public async Task<long> FindBook(string url)
        {
            var slug = Helpers.UrlToSlug(url);
            var rcv = await _client.ListBooksAsync(new ListingOptions() { filters = new[] { new Filter() { field = "slug", expr = slug } } });

            if(rcv == null)
            {
                return -1;
            }
            if(rcv.data.Length == 0)
            {
                return -1;
            }

            return rcv.data.First().id; 
        }

        public async Task GetBook(long id)
        {
           var book = await _client.ReadBookAsync(id);

          //serialize book to json;
          var json = JsonSerializer.Serialize(book);

        //write json to file

        }
    }
}
