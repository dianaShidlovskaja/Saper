using Saper.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper
{
    public partial class Form1 : Form
    {
        private Game game;
        public Form1()
        {
            InitializeComponent();
            game = new Game(Level.Easy);
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                game = new Game(Level.Easy);
                this.ClientSize = new System.Drawing.Size(280, 300);
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                game = new Game(Level.Medium);
                this.ClientSize = new System.Drawing.Size(380, 400);
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                game = new Game(Level.Hard);
                this.ClientSize = new System.Drawing.Size(480, 500);
            }

            tabControl1.SelectedTab = tabPage2;

            RefreshField();
            tabPage2.Controls.Clear();
            RefreshField();
            tabPage2.Refresh();
            this.Refresh();
        }

        private void RefreshField()
        {
            foreach (Field pField in game.Fields)
            {
                DrawField(pField);
            }
        }

        private void DrawField(Field pField)
        {
            Button pButton = null;
            foreach (object pC in tabPage2.Controls)
            {
                if (!(pC is Button)) continue;
                Field pTag = (pC as Button).Tag as Field;
                if (pTag == null) continue;
                if (pField.X == pTag.X && pField.Y == pTag.Y)
                {
                    pButton = pC as Button;
                }
            }

            if (pButton == null)
            {
                const int Size = 40;
                const int Padding = 10;

                pButton = new Button();
                pButton.Parent = tabPage2;
                pButton.Size = new Size(Size, Size);
                pButton.Left = Padding + pField.X * (Size + Padding);
                pButton.Top = Padding + pField.Y * (Size + Padding);
                pButton.Tag = pField;
                pButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
                pButton.Click += btnClick;
            }
        }

        private void btnClick(object sender, EventArgs e)
        {
            Field pTag = (sender as Control).Tag as Field;
            (sender as Control).Text = pTag.Bomb == true ? "" : pTag.Number.ToString();

            if (pTag.Number == 0)
            {
                (sender as Button).ForeColor = Color.Gray;
            }
            else if (pTag.Number == 1)
            {
                (sender as Button).ForeColor = Color.Black;
            }
            else if (pTag.Number == 2)
            {
                (sender as Button).ForeColor = Color.Blue;
            }
            else if (pTag.Number >= 3)
            {
                (sender as Button).ForeColor = Color.Red;
            }
        
            if (pTag.Bomb)
            {
                (sender as Button).BackColor = Color.Red;
                (sender as Button).Image = Image.FromFile("C:\\Users\\HP\\Desktop\\4 семестр\\ООП\\Курсач\\Saper\\Saper\\Properties\\bomb.png");
            }
            if (pTag == null)
            {
                return;
            }
            game.click(pTag);
            if (game.Status == GameStatus.Win)
            {
                MessageBox.Show("Вы выиграли");
                tabControl1.SelectedTab = tabPage1;
                this.ClientSize = new System.Drawing.Size(334, 237);
            }
            else if (game.Status == GameStatus.GameOver)
            {
                MessageBox.Show("Вы проиграли");
                tabControl1.SelectedTab = tabPage1;
                this.ClientSize = new System.Drawing.Size(334, 237);
            }
        }
    }
}
