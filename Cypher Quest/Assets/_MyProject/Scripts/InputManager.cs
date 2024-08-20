using UnityEngine;

public class InputManager : MonoBehaviour
{
	[SerializeField] private StringManager StringManager;

	private void Update()
	{
		foreach(char c in Input.inputString)
		{
			if (char.IsLetter(c))
			{
				StringManager.InputPressed(c);
			}
		}
	}
}
