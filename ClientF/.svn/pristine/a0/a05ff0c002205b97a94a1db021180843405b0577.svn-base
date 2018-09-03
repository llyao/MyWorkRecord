using System;
using System.Collections;
using System.IO;

namespace ClientCore.Config.Reader
{
	public class IniReaderWithSection
	{
		private Hashtable hashtable;
		private string[] textData;

		public IniReaderWithSection()
		{
			hashtable = new Hashtable();
		}

		public void Read(string fileName)
		{
			var currentSection = string.Empty;
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
							//[]表示节
							if(currentLine.StartsWith("[") && currentLine.EndsWith("]"))
							{
								currentSection = currentLine.Substring(1, currentLine.Length - 2);
							}
							else
							{
								var key = currentLine.Substring(0, currentLine.IndexOf('='));
								var value = currentLine.Substring(currentLine.IndexOf('=') + 1, currentLine.Length - currentLine.IndexOf('=') - 1);
								if(hashtable.ContainsKey(currentSection))
								{
									var hashtable = (Hashtable)this.hashtable[currentSection];
									hashtable.Add(key, value);
								}
								else
								{
									var hashtable2 = new Hashtable();
									hashtable2.Add(key, value);
									hashtable.Add(currentSection, hashtable2);
								}
							}
						}
					}
				}
			}
		}

		//读整数
		public int ReadInt(string section, string key, int defaultValue)
		{
			var result = defaultValue;
			if(hashtable.ContainsKey(section))
			{
				var hashtable = (Hashtable)this.hashtable[section];
				if(hashtable.ContainsKey(key))
				{
					result = Convert.ToInt32(hashtable[key]);
				}
			}
			return result;
		}

		public string ReadString(string section, string key, string defaultVal)
		{
			var result = defaultVal;
			if(hashtable.ContainsKey(section))
			{
				var hashtable = (Hashtable)this.hashtable[section];
				if(hashtable.ContainsKey(key))
				{
					result = hashtable[key].ToString();
				}
			}
			return result;
		}

		public float ReadSingle(string section, string key, float defaultValue)
		{
			var result = defaultValue;
			if(hashtable.ContainsKey(section))
			{
				var hashtable = (Hashtable)this.hashtable[section];
				if(hashtable.ContainsKey(key))
				{
					result = Convert.ToSingle(hashtable[key]);
				}
			}
			return result;
		}

		public double ReadDouble(string section, string key, double defaultValue)
		{
			var result = defaultValue;
			if(hashtable.ContainsKey(section))
			{
				var hashtable = (Hashtable)this.hashtable[section];
				if(hashtable.ContainsKey(key))
				{
					result = Convert.ToDouble(hashtable[key]);
				}
			}
			return result;
		}

		/// <summary>
		/// 写入
		/// </summary>
		/// <param name="section"></param>
		/// <param name="key"></param>
		/// <param name="val"></param>
		public void WriteString(string section, string key, string val)
		{
			if(IsSectionExists(section))
			{
				var hashtable = (Hashtable)this.hashtable[section];
				if(hashtable.ContainsKey(key))
				{
					hashtable[key] = val;
				}
				else
				{
					hashtable.Add(key, val);
				}
			}
			else
			{
				var hashtable2 = new Hashtable();
				hashtable.Add(section, hashtable2);
				hashtable2.Add(key, val);
			}
		}

		/// <summary>
		/// 字段是否存在
		/// </summary>
		/// <param name="section"></param>
		/// <returns></returns>
		public bool IsSectionExists(string section)
		{
			return hashtable.ContainsKey(section);
		}

		/// <summary>
		/// 获取字段
		/// </summary>
		/// <param name="section"></param>
		/// <returns></returns>
		public Hashtable GetSection(string section)
		{
			if(IsSectionExists(section))
			{
				return hashtable[section] as Hashtable;
			}
			return null;
		}


		/// <summary>
		/// value值是否存在
		/// </summary>
		/// <param name="section"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool IsValueExists(string section, string key)
		{
			return IsSectionExists(section) && ((Hashtable)hashtable[section]).ContainsKey(key);
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