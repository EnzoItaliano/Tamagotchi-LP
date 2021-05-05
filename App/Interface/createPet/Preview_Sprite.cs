using Godot;
using System;

public class Preview_Sprite : Sprite {
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		
	}

	public void update_Preview_Sprite(Texture img) {
		Texture = img;
	}
}
