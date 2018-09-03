using System.Data;
using ClientCore.DB;

namespace ClientApp.DB
{
	public class ProjectVersionData:BaseTableData
	{
		public int Id;
		public string Name;
		public string DesignerSvn;
		public string ClientSvn;
		public string ArtSvn;
		public string ContactInfo;

		public override void ReadFrom(DataRow collection)
		{
			base.ReadFrom(collection);
			Id = (int)(collection[0]);
			Name = collection[1].ToString();
			DesignerSvn = collection[2].ToString();
			ClientSvn = collection[3].ToString();
			ArtSvn = collection[4].ToString();
			ContactInfo = collection[5].ToString();
		}
	}
}