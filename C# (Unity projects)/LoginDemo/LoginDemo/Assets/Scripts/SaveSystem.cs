/// <summary>
/// Provides methods to save and load player data to/from an SQLite database.
/// </summary>
public static class SaveSystem
{
    // Connection string to the SQLite database.
    static string dbConnectionString = "URI=file:AdventureGame.db";

    // Indicates whether the last database operation was successful.
    static bool IsDataOperationOK { get; private set; }

    /// <summary>
    /// Updates the player's position in the database.
    /// </summary>
    public static void UpdatePlayer(Player player)
    {
        // Get the player's data.
        var pincode = player.Pincode;

        // Format the player's position as strings with three decimal places.
        var positionX = player.transform.position.x.ToString("F3", CultureInfo.InvariantCulture);
        var positionY = player.transform.position.y.ToString("F3", CultureInfo.InvariantCulture);
        var positionZ = player.transform.position.z.ToString("F3", CultureInfo.InvariantCulture);

        // Execute the update query.
        using (var connection = new SqliteConnection(dbConnectionString))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"UPDATE Players SET " +
                                      $"positionX = {positionX}, " +
                                      $"positionY = {positionY}, " +
                                      $"positionZ = {positionZ} " +
                                      $"WHERE pincode = '{pincode}'";

                int rowsAffected = command.ExecuteNonQuery();

                // Check if the update was successful.
                IsDataOperationOK = rowsAffected > 0;
            }

            connection.Close();
        }
    }

    /// <summary>
    /// Loads the player's data from the database.
    /// </summary>
    public static PlayerData LoadPlayer(Player player)
    {
        PlayerData playerData = new PlayerData();

        using (var connection = new SqliteConnection(dbConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM Players WHERE pincode = '{player.Pincode}'";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IsDataOperationOK = true;

                        // Parse the player's position from the database.
                        playerData.Position[0] = float.Parse(reader["positionX"].ToString());
                        playerData.Position[1] = float.Parse(reader["positionY"].ToString());
                        playerData.Position[2] = float.Parse(reader["positionZ"].ToString());
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
        return playerData;
    }
}
