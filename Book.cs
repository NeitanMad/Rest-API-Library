using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest_API_Library
{
    /// <summary>
    /// Модель объекта "книга"
    /// </summary>
    public class Book
    {

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("year")]
        public int Year { get; set; }
        [JsonProperty("isElectronicBook")]
        public bool IsElectronicBook { get; set; }

    }
}
