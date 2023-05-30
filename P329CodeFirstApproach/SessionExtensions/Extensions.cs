using Newtonsoft.Json;

namespace P329CodeFirstApproach.SessionExtensions
{
    public static class Extensions
    {
        public static void SetAsJson<T>(this ISession session, string key, T value)
        {
            var json = JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            session.SetString(key, json);
        }

        public static T GetFromJson<T>(this ISession session, string key) 
        {
            var json = session.GetString(key);

            var value = JsonConvert.DeserializeObject<T>(json);

            return value;
        }
    }
}
