using Godot;
using System;

public class Room : TextureRect {

	public void update_room_image(Texture image) {
		Texture = image;
	}
	
	public override void _Ready() {
		
	}
}
