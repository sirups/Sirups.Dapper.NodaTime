using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using NodaTime;
using Sirups.Dapper.NodaTime.TypeHandlers;
using Xunit;

namespace Sirups.Dapper.NodaTime.Tests.TypeHandlers {
	public class PeriodTypeHandlerTests {
		[Fact]
		public void SetsTheParameterValue() {
			//Arrange
			IDbDataParameter parameter = new DbDataParameterStub();
			Period period = Period.FromDays(3);

			//Act
			PeriodTypeHandler.Instance.SetValue(parameter, period);

			//Assert
			Assert.Equal(period, parameter.Value);
		}

		[Fact]
		public void ProperlySetsValue() {
			//Arrange
			Period period = Period.FromDays(3);

			//Act
			var result = PeriodTypeHandler.Instance.Parse(period);

			//Assert
			Assert.Equal(period, result);
		}

		[Fact]
		public void ThrowsNotImplementedOnInvalidType() {
			//Arrange
			List<object> objectTypes = new List<object> {
				Duration.FromHours(23),
				Instant.FromUtc(2000, 1, 21, 20, 0)
			};

			//Act
			foreach (object type in objectTypes) {
				//Assert
				Assert.Throws<NotImplementedException>(() => PeriodTypeHandler.Instance.Parse(type));
			}
		}
	}
}