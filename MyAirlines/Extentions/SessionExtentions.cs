using Newtonsoft.Json;

namespace MyAirlines.Extentions
{
    public static class SessionExtentions
    {
        // Zet een object in de Session
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            // Serialiseer het object naar een string (JSON)
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Haal een object uit de Session
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            // Deserialiseer het weer naar het oorspronkelijke object type
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
