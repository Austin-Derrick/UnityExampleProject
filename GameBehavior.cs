using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    // Text to prompt player about game goal
    public string labelText = "Collect all 4 items and win your freedom!";
    // Variable for the maximum number of items
    public int maxItems = 4;
    // bool variable to track win condition
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    // Variable to hold the number of Items the player collects
    private int _itemsCollected = 0;
    // Property for _itemsCollected
    public int Items
    {
        get { return _itemsCollected; }
        set {
            _itemsCollected = value;
            labelText = "Item found, only " + (maxItems - _itemsCollected) + " more to go!";
            Debug.LogFormat("Items: {0}", _itemsCollected);
            // Method call
            showWinScreen = UpdateStatus(maxItems, _itemsCollected,"You've found all the items!");
        }
    }
    // Variable to hold the players lives
    private int _playerLives = 3;
    // Property for _playerHP
    public int Lives
    {
        get { return _playerLives; }
        set {
            _playerLives = value;
            // Method call
            showLossScreen = UpdateStatus(_playerLives, 0, "You want another life with that?");
            Debug.LogFormat("Lives: {0}", _playerLives);
        }
    }

    // GUI method
    void OnGUI()
    {
        // Creates GUI boxes for player health, item count, and game objective
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health: " + _playerLives);
        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);
        // if statement tests if showWinScreen is true
        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WON!"))
            {
                RestartLevel();
            }
        }
        // if statement tests if showLossScreen is true
        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You Lose..."))
            {
                RestartLevel();
            }
        }
    }
    void RestartLevel()
    {
        // Re-Loads scene once button is clicked
        SceneManager.LoadScene(0);
        // Restarts all controls and behaviors once scene is reloaded
        Time.timeScale = 1.0f;
    }

    bool UpdateStatus(int max, int min, string _labelText)
    {
        // if statement to check given parameters
        if (max <= min)
        {
            // changes OnScreen text based on method call
            labelText = _labelText;
            // freezes game time
            Time.timeScale = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
}
