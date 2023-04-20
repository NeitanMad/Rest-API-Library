using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest_API_Library.Response_Schemas
{
    public class DeleteBookResponseSchema : ISchemaBase
    {
        [JsonProperty("result")]
        public bool IsDeleted { get; set; }
        public static JSchema Schema => new JSchemaGenerator().Generate(typeof(DeleteBookResponseSchema));
    }
}
