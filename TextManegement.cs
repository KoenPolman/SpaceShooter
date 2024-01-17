using System;

public class TextManegement
{
	public string currentText = "";
	int score = 0;
	public char state = 'm';
	public string UIText(int playerCount)
	{
		switch (state)
		{
			case 'g': //g voor game
                currentText = "score: " + score;
				break;
			case 'o': //o voor over
                currentText = "GAME OVER\nscore: " + score + "\npress 'r' to restart";
                break;
			case 'm': //m voor menu
                currentText = "SPACESHIP BATTLE ARENA\npress '1' to start single player\npress '2' to start hotseat 1v1\npress '3' to start hotseat 1v1v1\npress ESC at any time to quit the program";
                break;
			case 't': //t voor tutorial
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
}
