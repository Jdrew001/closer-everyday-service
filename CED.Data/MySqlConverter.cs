using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CED.Data
{
    class MySqlConverter
    {
        public static readonly int MAX_LENGTH_VARCHAR = 4000;
        public static readonly int GUID_LENGTH = 38;
        public static readonly int MAX_GUIDS_IN_MAX_LENGTH_VARCHAR = MySqlConverter.MAX_LENGTH_VARCHAR / MySqlConverter.GUID_LENGTH;
        public static readonly DateTime SQL_DATETIME_MIN = new DateTime(1753, 1, 1, 0, 0, 0);
        public static readonly DateTime SQL_DATETIME_MAX = new DateTime(9999, 12, 31, 23, 59, 59);

        public static object ToDBNull(Guid guid)
        {
            if (guid == Guid.Empty)
                return (object)DBNull.Value;
            return (object)guid;
        }

        public static object ToDBNull<T>(object o)
        {
            if (o == null || o.Equals((object)default(T)))
                return (object)DBNull.Value;
            return o;
        }

        public static object ToDBNull(DateTime d)
        {
            if (d.Year == DateTime.MinValue.Year)
                return (object)DBNull.Value;
            if (d.Year == DateTime.MaxValue.Year)
                return (object)DBNull.Value;
            return (object)d;
        }

        public static object ToDBNull(DateTime? d)
        {
            if (d.HasValue)
                return MySqlConverter.ToDBNull(d.Value);
            return (object)DBNull.Value;
        }

        public static object ToDBNull(int i)
        {
            if (i == 0)
                return (object)DBNull.Value;
            return (object)i;
        }

        public static object ToDBNull(byte b)
        {
            if (b == (byte)0)
                return (object)DBNull.Value;
            return (object)b;
        }

        public static object ToDBNull(long l)
        {
            if (l == 0L)
                return (object)DBNull.Value;
            return (object)l;
        }

        public static object ToDBNull(string s)
        {
            if (string.IsNullOrEmpty(s))
                return (object)DBNull.Value;
            return (object)s;
        }

        public static object ToDBNull(Decimal d)
        {
            if (d == new Decimal(0))
                return (object)DBNull.Value;
            return (object)d;
        }

        public static object ToDBNull(Decimal? d)
        {
            if (d.HasValue)
                return MySqlConverter.ToDBNull(d.Value);
            return (object)DBNull.Value;
        }

        public static int ToInt(object o)
        {
            if (o is DBNull || o == null)
                return 0;
            if (o is int)
                return (int)o;
            throw new Exception("Invalid type passed to ToInt. Should be DBNull, null, or Int32");
        }

        public static object ToDBNull(float f)
        {
            if ((double)f == 0.0)
                return (object)DBNull.Value;
            return (object)f;
        }

        public static DateTime ToDateTime(object obj)
        {
            if (MySqlConverter.IsNull(obj))
                return DateTime.MinValue;
            if (obj is DateTime)
                return (DateTime)obj;
            throw new InvalidCastException("Type must be of System.DateTime.");
        }

        public static T To<T>(object obj)
        {
            return MySqlConverter.To<T>(obj, default(T));
        }

        public static T To<T>(object obj, T defaultValue)
        {
            Type type = typeof(T);
            if (obj is DateTime)
                return (T)Convert.ChangeType(obj, typeof(DateTime));
            if (MySqlConverter.IsNull(obj))
                return defaultValue;
            if (type.IsEnum)
                return (T)Enum.Parse(type, obj.ToString());
            if (obj is T)
                return (T)obj;
            if (obj is string && type == typeof(Guid))
            {
                Guid guid = Guid.Empty;
                if (!string.IsNullOrEmpty(obj.ToString()))
                    guid = new Guid(obj.ToString());
                return (T)Convert.ChangeType(guid, typeof(Guid));
            }
            if (obj is string && string.IsNullOrEmpty(obj as string))
                return defaultValue;
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (obj == null)
                    return defaultValue;
                type = Nullable.GetUnderlyingType(type);
            }
            return (T)Convert.ChangeType(obj, type);
        }

        public static bool IsNull(object obj)
        {
            if (!(obj is DBNull))
                return obj == null;
            return true;
        }

        public static string DelimitedIDdList<T>(List<T> list, string separator)
        {
            PropertyInfo idValueProperty = (PropertyInfo)null;
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (property.IsDefined(typeof(Attribute), true))
                {
                    idValueProperty = property;
                    break;
                }
            }
            if (idValueProperty == (PropertyInfo)null)
                throw new NullReferenceException($"Could not find a property marked with the IdValue attribute for {(object)typeof(T).Name}");
            List<string> delimitedList = new List<string>();
            list.ForEach((Action<T>)(item => delimitedList.Add(idValueProperty.GetValue((object)item, (object[])null).ToString())));
            return string.Join(separator, (IEnumerable<string>)delimitedList);
        }
    }
}
