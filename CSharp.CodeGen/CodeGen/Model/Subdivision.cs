using CodesAndStandards.Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodesAndStandards.Model
{
	public class Subdivision : Jepit<Subdivision, int>
	{
		public int SubdivisionId { get { return Key; } set { Key = value; } }
		public int? OriginalSubdivisionId { get { return OriginalKey; } set { OriginalKey = value; } }
		public int CountryRegionId { get; set; }
		public int SubdivisionTypeId { get; set; }
		public SubdivisionType SubdivisionType { get; set; }

		protected List<Incits_38> _Incits_38s = new List<Incits_38>();
		public List<Incits_38> Incits_38s { get { return _Incits_38s; } set { _Incits_38s = value; } }

		public Subdivision()
		{

		}

		public void Hydrate(List<Incits_38> incits_38s, List<SubdivisionType> subdivisionTypes)
		{
			SubdivisionType = subdivisionTypes.Single(e => SubdivisionTypeId == e.SubdivisionTypeId);
			_Incits_38s = incits_38s.Where(e => e.Key == Key && e.WasEffectiveDuring(this)).ToList();
		}

		public override Subdivision CloneWithDurationFilter(DateTime dateFrom, DateTime dateTo)
		{
			var returnVal = Clone();
			returnVal.Incits_38s = Incits_38s.Where(e => e.WasEffectiveDuring(dateFrom, dateTo)).ToList();
			return returnVal;
		}

		public override Subdivision Clone()
		{
			var returnVal = base.Clone();
			returnVal.Incits_38s = Incits_38s.ToList();
			returnVal.SubdivisionTypeId = SubdivisionTypeId;
			returnVal.SubdivisionType = SubdivisionType;
			returnVal.CountryRegionId = CountryRegionId;
			return returnVal;
		}
	}
}
