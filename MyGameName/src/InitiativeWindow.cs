using Godot;
using System;

public partial class InitiativeWindow : Node2D
{
	private String[] playerArray = ["Don Santos", "'Don'", "Victor", "Brymoira", "'Bry'", "Labeen", "Albert"]; 
	public override void _Ready(){
		//connect signals
		ItemList CharacterList = GetNode<ItemList>("CharacterList");
		Button SubmitButton = GetNode<Button>("SubmitButton");
		Button ResetButton = GetNode<Button>("ResetButton"); 
		
		Error listErr = CharacterList.Connect("item_activated", new Callable(this, nameof(OnCharacterListItemActivated)));
		if (listErr != Error.Ok){ 
			GD.PrintErr("Not connected: ", listErr); 
		}
		Error SubmitErr = SubmitButton.Connect("pressed", new Callable(this, nameof(OnSubmitButtonPressed)));
		Error ResetErr = ResetButton.Connect("pressed", new Callable(this, nameof(OnResetButtonPressed)));
	}

	public void OnCharacterListItemActivated(int index){
		Label InitOrder = GetNode<Label>("CurrentOrder"); 
		ItemList CharacterList = GetNode<ItemList>("CharacterList");
		
		
		InitOrder.Text += playerArray[index] + "    "; 
	}
	
	//send information to the next person 
	public void OnSubmitButtonPressed() {
		Label InitOrder = GetNode<Label>("CurrentOrder"); 
		GD.Print(InitOrder.Text);
	}
	
	//send signal to reset text
	public void OnResetButtonPressed() {
		Label InitOrder = GetNode<Label>("CurrentOrder"); 
		InitOrder.Text = ""; 
	}

}
