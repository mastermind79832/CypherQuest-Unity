using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages all the functionality realted to the string
/// </summary>
public class StringManager : MonoBehaviour
{
    [SerializeField,Tooltip("This string is used for the Creating")]
	private List<string> m_StringList = new List<string>();

    private string m_DisplayString;
	[SerializeField,Tooltip("Prefab of the string for instantion")]
	private StringButton m_StringButtonPrefab;
	[SerializeField,Tooltip("parent object in which all the strign buttons are going to spawn")]
	private Transform m_StringParent;

	[SerializeField, Tooltip("Reference to GameManager")]
	private GameManger m_GameManger;
    
	private Dictionary<char, int> m_StringPair;
	private List<StringButton> m_StringButtons;

	private StringButton SelectedButton = null;

	/// <summary>
	/// Create all the string buttons
	/// </summary>
	[ContextMenu("Create string Buttons")]
    public void CreateStringButtons()
	{

		// Looping through all the letters
		foreach (char sLetter in m_DisplayString)
		{
			char letter = char.ToUpper(sLetter);

			// Creation and Set up of each buttons
			StringButton newButton = Instantiate(m_StringButtonPrefab, m_StringParent);
			newButton.StringManger = this;

			m_StringButtons.Add(newButton);

			if(!char.IsLetter(sLetter))
			{
				newButton.SetLetter(letter, false);
				continue;
			}

			newButton.SetLetter(letter);

			// check if the letter already exists
			if (m_StringPair.ContainsKey(letter))
			{
				newButton.SetNumberValue(m_StringPair[letter]);
				continue;
			}
			// checks if the number already exists
			int randomNumber;
			do
			{
				randomNumber = Random.Range(1, 27);
			}
			while (m_StringPair.ContainsValue(randomNumber));

			newButton.SetNumberValue(randomNumber);
			m_StringPair.Add(letter, randomNumber);

		}

		PickRandomButtonToReveal();

	}

	/// <summary>
	/// Random letters are revealed
	/// </summary>
	private void PickRandomButtonToReveal()
	{
		List<StringButton> letterButtons = m_StringButtons.FindAll(x => x.IsLetter == true);
		for (int i = 0; i < 5; i++)
		{
			letterButtons[Random.Range(0, letterButtons.Count)].Reveal();
		}
	}

	/// <summary>
	/// Set the selected Button
	/// </summary>
	/// <param name="button">Button to be selected</param>
	public void SetSelectedButton(StringButton button)
	{
		SelectedButton = button;
	}

	/// <summary>
	/// Get The character pressed
	/// </summary>
	/// <param name="c">input Character</param>
	public void InputPressed(char c)
	{
		if(SelectedButton == null) 
			return;

		c = char.ToUpper(c);
		SelectedButton.SetInput(c);

		IsAllFilled();
	}

	public void IsAllFilled()
	{
		foreach (StringButton button in m_StringButtons)
		{
			if (!button.IsCorrect)
			{
				return;
			}
		}

		m_GameManger.GameWin();
	}

	/// <summary>
	/// Call to create a new puzzle. This is where the game starts.
	/// </summary>
	public void NewPuzzle()
	{
		DeletePreviousButtons();
		CleanUpDictionary();

		if (m_StringList == null || m_StringList.Count == 0)
			return;

		PickRandomString();

		m_StringPair = new Dictionary<char, int>();
		m_StringButtons = new List<StringButton>();

		CreateStringButtons();
	}

	/// <summary>
	/// get random string for the string list.
	/// </summary>
	private void PickRandomString()
	{
		m_DisplayString = m_StringList[Random.Range(0, m_StringList.Count)];
	}

	public void CleanUpDictionary()
	{
		m_StringPair?.Clear();
	}

	/// <summary>
	/// Cleans up the previous buttons.
	/// </summary>
	private void DeletePreviousButtons()
	{
		if (m_StringButtons == null)
			return;

		foreach(StringButton button in m_StringButtons)
		{
			Destroy(button.gameObject);
		}

		m_StringButtons.Clear();
	}
}
