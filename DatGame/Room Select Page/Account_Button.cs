using Godot;

namespace DATGame
{
    public partial class Account_Button : Button
    {
        public void _Account_Button_Clicked()
        {
            Button deletebutton = GetNode<Button>("../btnDelete");

            deletebutton.Visible = !deletebutton.Visible;
        }

        public void _Delete_Button_Clicked()
        {
            GetNode<VBoxContainer>("../").Visible = false;
            GetNode<VBoxContainer>("../../Confirmation").Visible = true;
        }

        public void _Confirm_Delete_Clicked()
        {
            UserDAO.global.DeleteAccount(Main.get.Account);
            Main.get.Account = null;
            Main.get.SwitchScene("res://Login Page/Login.tscn");
        }

        public void _Cancel_Delete_Clicked()
        {
            GetNode<VBoxContainer>("../../Confirmation").Visible = false;
            GetNode<VBoxContainer>("../").Visible = true;
        }
    }
}
