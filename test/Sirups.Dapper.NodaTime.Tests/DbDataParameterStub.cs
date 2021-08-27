using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Sirups.Dapper.NodaTime.Tests {
	[ExcludeFromCodeCoverage]
	public class DbDataParameterStub : IDbDataParameter{
		public DbType DbType { get; set; }
		public ParameterDirection Direction { get; set; }
		public bool IsNullable { get; }
		public string ParameterName { get; set; }
		public string SourceColumn { get; set; }
		public DataRowVersion SourceVersion { get; set; }
		public object? Value { get; set; }
		public byte Precision { get; set; }
		public byte Scale { get; set; }
		public int Size { get; set; }
	}
}