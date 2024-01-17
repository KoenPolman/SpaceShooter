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
                currentText = "ASTEROID DEFENSE FORCE\npress '1' to start";
                break;
			case 't': //t voor tutorial
				currentText = "Player 1\npress \n       W\n      ASD\n         to move\nand\n    C\n    to fire\n\n";
				if (playerCount <= 2)
				{
					currentText = currentText + "Player 2\npress \n       I\n      JKL\n         to move\nand\n    .\n    to fire\n\n";
				}
				if (playerCount <= 3)
				{
                    currentText = currentText + "Player 3\npress \n       T\n      FGH\n         to move\nand\n    N\n    to fire\n\n";
                }
                break;
        }
		return currentText;
	}
}
