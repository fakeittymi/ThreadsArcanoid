using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;

namespace ThreadLab
{
    class Block
    {
        public Thread blockThread;
        public Block(Graphics g)
        {
            blockThread = new Thread(() =>
            {
                Run(g);
            });
            blockThread.Start();
        }

        
        
        private void Run(Graphics g)
        {
            
            Random rand = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            placement.X = rand.Next(20, 750);
            placement.Y = 0;
            while (true)
            {
                if (placement.Y > 730)
                {
                    Thread.CurrentThread.Abort();                                     
                }

                Rectangle rect = new Rectangle(placement.X, placement.Y, 30, 30);   
                
                if (Catched(ref rect))
                {
                    Form1.catchedCount++;
                    Thread.CurrentThread.Abort();                   
                }
                g.DrawRectangle(new Pen(Color.Black), rect);
                g.FillRectangle(new SolidBrush(blockColor), rect);
                placement.Y++;
                //Thread.Sleep(1);
            }
            
        }

        private bool Catched(ref Rectangle rect)
        {
            
            if (rect.IntersectsWith(Form1.platformRect))
            {
                return true;
            }
            else
                return false;
        }

        public Point placement;
        private Color blockColor = Color.Aquamarine;
    }
}
