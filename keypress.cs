using System;  
using System.Windows.Forms;  
using System.Drawing;  
using System.Runtime.InteropServices;




public partial class Form2 : Form
{
    private int h;
    private int w;
    private char[] alphabet;
    private int space;
    private int first;
    private int second;
    private Point origpos;

    private int xnudge;
    private int ynudge;
    private int nudge;

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
        this.second = -100;

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
        origpos = Cursor.Position;

        xnudge = 0;
        ynudge = 0;
        nudge = 15;
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

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
                    // show all grid if first is not active
                    if (first < 0) {
                        g.FillRectangle(green, newx, newy, 50, 30);
                        g.DrawString("" + alphabet[x] + alphabet[y], tt, black, newx, newy);  

                    } else if (x == first & second < 0 ){
                        g.FillRectangle(green, newx, newy, 50, 30);
                        g.DrawString("" + alphabet[x] + alphabet[y], tt, black, newx, newy);  
                    } else if ((x == first) ^ (y == second)){
                        g.FillRectangle(green, newx, newy, 50, 30);
                        g.DrawString("" + alphabet[x] + alphabet[y], tt, black, newx, newy);  

                    }
                }
            }
    }

    private void FormKeypress(object sender, KeyEventArgs e) {
        int LeftDown = (int)0x00000002;
        int LeftUp = (int)0x00000004;

        // Check if the Escape key was pressed
        if (e.KeyCode == Keys.Escape)
        {
            // reset mouse to start location
            Cursor.Position = origpos;
            // Exit the application
            Application.Exit();
        }
        // Console.Write("keypress " + e.KeyCode + "\r\n");
        if ( first < 0 ) {
            first = Array.IndexOf(alphabet, (char)e.KeyCode);
            this.Refresh();

        } else if (first > 0 & second < 0) {
            // we have our second letter
            second = Array.IndexOf(alphabet, (char)e.KeyCode);
            // Console.WriteLine(new Point(first,second));
            Cursor.Position = new Point(first * space + 15,second * space + 10);
            this.Refresh();
            // Application.Exit();
        } else if (first > 0 & second > 0){

            if (e.KeyCode == Keys.Enter) {
                this.WindowState = FormWindowState.Minimized;
                mouse_event((uint)LeftDown,(uint)(first*space + 15),(uint)(second * space + 15),0,0);
                mouse_event((uint)LeftUp,(uint)(first*space + 15),(uint)(second * space + 15),0,0);
                Application.Exit();
            } else if ((char)e.KeyCode == 'H' | e.KeyCode == Keys.Left){
                xnudge -= nudge;
            } else if ((char)e.KeyCode == 'L'| e.KeyCode == Keys.Right){
                xnudge += nudge;
            } else if ((char)e.KeyCode == 'J'| e.KeyCode == Keys.Down){
                ynudge += nudge;
            } else if ((char)e.KeyCode == 'K'| e.KeyCode == Keys.Up){
                ynudge -= nudge;
            }
            Cursor.Position = new Point(first * space + 15 + xnudge,second * space + 10 + ynudge);

        }
        // int pos = Array.IndexOf(alphabet, (char)e.KeyCode);
        //Console.WriteLine("pos" + pos);

    }

    public static void Main() {
        Application.Run(new Form2());

    }
}