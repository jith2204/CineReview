using Data.Exceptions;
using Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class ListofValuesService : IListofValuesService
    {
        public async Task<string> GetGenreList()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "genres.json");

            if (!File.Exists(filePath))
                throw new EntityNotFoundException("No json Found");

            var json = File.ReadAllText(filePath);
            
            return json;
        }

        public async Task<string> GetLanguageList()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "languages.json");

            if (!File.Exists(filePath))
                throw new EntityNotFoundException("No json Found");

            var json = File.ReadAllText(filePath);

            return json;
        }
    }
}
