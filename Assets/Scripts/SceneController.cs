using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("PabloScene");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
