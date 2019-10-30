using System;
using System.Collections.Generic;
using System.Text;

namespace SchemalessJson
{
    public class FormFieldMapper
    {
        private string FormGuid { get; set; }
        public List<KeyValuePair<string, FieldMapping>> FieldMappings { get; set; }

        public FormFieldMapper(string formGuid)
        {
            FormGuid = formGuid;
            FieldMappings = new List<KeyValuePair<string, FieldMapping>>();
        }

        public void AddField(string fieldName, FieldMapping fieldMap)
        {
            FieldMappings.Add(new KeyValuePair<string, FieldMapping>(fieldName, fieldMap));
        }

        public List<KeyValuePair<string, FieldMapping>> GetFieldMappings()
        {
            return FieldMappings;
        }

        public FieldMapping GetFieldMappingBykey(string key)
        {
            return FieldMappings.Find(f => f.Key == key).Value;
        }
    }
}
