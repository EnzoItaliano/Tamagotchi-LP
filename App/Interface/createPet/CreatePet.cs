using Godot;
using System;
using System.Text;

public class CreatePet : Control {

	public override void _Ready() {
		
	}
	private void _on_CreateHTTPRequest_request_completed(int result, int response_code,  string[] headers, byte[] body) {
		JSONParseResult json = JSON.Parse(Encoding.UTF8.GetString(body));
		GD.Print(json.Result);
		var notification = GetNode("CreateNotification");

		if (json.Result == null){
			notification.Call("update_CreateNotification_text", "Erro na criação do Pet!");
		}
		else {
			GetTree().ChangeScene("res://Interface/ListPet/ListPet.tscn");
		}
	}
	
	private void _on_CreateButton_pressed() {
		var pet_name = GetNode("LineEdit_NamePet");
		var pet_outfit = GetNode("OptionButton");
		var notification = GetNode("CreateNotification");
		var http = GetNode<HTTPRequest>("CreateHTTPRequest");
		var globalVariables = (Global)GetNode("/root/Global");

		string pet_name_text = Convert.ToString(pet_name.Call("get_NamePet_text"));
		int pet_outfit_id = (int)pet_outfit.Call("get_OutfitPet_id");
		int account_id = globalVariables.account_id;

		if (String.IsNullOrEmpty(pet_name_text)) {
			notification.Call("update_CreateNotification_text", "Nome inválido!");
			return;
		}

		server server = new server();
		server.register_pet(pet_name_text, pet_outfit_id, account_id, http);
	}
	
	private void _on_CreateBackButton_pressed() {
		GetTree().ChangeScene("res://Interface/ListPet/ListPet.tscn");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}



