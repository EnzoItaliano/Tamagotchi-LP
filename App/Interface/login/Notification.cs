using Godot;
using System;

public class LoginNotification : Label {
	public void update_LoginNotification_text(string notification) {
		Text = notification;
	}
}
