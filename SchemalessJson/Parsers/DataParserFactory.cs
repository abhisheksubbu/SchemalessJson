using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SchemalessJson.Parsers
{
    public static class DataParserFactory
    {
        public static IDataParser GetParser(string parserClass)
        {
            var parser = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .First(x => typeof(IDataParser).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract && x.Name == parserClass);
            var dataParser = Activator.CreateInstance(parser);
            return (IDataParser)dataParser;
        }
    }
}
