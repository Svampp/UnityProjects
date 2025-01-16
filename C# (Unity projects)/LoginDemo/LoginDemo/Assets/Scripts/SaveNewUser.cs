/// <summary>
/// Handles saving new user data to the SQLite database.
/// </summary>
public class SaveNewUser : MonoBehaviour
{
    static string dbConnectionString = "URI=file:AdventureGame.db";

    [SerializeField] TMP_InputField pincode; // Input field for entering the pincode.
    [SerializeField] TMP_InputField nickname; // Input field for entering the nickname.
    [SerializeField] GameObject errorMessageObject; // Error message UI element.
    TMP_Text errorMessage;

    void Awake()
    {
        errorMessage = errorMessageObject.GetComponent<TMP_Text>();
        errorMessageObject.SetActive(false); // Hide error message initially.
    }

    /// <summary>
    /// Validates and saves a new user to the database.
    /// </summary>
    public void SaveUser()
    {
        // Validate that the pincode matches a 4-digit format.
        if (!Regex.IsMatch(pincode.text, @"^\d{4}$"))
        {
            errorMessageObject.SetActive(true);
            return;
        }

        errorMessageObject.SetActive(false);

        using (var connection = new SqliteConnection(dbConnectionString))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                // Insert the new user into the database.
                command.CommandText = "INSERT INTO Logins (pincode, nick_name) VALUES (@pincode, @nickname)";
                command.Parameters.Add(new SqliteParameter("@pincode", pincode.text.Trim()));
                command.Parameters.Add(new SqliteParameter("@nickname", nickname.text.Trim()));

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqliteException ex)
                {
                    Debug.LogError(ex.Message); // Log any database errors.
                }
            }

            connection.Close();
        }
    }
}
