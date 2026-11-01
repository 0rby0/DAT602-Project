using Godot;

public partial class popup_handler : PopupPanel
{
    private string errorMessage;

    public string ErrorMessage { set => errorMessage = value; }

    public override void _Ready()
    {
        Popup();
        GetNode<Label>("Alignment/lblMessage").Text = $"Error: {errorMessage}";
    }

    public void ClosePopup()
    {
        QueueFree();
    }

}
