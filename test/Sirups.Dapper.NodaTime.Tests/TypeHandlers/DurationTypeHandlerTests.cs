using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using NodaTime;
using Sirups.Dapper.NodaTime.TypeHandlers;
using Xunit;

namespace Sirups.Dapper.NodaTime.Tests.TypeHandlers {
	public class DurationTypeHandlerTests {
		[Fact]
		public async Task SetsTheParameterValue() {
			//Arrange
			IDbDataParameter parameter = new DbDataParameterStub();

			Duration duration = Duration.FromHours(22);

			//Act
			DurationTypeHandler.Instance.SetValue(parameter, duration);

			//Assert
			Assert.Equal(duration, parameter.Value);
		}

		[Fact]
		public async Task ProperlyHandlesPeriodToDuration() {
			//Arrange
			Period period = Period.FromHours(23);

			//Act
			var result = DurationTypeHandler.Instance.Parse(period);

			//Assert
			Assert.Equal(Duration.FromHours(23), result);
		}

		[Fact]
		public async Task ThrowsNotImplementedOnInvalidType() {
			//Arrange
			List<object> objectTypes = new List<object> {
				Duration.FromHours(23),
				Instant.FromUtc(2000, 1, 21, 20, 0)
			};

			//Act
			foreach (object type in objectTypes) {
				//Assert
				Assert.Throws<NotImplementedException>(() => DurationTypeHandler.Instance.Parse(type));
			}
		}
	}
}