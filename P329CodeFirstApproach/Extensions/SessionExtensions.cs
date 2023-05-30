using Newtonsoft.Json;

namespace P329CodeFirstApproach.Extensions
{
    public static class SessionExtensions
    {
        public static void SetJson<T>(this ISession session, string key, T value)
        {
            var json = JsonConvert.SerializeObject(value,new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            session.SetString(key, json);
        }

        public static T GetFromJson<T>(this ISession session, string key)
        {
            var value = JsonConvert.DeserializeObject<T>(session.GetString(key));

            return value;
        }
    }
}
