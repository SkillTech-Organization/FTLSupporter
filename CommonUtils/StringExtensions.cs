﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils
{
    public static class StringExtensions
    {
        public static IsoDateTimeConverter IsoDateTimeConverter { get; set; } = new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm:ss" };

        public static string ToCompressedJson(this object m)
        {
            try
            {
                var json = JsonConvert.SerializeObject(m, Formatting.None, IsoDateTimeConverter);
                var res = StringCompressor.CompressStringGzip2(json);
                return res;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string ToDecompressedJson(this string m)
        {
            try
            {
                var json = StringCompressor.DecompressStringGzip2(m);
                return json;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static T ToDecompressedAndDeserializedJson<T>(this string m)
        {
            try
            {
                var json = StringCompressor.DecompressStringGzip2(m);
                var o = JsonConvert.DeserializeObject<T>(json, IsoDateTimeConverter);
                return o;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public static string ToJson(this object m)
        {
            try
            {
                var json = JsonConvert.SerializeObject(m, Formatting.None, IsoDateTimeConverter);
                return json;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static T ToDeserializedJson<T>(this string m)
        {
            try
            {
                var o = JsonConvert.DeserializeObject<T>(m, IsoDateTimeConverter);
                return o;
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}
