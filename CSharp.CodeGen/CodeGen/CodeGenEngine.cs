using CodesAndStandards.DataAccess;
using CodesAndStandards.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodesAndStandards
{
	public class CodeGenEngine
	{
		protected Lazy<CountryRegionDao> _CountryRegionDaoLazy;
		public CountryRegionDao CountryRegionDao { get { return _CountryRegionDaoLazy.Value; } }

		protected Lazy<SubdivisionDao> _SubdivisionDaoLazy;
		public SubdivisionDao SubdivisionDao { get { return _SubdivisionDaoLazy.Value; } }


		public CodeGenEngine(string dataDirectoryPath)
		{
			_CountryRegionDaoLazy = new Lazy<CountryRegionDao>(() => { return new CountryRegionDao(Path.Combine(dataDirectoryPath, CountryRegionDao.DefaultCountryRegionDirectory)); });
			_SubdivisionDaoLazy = new Lazy<SubdivisionDao>(() => { return new SubdivisionDao(Path.Combine(dataDirectoryPath, CountryRegionDao.DefaultCountryRegionDirectory, SubdivisionDao.DefaultSubdivisionDirectory)); });
		}

		public List<CountryRegion> GetCurrentHydratedCountryRegions()
		{
			var countryRegions = CountryRegionDao.GetCountryRegions();
			var countryRegionTypes = CountryRegionDao.GetCountryRegionTypes();
			var iso3166_1s = CountryRegionDao.GetIso3166_1s();
			var subdivisions = SubdivisionDao.GetSubdivisions();
			var subdivisionTypes = SubdivisionDao.GetSubdivisionTypes();
			var incits_38s = SubdivisionDao.GetIncits_38s();

			var now = DateTime.UtcNow;
			subdivisions.ForEach(e => e.Hydrate(incits_38s, subdivisionTypes));
			countryRegions.ForEach(e => e.Hydrate(iso3166_1s, subdivisions, countryRegionTypes));
			return countryRegions.Where(e => e.IsEffective(now)).Select(e => e.CloneWithPitFilter(now)).ToList();
		}
	}
}
