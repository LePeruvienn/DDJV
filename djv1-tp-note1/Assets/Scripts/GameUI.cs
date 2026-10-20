using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
	[Header("Room UI")]
	[SerializeField] private int startRoomNumber = 1;
	[SerializeField] private TextMeshProUGUI roomLabel;

	[Header("Game Over")]
	[SerializeField] private GameObject gameOverUI;
	[SerializeField] private TextMeshProUGUI gameOverLabel;

	private int _currentRoom = 0;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		// gameOverUI.SetActive(false);

		_currentRoom = startRoomNumber;
		SetCurrentRoom(_currentRoom);
	}

	public void SetCurrentRoom(int room)
	{
		roomLabel.text = $"Salle : {room}";
	}

	public void IncrementCurrentRoom()
	{
		++_currentRoom;
		SetCurrentRoom(_currentRoom);
	}

	public void ShowGameOver(string text)
	{
		gameOverLabel.text = text;
		gameOverUI.SetActive(true);
	}

	public void ReloadScene()
	{
		Debug.Log("Reload Scene !!");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
