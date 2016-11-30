using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
/*
 * Structure de base de tous les objets
 */
namespace BomberMiner
{
    class GameObject
    {
        public Rectangle position;
        public int vitesse;
        public Texture2D sprite;
        public bool estVivant;
        public Point scale;
        public int acceleration = 0;
    }
}
