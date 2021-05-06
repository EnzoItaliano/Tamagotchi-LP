using Godot;
using System;

public class LineEdit_NamePet : LineEdit {
	
	public string get_NamePet_text() {
		return Convert.ToString(Text);
	}
}
