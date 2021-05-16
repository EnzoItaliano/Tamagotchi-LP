using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class server : Node {
	String REGISTER_URL = "http://localhost:3000/user";
	String LOGIN_URL = "http://localhost:3000/user/?username=";
	String REGISTER_PET_URL = "http://localhost:3000/pet";
	String GET_PET_ACCOUNT_URL = "http://localhost:3000/pet/?account=";
	String GET_PET_URL = "http://localhost:3000/pet/";
	String GET_PET_UPDATE_URL = "http://localhost:3000/pet";
	

	private String MyDictionaryToJson(Dictionary<string, string> dict) {
		var entries = dict.Select(d =>
			string.Format("\"{0}\": \"{1}\"", d.Key, string.Join(",", d.Value)));
		return "{" + string.Join(",", entries) + "}";
	}

	public void register(String username, String password, HTTPRequest http) {
		Dictionary<string, string> body = new Dictionary<string, string>() {
			{"username", username},
			{"password", password}
		};

		string[] headers = new string[] { "Content-Type: application/json" };
		var result = http.Request(REGISTER_URL, headers, false, HTTPClient.Method.Post, MyDictionaryToJson(body));

	}

	public void login(String username, String password, HTTPRequest http) {
		Dictionary<string, string> body = new Dictionary<string, string>() {
			{"username", username},
			{"password", password}
		};

		LOGIN_URL += username;
		string[] headers = new string[] { "Content-Type: application/json" };
		var result = http.Request(LOGIN_URL, headers, false, HTTPClient.Method.Get, MyDictionaryToJson(body));
	}

	public void register_pet(String name_pet, int outfit_pet, int account_id , HTTPRequest http) {
		String body = "{\"name\": \"" + name_pet + "\", \"outfit\": " + Convert.ToString(outfit_pet) 
			+ ", \"account\": {\"id\": " +  Convert.ToString(account_id) + "}}";

		string[] headers = new string[] { "Content-Type: application/json" };
		var result = http.Request(REGISTER_PET_URL, headers, false, HTTPClient.Method.Post, body);
	}

	public void get_pet_account(int account_id, HTTPRequest http) {
		GET_PET_ACCOUNT_URL += Convert.ToString(account_id);

		string[] headers = new string[] { "Content-Type: application/json" };
		var result = http.Request(GET_PET_ACCOUNT_URL, headers, false, HTTPClient.Method.Get);
	}

	public void get_pet(int pid, HTTPRequest http) {
		GET_PET_URL += Convert.ToString(pid);

		string[] headers = new string[] { "Content-Type: application/json" };
		var result = http.Request(GET_PET_URL, headers, false, HTTPClient.Method.Get);
	}

	public void update_pet(int pid, string json, HTTPRequest http) {
		GD.Print(json);

		string[] headers = new string[] { "Content-Type: application/json" };
		var result = http.Request(GET_PET_UPDATE_URL, headers, false, HTTPClient.Method.Post, json);
	}

}
