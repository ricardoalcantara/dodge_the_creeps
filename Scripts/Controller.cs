using Godot;
using System;

public class Controller : CanvasLayer
{
    public void Show()
    {
        (GetNode("Up") as TouchScreenButton).Show();
        (GetNode("Down") as TouchScreenButton).Show();
        (GetNode("Left") as TouchScreenButton).Show();
        (GetNode("Right") as TouchScreenButton).Show();
    }

    public void Hide()
    {
        (GetNode("Up") as TouchScreenButton).Hide();
        (GetNode("Down") as TouchScreenButton).Hide();
        (GetNode("Left") as TouchScreenButton).Hide();
        (GetNode("Right") as TouchScreenButton).Hide();
    }
}
