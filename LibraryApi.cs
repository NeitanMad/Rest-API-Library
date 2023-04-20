using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Rest_API_Library
{
    public class LibraryApi
    {
        protected string Host;

        private RestClientOptions options;

        private RestClient client;
            
        

        public LibraryApi(string host)
        {
            Host = host;

            options = new RestClientOptions(host);

            client = new RestClient(options);   
        }

        public async Task<RestResponse> GetAllBooksAsync()
        {
            var request = new RestRequest("/api/books", Method.Get);

            return await client.GetAsync(request);
        }

        public async Task<RestResponse> GetBookByIdAsync(int id) 
        {
            var request = new RestRequest("/api/books/" + id, Method.Get);
            return await client.GetAsync(request);

        }

        public async Task<RestResponse> CreateBook(Book bk)
        {
            var request = new RestRequest("/api/books", Method.Post);

            string jsonbody = JsonConvert.SerializeObject(bk);

            request.AddBody(jsonbody);

            return await client.ExecutePostAsync(request);
        }
        public async Task<RestResponse> CreateBookWithoutBody()
        {
            var request = new RestRequest("/api/books", Method.Post);
            return await client.ExecutePostAsync(request);
        }

        public async Task<RestResponse> UpdateBookByIdWithJsonBody(Book bk, int id) 
        {
            var request = new RestRequest("/api/books/" + id, Method.Put);

            string jsonbody = JsonConvert.SerializeObject(bk);

            request.AddBody(jsonbody);

            return await client.ExecutePutAsync(request);
        }
        public async Task<RestResponse> DeleteBookById(int id) 
        {
            var request = new RestRequest("/api/books/" + id, Method.Delete);

            return await client.DeleteAsync(request);
        }

    }
}
