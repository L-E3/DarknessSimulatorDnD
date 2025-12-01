using Godot;
using System;

public partial class Player : CharacterBody2D {
	
	[Signal]
	public delegate void HitEventHandler();
	
	[Export]
  	public int Speed { get; set; } = 90; // How fast the player will move (pixels/sec).
	public int reach {get; set; } = 3; //scale of how large the attack range is (3 = 15ft, 1 = 5ft) 
	
  	public Vector2 ScreenSize;

  	public override void _Ready() {
		ScreenSize = GetViewportRect().Size;
  	}

	//reset position 
	public void Start(Vector2 position)
	{
		Position = position;
		Show();
		GetNode<CollisionShape2D>("DirectCollisionShape").Disabled = false;
	}

  	public override void _PhysicsProcess(double delta) {
		var velocity = Velocity; // The player's movement vector.
		
		//MOVEMENT   
		if (Input.IsActionPressed("move_right")) 
		  velocity.X += 1;
		else if (Input.IsActionPressed("move_left")) {
			velocity.X -= 1;
		}
		else if (Input.IsActionPressed("move_down")) 
		  velocity.Y += 1;
		else if (Input.IsActionPressed("move_up")) 
		  velocity.Y -= 1;
		else{ 
			velocity.X = 0;
			velocity.Y = 0;
		}
		Velocity = velocity.Normalized() * Speed;
		
		MoveAndSlide();

	}
	
	//TODO: Add the collisions
	//Add another collision shape 2d for range and hit? 
	//TODO: might need to rename to match (underscores?) 
	private void OnBodyEntered(Node2D body){ 
		//detect if in range to hit or was hit 
		EmitSignal(SignalName.Hit);
		GD.Print("body entered");
		GetNode<CollisionShape2D>("DirectCollisionShape").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}
	
	private void OnAreaEntered(Area2D area){ 
		GD.Print("area entered");
	}
}
