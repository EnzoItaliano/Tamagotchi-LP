using Godot;
using System;

public class LineEdit_Confirm : LineEdit {
	public string get_confirm_text() {
		return Convert.ToString(Text);
	}

	public override void _Ready() {
		
	}
}
