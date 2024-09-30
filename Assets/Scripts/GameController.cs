using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text _woodTxt;

    private Inventory _playerInventory;

    private void Awake()
    {
        if (FindObjectOfType<Inventory>())
        {
            _playerInventory = FindObjectOfType<Inventory>();
        }
    }

    private void FixedUpdate()
    {
        if (_playerInventory)
        {
            _woodTxt.text = "x" + Mathf.FloorToInt(_playerInventory.woodAmount);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
