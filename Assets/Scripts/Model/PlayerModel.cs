using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerModel {

    public static string currentSong;
    public static string difficulty;
    public static List<string> songRepository = new List<string>()
    {
        "ThisIsWhatYouCameFor",
        "Escape",
        "Rise"
    };


    public static bool GetUnlockStatsOfSongs(string songName, string level)
    {
        return PlayerPrefs.GetInt(songName + level, 0) == 1;
    }

    public static void SaveUnlockStatsData(string songName, string level)
    {
        PlayerPrefs.SetInt(songName + level, 1);
    }

    public static int GetCoinsData()
    {
        return PlayerPrefs.GetInt("Coins", 0);
    }

    public static bool SaveCoinsData(int modifier)
    {
        int currentCoin = GetCoinsData();
        if (currentCoin + modifier < 0)
            return false;
        else
            PlayerPrefs.SetInt("Coins", currentCoin + modifier);
        return true;
    }

    public static int GetHighScoreData()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    public static void SaveHighScoreData(int newScore)
    {
        if(newScore > GetHighScoreData())
            PlayerPrefs.SetInt("HighScore", newScore);
    }

    public static int GetMaxHpData()
    {
        return PlayerPrefs.GetInt("MaxHp", 100);
    }

    public static void SaveMaxHpData(int addition)
    {
        int hp = GetMaxHpData();
        PlayerPrefs.SetInt("MaxHp", hp + addition);
    }

    public static int GetInitialComboData()
    {
        return PlayerPrefs.GetInt("InitialCombo", 0);
    }

    public static void SaveInitialComboData(int addition)
    {
        int combo = GetInitialComboData();
        PlayerPrefs.SetInt("InitialCombo", combo + addition);
    }

    public static bool GetFirstOpenApp()
    {
        return PlayerPrefs.GetInt("FirstOpen", 0) == 0;
    }

    public static void SaveFirstOpenApp()
    {
        PlayerPrefs.SetInt("FirstOpen", 1);
    }
}
