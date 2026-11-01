using Godot;
using Godot.Collections;

namespace DATGame
{
    public partial class Message_Display : RichTextLabel
    {
        public override void _Ready()
        {
            base._Ready();

            RefreshChat();
        }

        public void RefreshChat()
        {

            Array rawchat = ChatDAO.global.GetMessages(Main.get.Room);

            string formattedchat = "";

            foreach (Dictionary message in rawchat)
            {
                formattedchat += $"{message["AccountName"]}: {message["Text"]}";

                // Add a newline for each line that isn't at the end of the log
                if (rawchat.IndexOf(message) != rawchat.Count) {
                    formattedchat += $"\n";
                }
            }

            Text = formattedchat;
        }

    }
}
