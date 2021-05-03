using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class server : Node {
	String REGISTER_URL = "";
	String LOGIN_URL = "";
	String current_token = "";
	

	private String MyDictionaryToJson(Dictionary<string, string> dict) {
		var entries = dict.Select(d =>
			string.Format("\"{0}\": [{1}]", d.Key, string.Join(",", d.Value)));
		return "{" + string.Join(",", entries) + "}";
	}

	private string _get_token_id_result(Godot.SignalAwaiter result) {
		
		return "";
	}

	public void register(String email, String password, HTTPRequest http) {
		Dictionary<string, string> body = new Dictionary<string, string>() {
			{"email", email},
			{"password", password}
		};

		http.Request(REGISTER_URL, [], false, HTTPClient.METHOD_POST, MyDictionaryToJson(body));
		var result = ToSignal(http, "request completed");

		if (result[1] == 200) {
			current_token = _get_token_id_result(result);
		}

	}
}
