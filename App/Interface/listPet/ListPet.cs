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

	private void _on_SelectedPetButton_pressed (int pid) {
		var globalVariables = (Global)GetNode("/root/Global");
		globalVariables.pet_id = pid;
		GetTree().ChangeScene("res://Pet/Pet.tscn");
	}


	private void CreateButtonPet(int pid, int top, JToken item) {
		var button = new Button();
		var name_pet = (string)item["name"];
		button.Text = name_pet;
		button.Name = Convert.ToString(pid);

		StyleBox theme = (StyleBox)GD.Load("res://assets/buttonStyle.tres");
		button.AddStyleboxOverride("normal", theme);
		button.AddStyleboxOverride("hover", theme);

		theme = (StyleBox)GD.Load("res://assets/input_styleboxflat.tres");
		button.AddStyleboxOverride("pressed", theme);

		Font them_font = (Font)GD.Load("res://assets/bold_dynamicfont.tres");
		button.AddFontOverride("font", them_font);

		Color color_pressed = new Color(0.82f, 0.16f, 1.0f);
		button.AddColorOverride("font_color_pressed", color_pressed);
		color_pressed = new Color(1.0f, 1.0f, 1.0f);
		button.AddColorOverride("font_color", color_pressed);

		var position = new Vector2();
		
		position.x = 140;
		position.y = top;
		button.SetPosition(position);

		position.x = 450;
		position.y = 70;
		button.SetSize(position);

		button.Connect("pressed", this, "_on_SelectedPetButton_pressed",  new Godot.Collections.Array() {pid});
		
		button.Show();
		AddChild(button);
	}
	
	private void _on_ListPetHTTPRequest_request_completed(int result, int response_code, string[] headers, byte[] body) {
		JSONParseResult json = JSON.Parse(Encoding.UTF8.GetString(body));
		GD.Print(json.Result);
		var value = JObject.Parse(Encoding.UTF8.GetString(body));

		var childrens = (JArray)JArray.Parse((string)value["response"]);

		int top = 80;
		foreach (var item in childrens) {
			top += 90;
			int pid = (int)item["id"];
			CreateButtonPet(pid, top, item);
		}

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		var http = GetNode<HTTPRequest>("ListPetHTTPRequest");
		var globalVariables = (Global)GetNode("/root/Global");
		server server = new server();
		server.get_pet_account(globalVariables.account_id, http);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
