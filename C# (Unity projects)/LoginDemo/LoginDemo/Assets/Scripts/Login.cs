/// <summary>
/// Handles player login functionality by validating pincode and loading the game scene.
/// </summary>
public class Login : MonoBehaviour
{
    string dbConnectionString;
    [SerializeField] TMP_InputField pincode; // Input field for entering the pincode.
    [SerializeField] GameObject errorMessageObject; // Error message UI element.
    TMP_Text errorMessage;

    public static string nickname = ""; // Player's nickname.
    public static string myPincode; // Pincode used for login.
    public bool IsLoginOk { get; private set; } // Indicates whether the login was successful.

    void Awake()
    {
        dbConnectionString = "URI=file:AdventureGame.db";

        errorMessage = errorMessageObject.GetComponent<TMP_Text>();
        errorMessageObject.SetActive(false); // Hide error message initially.
    }

    /// <summary>
    /// Handles login validation and scene transition if successful.
    /// </summary>
    public void HandleLogin()
    {
        IsLoginOk = false;

        using (var connection = new SqliteConnection(dbConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                // Query the database to validate the pincode.
                command.CommandText = $"SELECT * FROM Logins WHERE pincode = '{pincode.text}'";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IsLoginOk = true;
                        nickname = reader["nick_name"].ToString();
                        myPincode = pincode.text;
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }

        if (!IsLoginOk)
        {
            errorMessageObject.SetActive(true); // Show error message if login fails.
            return;
        }

        errorMessageObject.SetActive(false);
        SceneManager.LoadScene("Playground"); // Load the game scene on successful login.
    }
}
