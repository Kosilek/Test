using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection
{
    public static int ChoiceSkin;
    public static void SelectPack(int x)
    {
        ChoiceSkin = x;
    }

    public static void Save(string Save, int ChoiceSkin)
    {
        int x = 0;
        if (PlayerPrefs.HasKey(Save))
        {
            x = PlayerPrefs.GetInt(Save);
        }

        if (ChoiceSkin != x)
        {
            PlayerPrefs.SetInt(Save, ChoiceSkin);
        }
    }
}
