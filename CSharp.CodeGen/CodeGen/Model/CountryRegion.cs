using CodesAndStandards.Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodesAndStandards.Model
{
	public class CountryRegion : Jepit<int>
	{
		public int CountryRegionId { get { return Key; } set { Key = value; } }
		public int? OriginalCountryRegionId { get { return OriginalKey; } set { OriginalKey = value; } }
		public int CountryRegionTypeId { get; set; }
	}
}
