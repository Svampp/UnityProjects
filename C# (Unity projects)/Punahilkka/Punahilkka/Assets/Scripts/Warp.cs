using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles player teleportation between maps, including screen fade transitions
/// and displaying the name of the new area.
/// </summary>
public class Warp : MonoBehaviour
{
    [Header("TELEPORTATION")]
    [Tooltip("Target map to teleport to.")]
    [SerializeField] private GameObject targetMap; // Map to teleport to
    [Tooltip("Location where the player will appear.")]
    [SerializeField] private GameObject exitPoint; // Exit point for teleportation

    [Header("AREA TEXT")]
    [SerializeField] private bool needText; // Whether to display the area name
    [SerializeField] private string placeName; // Name of the area
    [SerializeField] private GameObject placeTextHolder; // UI holder for area name
    [SerializeField] private TMP_Text placeText; // Text component for area name

    /// <summary>
    /// Hides the warp visual elements on Awake.
    /// </summary>
    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    /// <summary>
    /// Sets the needText flag at the start.
    /// </summary>
    private void Start()
    {
        needText = true;
    }

    /// <summary>
    /// Handles teleportation when the player enters the trigger zone.
    /// </summary>
    /// <param name="collision">Collider of the entering object.</param>
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            ScreenFader screenFader = GameObject.FindGameObjectWithTag("ScreenFader").GetComponent<ScreenFader>();
            yield return StartCoroutine(screenFader.FadeToBlack());

            Vector3 currentPosition = collision.transform.position;
            Vector3 newPosition = exitPoint.transform.GetChild(0).transform.position;
            newPosition.z = currentPosition.z;
            collision.transform.position = newPosition;

            Camera.main.GetComponent<MainCamera>().SetBound(targetMap);

            if (needText)
            {
                StartCoroutine(ShowPlaceName());
            }

            collision.gameObject.SetActive(true);
            yield return StartCoroutine(screenFader.FadeToWhite());
        }
    }

    /// <summary>
    /// Displays the name of the new area for a few seconds.
    /// </summary>
    private IEnumerator ShowPlaceName()
    {
        placeTextHolder.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        placeTextHolder.SetActive(false);
    }
}
