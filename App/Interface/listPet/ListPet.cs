using Godot;
using System;

public class ListPet : Control {
	
	private void _on_ListPetBackButton_pressed() {
		GetTree().ChangeScene("res://Interface/login/Login.tscn");
	}


	private void _on_CreatePetButton_pressed() {
		GetTree().ChangeScene("res://Interface/createPet/CreatePet.tscn");
	}
	
	private void _on_ListPetHTTPRequest_request_completed(int result, int response_code, string[] headers, byte[] body) {
		// Replace with function body.
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
