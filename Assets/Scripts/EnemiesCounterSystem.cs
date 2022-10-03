using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemiesCounterSystem : MonoBehaviour
{
    public TextMeshProUGUI enemiesCount;
    
    void Update()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemiesCount.text = "Enemies left: " + enemies.Length / 2;

        if (enemies.Length <= 0)
        {
            SceneManager.LoadScene("WinScene");
        }
    }
}
