using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SchemalessJson.Helpers;
using SchemalessJson.Models;
using SchemalessJson.Parsers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reflection;

namespace SchemalessJson
{
    class Program
    {
        static FormFieldMapper mapper = new FormFieldMapper("1234-5678");
        static void Main(string[] args)
        {
            #region Simulating the form created event that created FieldMappings
            mapper.AddField("sif_bookTitle",
                new FieldMapping
                {
                    FieldLabel = "Book Title",
                    FieldName = "sif_bookTitle",
                    FieldAPIName = "Book_Title__c",
                    DataParserClass = "TextDataParser"
                });
            mapper.AddField("sif_penName",
                new FieldMapping
                {
                    FieldLabel = "Pen Name",
                    FieldName = "sif_penName",
                    FieldAPIName = "Pen_Name__c",
                    DataParserClass = "TextDataParser"
                });
            mapper.AddField("sif_coverImages",
               new FieldMapping
               {
                   FieldLabel = "Cover Images",
                   FieldName = "sif_coverImages",
                   FieldAPIName = "",
                   DataParserClass = "FileDataParser"
               });
            mapper.AddField("sif_manuscripts",
               new FieldMapping
               {
                   FieldLabel = "Manuscripts",
                   FieldName = "sif_manuscripts",
                   FieldAPIName = "",
                   DataParserClass = "FileDataParser"
               });
            mapper.AddField("sif_characters",
               new FieldMapping
               {
                   FieldLabel = "Characters",
                   FieldName = "sif_characters",
                   FieldAPIName = "",
                   DataParserClass = "DynamicTextDataParser"
               });

            #endregion

            
            Dictionary<string, dynamic> fieldNameValuePairsToUpdate = new Dictionary<string, dynamic>();
            fieldNameValuePairsToUpdate.Add("sif_bookTitle", "My Book Title Number 2");
            fieldNameValuePairsToUpdate.Add("sif_penName", "My Pen Name");
            fieldNameValuePairsToUpdate.Add("sif_coverImages", new List<Models.File> {
                                                new Models.File { ID=12345, Name="cover1.jpg", Url="https://something.authorsolutions.com/cover1.jpg"},
                                                new Models.File { ID=23456, Name="cover2.jpg", Url="https://something.authorsolutions.com/cover2.jpg"},
                                                new Models.File { ID=34567, Name="cover3.jpg", Url="https://something.authorsolutions.com/cover3.jpg"}
            });
            fieldNameValuePairsToUpdate.Add("sif_characters", new List<DynamicText> {
                                                new DynamicText { Sequence=1, Data="Tom" },
                                                new DynamicText { Sequence=2, Data="Jerry" }
            });

            #region Simulating Form Save Action
            dynamic formData = JValue.Parse(System.IO.File.ReadAllText("formData.json"));
            foreach (string fieldName in fieldNameValuePairsToUpdate.Keys)
            {
                FieldMapping fieldMapping = mapper.GetFieldMappingBykey(fieldName);
                IDataParser parser = DataParserFactory.GetParser(fieldMapping.DataParserClass);
                formData = parser.UpdateData(formData, fieldName, fieldNameValuePairsToUpdate[fieldName]);
            }
            #endregion

            // Save FormDataJSON
            System.IO.File.WriteAllText("formData.json", JsonConvert.SerializeObject(formData, Formatting.Indented));

            #region Retrieve Data for Summary
            Dictionary<string, FormData> fieldNameValuePairsToDisplay= new Dictionary<string, FormData>();
            foreach(var field in formData)
            {
                FieldMapping fieldMapping = mapper.GetFieldMappingBykey(field.Name);
                IDataParser parser = DataParserFactory.GetParser(fieldMapping.DataParserClass);
                fieldNameValuePairsToDisplay.Add(fieldMapping.FieldLabel, parser.RetrieveData(formData, field.Name, fieldMapping));
            }
            #endregion

            //Trying to Cast the FormData
            foreach (FormData dataObject in fieldNameValuePairsToDisplay.Values)
            {
                var strongTypedData = CastHelper.Cast(dataObject.Data, dataObject.CastClass);
            }
        }
    }
}
