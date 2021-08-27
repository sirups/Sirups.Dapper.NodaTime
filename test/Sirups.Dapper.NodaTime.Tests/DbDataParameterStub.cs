using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Sirups.Dapper.NodaTime.Tests {
	[ExcludeFromCodeCoverage]
	public class DbDataParameterStub : IDbDataParameter{
		public DbDataParameterStub() {
			IsNullable = false;
			ParameterName = string.Empty;
			SourceColumn = string.Empty;
		}
		
		public DbType DbType { get; set; }
		public ParameterDirection Direction { get; set; }
		public bool IsNullable { get; }
		[AllowNull]
		public string ParameterName { get; set; }
		[AllowNull]
		public string SourceColumn { get; set; }
		public DataRowVersion SourceVersion { get; set; }
		public object? Value { get; set; }
		public byte Precision { get; set; }
		public byte Scale { get; set; }
		public int Size { get; set; }
	}
}