using Godot;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;

public class Login : Control {
	string username_text = "";
	string password_text = "";
	private void _on_LoginHTTPRequest_request_completed(int result, int response_code, string[] headers, byte[] body) {
		JSONParseResult json = JSON.Parse(Encoding.UTF8.GetString(body));
		GD.Print(json.Result);
		var value = JObject.Parse(Encoding.UTF8.GetString(body));
		var notification = GetNode("LoginNotification");
		string request_username;
		string request_password;
		
		try {
			request_username = (string)JArray.Parse((string)value["response"]).Children()["username"].First();
			request_password = (string)JArray.Parse((string)value["response"]).Children()["password"].First();
		}
		catch (System.Exception) {
			notification.Call("update_LoginNotification_text", "Usuário ou Senha incorreto!");
			return;
		}

		if (json.Result == null){
			notification.Call("update_LoginNotification_text", "Usuário ou Senha incorreto!");
		}
		else if (request_username == username_text && request_password == password_text) {
			int request_account_id = (int)JArray.Parse((string)value["response"]).Children()["id"].First();
			var globalVariables = (Global)GetNode("/root/Global");

			globalVariables.account_id = request_account_id;
			GetTree().ChangeScene("res://Interface/ListPet/ListPet.tscn");
		}
		else {
			notification.Call("update_LoginNotification_text", "Usuário ou Senha incorreto!");
		}
	}
	
	private void _on_LoginButton_pressed() {
		var username = GetNode("LineEdit_LoginUsername");
		var password = GetNode("LineEdit_LoginPassword");
		var notification = GetNode("LoginNotification");
		var http = GetNode<HTTPRequest>("LoginHTTPRequest");

		username_text = Convert.ToString(username.Call("get_LoginUsername_text"));
		password_text = Convert.ToString(password.Call("get_LoginPassword_text"));

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
