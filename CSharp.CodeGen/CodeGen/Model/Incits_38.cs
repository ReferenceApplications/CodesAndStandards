using CodesAndStandards.Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodesAndStandards.Model
{
	public class Incits_38 : Jepit<int>
	{
		public int SubdivisionId { get { return Key; } set { Key = value; } }

		public string NumericCode { get; set; }
		public string UspsCode { get; set; }
		public string Iso639_1Code { get; set; }
		public string Name { get; set; }
	}
}
