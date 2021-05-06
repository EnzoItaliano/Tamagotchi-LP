using Godot;
using System;

public class OptionButton : Godot.OptionButton {

	public override void _Ready() {
		AddItem("Branca");
		AddItem("Morena");
		AddItem("Loira");
		AddItem("Ruiva");
		change_Outfit_Create_Pet();
	}

	private void change_Outfit_Create_Pet() {
		string local_file = "res://Pet/Outfits/";
		string extension_file = ".png";
		string file_name = "";
		var girl_sprite = GetNode("Preview_Sprite");

		if (Selected == 0) {
			Text = "Branca";
			file_name = "Branca/Normal";
		}
		else if (Selected == 1) {
			Text = "Morena";
			file_name = "Morena/Normal";
		}
		else if (Selected == 2) {
			Text = "Loira";
			file_name = "Loira/Normal";
		}
		else {
			Text = "Ruiva";
			file_name = "Ruiva/Normal";
		}

		file_name = local_file + file_name + extension_file;
		var img = (Texture)GD.Load(file_name);

		girl_sprite.Call("update_Preview_Sprite", img);
	}

	public int get_OutfitPet_id() {
		return Selected;
	}

	private void _on_OptionButton_item_selected(int index) {
		change_Outfit_Create_Pet();
	}
}



