using Godot;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Dynamic;
using System.Globalization;

public class Pet : Godot.Node2D {
	// Função de update do jogo.
	[Export] NodePath feed_button_path;
	[Export] NodePath toilet_button_path;
	[Export] NodePath play_button_path;
	[Export] NodePath cure_button_path;
	[Export] NodePath light_button_path;

	public int pid;


	// CONSTANTES
	public const int GREAT = 80;
	public const int TERRIBLE = 20;

	// Declare member variables here.
	private DateTime last_login;
	private string name;

	// ATRIBUTOS
	private int outfit;
	private int happy; //
	private int hunger; //
	private int health; //

	// STATUS
	private int tired; // tired
	private int dirty; // tempo
	private int sad; // 
	private int sleeping; //

	private bool normal; // ou está normal ou está doente
	private bool sick; //
	private bool dead; // vida chega em 0 morre
	private bool light; // apaga ou desliga



	private void _on_PetHTTPRequest_request_completed(int result, int response_code, string[] headers, byte[] body) {
		var value = JObject.Parse(Encoding.UTF8.GetString(body));
		
		GD.Print("\n\n");
		var pet = JObject.Parse((string)value["response"]);
	  	
		CultureInfo provider = CultureInfo.InvariantCulture;
		string format;
		format = "dd/MM/yyyy HH:mm:ss";
		
		last_login = DateTime.ParseExact(Convert.ToString(pet["time"]), format, provider);
		// GD.Print(last_login);
		
		happy = (int)pet["happy"];
		sad = (int)pet["sad"];
		hunger = (int)pet["hunger"];
		health = (int)pet["health"];
		tired = (int)pet["tired"];
		sleeping = (int)pet["sleeping"];
		dirty = (int)pet["dirty"];

		normal = (bool)pet["normal"];
		sick = (bool)pet["sick"];
		dead = (bool)pet["dead"];
		light = (bool)pet["light"];

		outfit = (int)pet["outfit"];
		name = (string)pet["name"];


		if(!light || dead) {
			state_button(true);
		}
	}

	private void _on_PetBackButton_pressed() {
		var http = GetNode<HTTPRequest>("PetUpdateHTTPRequest");

		var globalVariables = (Global)GetNode("/root/Global");
		
		
		dynamic json = new ExpandoObject();
		json.id = pid;
		json.name = name;
		json.outfit = outfit;
		json.happy = happy;
		json.hunger = hunger;
		json.health = health;
		json.tired = tired;
		json.dirty = dirty;
		json.sad = sad;
		json.sleeping = sleeping;
		json.normal = normal;
		json.sick = sick;
		json.dead = dead;
		json.light = light;
		json.time = Convert.ToString(last_login);

		int acc_id = globalVariables.account_id;
		dynamic account_json = new ExpandoObject();
		
		account_json.id = acc_id;
		json.account = account_json;

		string json_string = Newtonsoft.Json.JsonConvert.SerializeObject(json);
		
		server server = new server();
		server.update_pet(pid, json_string, http);	

	}

	private void _on_PetUpdateHTTPRequest_request_completed(int result, int response_code, string[] headers, byte[] body) {
		JSONParseResult json = JSON.Parse(Encoding.UTF8.GetString(body));
		// GD.Print(json.Result);
		
		var globalVariables = (Global)GetNode("/root/Global");
		globalVariables.pet_id = -1;
		GetTree().ChangeScene("res://Interface/ListPet/ListPet.tscn");
	}


	// AÇÕES
	public void action_feed(int pid) {
		int random_feed = random_num(1, 11);
		
		hunger += random_feed;

		if (hunger >= 100) {
			hunger = 100;
		}

		set_hunger(pid, hunger);

		update_outfit(pid);
		update_interface();
	}

	public void action_toilet(int pid) {
		int random_clean = random_num(1, 51);
		
		if (light && !dead) {
			dirty += random_clean;	
		}

		if (dirty > 100) {
			dirty = 100;
		}

		set_dirty(pid, dirty);

		update_outfit(pid);
		update_interface();
	}

