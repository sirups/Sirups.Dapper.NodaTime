using System.Data;
using Dapper;
using NodaTime;

namespace Sirups.Dapper.NodaTime.TypeHandlers {
	public class DurationTypeHandler : SqlMapper.TypeHandler<Duration> {
		private DurationTypeHandler() {
		}

		public static readonly DurationTypeHandler Instance = new DurationTypeHandler();
		
		public override void SetValue(IDbDataParameter parameter, Duration value) {
			parameter.Value = value;
		}

		public override Duration Parse(object value) {
			if (value is Period period)
				return period.ToDuration();
			
			throw new System.NotImplementedException();
		}
	}
}