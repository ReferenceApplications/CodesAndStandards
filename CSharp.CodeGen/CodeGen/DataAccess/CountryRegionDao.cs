using CodesAndStandards.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CodesAndStandards.DataAccess
{
	public class CountryRegionDao
	{
		public static readonly string DefaultCountryRegionDirectory = @"CountryRegion";
		public static readonly string DefaultCountryRegionJsonFilename = @"CountryRegion.json";
		public static readonly string DefaultCountryRegionTypeJsonFilename = @"CountryRegionType.en.json";


		protected string CountryRegionDirectoryPath { get; set; }
		protected string Iso3166_1DirectoryPath { get; set; }


		public CountryRegionDao(string countryRegionDirectoryPath)
		{
			CountryRegionDirectoryPath = countryRegionDirectoryPath;
			Iso3166_1DirectoryPath = Path.Combine(countryRegionDirectoryPath, "Iso3166.1");
		}

		public List<CountryRegion> GetCountryRegions()
		{
			var text = File.ReadAllText(Path.Combine(CountryRegionDirectoryPath, DefaultCountryRegionJsonFilename), Encoding.UTF8);
			JavaScriptSerializer ser = new JavaScriptSerializer();
			return ser.Deserialize<List<CountryRegion>>(text);
		}

		public List<CountryRegionType> GetCountryRegionTypes()
		{
			var text = File.ReadAllText(Path.Combine(CountryRegionDirectoryPath, DefaultCountryRegionTypeJsonFilename), Encoding.UTF8);
			JavaScriptSerializer ser = new JavaScriptSerializer();
			return ser.Deserialize<List<CountryRegionType>>(text);
		}

		public List<Iso3166_1> GetIso3166_1s()
		{
			List<Iso3166_1> iso3166_1s = new List<Iso3166_1>();
			var jsonFiles = Directory.EnumerateFiles(Iso3166_1DirectoryPath, "*.json");
			foreach (var jsonFile in jsonFiles)
			{
				var text = File.ReadAllText(jsonFile, Encoding.UTF8);
				JavaScriptSerializer ser = new JavaScriptSerializer();
				iso3166_1s.AddRange(ser.Deserialize<List<Iso3166_1>>(text));
			}
			return iso3166_1s;
		}
	}
}
