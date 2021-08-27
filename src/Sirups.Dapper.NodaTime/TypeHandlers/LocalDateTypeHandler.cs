using System.Data;
using Dapper;
using NodaTime;

namespace Sirups.Dapper.NodaTime.TypeHandlers {
	public class LocalDateTypeHandler : SqlMapper.TypeHandler<LocalDate> {
		private LocalDateTypeHandler() {
		}

		public static readonly LocalDateTypeHandler Instance = new LocalDateTypeHandler();
		
		public override void SetValue(IDbDataParameter parameter, LocalDate value) {
			parameter.Value = value;
		}

		public override LocalDate Parse(object value) {
			if (value is LocalDate localDate)
				return localDate;
			
			throw new System.NotImplementedException();
		}
	}
}