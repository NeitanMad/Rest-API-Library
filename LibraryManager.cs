using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Rest_API_Library.Response_Schemas;
using System.Net;
using System.Xml.Linq;

namespace Rest_API_Library
{
    public class LibraryManager
    {
        private LibraryApi libraryApi;
        public LibraryManager(string baseUrl)
        {
            libraryApi = new LibraryApi(baseUrl);
        }

        public async Task GetAllBooksAndValidateToSchemaAsync()
        {
            var response = await libraryApi.GetAllBooksAsync();

            Assert.IsTrue(response.IsSuccessful, $"Status code isnot ok!  Actual status code: {response.StatusCode}");

            Assert.IsTrue(JObject.Parse(response.Content).IsValid(GetAllBooksResponseSchema.Schema), "Response data is no valid to schema");
        }

        public async Task GetBookByIdAndValidateToSchemaAsync(int id)
        {
            var response = await libraryApi.GetBookByIdAsync(id);

            Assert.IsTrue(response.IsSuccessful, $"Status code isnot ok!  Actual status code: {response.StatusCode}");

            Assert.IsTrue(JObject.Parse(response.Content).IsValid(CreateBookResponseSchema.Schema), "Response data is no valid to schema");
        }

        public async Task CreateBookAndValidateToSchemaAsync(Book bk)
        {
            var response = await libraryApi.CreateBook(bk);

            Assert.IsTrue(response.IsSuccessful, $"Status code isnot ok!  Actual status code: {response.StatusCode}");
            
            Assert.IsTrue(JObject.Parse(response.Content).IsValid(CreateBookResponseSchema.Schema), "Response data is no valid to schema");
        }
        public async Task TryToCreateBookWithoutReqierdedParamsAsync(Book bk)
        {
            var response = await libraryApi.CreateBook(bk);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "Http status code is not correct!");
        }
        public async Task TryToCreateBookWithoutParams()
        {
            var response = await libraryApi.CreateBookWithoutBody();

            Assert.AreEqual(HttpStatusCode.BadRequest ,response.StatusCode, $"Status code is not 400!  Actual status code: {response.StatusCode}");
        }

        public async Task UpDateBookByIdAndValidateToSchemaAsync(Book bk, int id)
        {
            var response = await libraryApi.UpdateBookByIdWithJsonBody(bk, id);
            Assert.IsTrue(response.IsSuccessful, $"Status code isnot ok!  Actual status code: {response.StatusCode}");


            Assert.IsTrue(JObject.Parse(response.Content).IsValid(CreateBookResponseSchema.Schema), "Response data is no valid to schema");
        }

        public async Task DeleteBookByIdAndValidateToSchemaAsync(int id)
        {
            var response = await libraryApi.DeleteBookById(id);
            Assert.IsTrue(response.IsSuccessful, $"Status code isnot ok!  Actual status code: {response.StatusCode}");
            Assert.IsTrue(JObject.Parse(response.Content).IsValid(DeleteBookResponseSchema.Schema), "Response data is no valid to schema");
        }
        public async Task ValidateThatBookWasDeletedFromBooksListAsync(int id)
        {
            var booksList = await libraryApi.GetAllBooksAsync();
            var books = JsonConvert.DeserializeObject<GetAllBooksResponseSchema>(booksList.Content).AllBooks;

            Assert.IsTrue(books.All(book => book.Id != id), "Deleted book is in books list");
        }

        public async Task ValidateThatBookWasDeletedAsync(int id)
        {
            var response = await libraryApi.GetBookByIdAsync(id);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode, "Http status code is not correct!");
        }

        public async Task<int> GetRandomBookIdOrCreateOneAsync()
        {
            var response = await libraryApi.GetAllBooksAsync();
            var books = JsonConvert.DeserializeObject<GetAllBooksResponseSchema>(response.Content).AllBooks;
            if (books.Count != 0)
            {
                return books.First().Id;
            }
            var createdBookResponse = await libraryApi.CreateBook(new Book { Name = "Книга" });
            return JsonConvert.DeserializeObject<CreateBookResponseSchema>(createdBookResponse.Content).Book.Id;
        }
    }
}
