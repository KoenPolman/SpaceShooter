using System;

public class TextManegement
{
	public string currentText = "";
	int score = 0;
	public char state = 'g';
	public void SetScore()
	{
		score = 0;
		state = 'g';
		currentText = "score: " + score;
	}
	public void AddScore()
	{
		score++;
	}
	public void GameOver()
	{
		state = 'o';
		currentText = "GAME OVER\nscore: " + score + "\npress 'r' to restart";
	}
}
