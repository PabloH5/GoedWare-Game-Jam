using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusGame : MonoBehaviour
{
    [SerializeField] private GameObject _loosePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameController _gameController;

    public void Win()
    {
        _winPanel.SetActive(true);
        _gameController.PauseGame();
    }

    public void GameOver()
    {
        _loosePanel.SetActive(true);
        _gameController.PauseGame();
    }
}
