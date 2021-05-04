using Godot;
using System;
using System.Text;

public class Login : Control {
	
	private void _on_LoginHTTPRequest_request_completed(int result, int response_code, string[] headers, byte[] body) {
		JSONParseResult json = JSON.Parse(Encoding.UTF8.GetString(body));
		GD.Print(json.Result);

		// var notification = GetNode("LoginNotification");
		// if (json.Result == null){
		// 	notification.Call("update_LoginNotification_text", "Usuário ou Senha incorreto!");
		// }
		// else {
		// 	GD.Print("Entrou");
		// 	// GetTree().ChangeScene("res://Interface/login/Login.tscn");
		// }
	}
	
	private void _on_LoginButton_pressed() {
		var username = GetNode("LineEdit_LoginUsername");
		var password = GetNode("LineEdit_LoginPassword");
		var notification = GetNode("LoginNotification");
		var http = GetNode<HTTPRequest>("LoginHTTPRequest");

		string username_text = Convert.ToString(username.Call("get_LoginUsername_text"));
		string password_text = Convert.ToString(password.Call("get_LoginPassword_text"));

		if (String.IsNullOrEmpty(password_text) || String.IsNullOrEmpty(username_text)) {
			notification.Call("update_LoginNotification_text", "Senha ou usuário inválido.");
			return;
		}

		server server = new server();
		server.login(username_text, password_text, http);
	}
	
	private void _on_BackButton_pressed() {
		GetTree().ChangeScene("res://Interface/Main.tscn");
	}
}
