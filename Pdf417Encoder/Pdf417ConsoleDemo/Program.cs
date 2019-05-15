using System;
using System.IO;
using Pdf417EncoderLibrary;

namespace Pdf417ConsoleDemo
{
class Program
	{
	static void Main(string[] args)
		{
		#if DEBUG
		// current directory
		string CurDir = Environment.CurrentDirectory;
		int Ptr = CurDir.IndexOf("\\bin\\debug", StringComparison.OrdinalIgnoreCase);
		if(Ptr > 0)
			{ 
			string WorkDir = CurDir.Substring(0, Ptr) + "\\Work";
			if(Directory.Exists(WorkDir)) Environment.CurrentDirectory = WorkDir;
			}
		#endif

		try
			{
			Pdf417CommandLine.Encode(args);
			Console.WriteLine("Success");
			}
		catch (Exception Ex)
			{
			if(Ex.Message == "help")
				Console.WriteLine(Pdf417CommandLine.Help);
			else
				Console.WriteLine("Error:\r\n" + Ex.Message);
			}

		#if DEBUG
		Console.WriteLine("Press any key to close the window.");
		Console.ReadKey();
		#endif
		}
	}
}
