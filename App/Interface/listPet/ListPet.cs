using Godot;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

public class ListPet : Control {

	

	private void _on_ListPetBackButton_pressed() {
		var globalVariables = (Global)GetNode("/root/Global");
		globalVariables.account_id = -1;
		GetTree().ChangeScene("res://Interface/login/Login.tscn");
	}


	private void _on_CreatePetButton_pressed() {
		GetTree().ChangeScene("res://Interface/createPet/CreatePet.tscn");
	}
	
	private void _on_ListPetHTTPRequest_request_completed(int result, int response_code, string[] headers, byte[] body) {
		JSONParseResult json = JSON.Parse(Encoding.UTF8.GetString(body));
		GD.Print(json.Result);
		var value = JObject.Parse(Encoding.UTF8.GetString(body));
		GD.Print(value);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {

	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
