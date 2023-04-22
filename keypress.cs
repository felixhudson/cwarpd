using System;  
using System.Windows.Forms;  
using System.Drawing;  
using System.IO;

public partial class Form2 : Form
{
    private int h;
    private int w;
    private char[] alphabet;
    private int space;
    private int first;
    private int second;
    public Form2()
    {
        // change this to alter spacing
        this.space =80;

        // setup all keys
        this.alphabet = new char[26];
        for (int i = 0; i < 26; i++) {
                this.alphabet[i] = (char)('A' + i);
        }

        this.first = -100;

        // capture keypress's in this window
        this.KeyPreview = true;
        this.KeyDown += FormKeypress;
        this.Paint += new PaintEventHandler(GridPaint);
        // find size of current window
        Screen screen = Screen.PrimaryScreen;
        this.h = screen.Bounds.Height;
        this.w = screen.Bounds.Width;
        // move window to screen corner
        this.Bounds = new Rectangle(0,0,screen.Bounds.Width,screen.Bounds.Height);

        // make window transparent
        this.BackColor = Color.LimeGreen;
        this.TransparencyKey = Color.LimeGreen;  
        // remove window decorations
        this.ControlBox = false;
        this.Text = String.Empty;
    }


    private void GridPaint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;  
            SolidBrush green = new SolidBrush(Color.LightGreen);
            SolidBrush black = new SolidBrush(Color.Black);
            Font tt = new Font("Verdana", 14);
            int newx;
            int newy;
            for (int x = 0; x < this.w/this.space; x += 1) { 
                newx = x * this.space;
                for (int y = 0; y < h/this.space; y += 1) { 
                    newy = y * this.space;
                    g.FillRectangle(green, newx, newy, 50, 30);
                    g.DrawString("" + alphabet[x] + alphabet[y], tt, black, newx, newy);  
                }
            }
    }

    private void FormKeypress(object sender, KeyEventArgs e) {
        // Console.Write("keypress " + e.KeyCode + "\r\n");
        if ( first < 0 ) {
            first = Array.IndexOf(alphabet, (char)e.KeyCode) * space;

        } else {
            // we have our second letter
            second = Array.IndexOf(alphabet, (char)e.KeyCode) * space;
            // Console.WriteLine(new Point(first,second));
            Cursor.Position = new Point(first+ 15,second + 10);
            Application.Exit();
        }
        int pos = Array.IndexOf(alphabet, (char)e.KeyCode);
        //Console.WriteLine("pos" + pos);

        // Check if the Escape key was pressed
        if (e.KeyCode == Keys.Escape)
        {
            // Exit the application
            Application.Exit();
        }
    }

    public static void Main() {
        Application.Run(new Form2());

    }
}