using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace SchemalessJson.Parsers
{
    public class TextDataParser : IDataParser
    {
        public FormData RetrieveData(dynamic formData, string fieldName, FieldMapping fieldMapping)
        {
            string fieldValue = string.Empty;
            if (formData[fieldName] != null)
            {
                fieldValue = formData[fieldName];
            }
            else
            {
                fieldValue = string.Empty;
            }
            FormData data = new FormData();
            data.CastClass = "Text";
            data.Data = fieldValue;
            return data;
;
        }

        public dynamic UpdateData(dynamic formData, string fieldName, dynamic fieldValue)
        {
            if (formData[fieldName] == null)
            {
                formData[fieldName] = new JObject();
            }
            formData[fieldName] = fieldValue;
            return formData;
        }
    }
}
