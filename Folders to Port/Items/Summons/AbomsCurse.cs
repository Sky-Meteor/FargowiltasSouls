using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using FargowiltasSouls.NPCs.AbomBoss;
using Fargowiltas.Items.Tiles;
using Terraria.Chat;

namespace FargowiltasSouls.Items.Summons
{
    public class AbomsCurse : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abominationn's Curse");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 10));
        }
        public override int NumFrames => 10;
        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 48;
            item.rare = ItemRarityID.Purple;
            item.maxStack = 999;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = ItemUseStyleID.HoldUp;
            item.consumable = true;
            item.value = Item.buyPrice(gold: 8);
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.noUseGraphic = true;
        }

        public override bool UseItem(Player player)
        {
            int abom = NPC.FindFirstNPC(ModLoader.GetMod("Fargowiltas").NPCType("Abominationn"));

            if (abom > -1 && Main.npc[abom].active)
            {
                // TODO: Localization.
                string message = "Abominationn has awoken!";

                Main.npc[abom].Transform(ModContent.NPCType<AbomBoss>());

                if (Main.netMode == NetmodeID.SinglePlayer)
                    Main.NewText(message, 175, 75, 255);
                else if (Main.netMode == NetmodeID.Server)
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), new Color(175, 75, 255));
            }
            else
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<AbomBoss>());

            return true;
        }

        public override void AddRecipes() // Make this harder again when changed to abom's gift
        {
            CreateRecipe()
            .AddIngredient(ItemID.GoblinBattleStandard)
            .AddIngredient(ItemID.PirateMap)
            .AddIngredient(ItemID.PumpkinMoonMedallion)
            .AddIngredient(ItemID.NaughtyPresent)
            .AddIngredient(ItemID.SnowGlobe)
            .AddIngredient(ItemID.DD2ElderCrystal)
            .AddIngredient(ItemID.LunarBar, 5)
            .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
            
            .Register();
        }
    }
}