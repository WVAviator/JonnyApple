using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public static class GameManagement {

    public static int activeLevel;
    public static int activeCheckpoint;
    public static int lives;
    public static int score;
    public static float health;
    public static int appleAmmo;
    public static int orangeAmmo;
    public static int cornAmmo;
    public static int cantaloupeAmmo;
    public static int strawberryAmmo;

    public static int defaultLives = 3;
    public static int defaultCheckpoint = 0;
    public static int defaultLevel = 1;
    public static int defaultAppleAmmo = 10000000;
    public static int defaultOrangeAmmo = 0;
    public static int defaultCornAmmo = 0;
    public static int defaultCantaloupeAmmo = 0;
    public static int defaultStrawberryAmmo = 0;
    public static float defaultHealth = 150;

    public static void UpdateCheckpoint(int cp)
    {
        if (activeCheckpoint >= cp) return;
        activeCheckpoint = cp;
    }

    public static void NewGame()
    {
        lives = defaultLives;
        activeCheckpoint = defaultCheckpoint;
        activeLevel = defaultLevel;
        score = 0;

        appleAmmo = defaultAppleAmmo;
        orangeAmmo = defaultOrangeAmmo;
        cornAmmo = defaultCornAmmo;
        cantaloupeAmmo = defaultCantaloupeAmmo;
        strawberryAmmo = defaultStrawberryAmmo;

        SaveGame();
        PlayerPrefs.DeleteKey("L1:SaveableItems");
    }

    public static void StartGame()
    {
        if (activeLevel == 1) SceneManager.LoadSceneAsync("OrchardHeights");
    }

    public static void MoveToActiveCP(Transform plyr)
    {
        plyr.position = GetActiveCheckpointRespawn().position;
    }

    public static void DeductLife()
    {
        lives--;
        if (lives < 0) GameOver();
    }

    public static void AddLife()
    {
        lives++;
    }

    public static Transform GetActiveCheckpointRespawn()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject go in gos)
        {
            if (go.GetComponentInParent<Checkpoint>().checkpointIndex == activeCheckpoint) return go.transform;
        }
        return null;
    }

    static void SaveItems(int level)
    {
        GameObject[] saveableItems = GameObject.FindGameObjectsWithTag("SaveableItem");
        Array.Sort(saveableItems, (x, y) => String.Compare(x.name, y.name));
        string saveString = "";
        foreach (GameObject go in saveableItems)
        {
            saveString += go.GetComponent<SaveableItem>().isCollected ? "1" : "0";
        }
        PlayerPrefs.SetString("L" + level + ":SaveableItems", saveString);
    }

    public static bool WasCollected(GameObject go1)
    {
        if (!PlayerPrefs.HasKey("L" + activeLevel + ":SaveableItems")) return false;
        string saveString = PlayerPrefs.GetString("L" + activeLevel + ":SaveableItems");
        GameObject[] saveableItems = GameObject.FindGameObjectsWithTag("SaveableItems");
        Array.Sort(saveableItems, (x, y) => String.Compare(x.name, y.name));
        for (int i = 0; i < saveableItems.Length; i++)
        {
            if (go1.Equals(saveableItems[i])) return (saveString[i] == '1');
        }
        return false;
    }

    public static void SaveGame()
    {
        SaveItems(activeLevel);
        //Level
        PlayerPrefs.SetInt("Level", activeLevel);
        //CheckpointIndex
        PlayerPrefs.SetInt("Checkpoint", activeCheckpoint);
        //Music Volume
        PlayerPrefs.SetFloat("MusicVolume", SoundUtility.musicVolume);
        //Sound Volume
        PlayerPrefs.SetFloat("SoundVolume", SoundUtility.soundVolume);
        //Score
        PlayerPrefs.SetInt("Score", score);
        //Lives
        PlayerPrefs.SetInt("Lives", lives);
        //Health
        PlayerPrefs.SetFloat("Health", health);
        //Inventory Items
        PlayerPrefs.SetInt("AppleAmmo", appleAmmo);
        PlayerPrefs.SetInt("OrangeAmmo", orangeAmmo);
        PlayerPrefs.SetInt("CornAmmo", cornAmmo);
        PlayerPrefs.SetInt("CantaloupeAmmo", cantaloupeAmmo);
        PlayerPrefs.SetInt("StrawberryAmmo", strawberryAmmo);

        PlayerPrefs.Save();
    }

    public static void LoadGame()
    {
        
        //Level
        activeLevel = PlayerPrefs.GetInt("Level");
        //CheckpointIndex
        activeCheckpoint = PlayerPrefs.GetInt("Checkpoint");
        //Music Volume
        SoundUtility.musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        //Sound Volume
        SoundUtility.soundVolume = PlayerPrefs.GetFloat("SoundVolume");
        //Score
        score = PlayerPrefs.GetInt("Score");
        //Lives
        lives = PlayerPrefs.GetInt("Lives");
        //Health
        health = PlayerPrefs.GetFloat("Health");
        //Inventory Items
        appleAmmo = PlayerPrefs.GetInt("AppleAmmo");
        orangeAmmo = PlayerPrefs.GetInt("OrangeAmmo");
        cornAmmo = PlayerPrefs.GetInt("CornAmmo");
        cantaloupeAmmo = PlayerPrefs.GetInt("CantaloupeAmmo");
        strawberryAmmo = PlayerPrefs.GetInt("StrawberryAmmo");

    }

    public static bool SaveExists()
    {
        if (!PlayerPrefs.HasKey("Level")) return false;
        if (PlayerPrefs.GetInt("Level") == defaultLevel &&
            PlayerPrefs.GetInt("Checkpoint") == defaultCheckpoint &&
            PlayerPrefs.GetInt("Score") == 0 &&
            PlayerPrefs.GetInt("AppleAmmo") == defaultAppleAmmo) return false;
        return true;
    }

    public static void GameOver()
    {
        NewGame();
        SceneManager.LoadScene("GameOver");
    }

}
