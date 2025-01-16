/// <summary>
/// Represents the player and handles loading player data from the database.
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text playerNickname; // UI element for displaying the player's nickname.
    public string Pincode { get; private set; } // Player's unique identifier.

    void Start()
    {
        playerNickname.text = Login.nickname; // Display the player's nickname.
        Pincode = Login.myPincode; // Set the player's pincode.
        LoadPlayer(); // Load player data from the database.
    }

    /// <summary>
    /// Loads the player's position from the database and updates the player's transform.
    /// </summary>
    public void LoadPlayer()
    {
        PlayerData playerData = SaveSystem.LoadPlayer(this);

        Vector3 position = new Vector3()
        {
            x = playerData.Position[0],
            y = playerData.Position[1],
            z = playerData.Position[2]
        };

        transform.position = position;
    }
}
