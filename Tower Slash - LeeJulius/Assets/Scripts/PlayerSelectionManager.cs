using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerSelectionManager : Singleton<PlayerSelectionManager>
{
    [Header("Camera")]
    Camera Cam;
    Vector2 CamBounds;

    [Header("PlayerPrefabs")]
    [SerializeField]
    private GameObject PlayerPrefab;

    public GameObject[] SelectablePlayers;
    [HideInInspector] public int playerSelectionNumber;

    [HideInInspector] public Player CurrentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // Set Cam to Bottom Left
        Cam = Camera.main;
        CamBounds = Cam.ScreenToWorldPoint(Vector2.zero);

        playerSelectionNumber = 0;

        // Activate Character Selection UI
        ActivatePlayer(playerSelectionNumber);
    }

    public void OnPlayerSelected()
    {
        CurrentPlayer = this.GetComponent<Player>();

        // Spawn Player Prefab
        SpawnPlayer();

        // Set Player Characteristics
        switch (playerSelectionNumber)
        {
            // Regular Character Selected
            case 0:
                CurrentPlayer.SetLives(3);
                CurrentPlayer.SetDashIncrement(5);
                CurrentPlayer.SetDashDuration(10);
                break;

            // Tank Character Selected
            case 1:
                CurrentPlayer.SetLives(5);
                CurrentPlayer.SetDashIncrement(5);
                CurrentPlayer.SetDashDuration(10);
                break;

            // Rogue Character Selected
            case 2:
                CurrentPlayer.SetLives(3);
                CurrentPlayer.SetDashIncrement(10);
                CurrentPlayer.SetDashDuration(10);
                break;

            // No Character Selected
            default:
                Debug.LogWarning("No Selected Player");
                break;
        }
    }

    public void GoToNextPlayer()
    {
        playerSelectionNumber++;

        if (playerSelectionNumber >= SelectablePlayers.Length)
        {
            playerSelectionNumber = 0;
        }

        ActivatePlayer(playerSelectionNumber);
    }

    public void GoToPreviousPlayer()
    {
        playerSelectionNumber--;

        if (playerSelectionNumber < 0)
        {
            playerSelectionNumber = SelectablePlayers.Length - 1;
        }

        ActivatePlayer(playerSelectionNumber);
    }

    private void SpawnPlayer()
    {
        CurrentPlayer = Instantiate(PlayerPrefab, CamBounds * new Vector3(-0.5f, 0, 0), Quaternion.identity).GetComponent<Player>();
    }

    private void ActivatePlayer(int x)
    {
        // Close all the Character Panels
        foreach (GameObject go in SelectablePlayers)
        {
            go.SetActive(false);
        }

        // Open the Chosen Character Panel
        SelectablePlayers[x].SetActive(true);
    }
}
