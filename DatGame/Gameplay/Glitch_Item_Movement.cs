using Godot;
using ZstdSharp.Unsafe;

namespace DATGame {
    public partial class Glitch_Item_Movement : Timer
    {
        public void _GlitchItemTimeout()
        {
            if (PlayerDAO.global.GetPrimaryPlayer(Main.get.Room) == Main.get.Account)
            {
                MapDAO.global.Glitch(Main.get.Room);
            }
        }

    }
}
