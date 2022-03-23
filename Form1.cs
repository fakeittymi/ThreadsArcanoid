using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadLab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            g = pictureBox1.CreateGraphics();
            platformThread.Start();
        }

        public static int mousePosX;
        public static Rectangle platformRect = new Rectangle(mousePosX, 700, 100, 20);
        static Graphics g;
        private List<Block> blockList = new List<Block>();
        public static int catchedCount = 0;
        public int totalCount = 0;

        Thread platformThread = new Thread(() =>
        {
            while (true)
            {
                mousePosX = MousePosition.X;
                DrawPlatform();
            }
        });
      
        static public void DrawPlatform()
        {
            platformRect.X = mousePosX;
            g.DrawRectangle(new Pen(Color.Black), platformRect);
            g.FillRectangle(new SolidBrush(Color.Blue), platformRect);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
            if (IsThereNewBlock())
            {
                Graphics newG = pictureBox1.CreateGraphics();
                Block newBlock = new Block(newG);
                blockList.Add(newBlock);
                totalCount++;
                label1.Text = "Всего блоков: " + totalCount.ToString();
            }
            label2.Text = "Поймано: " + catchedCount.ToString();
            RemoveAborted();
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            platformThread.Abort();
        }

        private bool IsThereNewBlock()
        {
            Random rand = new Random();
            int value = rand.Next(1, 50);
            if (value == 5)
            {
                return true;
            }
            else
                return false;
        }

        private void RemoveAborted()
        {
            if (blockList.Count != 0 && !blockList[0].blockThread.IsAlive)
            {
                blockList.Remove(blockList[0]);
            }
        }
    }

    
}
