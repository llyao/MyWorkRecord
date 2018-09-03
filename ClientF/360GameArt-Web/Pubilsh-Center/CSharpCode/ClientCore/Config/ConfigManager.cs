using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using ClientCore.Config.Reader;
using ClientCore.Runtime;
using ClientCore.Utils;

namespace ClientCore.Config
{
	public class ConfigManager : Manager<ConfigManager>
	{
		private IniReaderWithoutSection settingReader;
		private XMLReader xmlReader;
		public IWork LoadAllConfig()
		{
			return new AtomWork(StartLoad);
		}

		protected virtual void StartLoad()
		{
			settingReader = new IniReaderWithoutSection();

			settingReader.Read(PathUtil.GetPath("Config/setting.txt"));
		}

		public string GetSetting(string value)
		{
			return settingReader.ReadString(value);
		}

		public T GetXML<T>(string filename) where T : IXmlParser, new()
		{
			if(xmlReader == null)
			{
				xmlReader = new XMLReader();
			}
			return xmlReader.Read<T>(filename);
		}

		public T UpdateXML<T>(string fileName, T t) where T : IXmlParser
		{
			if (xmlReader == null)
			{
				xmlReader = new XMLReader();
			}
			return xmlReader.Update<T>(fileName, t);
		}
	}
}