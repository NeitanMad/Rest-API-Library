using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Rest_API_Library.Response_Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest_API_Library
{
    public class GetAllBooksResponseSchema : ISchemaBase
    {
        [JsonProperty("books")]
        public List<Book> AllBooks { get; set; }
        public static JSchema Schema => new JSchemaGenerator().Generate(typeof(GetAllBooksResponseSchema));
    }
}
