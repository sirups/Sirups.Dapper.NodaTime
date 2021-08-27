# Sirups.Dapper.NodaTime

A simple set of typehandlers for using NodaTime with Dapper.

Supports the following types

1. Duration
2. Instant
3. LocalDate
4. LocalTime
5. OffsetTime
6. Period

Requires a supporting package like [Npgsql.NodaTime](https://www.nuget.org/packages/Npgsql.NodaTime) to actually transform the database values to the supported types.