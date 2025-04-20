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
using YamlConverter;

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

    public class FieldHashes
    {
        [JsonProperty("crc32"), IgnoreEmptyCollection]
        public List<string> CRCHashes { get; set; } = [];


        [JsonProperty("mmh3"), IgnoreEmptyCollection]
        public List<string> MurmurHashes { get; set; } = [];
    }

    public class BcsvHeader
    {
        [JsonProperty("hash", DefaultValueHandling = DefaultValueHandling.Ignore), DefaultValue("")]
        public string Hash { get; set; } // e.g. 0x36E8EBE

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore), DefaultValue("")]
        public string Name { get; set; } = string.Empty;  // e.g. Name string64 | Maybe prettify it on Editor time?

        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore), DefaultValue("")]
        public string DataType { get; set; } = string.Empty; //e.g. string | from GetName()
    }

    public class BcsvConfig
    {
        [JsonProperty("headers"), IgnoreEmptyCollection]
        public List<BcsvHeader> Headers { get; set; } = [];

        [JsonProperty("fieldHashes")]
        public FieldHashes FieldHashes { get; set; }
    }

    public static string GetName(this BCSVDataType type)
    {
        return type switch
        {
            BCSVDataType.String => "str",
            BCSVDataType.S8 => "s8",
            BCSVDataType.U8 => "u8",
            BCSVDataType.Int16 => "s16",
            BCSVDataType.UInt16 => "u16",
            BCSVDataType.Int32 => "s32",
            BCSVDataType.UInt32 => "u32",
            BCSVDataType.Float32 => "f32",
            BCSVDataType.Float64 => "f64",
            BCSVDataType.HashedCsc32 => "crc32",
            BCSVDataType.MultipleU8 => "u8[]",
            BCSVDataType.MultipleS8 => "s8[]",
            BCSVDataType.Murmur3 => "mmh3",
            _ => throw new NotImplementedException(),
        };
    }

    public static Type ToType(this BCSVDataType type)
    {
        return type switch
        {
            BCSVDataType.String => typeof(string),
            BCSVDataType.S8 => typeof(sbyte),
            BCSVDataType.U8 => typeof(byte),
            BCSVDataType.Int16 => typeof(short),
            BCSVDataType.UInt16 => typeof(ushort),
            BCSVDataType.Int32 => typeof(int),
            BCSVDataType.UInt32 => typeof(uint),
            BCSVDataType.Float32 => typeof(float),
            BCSVDataType.Float64 => typeof(double),

            BCSVDataType.MultipleU8 => typeof(byte[]),
            BCSVDataType.MultipleS8 => typeof(sbyte[]),

            //TODO: Give Murmur and CRC their own classes
            BCSVDataType.Murmur3 => typeof(uint),
            BCSVDataType.HashedCsc32 => typeof(uint),
            _ => throw new NotImplementedException(),
        };
    }
}
