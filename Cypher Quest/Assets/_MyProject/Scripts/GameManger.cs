using UnityEngine;

/// <summary>
/// Main Game Manager
/// </summary>
public class GameManger : MonoBehaviour
{
	[SerializeField, Tooltip("Panel For the Game win")] 
	private GameObject m_WinPanel;
	[SerializeField, Tooltip("String manager Refernece")]
	private StringManager m_StringManager;

	/// <summary>
	/// Call to show Game win
	/// </summary>
	public void GameWin()
	{
		m_WinPanel.SetActive(true);
	}

	/// <summary>
	/// Call to restart the game
	/// </summary>
	public void RestartGame()
	{
		m_WinPanel.SetActive(false);
		NewPuzzle();
		//Restart
	}

	/// <summary>
	/// Call to create a new Puzzle
	/// </summary>
	public void NewPuzzle()
	{
		m_StringManager.NewPuzzle();
	}
}
