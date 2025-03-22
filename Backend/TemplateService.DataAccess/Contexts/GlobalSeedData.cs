using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateService.DataAccess.Contexts
{
    public static class GlobalSeedData
    {
        public static Dictionary<string, object> SeedDictionary { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Retrieves a value for the specified key from the dictionary.
        /// If the key does not exist, a new Guid is generated, added to the dictionary, and returned.
        /// </summary>
        /// <param name="key">The key to look up in the dictionary.</param>
        /// <returns>The value associated with the key, or a newly generated Guid if the key was not found.</returns>
        public static Guid GetOrGenerateGuid(string key)
        {
            if (SeedDictionary.ContainsKey(key))
            {
                // If the key exists, return the value as a Guid
                return (Guid)SeedDictionary[key];
            }
            else
            {
                // Generate a new Guid, add it to the dictionary, and return it
                Guid newGuid = Guid.NewGuid();
                SeedDictionary[key] = newGuid;
                return newGuid;
            }
        }
    }

}
