using Godot;
using System;

public class Pet : StaticBody2D {
	// CONSTANTES
	public const int GREAT = 80;
	public const int TERRIBLE = 20;

	// Declare member variables here.
	private DateTime last_login;

	// ATRIBUTOS
	private int happy;
	private int hunger;
	private int health;

	// STATUS
	private int tired;
	private int dirty;
	private int sad;
	private int sleeping;

	private bool normal;
	private bool sick;
	private bool dead;
	private bool light;

	// AÇÕES
	public void action_feed(int pid) {

	}

	public void action_toilet(int pid) {

	}

	public void action_play(int pid) {

	}

	public void action_cure(int pid) {

	}

	public void action_light(int pid) {

	}


	// GET'S
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
		return random_num.Next(1, 1001);
	}

	// UPDATES
	private void update_happy(int pid) {
		// fome, saude, cansaço, tristeza, sono
		int value = happy;

		if (light) {
			int percent  = random_num(1, 1001);
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

		set_happy(pid, value);
		set_sad(pid, (100 - value));
	}

	private void update_hunger(int pid) {
		// tempo, luz
		int percent  = random_num(1, 1001);
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

		int percent  = random_num(1, 1001);

		if (increment <= 0 && percent <= 92) {
			value += increment;
		}
		else if (increment > 0 && percent <= 185){
			value += increment;
		}

		if(value > 100) {
			value = 100;
		}

		if(value < 0) {
			value = 0;
			dead = true;
			set_dead(pid, dead);
		}

		health = value;
		set_health(pid, health);
	}


	// Função de update do jogo.
	private void _on_update(int pid) {
		DateTime time_now = DateTime.Now;
		int ticks = (int)((time_now - last_login).TotalSeconds / 2);

		if (ticks <= 0) {
			ticks = 1;
		}
		GD.Print(ticks);

		int k = 0;
		while (k < ticks) {
			update_happy(pid);
			update_hunger(pid);
			update_health(pid);

			k++;
		}

		set_last_login(pid, time_now);

		GD.Print("Happy: ", happy, "\nSad: ", sad);
		GD.Print("Hunger: ", hunger);
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		last_login = DateTime.Now;
		happy = 50;
		sad = 50;
		hunger = 40;
		health = 80;
		tired = 20;
		sleeping = 20;
		dirty = 80;

		normal = true;
		sick = false;
		dead = false;
		light = true;
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