	public void action_play(int pid) {
		
		if (light && !dead) {
			int random_dirty = random_num(1, 11);
			int random_tired = random_num(1, 16);
			int random_sleeping = random_num(1, 16);
			int random_hunger = random_num(1, 21);
			int random_happy = random_num(1, 11);

			if (sick) {
				random_tired *= 2;
				random_sleeping *= 2;
				random_happy /= 2;
			}	

			if (hunger > GREAT) {
				health -= random_num(1, 6);

				if (health < 0) {
					health = 0;
					set_dead(pid, true);
					set_health(pid, health);
				}
			}

			dirty -= random_dirty;
			tired -= random_tired;
			sleeping -= random_sleeping;
			hunger -= random_hunger;
			happy += random_happy;

			if (dirty < 0) {
				dirty = 0;
			}
			if (tired < 0) {
				tired = 0;
			}
			if (sleeping < 0) {
				sleeping = 0;
			}
			if (hunger < 0) {
				hunger = 0;
			}
			if (happy > 100) {
				happy = 100;
			}

			set_dirty(pid, dirty);
			set_tired(pid, tired);
			set_sleeping(pid, sleeping);
			set_hunger(pid, hunger);
			set_happy(pid, happy);
			set_sad(pid, (100 - happy));

			update_outfit(pid);
			update_interface();
		}

		
	}

	public void action_cure(int pid) {
		int random_percent = random_num(1, 101);

		if (light) {
			if (random_percent <= 25 && sick) {
				sick = false;
				normal = true;
				health += random_num(1, 6);

				if (health > 100) {
					health = 100;
				}

				set_health(pid, health);
				set_sick(pid, sick);
			}
			else {
				health -= random_num(1, 6);
				// GD.Print(health);

				if (health <= 0) {
					health = 0;
					set_dead(pid, true);
				}

				set_health(pid, health);
			}
		}

		update_outfit(pid);
		update_interface();
	}

	public void action_light(int pid) {
		if(light) {
			light = false;

			state_button(true);
		}
		else {
			light = true;

			if (!dead) {
				state_button(false);
			}
		}

		set_light(pid, light);
		update_outfit(pid);
	}


	// GET'S
	public int get_outfit(int pid) {
		// outfit = rota_do_banco(pid);
		return outfit;
	}

	public DateTime get_last_login(int pid) {
		return last_login;
	}


	// ATRIBUTOS
	public int get_happy(int pid) {
		return happy;
	}

	public int get_hunger(int pid) {
		return hunger;
	}

	public int get_health(int pid) {
		return health;
	}


	// STATUS
	public int get_tired(int pid) {
		return tired;
	}

	public int get_dirty(int pid) {
		return dirty;
	}

	public int get_sad(int pid) {
		return sad;
	}

	public int get_sleeping(int pid) {
		return sleeping;
	}

	public bool get_normal(int pid) {
		return normal;
	}

	public bool get_sick(int pid) {
		return sick;
	}

	public bool get_dead(int pid) {
		return dead;
	}

	public bool get_light(int pid) {
		return light;
	}
	

	// SET'S
	private void set_last_login(int pid, DateTime time) {
		last_login = time;
	}

	// ATRIBUTOS
	private void set_happy(int pid, int value) {
		happy = value;
	}

	private void set_hunger(int pid, int value) {
		hunger = value;
	}

	private void set_health(int pid, int value) {
		health = value;
	}


	// STATUS
	private void set_tired(int pid, int value) {
		tired = value;
	}

	private void set_dirty(int pid, int value) {
		dirty = value;
	}

	private void set_sad(int pid, int value) {
		sad = value;
	}

	private void set_sleeping(int pid, int value) {
		sleeping = value;
	}

	private void set_normal(int pid, bool value) {
		normal = value;
	}

	private void set_sick(int pid, bool value) {
		sick = value;
	}

	private void set_dead(int pid, bool value) {
		stop_dead_fuctions(pid);
		
		dead = value;
	}

	private void set_light(int pid, bool value) {
		light = value;
	}


	// USEFUL METHODS
	private int random_num(int min, int max) {
		Random random_num = new Random();
		return random_num.Next(min, max);
	}

	// UPDATES
	private void update_happy(int pid) {
		// fome, saude, cansaço, tristeza, sono
		int value = happy;

		if (light) {
			int percent  = random_num(1, 10001);
			float increase = 0; 

			if (percent <= 100) {
				increase = (hunger * 310 + health * 150 + (100 - tired) * 150 + (100 - sad) * 80 + (100 - sleeping) * 310) / 10000;
			}

			if(sick) {
				increase /= 2;
			}

			value += (int)increase;
		}

		if (value > 100) {
			value = 100;
		}

		happy = value;
		set_happy(pid, happy);
		set_sad(pid, (100 - happy));
	}


