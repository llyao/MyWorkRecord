using System;
using System.Collections;
using System.IO;

namespace ClientCore.Config.Reader
{
	public class IniReaderWithoutSection
	{
		private Hashtable hashtable;
		private string[] textData;

		public IniReaderWithoutSection()
		{
			hashtable = new Hashtable();
		}

		public void Read(string fileName)
		{
			var text = LoadIni(fileName);
			if(text != null)
			{
				textData = text.Split('\n');
				for(var i = 0; i < textData.Length; i++)
				{
					var currentLine = textData[i].Trim();
					if(!currentLine.Equals(string.Empty))
					{
						//双斜杆表示注释
						if(!currentLine.Substring(0, 2).Equals("//"))
						{
							var key = currentLine.Substring(0, currentLine.IndexOf('='));
							var value = currentLine.Substring(currentLine.IndexOf('=') + 1, currentLine.Length - currentLine.IndexOf('=') - 1);
							hashtable.Add(key, value);
						}
					}
				}
			}
		}

		//读整数
		public int ReadInt( string key, int defaultValue = 0)
		{
			var result = defaultValue;
			if (hashtable.ContainsKey(key))
			{
				result = Convert.ToInt32(hashtable[key]);
			}
			return result;
		}

		public string ReadString(string key, string defaultVal = null)
		{
			var result = defaultVal;
			if (hashtable.ContainsKey(key))
			{
				result = hashtable[key].ToString();
			}
			return result;
		}

		public float ReadSingle(string key, float defaultValue = 0.0f)
		{
			var result = defaultValue;
			if (hashtable.ContainsKey(key))
			{
				result = Convert.ToSingle(hashtable[key]);
			}
			return result;
		}

		public double ReadDouble( string key, double defaultValue = 0f)
		{
			var result = defaultValue;
			if (hashtable.ContainsKey(key))
			{
				result = Convert.ToDouble(hashtable[key]);
			}
			return result;
		}

		/// <summary>
		/// 写入
		/// </summary>
		/// <param name="key"></param>
		/// <param name="val"></param>
		public void WriteString(string key, string val)
		{

			hashtable.Add(key, val);
		}

		/// <summary>
		/// 加载text文件
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		private string LoadIni(string url)
		{
			var text = string.Empty;
			text = File.ReadAllText(url);
			return text;
		}
	}
}