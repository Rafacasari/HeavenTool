using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Numerics;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.IO;
using System.Xml;
using YamlConverter;
using System.Threading.Tasks;

namespace HeavenTool.Utility.FileTypes.BCSV.Exporting;

public static class BCSVExporter
{
    #region yoinked from MSBT Editor temporarily just for exporting things to it

    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class IgnoreEmptyCollectionAttribute : Attribute;

    internal sealed class NumberConverter<T> : JsonConverter<T> where T : INumber<T>
    {
        public override bool CanRead => true;

        public override bool CanWrite => true;

        public override T? ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType is JsonToken.Null) return existingValue;
            if (reader.TokenType is JsonToken.Integer) return JToken.ReadFrom(reader).ToObject<T>();
            if (reader.TokenType is JsonToken.String)
            {
                var value = (string?)reader.Value;
                if (value is null) return existingValue;
                if (value.StartsWith("0x") || value.StartsWith("0X")) return T.Parse(value[2..], NumberStyles.AllowHexSpecifier, null);
                return T.Parse(value, NumberStyles.Integer, null);
            }

            throw new JsonSerializationException($"Unsupported token type {reader.TokenType}.");
        }

        public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer) => writer.WriteRaw(value?.ToString());
    }

    internal sealed class ContractResolver : DefaultContractResolver
    {
        public static readonly ContractResolver Instance = new();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            //handle empty collections
            if (member.MemberType is MemberTypes.Property && member.GetCustomAttribute<IgnoreEmptyCollectionAttribute>() is not null && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                property.ShouldSerialize = instance =>
                {
                    var enumerator = ((IEnumerable?)instance.GetType().GetProperty(member.Name)?.GetValue(instance, null))?.GetEnumerator();
                    if (enumerator is null) return property.NullValueHandling is NullValueHandling.Include;

                    var canIterate = enumerator.MoveNext();
                    if (enumerator is IDisposable disposable) disposable.Dispose();
                    return canIterate;
                };
            }

            return property;
        }
    }

    internal static readonly JsonSerializerSettings JsonSettings = new()
    {
        ContractResolver = ContractResolver.Instance,
        Converters =
        [
            new NumberConverter<byte>(),
            new NumberConverter<sbyte>(),
            new NumberConverter<short>(),
            new NumberConverter<ushort>(),
            new NumberConverter<int>(),
            new NumberConverter<uint>(),
            new NumberConverter<long>(),
            new NumberConverter<ulong>()
        ]
    };
    #endregion

    public static void ExportConfig (this GameConfig config, string path)
    {
        var content = YamlConvert.SerializeObject(config, JsonSettings);

        File.WriteAllText(path, content);
    }

    public class GameConfig
    {
        [JsonProperty("bcsv")]
        public BcsvConfig Bcsv { get; set; } = new();
    }

    public class BcsvHeader
    {
        [JsonProperty("hash")]
        public string Hash { get; set; } // e.g. 0x36E8EBE

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore), DefaultValue("")]
        public string Name { get; set; } = string.Empty;  // e.g. Name string64 | Maybe prettify it on Editor time?

        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore), DefaultValue("")]
        public string DataType { get; set; } = string.Empty; //e.g. string | from GetName()
    }

    public class BcsvConfig
    {
        [JsonProperty("header"), IgnoreEmptyCollection]
        public List<BcsvHeader> Headers { get; set; } = [];

        [JsonProperty("crc"), IgnoreEmptyCollection]
        public List<string> CRCHashes { get; set; } = [];

        [JsonProperty("murmur"), IgnoreEmptyCollection]
        public List<string> MurmurHashes { get; set; } = [];
    }

    public static string GetName(this BCSVDataType type)
    {
        return type switch
        {
            BCSVDataType.String => "string",
            BCSVDataType.S8 => "sbyte",
            BCSVDataType.U8 => "byte",
            BCSVDataType.Int16 => "int16",
            BCSVDataType.UInt16 => "uint16",
            BCSVDataType.Int32 => "int32",
            BCSVDataType.UInt32 => "uint32",
            BCSVDataType.Float32 => "float32",
            BCSVDataType.Float64 => "float64",
            BCSVDataType.HashedCsc32 => "crc32",
            BCSVDataType.MultipleU8 => "byte[]",
            BCSVDataType.MultipleS8 => "sbyte[]",
            BCSVDataType.Murmur3 => "murmur",
            _ => throw new NotImplementedException(),
        };
    }
}
