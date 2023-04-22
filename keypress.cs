using System;  
using System.Windows.Forms;  
using System.Drawing;  
using System.IO;

public partial class Form2 : Form
{
    private TextBox textBox1;
    private TextBox textBox2;
    public Form2()
    {
        //InitializeComponent();
        textBox1 = new TextBox();
        textBox2 = new TextBox();
        textBox2.Multiline = true;
        textBox2.ScrollBars = ScrollBars.Both;
        textBox2.Location =new Point(50,50);

        //Setup events that listens on keypress
        textBox1.KeyDown += TextBox1_KeyDown;
        textBox1.KeyPress += TextBox1_KeyPress;
        textBox1.KeyUp += TextBox1_KeyUp;

        this.Controls.Add(textBox1);
        this.Controls.Add(textBox2);

        // capture keypress 
        this.KeyPreview = true;
        this.KeyPress += FormKeypress;
        this.Paint += new PaintEventHandler(GridPaint);
        
    }

    private void GridPaint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;  
            SolidBrush green = new SolidBrush(Color.LightGreen);
            for (int x = 10; x < 500; x += 100) { 
                for (int y = 10; y < 500; y += 100) { 
                    g.FillRectangle(green, x, y, 50, 30);
                    g.DrawString("" + x + y, new Font("Verdana", 10), new SolidBrush(Color.Black), x, y);  
                }
            }

    }

    private void FormKeypress(object sender, KeyPressEventArgs e) {
        Console.Write("keypress " + e.KeyChar + "\r\n");
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