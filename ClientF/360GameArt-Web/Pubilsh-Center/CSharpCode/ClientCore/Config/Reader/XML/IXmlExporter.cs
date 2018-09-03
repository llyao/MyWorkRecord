using System.Xml;

namespace ClientCore.Config.Reader
{
	public interface IXmlExporter
	{
		XmlDocument Export();
	}
}