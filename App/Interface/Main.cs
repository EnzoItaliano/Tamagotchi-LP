using Godot;
using System;

public class Main : Control {
	private void _on_RegisterButton_pressed() {
		GetTree().ChangeScene("res://Interface/register/Register.tscn");
	}

	private void _on_LoginButton_pressed() {
		GetTree().ChangeScene("res://Interface/login/Login.tscn");
	}
}






