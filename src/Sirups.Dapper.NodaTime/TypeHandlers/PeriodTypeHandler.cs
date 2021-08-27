using System.Data;
using Dapper;
using NodaTime;

namespace Sirups.Dapper.NodaTime.TypeHandlers {
	public class PeriodTypeHandler : SqlMapper.TypeHandler<Period> {
		private PeriodTypeHandler() {
		}

		public static readonly PeriodTypeHandler Instance = new PeriodTypeHandler();
		
		public override void SetValue(IDbDataParameter parameter, Period value) {
			parameter.Value = value;
		}

		public override Period Parse(object value) {
			if (value is Period period)
				return period;
			
			throw new System.NotImplementedException();
		}
	}
}