using Godot;
using System;

public class LineEdit_Password : LineEdit {
	public string get_password_text() {
		return Convert.ToString(Text);
	}

	public void set_password_text(String text) {
		Text = text;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		
	}


}