	private void update_sad(int pid) {
		// fome, saude, cansaço, tristeza, sono
		int value = sad;

		if (light) {
			int percent  = random_num(1, 10001);
			float increase = 0; 

			if (percent <= 100) {
				increase = (hunger * 310 + health * 150 + (100 - tired) * 150 + (100 - happy) * 80 + (100 - sleeping) * 310) / 10000;
			}

			if(sick) {
				increase *= 2;
			}

			value += (int)increase;
		}

		if (value > 100) {
			value = 100;
		}

		sad = value;
		set_sad(pid, sad);
		set_happy(pid, (100 - sad));
	}


	private void update_hunger(int pid) {
		// tempo, luz
		int percent  = random_num(1, 10001);
		int value = hunger;

		if (!light) {
			if (percent <= 37) {
				value -= 1;
			}
		}
		else {
			if (percent <= 74) {
				value -= 1;
			}
		}

		if (value < 0) {
			value = 0;
		}

		hunger = value;
		set_hunger(pid, hunger);

	}


	private void update_health(int pid) {
		// hunger, tired, sad, sleeping
		int value = health;
		int increment = 0;

		if (hunger <= TERRIBLE || hunger > GREAT) {
			increment -= 1;

			if (sick) {
				increment *= 2;
			}
		}
		else {
			increment += 1;
		}

		if(tired <= TERRIBLE) {
			increment -= 1;

			if (sick) {
				increment *= 2;
			}
		}
		else {
			increment += 1;
		}

		if(sad >= GREAT) {
			increment -= 1;

			if (sick) {
				increment *= 2;
			}
		}
		else {
			increment += 1;
		}

		if(sleeping <= TERRIBLE) {
			increment -= 1;

			if (sick) {
				increment *= 2;
			}
		}
		else {
			increment += 1;
		}

		int percent  = random_num(1, 10001);

		if (increment <= 0 && percent <= 92) {
			value += increment;
		}
		else if (increment > 0 && percent <= 185){
			value += increment;
		}

		if(value > 100) {
			value = 100;
		}

		if(value <= 0) {
			value = 0;
			dead = true;
			set_dead(pid, dead);
		}

		health = value;
		set_health(pid, health);
	}

	
	private void update_tired(int pid) {
		int percent  = random_num(1, 10001);

		if(percent <= 34  && light) {
			if (sick) {
				tired -= 1 * 2;
			}
			else {
				tired -= 1;
			}
		}

		if(!light && percent <= 102) {
			if (sick) {
				tired += 1;
			}
			else {
				tired += 1 * 2;
			}
		}

		if (tired < 0) {
			tired = 0;
		}

		if (tired > 100) {
			tired = 100;
		}

		set_tired(pid, tired);
	}


	private void update_dirty(int pid) {
		int percent  = random_num(1, 10001);

		if(percent <= 46 && light) {
			dirty -= 1;
		}

		if (dirty < 0) {
			dirty = 0;
		}

		set_dirty(pid, dirty);
	}


	private void update_sleeping(int pid) {
		int percent  = random_num(1, 10001);

		if(light && percent <= 34) {
			if (sick) {
				sleeping -= 1 * 2;
			}
			else {
				sleeping -= 1;
			}
		}

		if(!light && percent <= 102) {
			if (sick) {
				sleeping += 1;
			}
			else {
				sleeping += 1 * 2;
			}
		}

		if (sleeping < 0) {
			sleeping = 0;
		}

		if (sleeping > 100) {
			sleeping = 100;
		}

		set_sleeping(pid, sleeping);
	}


	private void update_sick(int pid) {
		int percent  = random_num(1, 10001);
		
		int sick_percent = 0;

		if (sad < TERRIBLE) {
			sick_percent += 3;
		}
		else if (sad >= TERRIBLE && sad <= GREAT ) {
			sick_percent += 5;
		}
		else {
			sick_percent += 10;
		}

		if (hunger < TERRIBLE) {
			sick_percent += 10;
		}
		else if (hunger >= TERRIBLE && hunger <= GREAT ) {
			sick_percent += 5;
		}
		else {
			sick_percent += 3;
		}

		if (tired < TERRIBLE) {
			sick_percent += 10;
		}
		else if (tired >= TERRIBLE && tired <= GREAT ) {
			sick_percent += 5;
		}
		else {
			sick_percent += 3;
		}

		if (sleeping < TERRIBLE) {
			sick_percent += 10;
		}
		else if (sleeping >= TERRIBLE && sleeping <= GREAT ) {
			sick_percent += 5;
		}
		else {
			sick_percent += 3;
		}

		if (dirty < TERRIBLE) {
			sick_percent += 10;
		}
		else if (dirty >= TERRIBLE && dirty <= GREAT ) {
			sick_percent += 5;
		}
		else {
			sick_percent += 3;
		}

		// SICK COMPUTATION
		if (normal && percent <= sick_percent) {
			normal = false;
			sick = true;
			health -= 5;
		}
		else if (sick && percent <= sick_percent * 2) {
			health -= 10;
		}

		if(health <= 0) {
			health = 0;
			dead = true;
			set_dead(pid, dead);
		}

		set_health(pid, health);
		set_normal(pid, normal);
		set_sick(pid, sick);
	}


