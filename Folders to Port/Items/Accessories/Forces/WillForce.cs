﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using FargowiltasSouls.Items.Accessories.Enchantments;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class WillForce : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Will");
           
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "意志之力");
            
            string tooltip =
$"[i:{ModContent.ItemType<GoldEnchant>()}] Press the Gold hotkey to be encased in a Golden Shell\n" +
$"[i:{ModContent.ItemType<PlatinumEnchant>()}] 20% chance for enemies to drop 5x loot\n" +
$"[i:{ModContent.ItemType<GladiatorEnchant>()}] Spears will rain down on struck enemies\n" +
$"[i:{ModContent.ItemType<RedRidingEnchant>()}] Double tap down to create a localized rain of arrows\n" +
$"[i:{ModContent.ItemType<ValhallaKnightEnchant>()}] Increases the effectiveness of healing sources by 50%\n" +
"'A mind of unbreakable determination'";
            Tooltip.SetDefault(tooltip);

            string tooltip_ch =
@"攻击会造成迈达斯减益
按下'金身'键后会将你包裹在一个黄金壳中
被包裹时你无法移动或攻击，但你免疫所有伤害
敌人死亡时掉落的战利品有20%几率翻5倍
长矛将倾泄在被攻击的敌人身上
双击'下'键后令箭雨倾斜在光标位置
增加50%受治疗量
大幅强化弩车和爆炸机关的效果
拥有贪婪戒指效果
'坚不可摧的决心'";
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, tooltip_ch);

        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = ItemRarityID.Purple;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoSoulsPlayer modPlayer = player.GetModPlayer<FargoSoulsPlayer>();
            //super bleed on all
            modPlayer.WillForce = true;
            //midas, greedy ring, pet, zhonyas
            modPlayer.GoldEffect(hideVisual);
            //loot multiply
            modPlayer.PlatinumEnchant = true;
            //javelins and pets
            modPlayer.GladiatorEffect(hideVisual);
            //wizard bonuses if somehow wearing only other enchants and not forces
            modPlayer.WizardEnchant = true;
            //arrow rain, celestial shell, pet
            modPlayer.RedRidingEffect(hideVisual);
            modPlayer.HuntressEffect();
            //immune frame kill, pet
            modPlayer.ValhallaEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(null, "GoldEnchant")
            .AddIngredient(null, "PlatinumEnchant")
            .AddIngredient(null, "GladiatorEnchant")
            .AddIngredient(ModContent.ItemType<WizardEnchant>())
            .AddIngredient(null, "RedRidingEnchant")
            .AddIngredient(null, "ValhallaKnightEnchant")

            .AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"))

            
            .Register();
        }
    }
}
