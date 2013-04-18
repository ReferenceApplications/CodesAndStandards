using CodesAndStandards.Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodesAndStandards.Model
{
	public class Subdivision : Jepit<int>
	{
		public int SubdivisionId { get { return Key; } set { Key = value; } }
		public int? OriginalSubdivisionId { get { return OriginalKey; } set { OriginalKey = value; } }
		public int SubdivisionTypeId { get; set; }
	}
}
