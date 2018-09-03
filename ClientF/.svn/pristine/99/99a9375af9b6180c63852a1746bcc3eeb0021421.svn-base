using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ClientCore.Config.Reader
{
	public class XMLReader
	{
		private readonly Dictionary<string, IXmlParser> cache = new Dictionary<string, IXmlParser>();

		public T Read<T>(string fileName) where T : IXmlParser, new()
		{
			if(cache.ContainsKey(fileName))
			{
				return (T)(cache[fileName]);
			}

			if(!File.Exists(fileName))
			{
				return default(T);
			}

			var doc = new XmlDocument();
			doc.Load(fileName);
			var t = new T();
			t.Parse(doc);
			cache.Add(fileName, t);
			return t;
		}

		public T Update<T>(string fileName, T t) where T : IXmlParser
		{
			cache[fileName] = t;
			return t;
		}
	}
}