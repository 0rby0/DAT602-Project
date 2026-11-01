using Godot;
using Godot.Collections;

namespace DATGame
{
    public partial class Abilty_Menu_Loader : HBoxContainer
    {
        private Label _gaugetext;
        private HBoxContainer _abilitybuttons;

        public override void _Ready()
        {
            _gaugetext = GetNode<Label>("lblEnergy");
            _abilitybuttons = GetNode<HBoxContainer>("Ability Buttons");
        }

        public void _ReplenishEnergy()
        {
            PlayerDAO.global.ReplenishEnergy(Main.get.Account, Main.get.Room);
            UpdateEnergyGaugeDisplay();
        }

        public void UpdateEnergyGaugeDisplay()
        {
            int energy = PlayerDAO.global.GetEnergy(Main.get.Account, Main.get.Room);

            _gaugetext.Text = $"{energy} / 20";
        }

        public void UpdateAbilityButtons()
        {
            Array inventory = PlayerDAO.global.GetInventory(Main.get.Account, Main.get.Room);
            Array usables = [];

            foreach (Dictionary ability in inventory)
            {
                if ((int)ability["Cost"] > 0)
                {
                    usables.Add(ability);
                }
            }

            for (int rm = _abilitybuttons.GetChildren().Count; rm > usables.Count; rm--)
            {
                _abilitybuttons.GetChildren()[rm - 1].QueueFree();
            }

            for (int cr = _abilitybuttons.GetChildren().Count; cr < usables.Count; cr++)
            {
                TextureButton abilitybutton = (TextureButton)GD.Load<PackedScene>("res://Gameplay/Ability Button.tscn").Instantiate();
                _abilitybuttons.CallDeferred("add_child", abilitybutton);
            }

            for (int slot = 0; slot < _abilitybuttons.GetChildren().Count; slot++)
            {
                if (slot < usables.Count) {
                    Ability_Button_Config conf = (Ability_Button_Config)_abilitybuttons.GetChildren()[slot];
                    conf.UpdateContents((Dictionary)usables[slot]);
                }
            }
        }

    }
}
