﻿using System;

namespace uGUI
{
    public partial class uGui
    {
        public class uGuiColor
        {
            /* -------------------------------------------------------------------------------- */
            /* -- µGUI COLORS                                                                -- */
            /* -- Source: http://www.rapidtables.com/web/color/RGB_Color.htm                 -- */
            /* -------------------------------------------------------------------------------- */

#if USE_COLOR_RGB565

            public const UInt16 C_MAROON = 0x8000;
            public const UInt16 C_DARK_RED = 0x8800;
            public const UInt16 C_BROWN = 0xA145;
            public const UInt16 C_FIREBRICK = 0xB104;
            public const UInt16 C_CRIMSON = 0xD8A7;
            public const UInt16 C_RED = 0xF800;
            public const UInt16 C_TOMATO = 0xFB09;
            public const UInt16 C_CORAL = 0xFBEA;
            public const UInt16 C_INDIAN_RED = 0xCAEB;
            public const UInt16 C_LIGHT_CORAL = 0xEC10;
            public const UInt16 C_DARK_SALMON = 0xE4AF;
            public const UInt16 C_SALMON = 0xF40E;
            public const UInt16 C_LIGHT_SALMON = 0xFD0F;
            public const UInt16 C_ORANGE_RED = 0xFA20;
            public const UInt16 C_DARK_ORANGE = 0xFC60;
            public const UInt16 C_ORANGE = 0xFD20;
            public const UInt16 C_GOLD = 0xFEA0;
            public const UInt16 C_DARK_GOLDEN_ROD = 0xB421;
            public const UInt16 C_GOLDEN_ROD = 0xDD24;
            public const UInt16 C_PALE_GOLDEN_ROD = 0xEF35;
            public const UInt16 C_DARK_KHAKI = 0xBDAD;
            public const UInt16 C_KHAKI = 0xEF31;
            public const UInt16 C_OLIVE = 0x8400;
            public const UInt16 C_YELLOW = 0xFFE0;
            public const UInt16 C_YELLOW_GREEN = 0x9E66;
            public const UInt16 C_DARK_OLIVE_GREEN = 0x5346;
            public const UInt16 C_OLIVE_DRAB = 0x6C64;
            public const UInt16 C_LAWN_GREEN = 0x7FC0;
            public const UInt16 C_CHART_REUSE = 0x7FE0;
            public const UInt16 C_GREEN_YELLOW = 0xAFE6;
            public const UInt16 C_DARK_GREEN = 0x0320;
            public const UInt16 C_GREEN = 0x07E0;
            public const UInt16 C_FOREST_GREEN = 0x2444;
            public const UInt16 C_LIME = 0x07E0;
            public const UInt16 C_LIME_GREEN = 0x3666;
            public const UInt16 C_LIGHT_GREEN = 0x9772;
            public const UInt16 C_PALE_GREEN = 0x97D2;
            public const UInt16 C_DARK_SEA_GREEN = 0x8DD1;
            public const UInt16 C_MEDIUM_SPRING_GREEN = 0x07D3;
            public const UInt16 C_SPRING_GREEN = 0x07EF;
            public const UInt16 C_SEA_GREEN = 0x344B;
            public const UInt16 C_MEDIUM_AQUA_MARINE = 0x6675;
            public const UInt16 C_MEDIUM_SEA_GREEN = 0x3D8E;
            public const UInt16 C_LIGHT_SEA_GREEN = 0x2595;
            public const UInt16 C_DARK_SLATE_GRAY = 0x328A;
            public const UInt16 C_TEAL = 0x0410;
            public const UInt16 C_DARK_CYAN = 0x0451;
            public const UInt16 C_AQUA = 0x07FF;
            public const UInt16 C_CYAN = 0x07FF;
            public const UInt16 C_LIGHT_CYAN = 0xDFFF;
            public const UInt16 C_DARK_TURQUOISE = 0x0679;
            public const UInt16 C_TURQUOISE = 0x46F9;
            public const UInt16 C_MEDIUM_TURQUOISE = 0x4E99;
            public const UInt16 C_PALE_TURQUOISE = 0xAF7D;
            public const UInt16 C_AQUA_MARINE = 0x7FFA;
            public const UInt16 C_POWDER_BLUE = 0xAEFC;
            public const UInt16 C_CADET_BLUE = 0x64F3;
            public const UInt16 C_STEEL_BLUE = 0x4C16;
            public const UInt16 C_CORN_FLOWER_BLUE = 0x64BD;
            public const UInt16 C_DEEP_SKY_BLUE = 0x05FF;
            public const UInt16 C_DODGER_BLUE = 0x249F;
            public const UInt16 C_LIGHT_BLUE = 0xAEBC;
            public const UInt16 C_SKY_BLUE = 0x867D;
            public const UInt16 C_LIGHT_SKY_BLUE = 0x867E;
            public const UInt16 C_MIDNIGHT_BLUE = 0x18CE;
            public const UInt16 C_NAVY = 0x0010;
            public const UInt16 C_DARK_BLUE = 0x0011;
            public const UInt16 C_MEDIUM_BLUE = 0x0019;
            public const UInt16 C_BLUE = 0x001F;
            public const UInt16 C_ROYAL_BLUE = 0x435B;
            public const UInt16 C_BLUE_VIOLET = 0x897B;
            public const UInt16 C_INDIGO = 0x4810;
            public const UInt16 C_DARK_SLATE_BLUE = 0x49F1;
            public const UInt16 C_SLATE_BLUE = 0x6AD9;
            public const UInt16 C_MEDIUM_SLATE_BLUE = 0x7B5D;
            public const UInt16 C_MEDIUM_PURPLE = 0x939B;
            public const UInt16 C_DARK_MAGENTA = 0x8811;
            public const UInt16 C_DARK_VIOLET = 0x901A;
            public const UInt16 C_DARK_ORCHID = 0x9999;
            public const UInt16 C_MEDIUM_ORCHID = 0xBABA;
            public const UInt16 C_PURPLE = 0x8010;
            public const UInt16 C_THISTLE = 0xD5FA;
            public const UInt16 C_PLUM = 0xDD1B;
            public const UInt16 C_VIOLET = 0xEC1D;
            public const UInt16 C_MAGENTA = 0xF81F;
            public const UInt16 C_ORCHID = 0xDB9A;
            public const UInt16 C_MEDIUM_VIOLET_RED = 0xC0B0;
            public const UInt16 C_PALE_VIOLET_RED = 0xDB92;
            public const UInt16 C_DEEP_PINK = 0xF8B2;
            public const UInt16 C_HOT_PINK = 0xFB56;
            public const UInt16 C_LIGHT_PINK = 0xFDB7;
            public const UInt16 C_PINK = 0xFDF9;
            public const UInt16 C_ANTIQUE_WHITE = 0xF75A;
            public const UInt16 C_BEIGE = 0xF7BB;
            public const UInt16 C_BISQUE = 0xFF18;
            public const UInt16 C_BLANCHED_ALMOND = 0xFF59;
            public const UInt16 C_WHEAT = 0xF6F6;
            public const UInt16 C_CORN_SILK = 0xFFBB;
            public const UInt16 C_LEMON_CHIFFON = 0xFFD9;
            public const UInt16 C_LIGHT_GOLDEN_ROD_YELLOW = 0xF7DA;
            public const UInt16 C_LIGHT_YELLOW = 0xFFFB;
            public const UInt16 C_SADDLE_BROWN = 0x8A22;
            public const UInt16 C_SIENNA = 0x9A85;
            public const UInt16 C_CHOCOLATE = 0xD344;
            public const UInt16 C_PERU = 0xCC28;
            public const UInt16 C_SANDY_BROWN = 0xF52C;
            public const UInt16 C_BURLY_WOOD = 0xDDB0;
            public const UInt16 C_TAN = 0xD591;
            public const UInt16 C_ROSY_BROWN = 0xBC71;
            public const UInt16 C_MOCCASIN = 0xFF16;
            public const UInt16 C_NAVAJO_WHITE = 0xFEF5;
            public const UInt16 C_PEACH_PUFF = 0xFED6;
            public const UInt16 C_MISTY_ROSE = 0xFF1B;
            public const UInt16 C_LAVENDER_BLUSH = 0xFF7E;
            public const UInt16 C_LINEN = 0xF77C;
            public const UInt16 C_OLD_LACE = 0xFFBC;
            public const UInt16 C_PAPAYA_WHIP = 0xFF7A;
            public const UInt16 C_SEA_SHELL = 0xFFBD;
            public const UInt16 C_MINT_CREAM = 0xF7FE;
            public const UInt16 C_SLATE_GRAY = 0x7412;
            public const UInt16 C_LIGHT_SLATE_GRAY = 0x7453;
            public const UInt16 C_LIGHT_STEEL_BLUE = 0xAE1B;
            public const UInt16 C_LAVENDER = 0xE73E;
            public const UInt16 C_FLORAL_WHITE = 0xFFDD;
            public const UInt16 C_ALICE_BLUE = 0xEFBF;
            public const UInt16 C_GHOST_WHITE = 0xF7BF;
            public const UInt16 C_HONEYDEW = 0xEFFD;
            public const UInt16 C_IVORY = 0xFFFD;
            public const UInt16 C_AZURE = 0xEFFF;
            public const UInt16 C_SNOW = 0xFFDE;
            public const UInt16 C_BLACK = 0x0000;
            public const UInt16 C_DIM_GRAY = 0x6B4D;
            public const UInt16 C_GRAY = 0x8410;
            public const UInt16 C_DARK_GRAY = 0xAD55;
            public const UInt16 C_SILVER = 0xBDF7;
            public const UInt16 C_LIGHT_GRAY = 0xD69A;
            public const UInt16 C_GAINSBORO = 0xDEDB;
            public const UInt16 C_WHITE_SMOKE = 0xF7BE;
            public const UInt16 C_WHITE = 0xFFFF;
            
