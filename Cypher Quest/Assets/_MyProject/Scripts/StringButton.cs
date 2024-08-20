using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages each button functionality
/// </summary>
public class StringButton : MonoBehaviour
{
	[SerializeField, Tooltip("Reference for the text displayed")]
	private TextMeshProUGUI m_StringLetter;

	private int m_NumberValue;
	[SerializeField, Tooltip("Reference for the number displayed")]
	private TextMeshProUGUI m_LetterNumber;

	private char m_Letter;
	public bool IsLetter;

	public StringManager StringManger;

	public bool IsCorrect;

	/// <summary>
	/// Called when the button is clicked
	/// </summary>
	public void OnClick()
	{
		// Button click
		Debug.Log("Button Clicked : " + m_StringLetter.text);
		// set selected in the string manager

		StringManger.SetSelectedButton(this);
	}

	public void SetLetter(char letter, bool isLetter = true)
	{
		m_Letter = letter;

		IsLetter = isLetter;

		if (!isLetter)
		{
			m_StringLetter.text = letter.ToString();
			GetComponent<Button>().interactable = false;
			IsCorrect = true;
		}
		else
		{
			m_StringLetter.text = "_";
			IsCorrect = false;
		}

	}

	public void SetNumberValue(int randomNumber)
	{
		m_NumberValue = randomNumber;
		m_LetterNumber.text = m_NumberValue.ToString();
	}

	public void Reveal()
	{
		m_StringLetter.text = m_Letter.ToString();
		IsCorrect = true;
	}

	public void SetInput(char c)
	{
		m_StringLetter.text = c.ToString();

		if (c == m_Letter)
		{
			// letter is true;
			m_StringLetter.color = Color.green;
			IsCorrect = true;
		}
		else
		{
			// letter is false;
			m_StringLetter.color = Color.red;
			IsCorrect = false;	
		}
	}
}
