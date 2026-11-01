using Godot;

namespace DATGame
{
    public partial class Message_Controls : HBoxContainer
    {
        public void _Send_Message_Clicked()
        {
            string message = GetNode<LineEdit>("txtMessage").Text;

            ChatDAO.global.SendMessage(message, Main.get.Account, Main.get.Room);

            GetNode<LineEdit>("txtMessage").Text = "";
        }

    }
}