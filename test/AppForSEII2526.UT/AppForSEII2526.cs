namespace AppForSEII2526.UT {
        public AppForSEII25264SqliteUT() {
            // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
            // at the end of the test (see Dispose below).
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection).Options;

            // Create the schema and seed some data
            _context = new ApplicationDbContext(_contextOptions);
            if (_context.Database.EnsureCreated()) {
                using var viewCommand = _context.Database.GetDbConnection().CreateCommand();
                viewCommand.CommandText = @"
                CREATE VIEW AllResources AS
                SELECT Name
                FROM Movies;";
                viewCommand.ExecuteNonQuery();
            }
        }
    }
}