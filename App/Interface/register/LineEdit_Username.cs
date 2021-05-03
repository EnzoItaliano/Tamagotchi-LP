using Godot;
using System;

public class LineEdit_Username : LineEdit {
	
	public string get_username_text() {
		return Convert.ToString(Text);
	}
	
	public override void _Ready() {
		
	}


}
