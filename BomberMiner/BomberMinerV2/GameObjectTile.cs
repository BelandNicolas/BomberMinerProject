using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberMinerV2
{
    class GameObjectTile
    {
        //Le nombre total de tuiles pour les lignes qui entrent dans l'écran
        public const int LIGNE = 15;
        //Le nombre total de tuiles par colonne dans un écran
        public const int COLONNE = 15;
        //Dimensions d'une tuile
        public const int LARGEUR_TUILE = 128;
        public const int HAUTEUR_TUILE = 128;

        //La texture tileLayer
        public Texture2D texture;
        public Vector2 position;

        public Rectangle rectSource = new Rectangle(0, 0, 128, 128);

        //1 = pierre, 2 = roche, 3 = gazon
        public Rectangle rectPierre = new Rectangle(128, 0, 128, 128);
        public Rectangle rectRoche = new Rectangle(384, 0, 128, 128);
        public Rectangle rectGazon = new Rectangle(0, 0, 128, 128);

        //La carte est affiché
        public int[,] map =
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,3,3,3,3,3,3,3,3,3,3,3,3,3,1 },
            {1,3,1,3,1,3,1,3,1,3,1,3,1,3,1 },
            {1,3,3,3,3,3,3,3,3,3,3,3,3,3,1 },
            {1,3,1,3,1,3,1,3,1,3,1,3,1,3,1 },
            {1,3,3,3,3,3,3,3,3,3,3,3,3,3,1 },
            {1,3,1,3,1,3,1,3,1,3,1,3,1,3,1 },
            {1,3,3,3,3,3,3,3,3,3,3,3,3,3,1 },
            {1,3,1,3,1,3,1,3,1,3,1,3,1,3,1 },
            {1,3,3,3,3,3,3,3,3,3,3,3,3,3,1 },
            {1,3,1,3,1,3,1,3,1,3,1,3,1,3,1 },
            {1,3,3,3,3,3,3,3,3,3,3,3,3,3,1 },
            {1,3,1,3,1,3,1,3,1,3,1,3,1,3,1 },
            {1,3,3,3,3,3,3,3,3,3,3,3,3,3,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }
        };


        //On dessine la carte
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < LIGNE; i++)
            {
                position.Y = (i * LARGEUR_TUILE);
                for (int j = 0; j < COLONNE; j++)
                {
                    position.X = (j * HAUTEUR_TUILE);

                    switch (map[i, j])
                    {
                        case 1:
                            spriteBatch.Draw(texture, position, rectPierre, Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(texture, position, rectRoche, Color.White);
                            break;
                        case 3:
                            spriteBatch.Draw(texture, position, rectGazon, Color.White);
                            break;
                    }
                }
            }
        }
    }
}
