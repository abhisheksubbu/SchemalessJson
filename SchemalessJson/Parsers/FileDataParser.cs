using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SchemalessJson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchemalessJson.Parsers
{
    public class FileDataParser : IDataParser
    {
        public FormData RetrieveData(dynamic formData, string fieldName, FieldMapping fieldMapping)
        {
            dynamic fieldValue = string.Empty;
            if (formData[fieldName] != null)
            {
                fieldValue = formData[fieldName];
            }
            else
            {
                fieldValue = string.Empty;
            }
            List<Models.File> fileList = JsonConvert.DeserializeObject<List<Models.File>>(JsonConvert.SerializeObject(formData[fieldName]));
            FormData data = new FormData();
            data.CastClass = "File";
            data.Data = fileList;
            return data;
        }

        public dynamic UpdateData(dynamic formData, string fieldName, dynamic fieldValue)
        {
            if (formData[fieldName] == null)
            {
                formData[fieldName] = new JArray();
            }
            dynamic dynamicTextList = fieldValue;
            JArray jArrayData = new JArray();
            foreach (dynamic record in dynamicTextList)
            {
                jArrayData.Add(JToken.FromObject(record));
            }
            formData[fieldName] = jArrayData;
            return formData;
        }
    }
}
