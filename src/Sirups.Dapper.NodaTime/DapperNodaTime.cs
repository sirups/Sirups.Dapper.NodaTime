using System.Diagnostics.CodeAnalysis;
using Dapper;
using Sirups.Dapper.NodaTime.TypeHandlers;

namespace Sirups.Dapper.NodaTime {
	[ExcludeFromCodeCoverage]
	public static class DapperNodaTime {
		public static void RegisterAll() {
			SqlMapper.AddTypeHandler(DurationTypeHandler.Instance);
			SqlMapper.AddTypeHandler(InstantTypeHandler.Instance);
			SqlMapper.AddTypeHandler(LocalDateTypeHandler.Instance);
			SqlMapper.AddTypeHandler(LocalTimeTypeHandler.Instance);
			SqlMapper.AddTypeHandler(OffsetTimeTypeHandler.Instance);
			SqlMapper.AddTypeHandler(PeriodTypeHandler.Instance);
		}
	}
}