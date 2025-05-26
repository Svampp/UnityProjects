/// <summary>
/// Handles switching between different registration UI canvases.
/// </summary>
public class Register : MonoBehaviour
{
    [SerializeField] GameObject firstCanvas; // First registration UI canvas.
    [SerializeField] GameObject secondCanvas; // Second registration UI canvas.

    /// <summary>
    /// Switches between the two registration canvases.
    /// </summary>
    public void SwitchCanvas()
    {
        if (firstCanvas.activeSelf)
        {
            firstCanvas.SetActive(false);
            secondCanvas.SetActive(true);
        }
        else
        {
            firstCanvas.SetActive(true);
            secondCanvas.SetActive(false);
        }
    }
}
