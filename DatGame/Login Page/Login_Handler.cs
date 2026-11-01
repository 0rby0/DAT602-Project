using Godot;
using Godot.Collections;

namespace DATGame
{
    public partial class Login_Handler : Control
    {
        VBoxContainer logincontrols;
        VBoxContainer lock_msg;
        Label attempt_text;

        private string _attemptedaccount = "";

        public override void _Ready()
        {
            base._Ready();

            Control head = GetNode<Control>("CanvasLayer/Container");

            logincontrols = head.GetNode<VBoxContainer>("Login Controls");

            lock_msg = head.GetNode<VBoxContainer>("Lockout Message");
            attempt_text = logincontrols.GetNode<Label>("lblAttempts");
        }

        public override void _Process(double delta)
        {
            base._Process(delta);

            Dictionary attempteduserdetails = UserDAO.global.GetAccount(_attemptedaccount);

            if (attempteduserdetails.Keys.Count == 1)
            {
                return;
            }

            if ((bool)attempteduserdetails["Locked"] == true){
                lock_msg.Visible = true;
                logincontrols.Visible = false;
            }

            attempt_text.Text = $"Attempt {(int)attempteduserdetails["LoginAttempts"]}/5";
        }

        internal void _Submit_Button_Clicked()
        {
            Control head = GetNode<Control>("CanvasLayer/Container");

            VBoxContainer accountpopup = head.GetNode<VBoxContainer>("New Account Controls");

            string uname = logincontrols.GetNode<LineEdit>("txtboxUsername").Text;
            string pword = logincontrols.GetNode<LineEdit>("txtboxPassword").Text;

            Label warninglabel = logincontrols.GetNode<Label>("lblWarning");
            Label message = accountpopup.GetNode<Label>("lblMessage");

            _attemptedaccount = uname;

            string login_result = UserDAO.global.Login(uname, pword);
            if (login_result =="Login success")
            {
                
                Main.get.Account = uname;
                Main.get.SwitchScene("res://Room Select Page/Room Select.tscn");
            }
            else if (login_result == "Something went wrong: Account not found")
            {
                message.Text = $"Username '{uname}' does not exist.\nCreate new account?";
                logincontrols.Visible = false;
                accountpopup.Visible = true;
            }
            else
            {
                warninglabel.Text = login_result;
                warninglabel.Visible = true;
            }
        }

        internal void _Create_Confirm_Clicked()
        {
            Control head = GetNode<Control>("CanvasLayer/Container");

            VBoxContainer logincontrols = head.GetNode<VBoxContainer>("Login Controls");
            VBoxContainer accountpopup = head.GetNode<VBoxContainer>("New Account Controls");

            string uname = logincontrols.GetNode<LineEdit>("txtboxUsername").Text;
            string pword = logincontrols.GetNode<LineEdit>("txtboxPassword").Text;

            Label warninglabel = logincontrols.GetNode<Label>("lblWarning");

            string creation_result = UserDAO.global.CreateAccount(uname, pword);
            if (creation_result ==  "Account created successfully")
            {
                Main.get.Account = uname;
                Main.get.SwitchScene("res://Room Select Page/Room Select.tscn");
            }
            else
            {
                warninglabel.Text = creation_result;

                logincontrols.Visible = true;
                accountpopup.Visible = false;

                warninglabel.Visible = true;
            }
        }
        
        internal void _Cancel_Creation()
        {
            Control head = GetNode<Control>("CanvasLayer/Container");

            VBoxContainer logincontrols = head.GetNode<VBoxContainer>("Login Controls");
            VBoxContainer accountpopup = head.GetNode<VBoxContainer>("New Account Controls");

            logincontrols.Visible = true;
            accountpopup.Visible = false;

            logincontrols.GetNode<LineEdit>("txtboxUsername").Text = "";
            logincontrols.GetNode<LineEdit>("txtboxPassword").Text = "";

            logincontrols.GetNode<Label>("lblWarning").Visible = false;
        }
    }
}
