using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using NodaTime;
using Sirups.Dapper.NodaTime.TypeHandlers;
using Xunit;

namespace Sirups.Dapper.NodaTime.Tests.TypeHandlers {
	public class LocalTimeTypeHandlerTests {
		[Fact]
		public async Task ProperlySetsValue() {
			//Arrange
			IDbDataParameter parameter = new DbDataParameterStub();
			LocalTime localTime = LocalTime.FromHourMinuteSecondTick(10, 20, 0, 0);

			//Act
			LocalTimeTypeHandler.Instance.SetValue(parameter, localTime);

			//Assert
			Assert.Equal(localTime, parameter.Value);
		}

		[Fact]
		public async Task ProperlyParsesValue() {
			//Arrange
			LocalTime localTime = LocalTime.FromHourMinuteSecondTick(10, 20, 0, 0);

			//Act
			var result = LocalTimeTypeHandler.Instance.Parse(localTime);

			//Assert
			Assert.Equal(localTime, result);
		}

		[Fact]
		public async Task ThrowsNotImplementedOnInvalidType() {
			//Arrange
			List<object> types = new List<object> {
				DateTime.Now
			};

			//Act
			foreach (object type in types) {
				//Assert
				Assert.Throws<NotImplementedException>(() => LocalTimeTypeHandler.Instance.Parse(type));
			}
		}
	}
}