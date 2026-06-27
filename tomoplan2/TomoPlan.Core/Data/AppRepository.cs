using MySqlConnector;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using TomoPlan.Core.Data.Entities;

namespace TomoPlan.Core.Data;

public  class AppRepository
{
    private readonly MySqlConnection _dbConnection;

    public AppRepository(MySqlConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<bool> UserExists(string email)
    {
        await _dbConnection.OpenAsync();

        var query = 
            """
                SELECT *
                FROM users
                WHERE Email = @Email
            """;

        await using var cmd = new MySqlCommand(query, _dbConnection);
        cmd.Parameters.AddWithValue("@Email", email);

        var result = await cmd.ExecuteScalarAsync();
        return result != null && (int)result == 1;
    }

    public async Task<User?> GetUser(string email)
    {
        await _dbConnection.OpenAsync();

        var query =
            """
                SELECT *
                FROM users
                WHERE Email = @Email
            """;

        await using var cmd = new MySqlCommand(query, _dbConnection);
        cmd.Parameters.AddWithValue("@Email", email);

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new User
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
            };
        }

        return null;
    }

    internal async Task AddUser(Guid id, string email, string passwordHash)
    {
        if (_dbConnection.State != ConnectionState.Open) {
            await _dbConnection.OpenAsync();
        }

        var query =
            """
                INSERT INTO users (Id, Email, PasswordHash)
                VALUES (@Id, @Email, @PasswordHash)
            """;

        await using var cmd = new MySqlCommand(query, _dbConnection);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@Email", email);
        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

       await cmd.ExecuteNonQueryAsync();
    }

    public async Task TestConnectionAsync()
    {
        await _dbConnection.OpenAsync();

        var userId = Guid.NewGuid();
        var email = $"test_{Guid.NewGuid():N}@example.com";

        // 1. INSERT USER (password hashing inlined)
        const string insertSql = """
            INSERT INTO Users
                (Id, Email, PasswordHash, FirstName, LastName, EmailVerified)
            VALUES
                (@Id, @Email, @PasswordHash, @FirstName, @LastName, @EmailVerified);
            """;

        await using (var cmd = new MySqlCommand(insertSql, _dbConnection))
        {
            cmd.Parameters.AddWithValue("@Id", userId.ToString());
            cmd.Parameters.AddWithValue("@Email", email);

            cmd.Parameters.AddWithValue(
                "@PasswordHash",
                ComputeMd5("foo")
            );

            cmd.Parameters.AddWithValue("@FirstName", "Test");
            cmd.Parameters.AddWithValue("@LastName", "User");
            cmd.Parameters.AddWithValue("@EmailVerified", false);

            await cmd.ExecuteNonQueryAsync();
        }

        // 2. READ USER BACK
        const string selectSql = """
            SELECT
                Id,
                Email,
                PasswordHash,
                FirstName,
                LastName,
                EmailVerified
            FROM Users
            WHERE Id = @Id;
            """;

        await using (var cmd = new MySqlCommand(selectSql, _dbConnection))
        {
            cmd.Parameters.AddWithValue("@Id", userId.ToString());

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                Console.WriteLine("✅ USER ROUNDTRIP SUCCESS");
                Console.WriteLine($"Id: {reader.GetString("Id")}");
                Console.WriteLine($"Email: {reader.GetString("Email")}");
                Console.WriteLine($"Name: {reader.GetString("FirstName")} {reader.GetString("LastName")}");
                Console.WriteLine($"Verified: {reader.GetBoolean("EmailVerified")}");
            }
            else
            {
                Console.WriteLine("❌ USER NOT FOUND");
            }
        }
    }

    public static string ComputeMd5(string input)
    {
        using var md5 = MD5.Create();

        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes); // uppercase hex string
    }
}
