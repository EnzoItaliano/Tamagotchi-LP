using Godot;
using System;

public class Pet : Godot.Node2D {
	// CONSTANTES
	public const int GREAT = 80;
	public const int TERRIBLE = 20;

	// Declare member variables here.
	private DateTime last_login;

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

	// AÇÕES
	public void action_feed(int pid) {
		int random_feed = random_num(1, 11);
		
		hunger += random_feed;
		set_dirty(pid, hunger);
	}

	public void action_toilet(int pid) {
		int random_clean = random_num(1, 51);
		
		if (light && !dead) {
			dirty += random_clean;	
		}

		set_dirty(pid, dirty);
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
				set_dead(pid, normal);
			}
			else {
				health -= random_num(1, 6);

				if (health < 0) {
					health = 0;
					set_dead(pid, true);
				}

				set_health(pid, health);
			}
		}
	}

	public void action_light(int pid) {
		if(light) {
			light = false;
		}
		else {
			light = true;
		}

		set_light(pid, light);
	}


	// GET'S
	public int get_outfit(int pid) {
		// outfit = rota_do_banco(pid);
		return outfit;
	}

	public DateTime get_last_login(int pid) {
		// last_login = rota_do_banco(pid);
		return last_login;
	}


	// ATRIBUTOS
	public int get_happy(int pid) {
		// happy = rota_do_banco(pid);
		return happy;
	}

	public int get_hunger(int pid) {
		// hunger = rota_do_banco(pid);
		return hunger;
	}

	public int get_health(int pid) {
		// health = rota_do_banco(pid);
		return health;
	}


	// STATUS
	public int get_tired(int pid) {
		// tired = rota_do_banco(pid);
		return tired;
	}

	public int get_dirty(int pid) {
		// dirty = rota_do_banco(pid);
		return dirty;
	}

	public int get_sad(int pid) {
		// sad = rota_do_banco(pid);
		return sad;
	}

	public int get_sleeping(int pid) {
		// sleeping = rota_do_banco(pid);
		return sleeping;
	}

	public bool get_normal(int pid) {
		// normal = rota_do_banco(pid);
		return normal;
	}

	public bool get_sick(int pid) {
		// sick = rota_do_banco(pid);
		return sick;
	}

	public bool get_dead(int pid) {
		// dead = rota_do_banco(pid);
		return dead;
	}

	public bool get_light(int pid) {
		// light = rota_do_banco(pid);
		return light;
	}
	

	// SET'S
	private void set_last_login(int pid, DateTime time) {
		last_login = time;
		// rota_do_banco(pid, time);
	}

	// ATRIBUTOS
	private void set_happy(int pid, int value) {
		happy = value;
		// rota_do_banco(pid, happy);
	}

	private void set_hunger(int pid, int value) {
		hunger = value;
		// rota_do_banco(pid, hunger);
	}

	private void set_health(int pid, int value) {
		health = value;
		// rota_do_banco(pid, health);
	}


	// STATUS
	private void set_tired(int pid, int value) {
		tired = value;
		// rota_do_banco(pid, tired);
	}

	private void set_dirty(int pid, int value) {
		dirty = value;
		// rota_do_banco(pid, dirty);
	}

	private void set_sad(int pid, int value) {
		sad = value;
		// rota_do_banco(pid, sad);
	}

	private void set_sleeping(int pid, int value) {
		sleeping = value;
		// rota_do_banco(pid, sleeping);
	}

	private void set_normal(int pid, bool value) {
		normal = value;
		// rota_do_banco(pid, normal);
	}

	private void set_sick(int pid, bool value) {
		sick = value;
		// rota_do_banco(pid, sick);
	}

	private void set_dead(int pid, bool value) {
		dead = value;
		// rota_do_banco(pid, dead);
	}

	private void set_light(int pid, bool value) {
		light = value;
		// rota_do_banco(pid, light);
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
		int value = happy;
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

		float sad_percent = sad + 1;
		float hunger_percent = hunger + 1;
		float tired_percent = tired + 1;
		float sleeping_percent = sleeping + 1;
		float dirty_percent = sad + 1;
		
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

		if (outfit == 1) {
			file_name = "Branca/";
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
				file_name += "1";
			}
			else if (hunger <= TERRIBLE) {
				file_name += "Annoyed";
			}
			else if (tired <= TERRIBLE || sleeping <= TERRIBLE) {
				file_name += "Sleepy";
			}
			else if (dirty <= TERRIBLE) {
				file_name += "5";
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
		file_name = local_file + file_name + extension_file;

		var img = (Texture)GD.Load(file_name);

		GD.Print("Outfit: ", file_name);
		girl_sprite.Call("update_sprite", img);

	}

	// Função de update do jogo.
	private void _on_update(int pid) {
		DateTime time_now = DateTime.Now;
		int ticks = (int)((time_now - last_login).TotalSeconds / 2);

		if (ticks <= 0) {
			ticks = 1;
		}
		// GD.Print(ticks);
		GD.Print("Health: ", health);
		
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
				update_outfit(pid);
			}

			k++;
		}

		set_last_login(pid, time_now);

		
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		last_login = DateTime.Now;
		happy = 50;
		sad = 50;
		hunger = 40;
		health = 80;
		tired = 40;
		sleeping = 20;
		dirty = 80;

		normal = true;
		sick = false;
		dead = false;
		light = true;

		outfit = 1;
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
