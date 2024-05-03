using System;
using System.Numerics;

public class TextManagement
{
	public Vector2 position    = new Vector2(400, 400);
	public string  currentText = "";
	int[]          score       = new int[] {0, 0, 0 };
	public char    state = 'm';

	public TextManagement()
	{

	}
	public string UIText(int playerCount, int[] score)
    {
		switch (state)
		{
			case 'g': //g for game
				currentText = "";
				for (int i = 0; i < playerCount; i++)
				{
					currentText += "player " + (i + 1) + " score: " + score[i] + "\n\n";
				}
				break;
			case 'm': //m for menu
                position = new Vector2(500, 100);
                currentText = "SPACESHIP BATTLE ARENA\n\npress '2' to start local 1v1\npress '3' to start local 1v1v1\npress ESC at any time to quit the program";
				break;
			case 't': //t for tutorial
                position = new Vector2(50, 50);
                currentText = "Press R to return to the main menu\nPlayer 1\npress WASD to move and C to fire\n\n";
				if (playerCount >= 2)
				{
					currentText = currentText + "Player 2\npress IJKL to move and M to fire\n\n";
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
