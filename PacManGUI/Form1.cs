using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacMan.GameGL;
using EZInput;
using PacManGUI.GameGL;

namespace PacManGUI
{
    public partial class Form1 : Form
    {
        List<Ghost> ghost = new List<Ghost>();
        GamePacManPlayer pacman;
        HorizontalGhost g;
        VerticalGhost v;
        RandomGhost r;
        SmartGhost s;
        GameGrid grid;
        int score = 0;
        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            grid = new GameGrid("maze.txt", 24, 70);
            Image pacManImage = Game.getGameObjectImage('P');

            GameCell startCell = grid.getCell(1, 8);
            GameCell cell1 = grid.getCell(1, 19);
            GameCell cell2 = grid.getCell(3, 22);
            GameCell cell3 = grid.getCell(18, 3);
            GameCell cell4 = grid.getCell(1, 60);
            pacman = new GamePacManPlayer(pacManImage, startCell);
            g = new HorizontalGhost(Properties.Resources.ghost_blue, cell1, GameObjectType.ENEMY, GameDirection.Left);
            v = new VerticalGhost(Properties.Resources.ghost_red, cell2, GameObjectType.ENEMY, GameDirection.Up);
            r = new RandomGhost(Properties.Resources.ghost_pink, cell3, GameObjectType.ENEMY, GameDirection.Up);
            s = new SmartGhost(Properties.Resources.ghost_orange, cell4, GameObjectType.ENEMY, GameDirection.Up, pacman);
            ghost.Add(g);
            ghost.Add(v);
            ghost.Add(r);
            ghost.Add(s);
            printMaze(grid);
        }



        void printMaze(GameGrid grid)
        {
            for (int x = 0; x < grid.Rows; x++)
            {

                for (int y = 0; y < grid.Cols; y++)
                {
                    GameCell cell = grid.getCell(x, y);
                    this.Controls.Add(cell.PictureBox);

                }

            }
        }

        static void printCell(GameCell cell)
        {
            Console.SetCursorPosition(cell.Y, cell.X);
            Console.Write(cell.CurrentGameObject.DisplayCharacter);
        }




        private void gameLoop_Tick(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyPressed(Key.LeftArrow))
            {
                pacman.move(GameDirection.Left);
                
            }
            if (Keyboard.IsKeyPressed(Key.RightArrow))
            {
                pacman.move(GameDirection.Right);
                
            }
            if (Keyboard.IsKeyPressed(Key.UpArrow))
            {
                pacman.move(GameDirection.Up);
                
            }
            if (Keyboard.IsKeyPressed(Key.DownArrow))
            {
                pacman.move(GameDirection.Down);
                
            }
            foreach (Ghost i in ghost)
            {
                i.MoveGhost(grid);
                
            }
            score = Game.ReturnScore();
            lblScore.Text = score.ToString();

            if(score >= 100)
            {
                gameLoop.Enabled = false;
                MessageBox.Show("You Win");
            }
            if(!Game.GetFlag())
            {
                gameLoop.Enabled = false;
                MessageBox.Show("You Loose");
            }

        }
        

    }
}