            public UInt16 color { get; set; }

            public uGuiColor(UInt16 color)
            {
                this.color = color;
            }
            public UInt16 R
            {
                get
                {
                    UInt16 r = (UInt16)((color & 0xF800) >> 11);
                    if (r != 0)
                    {
                        r <<= 3;
                        r |= 0x0007;
                    }
                    return r;
                }
            }
            public UInt16 G
            {
                get
                {
                    UInt16 g = (UInt16)((color & 0x07E0) >> 5);
                    if (g != 0)
                    {
                        g <<= 2;
                        g |= 0x0003;
                    }
                    return g;
                }
            }
            public UInt16 B
            {
                get
                {
                    UInt16 b = (UInt16)((color & 0x001F) >> 0);
                    if (b != 0)
                    {
                        b <<= 3;
                        b |= 0x0007;
                    }
                    return b;
                }
            }
#endif

#if !USE_COLOR_RGB565

            public const UInt32 C_MAROON = 0x800000;
            public const UInt32 C_DARK_RED = 0x8B0000;
            public const UInt32 C_BROWN = 0xA52A2A;
            public const UInt32 C_FIREBRICK = 0xB22222;
            public const UInt32 C_CRIMSON =  0xDC143C;
            public const UInt32 C_RED = 0xFF0000;
            public const UInt32 C_TOMATO = 0xFF6347;
            public const UInt32 C_CORAL = 0xFF7F50;
            public const UInt32 C_INDIAN_RED = 0xCD5C5C;
            public const UInt32 C_LIGHT_CORAL = 0xF08080;
            public const UInt32 C_DARK_SALMON = 0xE9967A;
            public const UInt32 C_SALMON = 0xFA8072;
            public const UInt32 C_LIGHT_SALMON = 0xFFA07A;
            public const UInt32 C_ORANGE_RED = 0xFF4500;
            public const UInt32 C_DARK_ORANGE = 0xFF8C00;
            public const UInt32 C_ORANGE = 0xFFA500;
            public const UInt32 C_GOLD = 0xFFD700;
            public const UInt32 C_DARK_GOLDEN_ROD = 0xB8860B;
            public const UInt32 C_GOLDEN_ROD = 0xDAA520;
            public const UInt32 C_PALE_GOLDEN_ROD = 0xEEE8AA;
            public const UInt32 C_DARK_KHAKI = 0xBDB76B;
            public const UInt32 C_KHAKI = 0xF0E68C;
            public const UInt32 C_OLIVE = 0x808000;
            public const UInt32 C_YELLOW = 0xFFFF00;
            public const UInt32 C_YELLOW_GREEN = 0x9ACD32;
            public const UInt32 C_DARK_OLIVE_GREEN = 0x556B2F;
            public const UInt32 C_OLIVE_DRAB = 0x6B8E23;
            public const UInt32 C_LAWN_GREEN = 0x7CFC00;
            public const UInt32 C_CHART_REUSE = 0x7FFF00;
            public const UInt32 C_GREEN_YELLOW = 0xADFF2F;
            public const UInt32 C_DARK_GREEN = 0x006400;
            public const UInt32 C_GREEN = 0x00FF00;
            public const UInt32 C_FOREST_GREEN = 0x228B22;
            public const UInt32 C_LIME = 0x00FF00;
            public const UInt32 C_LIME_GREEN = 0x32CD32;
            public const UInt32 C_LIGHT_GREEN = 0x90EE90;
            public const UInt32 C_PALE_GREEN = 0x98FB98;
            public const UInt32 C_DARK_SEA_GREEN = 0x8FBC8F;
            public const UInt32 C_MEDIUM_SPRING_GREEN = 0x00FA9A;
            public const UInt32 C_SPRING_GREEN = 0x00FF7F;
            public const UInt32 C_SEA_GREEN = 0x2E8B57;
            public const UInt32 C_MEDIUM_AQUA_MARINE = 0x66CDAA;
            public const UInt32 C_MEDIUM_SEA_GREEN = 0x3CB371;
            public const UInt32 C_LIGHT_SEA_GREEN = 0x20B2AA;
            public const UInt32 C_DARK_SLATE_GRAY = 0x2F4F4F;
            public const UInt32 C_TEAL = 0x008080;
            public const UInt32 C_DARK_CYAN = 0x008B8B;
            public const UInt32 C_AQUA = 0x00FFFF;
            public const UInt32 C_CYAN = 0x00FFFF;
            public const UInt32 C_LIGHT_CYAN = 0xE0FFFF;
            public const UInt32 C_DARK_TURQUOISE = 0x00CED1;
            public const UInt32 C_TURQUOISE = 0x40E0D0;
            public const UInt32 C_MEDIUM_TURQUOISE = 0x48D1CC;
            public const UInt32 C_PALE_TURQUOISE = 0xAFEEEE;
            public const UInt32 C_AQUA_MARINE = 0x7FFFD4;
            public const UInt32 C_POWDER_BLUE = 0xB0E0E6;
            public const UInt32 C_CADET_BLUE = 0x5F9EA0;
            public const UInt32 C_STEEL_BLUE = 0x4682B4;
            public const UInt32 C_CORN_FLOWER_BLUE = 0x6495ED;
            public const UInt32 C_DEEP_SKY_BLUE = 0x00BFFF;
            public const UInt32 C_DODGER_BLUE = 0x1E90FF;
            public const UInt32 C_LIGHT_BLUE = 0xADD8E6;
            public const UInt32 C_SKY_BLUE = 0x87CEEB;
            public const UInt32 C_LIGHT_SKY_BLUE = 0x87CEFA;
            public const UInt32 C_MIDNIGHT_BLUE = 0x191970;
            public const UInt32 C_NAVY = 0x000080;
            public const UInt32 C_DARK_BLUE = 0x00008B;
            public const UInt32 C_MEDIUM_BLUE = 0x0000CD;
            public const UInt32 C_BLUE = 0x0000FF;
            public const UInt32 C_ROYAL_BLUE = 0x4169E1;
            public const UInt32 C_BLUE_VIOLET = 0x8A2BE2;
            public const UInt32 C_INDIGO = 0x4B0082;
            public const UInt32 C_DARK_SLATE_BLUE = 0x483D8B;
            public const UInt32 C_SLATE_BLUE = 0x6A5ACD;
            public const UInt32 C_MEDIUM_SLATE_BLUE = 0x7B68EE;
            public const UInt32 C_MEDIUM_PURPLE = 0x9370DB;
            public const UInt32 C_DARK_MAGENTA = 0x8B008B;
            public const UInt32 C_DARK_VIOLET = 0x9400D3;
            public const UInt32 C_DARK_ORCHID = 0x9932CC;
            public const UInt32 C_MEDIUM_ORCHID = 0xBA55D3;
            public const UInt32 C_PURPLE = 0x800080;
            public const UInt32 C_THISTLE = 0xD8BFD8;
            public const UInt32 C_PLUM = 0xDDA0DD;
            public const UInt32 C_VIOLET = 0xEE82EE;
            public const UInt32 C_MAGENTA = 0xFF00FF;
            public const UInt32 C_ORCHID = 0xDA70D6;
            public const UInt32 C_MEDIUM_VIOLET_RED = 0xC71585;
            public const UInt32 C_PALE_VIOLET_RED = 0xDB7093;
            public const UInt32 C_DEEP_PINK = 0xFF1493;
            public const UInt32 C_HOT_PINK = 0xFF69B4;
            public const UInt32 C_LIGHT_PINK = 0xFFB6C1;
            public const UInt32 C_PINK = 0xFFC0CB;
            public const UInt32 C_ANTIQUE_WHITE = 0xFAEBD7;
            public const UInt32 C_BEIGE = 0xF5F5DC;
            public const UInt32 C_BISQUE = 0xFFE4C4;
            public const UInt32 C_BLANCHED_ALMOND = 0xFFEBCD;
            public const UInt32 C_WHEAT = 0xF5DEB3;
            public const UInt32 C_CORN_SILK = 0xFFF8DC;
            public const UInt32 C_LEMON_CHIFFON = 0xFFFACD;
            public const UInt32 C_LIGHT_GOLDEN_ROD_YELLOW = 0xFAFAD2;
            public const UInt32 C_LIGHT_YELLOW = 0xFFFFE0;
            public const UInt32 C_SADDLE_BROWN = 0x8B4513;
            public const UInt32 C_SIENNA = 0xA0522D;
            public const UInt32 C_CHOCOLATE = 0xD2691E;
            public const UInt32 C_PERU = 0xCD853F;
            public const UInt32 C_SANDY_BROWN = 0xF4A460;
            public const UInt32 C_BURLY_WOOD = 0xDEB887;
            public const UInt32 C_TAN = 0xD2B48C;
            public const UInt32 C_ROSY_BROWN = 0xBC8F8F;
            public const UInt32 C_MOCCASIN = 0xFFE4B5;
            public const UInt32 C_NAVAJO_WHITE = 0xFFDEAD;
            public const UInt32 C_PEACH_PUFF = 0xFFDAB9;
            public const UInt32 C_MISTY_ROSE = 0xFFE4E1;
            public const UInt32 C_LAVENDER_BLUSH = 0xFFF0F5;
            public const UInt32 C_LINEN = 0xFAF0E6;
            public const UInt32 C_OLD_LACE = 0xFDF5E6;
            public const UInt32 C_PAPAYA_WHIP = 0xFFEFD5;
            public const UInt32 C_SEA_SHELL = 0xFFF5EE;
            public const UInt32 C_MINT_CREAM = 0xF5FFFA;
            public const UInt32 C_SLATE_GRAY = 0x708090;
            public const UInt32 C_LIGHT_SLATE_GRAY = 0x778899;
            public const UInt32 C_LIGHT_STEEL_BLUE = 0xB0C4DE;
            public const UInt32 C_LAVENDER = 0xE6E6FA;
            public const UInt32 C_FLORAL_WHITE = 0xFFFAF0;
            public const UInt32 C_ALICE_BLUE = 0xF0F8FF;
            public const UInt32 C_GHOST_WHITE = 0xF8F8FF;
            public const UInt32 C_HONEYDEW = 0xF0FFF0;
            public const UInt32 C_IVORY = 0xFFFFF0;
            public const UInt32 C_AZURE = 0xF0FFFF;
            public const UInt32 C_SNOW = 0xFFFAFA;
            public const UInt32 C_BLACK = 0x000000;
            public const UInt32 C_DIM_GRAY = 0x696969;
            public const UInt32 C_GRAY = 0x808080;
            public const UInt32 C_DARK_GRAY = 0xA9A9A9;
            public const UInt32 C_SILVER = 0xC0C0C0;
            public const UInt32 C_LIGHT_GRAY = 0xD3D3D3;
            public const UInt32 C_GAINSBORO = 0xDCDCDC;
            public const UInt32 C_WHITE_SMOKE = 0xF5F5F5;
            public const UInt32 C_WHITE = 0xFFFFFF;
            

            public UInt32 color { get; set; }

            public uGuiColor(UInt32 color)
            {
                this.color = color;
            }

            public UInt16 R
            {
                get
                {
                    UInt16 r = (UInt16)((color & 0xFF0000) >> 16);
                    return r;
                }
            }
            public UInt16 G
            {
                get
                {
                    UInt16 g = (UInt16)((color & 0x00FF00) >> 8);
                    return g;
                }
            }
            public UInt16 B
            {
                get
                {
                    UInt16 b = (UInt16)((color & 0x0000FF) >> 0);
                    return b;
                }
            }
#endif

            public uGuiColor()
            {
                this.color = C_BLACK;
            }
        }
    }
}