	private void update_outfit(int pid) {
		string local_file = "res://Pet/Outfits/";
		string extension_file = ".png";
		string file_name = "";

		var girl_sprite = GetNode("Girl_Sprite");

		if (outfit == 0) {
			file_name = "Branca/";
		}
		else if  (outfit == 1) {
			file_name = "Morena/";
		}
		else if (outfit == 2) {
			file_name = "Loira/";
		}
		else {
			file_name = "Ruiva/";
		}


		if (dead) {
			file_name += "Dead";
		}
		else {
			// dormindo, doente, fome, sonolenta, cansada, sujo, triste/feliz

			if (!light) {
				file_name += "Sleeping";
			}
			else if (sick) {
				file_name += "Sick";
			}
			else if (hunger <= TERRIBLE) {
				file_name += "Annoyed";
			}
			else if (tired <= TERRIBLE || sleeping <= TERRIBLE) {
				file_name += "Sleepy";
			}
			else if (dirty <= TERRIBLE) {
				file_name += "Dirty";
			}
			else if (sad > 50) {
				file_name += "Sad";
			}
			else if (happy > 50) {
				file_name += "Smile";
			}
			else {
				file_name += "Normal";
			}
		}
		// GD.Print(file_name);	
		file_name = local_file + file_name + extension_file;

		var img = (Texture)GD.Load(file_name);

		girl_sprite.Call("update_sprite", img);

	}


	private void state_button(bool state) {
		var feed_button = GetNode(feed_button_path);
		var toilet_button = GetNode(toilet_button_path);
		var play_button = GetNode(play_button_path);
		var cure_button = GetNode(cure_button_path);

		feed_button.Call("set_disabled", state);
		toilet_button.Call("set_disabled", state);
		play_button.Call("set_disabled", state);
		cure_button.Call("set_disabled", state);
	}

	private void update_interface() {
		var HP = GetNode("HP");
		HP.Call("update_health_text", Convert.ToString(health));

		var Hunger = GetNode("Hunger");
		Hunger.Call("update_hunger_text", Convert.ToString(hunger));

		var Dirty = GetNode("Dirty");
		Dirty.Call("update_dirty_text", Convert.ToString(dirty));

		var Sleep = GetNode("Sleep");
		Sleep.Call("update_sleep_text", Convert.ToString(sleeping));

		var Tired = GetNode("Tired");
		Tired.Call("update_tired_text", Convert.ToString(tired));
	}

	private void stop_dead_fuctions(int pid) {
		state_button(true);
	}



	private void _on_update() {
		DateTime time_now = DateTime.Now;
		int ticks = (int)((time_now - last_login).TotalSeconds / 2);

		if (ticks <= 0) {
			ticks = 1;
		}
		// GD.Print(ticks);
		// GD.Print("Hunger: ", hunger);
		
		int k = 0;
		while (k < ticks) {
			if(!dead) {
				update_happy(pid);
				update_hunger(pid);
				update_health(pid);
				update_tired(pid);
				update_dirty(pid);
				update_sad(pid);
				update_sleeping(pid);
				update_sick(pid);
			}

			update_interface();
			update_outfit(pid);
			k++;
		}

		set_last_login(pid, time_now);
		
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		var feed_button = GetNode(feed_button_path);
		var toilet_button = GetNode(toilet_button_path);
		var play_button = GetNode(play_button_path);
		var cure_button = GetNode(cure_button_path);
		var light_button = GetNode(light_button_path);
		
		var globalVariables = (Global)GetNode("/root/Global");
		pid = globalVariables.pet_id;

		feed_button.Connect("pressed", this, "action_feed", new Godot.Collections.Array() {pid});
		toilet_button.Connect("pressed", this, "action_toilet", new Godot.Collections.Array() {pid});
		play_button.Connect("pressed", this, "action_play", new Godot.Collections.Array() {pid});
		cure_button.Connect("pressed", this, "action_cure", new Godot.Collections.Array() {pid});
		light_button.Connect("pressed", this, "action_light", new Godot.Collections.Array() {pid});

		var http = GetNode<HTTPRequest>("PetHTTPRequest");
		server server = new server();
		server.get_pet(pid, http);		
	}
}
