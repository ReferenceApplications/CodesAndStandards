using CodesAndStandards.Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodesAndStandards.Model
{
	public class Iso3166_1 : Jepit<int>
	{
		protected string _NumericCode = "0";
		public string NumericCode
		{
			get { return _NumericCode; }
			set
			{
				Key = Int32.Parse(value);
				_NumericCode = value;
			}
		}

		protected string _OriginalNumericCode = null;
		public string OriginalNumericCode
		{
			get { return _OriginalNumericCode; }
			set
			{
				int originalKey;
				if (!String.IsNullOrWhiteSpace(value) && Int32.TryParse(value, out originalKey))
				{
					OriginalKey = originalKey;
				}
				else
				{
					OriginalKey = null;
				}
				_OriginalNumericCode = value;
			}
		}

		public string Alpha2Code { get; set; }
		public string Alpha3Code { get; set; }
		public string Iso639_1Code { get; set; }
		public string CountryShortName { get; set; }
	}
}
