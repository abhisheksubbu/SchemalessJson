using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SchemalessJson.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchemalessJson.Parsers
{
    public class DynamicTextDataParser : IDataParser
    {
        public FormData RetrieveData(dynamic formData, string fieldName, FieldMapping fieldMapping)
        {
            var fieldValue = string.Empty;
            if (formData[fieldName] != null)
            {
                var dynamicTextList = JArray.Parse(JsonConvert.SerializeObject(formData[fieldName]));
                foreach (dynamic record in dynamicTextList)
                {
                    fieldValue += $"{ record.Sequence} - {record.Data}" + System.Environment.NewLine;
                }
            }
            else
            {
                fieldValue = string.Empty;
            }
            List<DynamicText> dynamicTextValueList = JsonConvert.DeserializeObject<List<DynamicText>>(JsonConvert.SerializeObject(formData[fieldName]));
            FormData data = new FormData();
            data.CastClass = "DynamicText";
            data.Data = dynamicTextValueList;
            return data;
        }

        private int List<T>(dynamic dynamic)
        {
            throw new NotImplementedException();
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
