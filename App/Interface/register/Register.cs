using Godot;
using System;
using System.Text;
using Newtonsoft.Json.Linq;

public class Register : Control {

	private void _on_HTTPRequest_request_completed(int result, int response_code, string[] headers, byte[] body) {
		JSONParseResult json = JSON.Parse(Encoding.UTF8.GetString(body));
		GD.Print(json.Result);

		
		var value = JObject.Parse(Encoding.UTF8.GetString(body));

		var notification = GetNode("Notification");
		if (json.Result == null){
			notification.Call("update_notification_text", "Erro na requisição");
		}
		else if (!(bool)value["isSuccess"]) {
			notification.Call("update_notification_text", "Já existe um usuário com esse nome!");
		}
		else {
			notification.Call("update_notification_text", "Conta criada com sucesso!");
			var username = GetNode("LineEdit_Username");
			var password = GetNode("LineEdit_Password");
			var confirm = GetNode("LineEdit_Confirm");
			
			username.Call("set_username_text", "");
			password.Call("set_password_text", "");
			confirm.Call("set_confirm_text", "");
		}

	}


	private void _on_RegisterButton_pressed() {
		var username = GetNode("LineEdit_Username");
		var password = GetNode("LineEdit_Password");
		var confirm = GetNode("LineEdit_Confirm");
		var notification = GetNode("Notification");
		var http = GetNode<HTTPRequest>("HTTPRequest");
	
		string username_text = Convert.ToString(username.Call("get_username_text"));
		string password_text = Convert.ToString(password.Call("get_password_text"));
		string confirm_text = Convert.ToString(confirm.Call("get_confirm_text"));


		if (String.IsNullOrEmpty(password_text) || !password_text.Equals(confirm_text) || String.IsNullOrEmpty(username_text)) {
			notification.Call("update_notification_text", "Senha ou usuário inválido.");
			return;
		}

		server server = new server();
		server.register(username_text, password_text, http);
	}

	private void _on_BackButton_pressed() {
		GetTree().ChangeScene("res://Interface/Main.tscn");
	}
}



