using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class server : Node {
	String REGISTER_URL = "http://localhost:3000/user";
	String LOGIN_URL = "http://localhost:3000/user";
	

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

		string[] headers = new string[] { "Content-Type: application/json" };
		var result = http.Request(LOGIN_URL, headers, false, HTTPClient.Method.Get, MyDictionaryToJson(body));

	}
}
