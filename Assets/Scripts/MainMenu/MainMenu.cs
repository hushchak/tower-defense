using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel(LevelDataSO data)
    {
        LevelLoader.LoadLevel(data);
    }
}
