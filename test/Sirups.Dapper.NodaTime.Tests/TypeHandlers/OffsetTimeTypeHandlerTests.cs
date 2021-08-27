using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using NodaTime;
using Sirups.Dapper.NodaTime.TypeHandlers;
using Xunit;

namespace Sirups.Dapper.NodaTime.Tests.TypeHandlers {
	public class OffsetTimeTypeHandlerTests {
		[Fact]
		public void SetsTheParameterValue() {
			//Arrange
			IDbDataParameter parameter = new DbDataParameterStub();
			OffsetTime offsetTime = new OffsetTime(LocalTime.Noon, Offset.FromHours(2));

			//Act
			OffsetTimeTypeHandler.Instance.SetValue(parameter, offsetTime);

			//Assert
			Assert.Equal(offsetTime, parameter.Value);
		}

		[Fact]
		public void ProperlySetsValue() {
			//Arrange
			OffsetTime offsetTime = new OffsetTime(LocalTime.Noon, Offset.FromHours(2));

			//Act
			var result = OffsetTimeTypeHandler.Instance.Parse(offsetTime);

			//Assert
			Assert.Equal(offsetTime, result);
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
				Assert.Throws<NotImplementedException>(() => OffsetTimeTypeHandler.Instance.Parse(type));
			}
		}
	}
}