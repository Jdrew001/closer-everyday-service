using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Data
{
    public class DataReaderHelper : IDataReader, IDisposable, IDataRecord
    {
        private Dictionary<string, int> m_Fields = new Dictionary<string, int>((IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase);
        private DbDataReader m_Reader;

        public DataReaderHelper(DbDataReader reader)
        {
            this.Initialize(reader);
        }

        private void Initialize(DbDataReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader), "Reader cannot be null.");
            if (reader.IsClosed)
                throw new ArgumentException("Reader must be open and positioned at a valid record set.", nameof(reader));
            this.m_Reader = reader;
            this.RefreshFields();
        }

        private void RefreshFields()
        {
            this.m_Fields.Clear();
            for (int ordinal = 0; ordinal < this.m_Reader.FieldCount; ++ordinal)
                this.m_Fields[this.m_Reader.GetName(ordinal)] = ordinal;
        }

        public Dictionary<string, int> Fields
        {
            get
            {
                return this.m_Fields;
            }
        }

        public T Get<T>(string name)
        {
            return this.Get<T>(name, default(T));
        }

        public T Get<T>(string name, T defaultValue)
        {
            T obj = defaultValue;
            if (this.m_Fields.ContainsKey(name))
                obj = MySqlConverter.To<T>(this.m_Reader[this.m_Fields[name]]);
            return obj;
        }

        public static implicit operator DataReaderHelper(DbDataReader reader)
        {
            return new DataReaderHelper(reader);
        }

        public bool HasRows
        {
            get
            {
                return this.m_Reader.HasRows;
            }
        }

        public bool Read()
        {
            return this.m_Reader.Read();
        }

        public bool NextResult()
        {
            bool flag = this.m_Reader.NextResult();
            if (flag)
                this.RefreshFields();
            return flag;
        }

        public object this[string columnName]
        {
            get
            {
                if (!this.HasColumn(columnName))
                    return (object)DBNull.Value;
                return this.m_Reader[this.m_Fields[columnName]];
            }
        }

        public DbDataReader BaseReader
        {
            get
            {
                return this.m_Reader;
            }
        }

        public string GetName(int ordinal)
        {
            return this.m_Reader.GetName(ordinal);
        }

        public bool HasColumn(string columnName)
        {
            return this.m_Fields.ContainsKey(columnName);
        }

        public void Dispose()
        {
            this.m_Reader.Dispose();
        }

        void IDataReader.Close()
        {
            this.m_Reader.Close();
        }

        int IDataReader.Depth
        {
            get
            {
                return this.m_Reader.Depth;
            }
        }

        DataTable IDataReader.GetSchemaTable()
        {
            return this.m_Reader.GetSchemaTable();
        }

        bool IDataReader.IsClosed
        {
            get
            {
                return this.m_Reader.IsClosed;
            }
        }

        int IDataReader.RecordsAffected
        {
            get
            {
                return this.m_Reader.RecordsAffected;
            }
        }

        int IDataRecord.FieldCount
        {
            get
            {
                return this.m_Reader.FieldCount;
            }
        }

        bool IDataRecord.GetBoolean(int i)
        {
            return this.m_Reader.GetBoolean(i);
        }

        byte IDataRecord.GetByte(int i)
        {
            return this.m_Reader.GetByte(i);
        }

        long IDataRecord.GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length)
        {
            return this.m_Reader.GetBytes(i, fieldOffset, buffer, bufferOffset, length);
        }

        char IDataRecord.GetChar(int i)
        {
            return this.m_Reader.GetChar(i);
        }

        long IDataRecord.GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length)
        {
            return this.m_Reader.GetChars(i, fieldOffset, buffer, bufferOffset, length);
        }

        IDataReader IDataRecord.GetData(int i)
        {
            return ((IDataRecord)this.m_Reader).GetData(i);
        }

        string IDataRecord.GetDataTypeName(int i)
        {
            return this.m_Reader.GetDataTypeName(i);
        }

        DateTime IDataRecord.GetDateTime(int i)
        {
            return this.m_Reader.GetDateTime(i);
        }

        Decimal IDataRecord.GetDecimal(int i)
        {
            return this.m_Reader.GetDecimal(i);
        }

        double IDataRecord.GetDouble(int i)
        {
            return this.m_Reader.GetDouble(i);
        }

        Type IDataRecord.GetFieldType(int i)
        {
            return this.m_Reader.GetFieldType(i);
        }

        float IDataRecord.GetFloat(int i)
        {
            return this.m_Reader.GetFloat(i);
        }

        Guid IDataRecord.GetGuid(int i)
        {
            return this.m_Reader.GetGuid(i);
        }

        short IDataRecord.GetInt16(int i)
        {
            return this.m_Reader.GetInt16(i);
        }

        int IDataRecord.GetInt32(int i)
        {
            return this.m_Reader.GetInt32(i);
        }

        long IDataRecord.GetInt64(int i)
        {
            return this.m_Reader.GetInt64(i);
        }

        string IDataRecord.GetName(int i)
        {
            return this.m_Reader.GetName(i);
        }

        int IDataRecord.GetOrdinal(string name)
        {
            return this.m_Reader.GetOrdinal(name);
        }

        string IDataRecord.GetString(int i)
        {
            return this.m_Reader.GetString(i);
        }

        object IDataRecord.GetValue(int i)
        {
            return this.m_Reader.GetValue(i);
        }

        int IDataRecord.GetValues(object[] values)
        {
            return this.m_Reader.GetValues(values);
        }

        bool IDataRecord.IsDBNull(int i)
        {
            return this.m_Reader.IsDBNull(i);
        }

        object IDataRecord.this[string name]
        {
            get
            {
                return this.m_Reader[name];
            }
        }

        object IDataRecord.this[int i]
        {
            get
            {
                return this.m_Reader[i];
            }
        }
    }
}
