﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.FinalUpgrades
{
    public class StyxGazer : SoulsItem
    {
        public bool flip;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Styx Gazer");
            Tooltip.SetDefault(@"Right click to wield a blade of infernal magic
'The blazing scythe wand sword destruction ray of a defeated foe...'");
        }

        public override void SetDefaults()
        {
            item.damage = 1700;
            item.useStyle = ItemUseStyleID.Swing;
            item.useAnimation = 22;
            item.useTime = 22;
            item.shootSpeed = 16f;
            item.knockBack = 14f;
            item.width = 20;
            item.height = 20;
            item.scale = 1f;
            item.rare = ItemRarityID.Purple;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<Projectiles.BossWeapons.StyxScythe>();
            item.value = Item.sellPrice(0, 70);
            //item.noMelee = true; //no melee hitbox
            //item.noUseGraphic = true; //dont draw item
            Item.DamageType = DamageClass.Melee;
            item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.shoot = ModContent.ProjectileType<Projectiles.BossWeapons.StyxGazer>();
                item.useStyle = ItemUseStyleID.Shoot;
                Item.DamageType = DamageClass.Magic;
                item.melee = false;
                item.noUseGraphic = true;
                item.noMelee = true;
                item.mana = 200;
            }
            else
            {
                item.shoot = ModContent.ProjectileType<Projectiles.BossWeapons.StyxScythe>();
                item.useStyle = ItemUseStyleID.Swing;
                item.magic = false;
                Item.DamageType = DamageClass.Melee;
                item.noUseGraphic = false;
                item.noMelee = false;
                item.mana = 0;
            }
            return true;
        }

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.mod == "Terraria" && line.Name == "ItemName")
            {
                Main.spriteBatch.End(); //end and begin main.spritebatch to apply a shader
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                var lineshader = GameShaders.Misc["PulseDiagonal"].UseColor(new Color(255, 170, 12)).UseSecondaryColor(new Color(210, 69, 203));
                lineshader.Apply(null);
                Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1); //draw the tooltip manually
                Main.spriteBatch.End(); //then end and begin again to make remaining tooltip lines draw in the default way
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }

        public override string Texture => base.Texture;

        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            flip = !flip;
            Vector2 speed = new Vector2(speedX, speedY);

            if (player.altFunctionUse == 2) //right click
            {
                speed = speed.RotatedBy(Math.PI / 2 * (flip ? 1 : -1));
                Projectile.NewProjectile(position, speed, type, damage, knockBack, player.whoAmI, (float)Math.PI / 120 * (flip ? -1 : 1));
            }
            else
            {
                const int max = 5;
                for (int i = 0; i < max; i++)
                {
                    Projectile.NewProjectile(position, speed.RotatedBy(2 * Math.PI / max * i), type,
                        damage, knockBack, player.whoAmI, 0, (Main.MouseWorld - position).Length() * (flip ? 1 : -1));
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()

            //.AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerMoon"));
            .AddIngredient(ModContent.ItemType<Sadism>(), 30)
            .AddIngredient(ModContent.ItemType<AbomEnergy>(), 30)
            .AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 30)
            .AddIngredient(ModContent.ItemType<BrokenHilt>())
            .AddIngredient(ModContent.ItemType<CyclonicFin>())

            .AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"))
            
            .Register();
        }
    }
}