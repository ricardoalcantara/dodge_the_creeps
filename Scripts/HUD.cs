using Godot;
using System;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void StartGame();

    public void ShowMessage(string text)
    {
        var messageTimer = GetNode("MessageTimer") as Timer;
        var messageLabel = GetNode("MessageLabel") as Label;

        messageLabel.Text = text;
        messageLabel.Show();
        messageTimer.Start();
    }

    async public void ShowGameOver()
    {
        var startButton = GetNode("StartButton") as Button;
        var messageTimer = GetNode("MessageTimer") as Timer;
        var messageLabel = GetNode("MessageLabel") as Label;

        ShowMessage("Game Over");
        await ToSignal(messageTimer, "timeout");
        messageLabel.Text = "Dodge the Creeps!";
        messageLabel.Show();
        startButton.Show();
    }

    public void UpdateScore(int score)
    {
        var scoreLabel = GetNode("ScoreLabel") as Label;
        scoreLabel.Text = score.ToString();
    }

    public void OnStartButtonPressed()
    {
        var startButton = GetNode("StartButton") as Button;
        startButton.Hide();

        EmitSignal("StartGame");
    }

    public void OnMessageTimerTimeout()
    {
        var messageLabel = GetNode("MessageLabel") as Label;
        messageLabel.Hide();
    }
}
