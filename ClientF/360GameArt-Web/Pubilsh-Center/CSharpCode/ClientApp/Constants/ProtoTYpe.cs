using System.Collections.Generic;

namespace ClientApp.Constants
{
	public static class ProtoType
	{
		public const string BOOL = "bool";
		public const string STRING = "string";
		public const string INT32 = "int32";
		public const string UINT32 = "uint32";
		public const string UINT64 = "uint64";
		public const string FLOAT = "float";
		public const string DOUBLE = "double";
		public const string BYTES = "bytes";
		public const string STRUCT = "struct";
		public const string REPEATED = "repeated";

		private static List<string> all = new List<string>();

		static ProtoType()
		{
			all.Add(BOOL);
			all.Add(STRING);
			all.Add(INT32);
			all.Add(FLOAT);
			all.Add(DOUBLE);
			all.Add(BYTES);
			all.Add(UINT32);
			all.Add(UINT64);
			all.Add(REPEATED);
		}

		public static bool IsDefined(string type)
		{
			return all.IndexOf(type) != -1;
		}
	}
}