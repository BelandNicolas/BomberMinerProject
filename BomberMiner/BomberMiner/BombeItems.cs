using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
/*
 * Extention pour les bombes
 */
namespace BomberMiner
{
    class BombeItems : GameObject
    {
        public int grandeurExplosion;
        public bool isExploded;
        public int timeToExplode;
        public int timeToDispel;
        public Texture2D IdleExplosion;
        public Texture2D UpExplosion;
        public Texture2D RightExplosion;
        public Texture2D LeftExplosion;
        public Texture2D DownExplosion;
    }
}
