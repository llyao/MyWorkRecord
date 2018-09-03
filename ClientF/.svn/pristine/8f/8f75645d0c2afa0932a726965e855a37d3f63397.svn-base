using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace ClientCore.Utils
{
	public class Zipper : IDisposable
	{
		private ZipOutputStream zip;
		private MemoryStream mem;
		private byte[] buffer = new byte[4096];

		public Zipper()
		{
			mem = new MemoryStream();
			zip = new ZipOutputStream(mem);
		}

		public void Append(string name, string file)
		{
			ZipEntry entry = new ZipEntry(name);
			FileInfo info = new FileInfo(file);
			entry.DateTime = info.LastWriteTime;
			entry.IsUnicodeText = true;         // Mono 移动版只支持utf8，其他的i18n.dll都被删掉了。

			zip.PutNextEntry(entry);
			using (var reader = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				StreamUtils.Copy(reader, zip, buffer);
			}
			zip.CloseEntry();
		}

		public void Append(string name, Stream stream)
		{
			var entry = new ZipEntry(name)
			{
				DateTime = DateTime.Now,
				IsUnicodeText = true
			};
			zip.PutNextEntry(entry);
			StreamUtils.Copy(stream, zip, buffer);
			zip.CloseEntry();
		}

		public void Append(string name, byte[] data)
		{
			var ms = new MemoryStream(data);
			Append(name, ms);
		}

		public byte[] Finish()
		{
			zip.IsStreamOwner = true;
			zip.Close();

			var result = mem.ToArray();
			mem.Close();
			return result;
		}

		public void Dispose()
		{
			zip.Dispose();
			mem.Dispose();
			buffer = null;
		}
	}
}