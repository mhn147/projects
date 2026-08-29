using MySqlConnector;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using TomoPlan.Core.Data.Entities;

namespace TomoPlan.Core.Data;

public class AppRepository
{
    private readonly MySqlConnection _dbConnection;

    public AppRepository(MySqlConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<bool> UserExists(string email)
    {
        if (_dbConnection.State != ConnectionState.Open)
        {
            await _dbConnection.OpenAsync();
        }

        var query =
            """
                SELECT *
                FROM users
                WHERE email = @Email
            """;

        await using var cmd = new MySqlCommand(query, _dbConnection);
        cmd.Parameters.AddWithValue("@Email", email);

        var result = await cmd.ExecuteScalarAsync();
        return result != null;
    }

    public async Task<User?> GetUser(string email)
    {
        if (_dbConnection.State != ConnectionState.Open)
        {
            await _dbConnection.OpenAsync();
        }

        var query =
            """
                SELECT *
                FROM users
                WHERE email = @Email
            """;

        await using var cmd = new MySqlCommand(query, _dbConnection);
        cmd.Parameters.AddWithValue("@Email", email);

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new User
            {
                Id = reader.GetGuid(reader.GetOrdinal("id")),
                Email = reader.GetString(reader.GetOrdinal("email")),
                PasswordHash = reader.GetString(reader.GetOrdinal("password_hash")),
            };
        }

        return null;
    }

    public async Task AddUser(Guid id, string email, string passwordHash)
    {
        if (_dbConnection.State != ConnectionState.Open) {
            await _dbConnection.OpenAsync();
        }

        var query =
            """
                INSERT INTO users (id, email, password_hash, first_name, last_name)
                VALUES (@Id, @Email, @PasswordHash, @FirstName, @LastName)
            """;

        await using var cmd = new MySqlCommand(query, _dbConnection);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@Email", email);
        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
        cmd.Parameters.AddWithValue("@FirstName", "foo");
        cmd.Parameters.AddWithValue("@LastName", "bar");

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<DailyPlan?> GetPlan(Guid userId, DateOnly date)
    {
        if (_dbConnection.State != ConnectionState.Open)
        {
            await _dbConnection.OpenAsync();
        }

        var query =
            """
                SELECT *
                FROM daily_plans
                WHERE user_id = @UserId AND date = @Date
            """;

        await using var cmd = new MySqlCommand(query, _dbConnection);
        cmd.Parameters.AddWithValue("@UserId", userId.ToString());
        cmd.Parameters.AddWithValue("@Date", date);

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new DailyPlan
            {
                Id = reader.GetGuid(reader.GetOrdinal("id")),
                UserId = reader.GetGuid(reader.GetOrdinal("user_id")),
                Date = reader.GetDateOnly(reader.GetOrdinal("date")),
            };
        }

        return null;
    }

    public async Task<DailyPlan> AddPlan(Guid userId, DateOnly date)
    {
        if (_dbConnection.State != ConnectionState.Open)
        {
            await _dbConnection.OpenAsync();
        }

        var id = Guid.NewGuid().ToString();

        var query =
            """
                INSERT INTO daily_plans (id, user_id, date)
                VALUES (@Id, @UserId, @Date)
            """;

        await using var cmd = new MySqlCommand(query, _dbConnection);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@UserId", userId.ToString());
        cmd.Parameters.AddWithValue("@Date", date.ToString("O"));

        await cmd.ExecuteNonQueryAsync();

        query =
            """
                SELECT *
                FROM daily_plans
                WHERE Id = @Id
            """;

        await using var selectCmd = new MySqlCommand(query, _dbConnection);
        selectCmd.Parameters.AddWithValue("@Id", id);

        using var reader = await selectCmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new DailyPlan
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                Date = reader.GetDateOnly(reader.GetOrdinal("Date")),
            };
        }

        throw new Exception("foo");
    }
}
