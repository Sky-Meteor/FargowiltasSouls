﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs
{
    public class SlimeSwarm : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_1";

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
            //NPCID.Sets.TrailCacheLength[NPC.type] = 6;
            //NPCID.Sets.TrailingMode[NPC.type] = 1;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;

            NPCID.Sets.SpecificDebuffImmunity[Type] = NPCID.Sets.SpecificDebuffImmunity[NPCID.KingSlime];

            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, new NPCID.Sets.NPCBestiaryDrawModifiers
            {
                Hide = true
            });
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.BlueSlime);

            NPC.aiStyle = -1;
            NPC.knockBackResist = 0;
            //NPC.timeLeft = NPC.activeTime * 30;
            NPC.timeLeft = 60 * 10;
            NPC.noTileCollide = false;

            NPC.noGravity = false;

            //NPC.scale *= 1.5f;
            NPC.lifeMax *= 3;

            NPC.GravityMultiplier *= 2;
        }

        //public override bool CanHitNPC(NPC target) => false;

        //public override bool CanHitPlayer(Player target, ref int cooldownSlot) => false;

        public override void AI()
        {
            ref float direction = ref NPC.ai[0];

            float accelX = 0.1f;
            int maxSpeedX = 12;
            int jumpSpeed = 5;

            if (Math.Abs(NPC.velocity.X) < maxSpeedX)
            {
                NPC.velocity.X += accelX * direction;
            }
            if (NPC.velocity.Y == 0)
            {
                NPC.velocity.Y = -jumpSpeed;
            }
            
        }


        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter > 4)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
            }
            if (NPC.frame.Y >= Main.npcFrameCount[NPC.type] * frameHeight)
                NPC.frame.Y = 0;
        }
    }
}
