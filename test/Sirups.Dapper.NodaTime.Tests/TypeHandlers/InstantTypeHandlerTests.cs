using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using NodaTime;
using Sirups.Dapper.NodaTime.TypeHandlers;
using Xunit;

namespace Sirups.Dapper.NodaTime.Tests.TypeHandlers {
	public class InstantTypeHandlerTests {
		[Fact]
		public void ProperlySetsValue() {
			//Arrange
			IDbDataParameter parameter = new DbDataParameterStub();
			Instant instant = Instant.FromUtc(2021, 1, 21, 22, 0);

			//Act
			InstantTypeHandler.Instance.SetValue(parameter, instant);

			//Assert
			Assert.Equal(instant, parameter.Value);
		}

		[Fact]
		public void ProperlyParsesValue() {
			//Arrange
			Instant instant = Instant.FromUtc(2021, 1, 21, 22, 0);

			//Act
			Instant value = InstantTypeHandler.Instance.Parse(instant);

			//Assert
			Assert.Equal(instant, value);
		}

		[Fact]
		public void ThrowsNotImplementedOnUnsupportedTypes() {
			//Arrange
			List<object> objectTypes = new List<object> {
				DateTime.Now,
				DateTimeOffset.Now
			};

			//Act
			foreach (object type in objectTypes) {
				//Assert
				Assert.Throws<NotImplementedException>(() => InstantTypeHandler.Instance.Parse(type));
			}
		}
	}
}