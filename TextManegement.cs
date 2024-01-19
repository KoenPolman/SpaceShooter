using System;
using System.Numerics;

public class TextManegement
{
	public Vector2 position = new Vector2(400, 400);
	public string currentText = "";
	int[] score = new int[] {0, 0, 0};
	public char state = 'm';
	public string UIText(int playerCount)
	{
		switch (state)
		{
			case 'g': //g voor game
				currentText = "";
				for (int i = 0; i < playerCount; i++)
				{
					currentText += "\n\nplayer " + (i + 1) + " score: " + score[i];
				}
				break;
			case 'm': //m voor menu
                position = new Vector2(600, 400);
                currentText = "SPACESHIP BATTLE ARENA\n\npress '1' to start single player\npress '2' to start local 1v1\npress '3' to start local 1v1v1\npress ESC at any time to quit the program";
				break;
			case 't': //t voor tutorial
                position = new Vector2(100, 200);
                currentText = "Press R to return to the main menu\nPlayer 1\npress WASD to move and C to fire\n\n";
				if (playerCount >= 2)
				{
					currentText = currentText + "Player 2\npress IJKL to move and . to fire\n\n";
				}
				if (playerCount >= 3)
				{
					currentText = currentText + "Player 3\npress TFGH to move and N to fire\n\n";
				}
				break;
		}
		return currentText;
	}
	public void ResetScore()
	{
		score = new int[] { 0, 0, 0 };
	}
	public void AddScore(int playerIndex)
	{
		score[playerIndex]++;
	}
}
