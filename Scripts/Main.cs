using Godot;
using System;

public class Main : Node
{
    [Export]
    private PackedScene _mob;

    public int Score { get; set; }

    private Random rand = new Random();
    
    private HUD _hud;

    public override void _Ready()
    {
        ShowTouchController(false);
        _hud = GetNode("HUD") as HUD;
    }

    private void ShowTouchController(bool show)
    {
        if (OS.HasTouchscreenUiHint() && show)
        {
            (GetNode("Controller") as Controller).Show();
        }
        else
        {
            (GetNode("Controller") as Controller).Hide();
        }

    }

    private float RandRand(float min, float max)
    {
        return (float)(rand.NextDouble() * (max - min) + min);
    }

    public void GameOver()
    {
        var mobTimer = GetNode("MobTimer") as Timer;
        var scoreTimer = GetNode("ScoreTimer") as Timer;

        (GetNode("Music") as AudioStreamPlayer).Stop();
        (GetNode("DeathSound") as AudioStreamPlayer).Play();
        
        ShowTouchController(false);
        scoreTimer.Stop();
        mobTimer.Stop();

        _hud.ShowGameOver();
    }

    public void NewGame()
    {
        Score = 0;
        var player = GetNode("Player") as Player;
        var startTimer = GetNode("StartTimer") as Timer;
        var startPosition = GetNode("StartPosition") as Position2D;

        (GetNode("Music") as AudioStreamPlayer).Play();

        player.Start(startPosition.Position);
        ShowTouchController(true);
        startTimer.Start();

        _hud.UpdateScore(Score);
        _hud.ShowMessage("Get Ready!");
    }

    public void OnStartTimerTimeout()
    {
        var mobTimer = GetNode("MobTimer") as Timer;
        var scoreTimer = GetNode("ScoreTimer") as Timer;

        scoreTimer.Start();
        mobTimer.Start();
    }

    public void OnScoreTimerTimeout()
    {
        Score += 1;
        _hud.UpdateScore(Score);
    }

    public void OnMobTimerTimeout()
    {
        var mobSpawnLocation = GetNode("MobPath/MobSpawnLocation") as PathFollow2D;
        mobSpawnLocation.SetOffset(rand.Next());

        var mobInstance = _mob.Instance() as RigidBody2D;
        AddChild(mobInstance);

        // 2pi = 360
        // pi = 180
        // p1 / 2 = 90;
        // Rotaciona em L perpendicular
        float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

        mobInstance.Position = mobSpawnLocation.Position;

        direction += RandRand(-Mathf.Pi / 4, Mathf.Pi / 4);
        mobInstance.Rotation = direction;

        mobInstance.SetLinearVelocity(new Vector2(RandRand(150f, 250f), 0).Rotated(direction));
    }
}
