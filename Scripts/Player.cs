using Godot;
using System;

public class Player : Area2D
{
    [Signal]
    public delegate void Hit();

    [Export]
    public int _speed = 0;

    private Vector2 _screenSize;

    public override void _Ready()
    {
        _screenSize = GetViewport().GetSize();
        Hide();
    }

    public override void _Process(float delta)
    {
        var velocity = new Vector2();
        if (Input.IsActionPressed("ui_right"))
        {
            velocity.x += 1;
        }

        if (Input.IsActionPressed("ui_left"))
        {
            velocity.x -= 1;
        }

        if (Input.IsActionPressed("ui_down"))
        {
            velocity.y += 1;
        }

        if (Input.IsActionPressed("ui_up"))
        {
            velocity.y -= 1;
        }

        var animatedSprite = GetNode("AnimatedSprite") as AnimatedSprite;
        var trail = GetNode("Trail") as Particles2D;
        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * _speed;
            animatedSprite.Play();
            trail.Emitting = true;
        }
        else
        {
            animatedSprite.Stop();
            trail.Emitting = false;
        }

        Position += velocity * delta;
        Position = new Vector2(
            Mathf.Clamp(Position.x, 0, _screenSize.x),
            Mathf.Clamp(Position.y, 0, _screenSize.y)
        );

        if (velocity.x != 0)
        {
            animatedSprite.Animation = "right";
        }
        else if (velocity.y != 0)
        {
            animatedSprite.Animation = "up";
        }

        animatedSprite.FlipH = velocity.x < 0;
        animatedSprite.FlipV = velocity.y > 0;

        // if (velocity.x != 0)
        // {
        //     animatedSprite.Animation = "right";
        //     animatedSprite.FlipH = velocity.x < 0;
        //     animatedSprite.FlipV = false;
        // }
        // else if (velocity.y != 0)
        // {
        //     animatedSprite.Animation = "up";
        //     animatedSprite.FlipV = velocity.y > 0;
        // }
    }

    public void OnPlayerBodyEntered(Godot.Object body)
    {
        Hide(); // Player disappears after being hit.
        EmitSignal("Hit");

        // For the sake of this example, but it's better to create a class var
        // then assign the variable inside _Ready()
        var collisionShape2D = GetNode("CollisionShape2D") as CollisionShape2D;
        collisionShape2D.Disabled = true;
    }

    public void Start(Vector2 pos)
    {
        Position = pos;
        Show();

        var collisionShape2D = GetNode("CollisionShape2D") as CollisionShape2D;
        collisionShape2D.Disabled = false;
    }
}
