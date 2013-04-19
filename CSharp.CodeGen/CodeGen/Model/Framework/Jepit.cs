using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodesAndStandards.Model.Framework
{
	public abstract class Jepit<TJepit, TKey> : Jepit<TKey>
		where TJepit : Jepit<TKey>, new()
		where TKey : struct
	{
		public virtual TJepit CloneWithDurationFilter(Jepit jepit)
		{
			var effectiveDates = jepit.GetEffectiveDateRange();
			return CloneWithDurationFilter(effectiveDates.Item1, effectiveDates.Item2);
		}

		public virtual TJepit CloneWithPitFilter(DateTime date)
		{
			return CloneWithDurationFilter(date, date);
		}

		public abstract TJepit CloneWithDurationFilter(DateTime dateFrom, DateTime dateTo);

		public virtual TJepit Clone()
		{
			return base.Clone<TJepit>();
		}
	}


	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	public abstract class Jepit<TKey> : Jepit
		where TKey : struct
	{
		public TKey Key { get; set; }
		public Nullable<TKey> OriginalKey { get; set; }


		protected new TJepit Clone<TJepit>()
			where TJepit : Jepit<TKey>, new()
		{
			var returnVal = base.Clone<TJepit>();
			returnVal.Key = Key;
			returnVal.OriginalKey = OriginalKey;
			return returnVal;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public abstract class Jepit
	{
		public DateTime DateCreated { get; set; }
		public DateTime DateEffective { get; set; }
		public DateTime? DateReplaced { get; set; }
		public DateTime? DateEnded { get; set; }
		public string Reason { get; set; }
		public string Author { get; set; }

		protected TJepit Clone<TJepit>()
			where TJepit : Jepit, new()
		{
			return new TJepit()
			{
				Author = Author,
				DateCreated = DateCreated,
				DateEffective = DateEffective,
				DateEnded = DateEnded,
				DateReplaced = DateReplaced,
				Reason = Reason
			};
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="includeAuditDates"></param>
		/// <returns></returns>
		public Tuple<DateTime, DateTime> GetEffectiveDateRange(bool includeAuditDates = false)
		{
			DateTime startDate = DateEffective;
			if (includeAuditDates && startDate > DateCreated)
			{
				startDate = DateCreated;
			}

			DateTime endDate = DateTime.MaxValue;
			if (DateReplaced.HasValue && endDate.AddDays(-1) > DateReplaced.Value)
			{
				endDate = DateReplaced.Value;
			}
			if (DateEnded.HasValue && endDate.AddDays(-1) > DateEnded.Value)
			{
				endDate = DateEnded.Value;
			}
			return new Tuple<DateTime, DateTime>(startDate, endDate);
		}

		/// <summary>
		/// Was effective at one time during the jepit's duration
		/// </summary>
		/// <param name="jepit"></param>
		/// <param name="includeAuditDates"></param>
		/// <returns></returns>
		public bool WasEffectiveDuring(Jepit jepit, bool includeAuditDates = false)
		{
			var dateRange = jepit.GetEffectiveDateRange();
			return WasEffectiveDuring(dateRange.Item1, dateRange.Item2, includeAuditDates);
		}

		/// <summary>
		/// Was effective at one time during the date range
		/// </summary>
		/// <param name="dateRange"></param>
		/// <param name="includeAuditDates"></param>
		/// <returns></returns>
		public bool WasEffectiveDuring(DateTime dateFrom, DateTime dateToAndIncluding, bool includeAuditDates = false)
		{
			return (!includeAuditDates || DateCreated <= dateToAndIncluding)
				&& DateEffective <= dateToAndIncluding
				&& (!DateReplaced.HasValue || DateReplaced > dateFrom)
				&& (!DateEnded.HasValue || DateEnded > dateFrom);
		}

		/// <summary>
		/// Is effective over the entire duration of the jepit
		/// </summary>
		/// <param name="jepit"></param>
		/// <param name="includeAuditDates"></param>
		/// <returns></returns>
		public bool IsEffectiveBetween(Jepit jepit, bool includeAuditDates = false)
		{
			var dateRange = jepit.GetEffectiveDateRange();
			return IsEffectiveBetween(dateRange.Item1, dateRange.Item2, includeAuditDates);
		}

		/// <summary>
		/// Is efftive over the entire duration of the date range
		/// </summary>
		/// <param name="dateRange"></param>
		/// <param name="includeAuditDates"></param>
		/// <returns></returns>
		public bool IsEffectiveBetween(DateTime dateFrom, DateTime dateToAndIncluding, bool includeAuditDates = false)
		{
			return (!includeAuditDates || DateCreated <= dateFrom)
							&& (DateEffective <= dateFrom)
							&& (!DateReplaced.HasValue || DateReplaced > dateToAndIncluding)
							&& (!DateEnded.HasValue || DateEnded > dateToAndIncluding);
		}

		public bool IsEffective(DateTime pit, bool includeAuditDates = false)
		{
			return (!includeAuditDates || DateCreated <= pit)
				&& DateEffective <= pit
				&& (!DateReplaced.HasValue || DateReplaced > pit)
				&& (!DateEnded.HasValue || DateEnded > pit);
		}
	}
}
