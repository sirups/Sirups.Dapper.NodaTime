using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using NodaTime;
using Sirups.Dapper.NodaTime.TypeHandlers;
using Xunit;

namespace Sirups.Dapper.NodaTime.Tests.TypeHandlers {
	public class LocalDateTypeHandlerTests {
		[Fact]
		public async Task ProperlySetsValue() {
			//Arrange
			LocalDate localDate = LocalDate.FromYearMonthWeekAndDay(2020, 2, 1, IsoDayOfWeek.Monday);
			IDbDataParameter parameter = new DbDataParameterStub();

			//Act
			LocalDateTypeHandler.Instance.SetValue(parameter, localDate);

			//Assert
			Assert.Equal(localDate, parameter.Value);
		}

		[Fact]
		public async Task ParsesSupportedType() {
			//Arrange
			LocalDate localDate = LocalDate.FromYearMonthWeekAndDay(2020, 2, 1, IsoDayOfWeek.Monday);

			//Act
			var result = LocalDateTypeHandler.Instance.Parse(localDate);

			//Assert
			Assert.Equal(localDate, result);
		}

		[Fact]
		public async Task ThrowsNotImplementedOnUnspportedTypes() {
			//Arrange
			List<object> types = new List<object> {
				DateTime.Now,
				DateTimeOffset.Now
			};

			//Act
			foreach (object type in types) {
				//Assert
				Assert.Throws<NotImplementedException>(() => LocalDateTypeHandler.Instance.Parse(type));
			}
		}
	}
}