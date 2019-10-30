using System;
using System.Collections.Generic;
using System.Text;

namespace SchemalessJson.Parsers
{
    public interface IDataParser
    {
        dynamic UpdateData(dynamic formData, string fieldName, dynamic fieldValue);
        FormData RetrieveData(dynamic formData, string fieldName, FieldMapping fieldMapping);
    }
}
