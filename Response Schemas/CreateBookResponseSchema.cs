using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Schema;
using Rest_API_Library.Response_Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest_API_Library
{
    internal class CreateBookResponseSchema : ISchemaBase
    {
        [JsonProperty("book")]
        public Book Book;
        public static JSchema Schema => new JSchemaGenerator().Generate(typeof(CreateBookResponseSchema));
    }
}
