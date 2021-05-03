using Godot;
using System;

public class Register : Control {

	private void _on_HTTPRequest_request_completed(int result, int response_code, string[] headers, byte[] body) {
		// Replace with function body.
	}


	private void _on_RegisterButton_pressed() {
		var username = GetNode("LineEdit_Username");
		var password = GetNode("LineEdit_Password");
		var confirm = GetNode("LineEdit_Confirm");
		var notification = GetNode("Notification");
		var http = GetNode("HTTPRequest");
	
		string username_text = Convert.ToString(username.Call("get_username_text"));
		string password_text = Convert.ToString(password.Call("get_password_text"));
		string confirm_text = Convert.ToString(confirm.Call("get_confirm_text"));

		if (password_text == null || password_text != confirm_text || username == null) {
			notification.Call("update_notification_text", "Senha ou usuário inválido.");
			return;
		}
		server.register(Convert.ToString(username.Call("get_username_text")), Convert.ToString(password.Call("get_password_text")), http);
	}
}



