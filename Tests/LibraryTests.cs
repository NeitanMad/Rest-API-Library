using Rest_API_Library.Managers;

namespace Rest_API_Library.Tests
{
    public class Tests
    {
        /// <summary>
        /// TC-1. �������� ������ ���� ����.
        /// </summary>
        [Test]
        public async Task GetAllBooksTest()
        {

            LibraryManager libraryManager = new LibraryManager("http://localhost:5000");

            await libraryManager.GetAllBooksAndValidateToSchemaAsync();
        }

        /// <summary>
        /// TC-2 �������� ����� �� id
        /// </summary>
        [Test]
        public async Task GetBookTest()
        {
            LibraryManager libraryManager = new LibraryManager("http://localhost:5000");

            var id = await libraryManager.GetRandomBookIdOrCreateOneAsync();

            await libraryManager.GetBookByIdAndValidateToSchemaAsync(id);
        }

        /// <summary>
        /// TC-3 �������� ����� ����� ������ � ������������ ����������
        /// </summary>
        [Test]
        public async Task CreateBookWithRequiredParamsOnly()
        {
            LibraryManager libraryManager = new LibraryManager("http://localhost:5000");

            var book = new Book { Name = "����� � ������������ ���������� name " };

            await libraryManager.CreateBookAndValidateToSchemaAsync(book);
        }

        /// <summary>
        /// TC-4.�������� ����� ����� � ������������ � ��������������� �����������
        /// </summary>
        [Test]
        public async Task CreateBookWithOptionalAndRequiredParameters()
        {
            LibraryManager libraryManager = new LibraryManager("http://localhost:5000");

            var book = new Book
            {
                Name = "����� � ������������ ���������� name ",
                Author = "�������������� �������� Author",
                Year = 2000,
                IsElectronicBook = true
            };

            await libraryManager.CreateBookAndValidateToSchemaAsync(book);
        }
        /// <summary>
        /// TC-5. �������� ���������� � ����� �� �� id
        /// </summary>
        [Test]
        public async Task UpdateBook()
        {
            LibraryManager libraryManager = new LibraryManager("http://localhost:5000");

            var book = new Book
            {
                Name = "��� ���������",
                Author = "����� ��������",
                Year = 1111,
                IsElectronicBook = false
            };
            var id = await libraryManager.GetRandomBookIdOrCreateOneAsync();

            await libraryManager.UpDateBookByIdAndValidateToSchemaAsync(book, id);
        }

        /// <summary>
        /// TC-6. ������� ����� �� id
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
        /// TC-7. �������� ����� ����� ��� ����������
        /// </summary>
        [Test]
        public async Task CreateBookWithoutParams()
        {
            LibraryManager libraryManager = new LibraryManager("http://localhost:5000");

            await libraryManager.TryToCreateBookWithoutParams();
        }
        /// <summary>
        /// TC-8. �������� ����� ����� ��� ������������� ���������
        /// </summary>
        [Test]
        public async Task CreateBookWithoutRequieredParams()
        {
            LibraryManager libraryManager = new LibraryManager("http://localhost:5000");

            var book = new Book
            {
                Author = "�������������� �������� Author",
                Year = 2000,
                IsElectronicBook = true
            };

            await libraryManager.TryToCreateBookWithoutReqierdedParamsAsync(book);
        }

    }
}