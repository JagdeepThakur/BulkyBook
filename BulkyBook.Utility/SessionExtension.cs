using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BulkyBook.Utility
{
    public static class SessionExtension
    {
        public static void SetObject(this ISession session, string Key, object value)
        {
            session.SetString(Key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string Key)
        {
            var value = session.GetString(Key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
