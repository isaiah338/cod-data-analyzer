using System.Text;
using System.Text.Json;

namespace cod_data_analyzer.Services.Util
{
    public class StringUtil
    {
        public static string Normalize(String inStr)
            => inStr.Replace(" ", string.Empty).Replace("-", string.Empty).ToLower();
        public static List<TValue> DeserializedEncodedJson<TValue>(string encoded)
            => JsonSerializer.Deserialize<List<TValue>>(EncodeFromBase64String(encoded)) ?? [];

        public static string EncodeFromBase64String(string encoded)
            => Encoding.UTF8.GetString(Convert.FromBase64String(encoded));

        public static string SerializedEncodedJson<TValue>(List<TValue> value)
            => Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value)));

        public static byte[] EncodeToBase64String<TValue>(List<TValue> toEncode)
            => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(toEncode));
    }
}
