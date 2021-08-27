using System.Data;
using Dapper;
using NodaTime;

namespace Sirups.Dapper.NodaTime.TypeHandlers {
	public class OffsetTimeTypeHandler : SqlMapper.TypeHandler<OffsetTime> {
		private OffsetTimeTypeHandler() {
		}

		public static readonly OffsetTimeTypeHandler Instance = new OffsetTimeTypeHandler();
		
		public override void SetValue(IDbDataParameter parameter, OffsetTime value) {
			parameter.Value = value;
		}

		public override OffsetTime Parse(object value) {
			if (value is OffsetTime time)
				return time;
			
			throw new System.NotImplementedException();
		}
	}
}