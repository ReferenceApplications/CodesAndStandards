using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodesAndStandards;
using System.Linq;

namespace CodeGen.Test
{
	[TestClass]
	public class CodeGenUnitTest
	{
		[TestMethod]
		public void TestGetCurrentHydratedCountryRegions()
		{
			CodeGenEngine engine = new CodeGenEngine("../../../../Data");
			var countryRegions = engine.GetCurrentHydratedCountryRegions();
			Assert.IsTrue(countryRegions.Count() == 249);

			var usa = countryRegions.Single(e => e.CountryRegionId == 840);
			Assert.IsTrue(usa.Subdivisions.Count() == 51);

			foreach (var countryRegion in countryRegions)
			{
				Assert.IsTrue(countryRegion.Iso3166_1s.Count(e => e.Iso639_1Code == "en")  == 1);
			}
		}
	}
}
