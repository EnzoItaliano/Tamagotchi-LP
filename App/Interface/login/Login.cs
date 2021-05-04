using Godot;
using System;
using System.Text;

public class Login : Control {
	
	private void _on_HTTPRequest_request_completed(int result, int response_code, string[] headers, byte[] body) {
		JSONParseResult json = JSON.Parse(Encoding.UTF8.GetString(body));
		GD.Print(json.Result);

		var notification = GetNode("LoginNotification");
		if (json.Result == null){
			notification.Call("update_LoginNotification_text", "Usuário ou Senha incorreto!");
		}
		else {
			GD.Print("Entrou");
			// GetTree().ChangeScene("res://Interface/login/Login.tscn");
		}
	}
	
	private void _on_LoginButton_pressed() {
		// Replace with function body.
	}
	
	private void _on_BackButton_pressed() {
		GetTree().ChangeScene("res://Interface/Main.tscn");
	}
}
