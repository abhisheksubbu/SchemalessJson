using System;
using System.Collections.Generic;
using System.Text;

namespace SchemalessJson.Helpers
{
    public static class CastHelper
    {
        public static dynamic Cast(dynamic source, string castClass)
        {
            // Type.GetType with Convert.ChangeType was a better method.
            // For this, File & DynamicText needs to be implementing IConvertible.
            // If you do this, it will mess up the way other dynamic code to understand types
            // That's why we are using Switch-Case
           switch(castClass)
            {
                case "Text":
                    {
                        return (string)source;
                    }
                case "File":
                    {
                        return (List<Models.File>)source;
                    }
                case "DynamicText":
                    {
                        return (List<string>)source;
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
