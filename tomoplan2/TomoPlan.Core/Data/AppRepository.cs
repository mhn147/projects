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

    public async Task<DailyPlan?> GetPlan(Guid userId, DateTimeOffset date)
    {
        if (_dbConnection.State != ConnectionState.Open)
        {
            await _dbConnection.OpenAsync();
        }

        var query =
            """
                SELECT p.id as plan_id, p.user_id as user_id, p.date as date, p.created_at, p.updated_at, p.is_complete,
                        t.id as task_id, t.body as task_body, t.title as task_title, t.start_time as task_start_time, t.end_time as task_end_time,
                        t.created_at as task_created_at, t.updated_at as task_updated_at, t.is_complete as task_is_complete
                FROM daily_plans p
                LEFT JOIN daily_tasks t ON p.id = t.daily_plan_id 
                WHERE p.user_id = @UserId AND Date(p.date) = @Date
            """;

        await using var cmd = new MySqlCommand(query, _dbConnection);
        cmd.Parameters.AddWithValue("@UserId", userId.ToString());
        cmd.Parameters.AddWithValue("@Date", DateOnly.FromDateTime(date.DateTime).ToString("O"));

        using var reader = await cmd.ExecuteReaderAsync();

        DailyPlan? plan = null;
        
        while (await reader.ReadAsync())
        {
            if (plan == null)
            {
                plan = new DailyPlan
                {
                    Id = reader.GetGuid(reader.GetOrdinal("plan_id")), 
                    UserId = reader.GetGuid(reader.GetOrdinal("user_id")), 
                    Date = reader.GetDateTimeOffset(reader.GetOrdinal("date")),
                };
            }

            if (!reader.IsDBNull(reader.GetOrdinal("task_id")))
            {
                var taskId = reader.GetGuid(reader.GetOrdinal("task_id"));
                plan.Tasks.Add(new DailyTask 
                {
                    Id = taskId,
                    StartTime = reader.GetTimeOnly(reader.GetOrdinal("task_start_time")),
                    EndTime = reader.GetTimeOnly(reader.GetOrdinal("task_end_time")),
                    Body = reader.GetString(reader.GetOrdinal("task_body")),
                    Title = reader.GetString(reader.GetOrdinal("task_title"))
                });
            }
        }

        return plan;
    }

    public async Task<DailyPlan?> GetPlan(Guid planId)
    {
        if (_dbConnection.State != ConnectionState.Open)
        {
            await _dbConnection.OpenAsync();
        }

        var query =
            """
                SELECT 
                    p.id as plan_id, p.user_id as user_id, p.date as date, p.created_at, p.updated_at, p.is_complete,
                    t.id as task_id, t.body as task_body, t.title as task_title, t.start_time as task_start_time, t.end_time as task_end_time,
                      t.created_at as task_created_at, t.updated_at as task_updated_at, t.is_complete as task_is_complete
                FROM daily_plans p
                LEFT JOIN daily_tasks t ON p.id = t.daily_plan_id 
                WHERE p.id = @Id
            """;

        await using var cmd = new MySqlCommand(query, _dbConnection);
        cmd.Parameters.AddWithValue("@Id", planId.ToString());

        await using var reader = await cmd.ExecuteReaderAsync();

        DailyPlan? plan = null;
        
        while (await reader.ReadAsync())
        {
            if (plan == null)
            {
                plan = new DailyPlan
                {
                    Id = reader.GetGuid(reader.GetOrdinal("plan_id")), 
                    UserId = reader.GetGuid(reader.GetOrdinal("user_id")), 
                    Date = reader.GetDateTimeOffset(reader.GetOrdinal("date")),
                };
            }

            if (!reader.IsDBNull(reader.GetOrdinal("task_id")))
            {
                var taskId = reader.GetGuid(reader.GetOrdinal("task_id")); 
                plan.Tasks.Add(new DailyTask
                {
                    Id = taskId,
                    StartTime = reader.GetTimeOnly(reader.GetOrdinal("task_start_time")),
                    EndTime = reader.GetTimeOnly(reader.GetOrdinal("task_end_time")),
                    Body = reader.GetString(reader.GetOrdinal("task_body")),
                    Title = reader.GetString(reader.GetOrdinal("task_title"))
                });
            }
        }

        return plan;
    }
    
    public async Task<DailyPlan> AddPlan(Guid userId, DateTimeOffset date)
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
                Id = reader.GetGuid(reader.GetOrdinal("id")),
                UserId = reader.GetGuid(reader.GetOrdinal("user_id")),
                Date = reader.GetDateTimeOffset(reader.GetOrdinal("date")),
            };
        }

        throw new Exception("foo");
    }

    public async Task AddTask(Guid planId, TimeOnly startTime, TimeOnly endTime, string body, string title)
    {
        if (_dbConnection.State != ConnectionState.Open)
        {
            await _dbConnection.OpenAsync();
        }

        var id = Guid.NewGuid().ToString();

        var query =
            """
                INSERT INTO daily_tasks (id, daily_plan_id, start_time, end_time, body, title)
                VALUES (@Id, @PlanId, @StartTime, @EndTime, @Body, @Title)
            """;

        await using var cmd = new MySqlCommand(query, _dbConnection); 
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@PlanId", planId.ToString());
        cmd.Parameters.AddWithValue("@StartTime", startTime.ToString("O"));
        cmd.Parameters.AddWithValue("@EndTime", endTime.ToString("O"));
        cmd.Parameters.AddWithValue("@Body", body);
        cmd.Parameters.AddWithValue("@Title", title);

        await cmd.ExecuteNonQueryAsync();
    }
}
