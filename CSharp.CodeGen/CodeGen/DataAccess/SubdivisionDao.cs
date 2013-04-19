using CodesAndStandards.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CodesAndStandards.DataAccess
{
	public class SubdivisionDao
	{
		public static readonly string DefaultSubdivisionDirectory = @"Subdivision";
		public static readonly string DefaultSubdivisionJsonFilename = @"Subdivision.json";
		public static readonly string DefaultSubdivisionTypeJsonFilename = @"SubdivisionType.en.json";

		protected string SubdivisionDirectoryPath { get; set; }
		protected string Incits_38DirectoryPath { get; set; }

		public SubdivisionDao(string subdivisionDirectoryPath)
		{
			SubdivisionDirectoryPath = Path.Combine(subdivisionDirectoryPath);
			Incits_38DirectoryPath = Path.Combine(subdivisionDirectoryPath, "Incits_38");
		}

		public List<Subdivision> GetSubdivisions()
		{
			var text = File.ReadAllText(Path.Combine(SubdivisionDirectoryPath, DefaultSubdivisionJsonFilename), Encoding.UTF8);
			JavaScriptSerializer ser = new JavaScriptSerializer();
			return ser.Deserialize<List<Subdivision>>(text);
		}

		public List<SubdivisionType> GetSubdivisionTypes()
		{
			var text = File.ReadAllText(Path.Combine(SubdivisionDirectoryPath, DefaultSubdivisionTypeJsonFilename), Encoding.UTF8);
			JavaScriptSerializer ser = new JavaScriptSerializer();
			return ser.Deserialize<List<SubdivisionType>>(text);
		}

		public List<Incits_38> GetIncits_38s()
		{
			List<Incits_38> incits_38 = new List<Incits_38>();
			var jsonFiles = Directory.EnumerateFiles(Incits_38DirectoryPath, "*.json");
			foreach (var jsonFile in jsonFiles)
			{
				var text = File.ReadAllText(jsonFile, Encoding.UTF8);
				JavaScriptSerializer ser = new JavaScriptSerializer();
				incits_38.AddRange(ser.Deserialize<List<Incits_38>>(text));
			}
			return incits_38;
		}
	}
}
