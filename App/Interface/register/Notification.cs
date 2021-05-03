using Godot;
using System;

public class Notification : Label {
	
	public void update_notification_text(string notification) {
		Text = notification;
	}

	public override void _Ready() {
		
	}
}
