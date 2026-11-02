using Godot;

public partial class popup_handler : Window
{
    private string errorMessage;

    public string ErrorMessage { set => errorMessage = value; }

    public override void _Ready()
    {
        GetNode<Label>("Alignment/lblMessage").Text = $"Error: {errorMessage}";
    }

    public void ClosePopup()
    {
        QueueFree();
    }

}
