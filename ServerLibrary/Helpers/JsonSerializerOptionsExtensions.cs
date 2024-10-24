using System.Text.Json;
using System.Text.Json.Serialization;

namespace ServerLibrary.Helpers
{

    public static class JsonSerializerOptionsExtensions
    {
        public static JsonSerializerOptions GetPreserveReferencesOptions()
        {
            return new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
        }
    }

}
