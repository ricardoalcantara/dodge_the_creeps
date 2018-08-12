using Godot;
using System;

public class Mob : RigidBody2D
{
    [Export]
    private int _minSpeed = 150;

    [Export]
    private int _maxSpeed = 250;

    private string[] _mobTypes = { "walk", "swim", "fly" };

    public override void _Ready()
    {
        var animatedSprite = GetNode("AnimatedSprite") as AnimatedSprite;

        var randomMob = new Random();

        animatedSprite.Animation = _mobTypes[randomMob.Next(0, _mobTypes.Length)];
    }

    public void OnVisibilityScreenExited()
    {
        QueueFree();
    }
}
