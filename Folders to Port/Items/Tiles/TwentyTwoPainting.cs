using Terraria.ID;

namespace FargowiltasSouls.Items.Tiles
{
    public class TwentyTwoPainting : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("22 Painting");
            Tooltip.SetDefault("'Keuhm E. Dee'");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.Swing;
            item.consumable = true;
            item.rare = ItemRarityID.Blue;
            item.createTile = ModContent.TileType<TwentyTwoPaintingSheet>();
        }
    }
}