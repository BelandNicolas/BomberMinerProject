using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberMinerV2
{
    class GameObjectPlayer
    {
        //CARACTÉRISTIQUES
        public Texture2D sprite;
        public Vector2 direction;
        public Vector2 vitesse;
        public Rectangle position;
        public Rectangle spriteAfficher; //Le rectangle affiché à l'écran
        public bool estVivant;

        public enum etats { attenteDroite, attenteGauche, runDroite, runGauche, attenteHaut, runHaut, attenteBas, runBas };
        public etats objetState;

        //Compteur qui changera le sprite affiché
        private int cpt = 0;

        //GESTION DES TABLEAUX DE SPRITES (chaque sprite est un rectangle dans le tableau)
        int runState = 0; //État de départ
        int nbEtatRun = 4; //Combien il y a de rectangles pour l’état “courrir”
        public Rectangle[] tabRunDroite = {
            new Rectangle(69, 2, 17, 23),
            new Rectangle(87, 2, 17, 23),
            new Rectangle(69, 2, 17, 23),
            new Rectangle(104, 2, 17, 23),
        };

        public Rectangle[] tabRunGauche = {
            new Rectangle(179, 2, 17, 23),
            new Rectangle(197, 2, 17, 23),
            new Rectangle(179, 2, 17, 23),
            new Rectangle(215, 2, 17, 23)

        };

        public Rectangle[] tabRunHaut = {
            new Rectangle(14, 2, 17, 23),
            new Rectangle(31, 2, 17, 23),
            new Rectangle(14, 2, 17, 23),
            new Rectangle(48, 2, 17, 23),
        };

        public Rectangle[] tabRunBas = {
            new Rectangle(124, 2, 17, 23),
            new Rectangle(141, 2, 17, 23),
            new Rectangle(124, 2, 17, 23),
            new Rectangle(158, 2, 17, 23),
        };

        int waitState = 0;
        public Rectangle[] tabAttenteDroite =
        {
            new Rectangle(69, 2, 17, 23)
        };

        public Rectangle[] tabAttenteGauche =
        {
            new Rectangle(179, 2, 17, 23)
        };

        public Rectangle[] tabAttenteHaut =
        {
            new Rectangle(14, 2, 17, 23)
        };

        public Rectangle[] tabAttenteBas =
        {
            new Rectangle(124, 2, 17, 23)
        };

        public virtual void Update(GameTime gameTime)
        {
            if (objetState == etats.attenteDroite)
            {
                spriteAfficher = tabAttenteDroite[waitState];
            }
            if (objetState == etats.attenteGauche)
            {
                spriteAfficher = tabAttenteGauche[waitState];
            }
            if (objetState == etats.runDroite)
            {
                spriteAfficher = tabRunDroite[runState];
            }
            if (objetState == etats.runGauche)
            {
                spriteAfficher = tabRunGauche[runState];
            }
            if (objetState == etats.attenteHaut)
            {
                spriteAfficher = tabAttenteHaut[waitState];
            }
            if (objetState == etats.runHaut)
            {
                spriteAfficher = tabRunHaut[runState];
            }
            if (objetState == etats.attenteBas)
            {
                spriteAfficher = tabAttenteBas[waitState];
            }
            if (objetState == etats.runBas)
            {
                spriteAfficher = tabRunBas[runState];
            }
            //Compteur permettant de gérer le changement d'images
            cpt++;
            if (cpt == 6) //Vitesse défilement
            {
                //Gestion de la course
                runState++;
                if (runState == nbEtatRun)
                {
                    runState = 0;
                }
                cpt = 0;
            }
        }


    }
}
