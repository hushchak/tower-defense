using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel(LevelData data)
    {
        LevelLoader.LoadLevel(data);
    }
}
