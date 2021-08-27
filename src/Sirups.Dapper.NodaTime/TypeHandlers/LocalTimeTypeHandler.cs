using System.Data;
using Dapper;
using NodaTime;

namespace Sirups.Dapper.NodaTime.TypeHandlers {
	public class LocalTimeTypeHandler : SqlMapper.TypeHandler<LocalTime> {
		private LocalTimeTypeHandler() {
		}

		public static readonly LocalTimeTypeHandler Instance = new LocalTimeTypeHandler();
		
		public override void SetValue(IDbDataParameter parameter, LocalTime value) {
			parameter.Value = value;
		}

		public override LocalTime Parse(object value) {
			if (value is LocalTime localTime)
				return localTime;
			
			throw new System.NotImplementedException();
		}
	}
}