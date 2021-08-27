using System.Data;
using Dapper;
using NodaTime;

namespace Sirups.Dapper.NodaTime.TypeHandlers {
	public class InstantTypeHandler : SqlMapper.TypeHandler<Instant> {
		private InstantTypeHandler() {
		}

		public static readonly InstantTypeHandler Instance = new InstantTypeHandler();
		
		public override void SetValue(IDbDataParameter parameter, Instant value) {
			parameter.Value = value;
		}

		public override Instant Parse(object value) {
			if (value is Instant instant)
				return instant;
			
			throw new System.NotImplementedException();
		}
	}
}