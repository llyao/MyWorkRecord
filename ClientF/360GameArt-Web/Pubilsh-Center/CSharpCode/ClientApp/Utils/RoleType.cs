using System.Collections.Generic;

namespace ClientApp.Utils
{
	public class RoleType
	{
		public static List<RoleType> ALL = new List<RoleType>();

		static RoleType()
		{
			ALL.Add(new RoleType("管理员", 1024));
			ALL.Add(new RoleType("总监", 100));
			ALL.Add(new RoleType("组长", 10));
			ALL.Add(new RoleType("QA", 4));
			ALL.Add(new RoleType("程序员", 3));
			ALL.Add(new RoleType("策划", 2));
			ALL.Add(new RoleType("美术", 1));
		}

		public const int MIN_PRIORITY = 1;

		public string RoleName;
		public int Authority;

		public RoleType(string roleName, int authority)
		{
			RoleName = roleName;
			Authority = authority;
		}

		public static string GetRoleName(int authority)
		{
			for(int i = 0; i < ALL.Count; i++)
			{
				if(authority >= ALL[i].Authority)
				{
					return ALL[i].RoleName;
				}
			}
			
			return "";
		}
	}
}