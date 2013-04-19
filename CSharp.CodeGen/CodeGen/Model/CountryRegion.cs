using CodesAndStandards.Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodesAndStandards.Model
{
	public class CountryRegion : Jepit<CountryRegion, int>
	{
		public int CountryRegionId { get { return Key; } set { Key = value; } }
		public int? OriginalCountryRegionId { get { return OriginalKey; } set { OriginalKey = value; } }
		public int CountryRegionTypeId { get; set; }
		public CountryRegionType CountryRegionType { get; set; }

		protected List<Iso3166_1> _Iso3166_1s = new List<Iso3166_1>();
		public List<Iso3166_1> Iso3166_1s { get { return _Iso3166_1s; } set { _Iso3166_1s = value; } }

		protected List<Subdivision> _Subdivisions = new List<Subdivision>();
		public List<Subdivision> Subdivisions { get { return _Subdivisions; } set { _Subdivisions = value; } }

		public void Hydrate(List<Iso3166_1> iso3166_1s, List<Subdivision> subdivisions, List<CountryRegionType> countryRegionTypes)
		{
			CountryRegionType = countryRegionTypes.Single(e => CountryRegionTypeId == e.CountryRegionTypeId);
			_Subdivisions = subdivisions.Where(e => e.CountryRegionId == CountryRegionId && e.WasEffectiveDuring(this)).Select(e => e.CloneWithDurationFilter(this)).ToList();
			_Iso3166_1s = iso3166_1s.Where(e => e.Key == Key && e.WasEffectiveDuring(this)).ToList();
		}

		public override CountryRegion CloneWithDurationFilter(DateTime dateFrom, DateTime dateTo)
		{
			var returnVal = base.Clone();
			returnVal.Iso3166_1s = Iso3166_1s.Where(e => e.WasEffectiveDuring(dateFrom, dateTo)).ToList();
			returnVal.Subdivisions = Subdivisions.Where(e => e.WasEffectiveDuring(dateFrom, dateTo)).Select(e => e.CloneWithDurationFilter(dateFrom, dateTo)).ToList();
			return returnVal;
		}

		public override CountryRegion Clone()
		{
			var returnVal = base.Clone();
			returnVal.CountryRegionTypeId = CountryRegionTypeId;
			returnVal.CountryRegionType = CountryRegionType;
			returnVal.Iso3166_1s = Iso3166_1s;
			returnVal.Subdivisions = Subdivisions;
			return returnVal;
		}
	}
}
