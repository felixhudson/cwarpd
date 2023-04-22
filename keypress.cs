using System;  
using System.Windows.Forms;  
using System.Drawing;  
using System.IO;

public partial class Form2 : Form
{
    private TextBox textBox1;
    private TextBox textBox2;
    private int h;
    private int w;
    private char[] alphabet;
    public Form2()
    {
        //InitializeComponent();
        textBox1 = new TextBox();
        textBox2 = new TextBox();
        textBox2.Multiline = true;
        textBox2.ScrollBars = ScrollBars.Both;
        textBox2.Location =new Point(50,50);

        this.alphabet = new char[26];
        for (int i = 0; i < 26; i++) {
                this.alphabet[i] = (char)('a' + i);
        }



        //Setup events that listens on keypress
        textBox1.KeyDown += TextBox1_KeyDown;
        textBox1.KeyPress += TextBox1_KeyPress;
        textBox1.KeyUp += TextBox1_KeyUp;

        this.Controls.Add(textBox1);
        this.Controls.Add(textBox2);

        // capture keypress 
        this.KeyPreview = true;
        this.KeyDown += FormKeypress;
        this.Paint += new PaintEventHandler(GridPaint);
        this.screensize();
        Screen screen = Screen.PrimaryScreen;
        this.h = screen.Bounds.Height;
        this.w = screen.Bounds.Width;
        this.Bounds = new Rectangle(0,0,screen.Bounds.Width,screen.Bounds.Height);
        this.BackColor = Color.LimeGreen;
        this.TransparencyKey = Color.LimeGreen;  
    }

    private void screensize(){
        Screen screen = Screen.PrimaryScreen;
        Console.WriteLine("Screen Resolution: " + screen.Bounds.Width + "x" + screen.Bounds.Height);
    }

    private void GridPaint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;  
            SolidBrush green = new SolidBrush(Color.LightGreen);
            for (int x = 10; x < this.w; x += 100) { 
                for (int y = 10; y < h; y += 100) { 
                    g.FillRectangle(green, x, y, 50, 30);
                    g.DrawString("" + x + y, new Font("Verdana", 10), new SolidBrush(Color.Black), x, y);  
                }
            }

    }

    private void FormKeypress(object sender, KeyEventArgs e) {
        Console.Write("keypress " + e.KeyCode + "\r\n");
        // Check if the Escape key was pressed
        if (e.KeyCode == Keys.Escape)
        {
            // Exit the application
            Application.Exit();
        }
    }

    // Handle the KeyUp event to print the type of character entered into the control.
    private void TextBox1_KeyUp(object sender, KeyEventArgs e)
    {
        textBox2.AppendText( "KeyUp code: {e.KeyCode}, value: {e.KeyValue}, modifiers: {e.Modifiers}" + "\r\n");
    }

    // Handle the KeyPress event to print the type of character entered into the control.
    private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
    {
        textBox2.AppendText( "KeyPress keychar: {e.KeyChar}" + "\r\n");
    }

    // Handle the KeyDown event to print the type of character entered into the control.
    private void TextBox1_KeyDown(object sender, KeyEventArgs e)
    {
        textBox2.AppendText( "KeyDown code: {e.KeyCode}, value: {e.KeyValue}, modifiers: {e.Modifiers}" + "\r\n");
    }

    public static void Main() {
        Application.Run(new Form2());

    }
}