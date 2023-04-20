using Rest_API_Library.Managers;

namespace Rest_API_Library.Tests
{
    public class Tests
    {
        /// <summary>
        /// TC-1. Получить список всех книг.
        /// </summary>
        [Test]
        public async Task GetAllBooksTest()
        {

            LibraryManager libraryManager = new LibraryManager("http://localhost:5000");

            await libraryManager.GetAllBooksAndValidateToSchemaAsync();
        }

        /// <summary>
        /// TC-2 Получить книгу по id
        /// </summary>
        [Test]
        public async Task GetBookTest()
        {
            LibraryManager libraryManager = new LibraryManager("http://localhost:5000");

            var id = await libraryManager.GetRandomBookIdOrCreateOneAsync();

            await libraryManager.GetBookByIdAndValidateToSchemaAsync(id);
        }

        /// <summary>
        /// TC-3 Добавить новую книгу только с обязательным параметром
        /// </summary>
        [Test]
        public async Task CreateBookWithRequiredParamsOnly()
        {
            LibraryManager libraryManager = new LibraryManager("http://localhost:5000");

            var book = new Book { Name = "Книга с обязательным параметром name " };

            await libraryManager.CreateBookAndValidateToSchemaAsync(book);
        }

        /// <summary>
        /// TC-4.Добавить новую книгу с обязательным и необязательными параметрами
        /// </summary>
        [Test]
        public async Task CreateBookWithOptionalAndRequiredParameters()
        {
            LibraryManager libraryManager = new LibraryManager("http://localhost:5000");

            var book = new Book
            {
                Name = "Книга с обязательным параметром name ",
                Author = "Необязательный параметр Author",
                Year = 2000,
                IsElectronicBook = true
            };

            await libraryManager.CreateBookAndValidateToSchemaAsync(book);
        }
        /// <summary>
        /// TC-5. Обновить информацию о книге по ее id
        /// </summary>
        [Test]
        public async Task UpdateBook()
        {
            LibraryManager libraryManager = new LibraryManager("http://localhost:5000");

            var book = new Book
            {
                Name = "Имя обновлено",
                Author = "Автор обновлен",
                Year = 1111,
                IsElectronicBook = false
            };
            var id = await libraryManager.GetRandomBookIdOrCreateOneAsync();

            await libraryManager.UpDateBookByIdAndValidateToSchemaAsync(book, id);
        }

        /// <summary>
        /// TC-6. Удалить книгу по id
        /// </summary>
        [Test]
        public async Task DeleteBook()
        {
            LibraryManager libraryManager = new LibraryManager("http://localhost:5000");
            var id = await libraryManager.GetRandomBookIdOrCreateOneAsync();
            await libraryManager.DeleteBookByIdAndValidateToSchemaAsync(id);
            await libraryManager.ValidateThatBookWasDeletedFromBooksListAsync(id);
            await libraryManager.ValidateThatBookWasDeletedAsync(id);
        }
        /// <summary>
        /// TC-7. Добавить новую книгу без параметров
        /// </summary>
        [Test]
        public async Task CreateBookWithoutParams()
        {
            LibraryManager libraryManager = new LibraryManager("http://localhost:5000");

            await libraryManager.TryToCreateBookWithoutParams();
        }
        /// <summary>
        /// TC-8. Добавить новую книгу без обязательного параметра
        /// </summary>
        [Test]
        public async Task CreateBookWithoutRequieredParams()
        {
            LibraryManager libraryManager = new LibraryManager("http://localhost:5000");

            var book = new Book
            {
                Author = "Необязательный параметр Author",
                Year = 2000,
                IsElectronicBook = true
            };

            await libraryManager.TryToCreateBookWithoutReqierdedParamsAsync(book);
        }

    }
}