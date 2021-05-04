using Godot;
using System;

public class LineEdit_LoginPassword : LineEdit {
	public string get_LoginPassword_text() {
		return Convert.ToString(Text);
	}
}
