using Godot;
using System;

public class LineEdit_Username : LineEdit {
	
	public string get_username_text() {
		return Convert.ToString(Text);
	}

	public void set_username_text(String text) {
		Text = text;
	}
	
	public override void _Ready() {
		
	}


}
