using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.IO;


//Dear creator, your code is aweful from creator
namespace DBZFinal
{
    public partial class CharacterSelect : Form
    {
        public CharacterSelect()
        {
            InitializeComponent();
        }

        int x = 250, y = 320, counter = 1, counter2 = 1;
        Character P1, P2, P3, P4, P10, P20, P30, P40;
        string play1, play2, play10, play20, play30, play40, first, second, Turn = "";
        Setting settings;
        double time = 45;
        WinPercentage p1win, p2win, p10win, p20win, p30win, p40win;
        Random r = new Random();
        Context _context = new Context();
        public bool team, Draft;
       
        #region Timer
        private void countdowntimer_Tick(object sender, EventArgs e)
        {
            time -= 0.1;
            string time2 = time.ToString("0.00");
            timer.Text = time2;
        }
        private void reset_Tick(object sender, EventArgs e)
        {

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            List<Button> allbuttons = this.Controls.OfType<Button>().ToList();
            foreach (var button in allbuttons)
            {
                if (button.Focused)
                {
                    if (P3 != null & P1 == null)
                    {
                        P3 = _context.Characters.Where(x => x.Name == button.Name & x.Form == P3.Form + 1).FirstOrDefault();
                        if (P3 != null)
                        {
                            pictureBox1.Image = Image.FromFile(P3.PortraitLeft);
                        }
                    }
                    if (P4 != null & P2 == null)
                    {
                        P4 = _context.Characters.Where(x => x.Name == button.Name & x.Form == P4.Form + 1).FirstOrDefault();
                        if (P4 != null)
                        {
                            pictureBox2.Image = Image.FromFile(P4.PortraitRight);
                        }
                    }
                }
            }
        }
        #endregion
        #region RockPaperScissors
        private void button9_Click(object sender, EventArgs e)
        {
            second = "R";
            check();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            second = "P";
            check();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            second = "S";
            check();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            first = "S";
            check();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            first = "P";
            check();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            first = "R";
            check();
        }
        public void check()
        {
            if (Turn == "")
            {
                if (first != null & second != null)
                {
                    if (first == "R" & second == "P")
                    {
                        Turn = "Player 2 turn";
                    }
                    else if (first == "R" & second == "S")
                    {
                        Turn = "Player 1 turn";
                    }
                    else if (first == "R" & second == "R")
                    {
                        Turn = "";
                    }
                    else if (first == "S" & second == "R")
                    {
                        Turn = "Player 2 turn";
                    }
                    else if (first == "S" & second == "S")
                    {
                        Turn = "";
                    }
                    else if (first == "S" & second == "P")
                    {
                        Turn = "Player 1 turn";
                    }
                    else if (first == "P" & second == "R")
                    {
                        Turn = "Player 1 turn";
                    }
                    else if (first == "P" & second == "S")
                    {
                        Turn = "Player 2 turn";
                    }
                    else
                    {
                        Turn = "";
                    }
                    if (Turn != "")
                    {
                        panel1.Visible = false;
                        label7.Text = "Banning Phase";
                        countdowntimer.Start();
                        reset.Start();
                    }
                }
            }
        }
        #endregion  
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Down || keyData == Keys.Up || keyData == Keys.Left || keyData == Keys.Right)
            {
                List<Button> allbuttons = this.Controls.OfType<Button>().ToList();
                if (keyData == Keys.Left)
                {
                    foreach (var button in allbuttons)
                    {
                        if (button.Focused & Convert.ToInt32(button.Tag) == 1 | button.Focused & Convert.ToInt32(button.Tag) == 9)
                        {
                            counter = Convert.ToInt32(button.Tag) + 7;
                            break;
                        }
                        else if (button.Focused & Convert.ToInt32(button.Tag) == 17)
                        {
                            counter = Convert.ToInt32(button.Tag) + 6;
                            break;
                        }
                        else if (button.Focused)
                        {
                            counter = Convert.ToInt32(button.Tag) - 1;
                            break;
                        }
                    }
                }
                if (keyData == Keys.Up)
                {
                    foreach (var button in allbuttons)
                    {
                        if (button.Focused & Convert.ToInt32(button.Tag) < 8)
                        {
                            counter = Convert.ToInt32(button.Tag) + 16;
                            break;
                        }
                        else if (button.Focused & Convert.ToInt32(button.Tag) == 8)
                        {
                            counter = Convert.ToInt32(button.Tag) + 8;
                            break;
                        }
                        else if (button.Focused)
                        {
                            counter = Convert.ToInt32(button.Tag) - 8;
                            break;
                        }
                    }
                }
                if (keyData == Keys.Down)
                {
                    foreach (var button in allbuttons)
                    {
                        if (button.Focused & Convert.ToInt32(button.Tag) > 16)
                        {
                            counter = Convert.ToInt32(button.Tag) - 16;
                            break;
                        }
                        else if (button.Focused & Convert.ToInt32(button.Tag) == 16)
                        {
                            counter = Convert.ToInt32(button.Tag) - 8;
                            break;
                        }
                        else if (button.Focused)
                        {
                            counter = Convert.ToInt32(button.Tag) + 8;
                            break;
                        }
                    }
                }
                if (keyData == Keys.Right)
                {
                    foreach (var button in allbuttons)
                    {
                        if (button.Focused & Convert.ToInt32(button.Tag) == 8 | button.Focused & Convert.ToInt32(button.Tag) == 16)
                        {
                            counter = Convert.ToInt32(button.Tag) - 7;
                            break;
                        }
                        else if (button.Focused & Convert.ToInt32(button.Tag) == 23)
                        {
                            counter = Convert.ToInt32(button.Tag) - 6;
                            break;
                        }
                        else if (button.Focused)
                        {
                            counter = Convert.ToInt32(button.Tag) + 1;
                            break;
                        }
                    }
                }
                if (keyData == Keys.Left | keyData == Keys.Right | keyData == Keys.Up | keyData == Keys.Down)
                {
                    foreach (var button in allbuttons)
                    {
                        if (Convert.ToInt32(button.Tag) == counter & button.Enabled)
                        {
                            button.Focus();
                        }
                    }
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #region HowToPlayLabel
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Visible = false;
        }
        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.Visible = true;
        }
        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.Visible = false;
        }
        #endregion
        private void CharacterSelect_Load(object sender, EventArgs e)
        {
            pictureBox1.Select();
            using (Context _context = new Context())
            {
                foreach (var i in _context.Characters)
                {
                    if(i.Form == 1)
                    {
                        Button nb = new Button()
                        {
                            BackColor = Color.Transparent,
                            BackgroundImageLayout = ImageLayout.Zoom,
                            FlatStyle = FlatStyle.Flat,
                            Image = Properties.Resources.border,
                            Text = "",
                            Size = new Size(95, 95),
                            Name = i.Name,
                            BackgroundImage = Image.FromFile(i.Portrait),
                            Location = new Point(x, y),
                            Tag = counter
                        };
                        counter++;
                        nb.FlatAppearance.BorderSize = 0;
                        nb.FlatAppearance.MouseDownBackColor = Color.Transparent;
                        nb.FlatAppearance.MouseOverBackColor = Color.Transparent;
                        nb.KeyPress += new KeyPressEventHandler(test2);
                        nb.GotFocus += new EventHandler(test3);
                        nb.MouseHover += new EventHandler(test3);
                        nb.LostFocus += new EventHandler(test4);
                        nb.MouseLeave += new EventHandler(test4);
                        nb.Anchor = AnchorStyles.Bottom;
                        if(nb.Name == "Zeno")
                        {
                            nb.Enabled = false;
                        }
                        else
                        {
                            nb.Focus();
                        }
                        this.Controls.Add(nb);
                        nb.BringToFront();
                        panel1.BringToFront();
                        x += 100;
                        if (x == 1050)
                        {
                            x = 250;
                            y += 100;
                        }
                    }
                }
            }
            if (Draft)
            {
                draft();
            }
        }
        //dehover
        protected void test4(object sender, EventArgs e)
        {
            if(play1 != (sender as Button).Name & play2 != (sender as Button).Name & play10 != (sender as Button).Name & play20 != (sender as Button).Name & play30 != (sender as Button).Name & play40 != (sender as Button).Name)
            {
                (sender as Button).Image = Properties.Resources.border;
            }
            else if(play1 == (sender as Button).Name | play10 == (sender as Button).Name | play30 == (sender as Button).Name)
            {
                (sender as Button).Image = Properties.Resources.p1;
            }
            else if (play2 == (sender as Button).Name & play20 == (sender as Button).Name & play40 == (sender as Button).Name)
            {
                (sender as Button).Image = Properties.Resources.p2;
            }
        }
        //hover
        protected void test3(object sender, EventArgs e)
        {
            timer2.Stop();
            timer2.Start();
            string character = (sender as Button).Name;
            //when you hover a button highlight the button
            (sender as Button).Image = Properties.Resources.highlight;

            using (Context _context = new Context())
            {
                P3 = _context.Characters.Where(x => x.Name == character).FirstOrDefault();
                P4 = _context.Characters.Where(x => x.Name == character).FirstOrDefault();
                p1win = _context.WinPercentages.Where(x => x.Player == "P1" & x.Character == character).FirstOrDefault();
                p2win = _context.WinPercentages.Where(x => x.Player == "P2" & x.Character == character).FirstOrDefault();
                p10win = _context.WinPercentages.Where(x => x.Player == "P1" & x.Character == character).FirstOrDefault();
                p20win = _context.WinPercentages.Where(x => x.Player == "P2" & x.Character == character).FirstOrDefault();
                p30win = _context.WinPercentages.Where(x => x.Player == "P1" & x.Character == character).FirstOrDefault();
                p40win = _context.WinPercentages.Where(x => x.Player == "P2" & x.Character == character).FirstOrDefault();
                //if both pb are null change both pb
                lmao(P3);
                if (play1 == null & play2 == null)
                {
                    pictureBox1.Image = Image.FromFile(P3.PortraitLeft);
                    pictureBox2.Image = Image.FromFile(P3.PortraitRight);
                    P1Port.Image = Image.FromFile(P3.Portrait);
                    P2Port.Image = Image.FromFile(P3.Portrait);
                    label3.Text = $"Wins: {p1win.Wins} Loses: {p1win.Loses} win%: {p1win.Percentage}";
                    label4.Text = $"Wins: {p2win.Wins} Loses: {p2win.Loses} win%: {p2win.Percentage}";

                } //if only pb1 is null
                else if(play1 == null)
                {
                    P1Port.Image = Image.FromFile(P3.Portrait);
                    pictureBox1.Image = Image.FromFile(P3.PortraitLeft);
                    label3.Text = $"Wins: {p1win.Wins} Loses: {p1win.Loses} win%: {p1win.Percentage}";
                }
                else if (play2 == null)//if only pb2 is null
                {
                    P2Port.Image = Image.FromFile(P3.Portrait);
                    pictureBox2.Image = Image.FromFile(P3.PortraitRight);
                    label4.Text = $"Wins: {p2win.Wins} Loses: {p2win.Loses} win%: {p2win.Percentage}";
                }
                if (play10 == null & play1 != null)
                {
                    P10Port.Image = Image.FromFile(P3.Portrait);
                    label3.Text = $"Wins: {p10win.Wins} Loses: {p10win.Loses} win%: {p10win.Percentage}";
                }
                else if (play30 == null & play1 != null)
                {
                    P30Port.Image = Image.FromFile(P3.Portrait);
                    label3.Text = $"Wins: {p30win.Wins} Loses: {p30win.Loses} win%: {p30win.Percentage}";
                }
                if (play20 == null & play2 != null)
                {
                    P20Port.Image = Image.FromFile(P3.Portrait);
                    label4.Text = $"Wins: {p20win.Wins} Loses: {p20win.Loses} win%: {p20win.Percentage}";
                }
                else if (play40 == null & play2 != null)
                {
                    P40Port.Image = Image.FromFile(P3.Portrait);
                    label4.Text = $"Wins: {p40win.Wins} Loses: {p40win.Loses} win%: {p40win.Percentage}";
                }
            }
        }
        private void test2(object sender, KeyPressEventArgs e)
        {
            using (Context _context = new Context())
            {
                settings = _context.Settings.FirstOrDefault();
                e.KeyChar.ToString();

                if (e.KeyChar.ToString() == settings.P1Attack)
                {
                    test(sender, e, settings.P1Attack);
                }
                if (e.KeyChar.ToString() == settings.P1PowerUp)
                {
                    test(sender, e, settings.P1PowerUp);
                }
                if (e.KeyChar.ToString() == settings.P1Block)
                {
                    test(sender, e, settings.P1Block);
                }
                if (e.KeyChar.ToString() == settings.P1Transform)
                {
                    test(sender, e, settings.P1Transform);
                }
                if (e.KeyChar.ToString() == settings.P2Attack)
                {
                    test(sender, e, settings.P2Attack);
                }
                if (e.KeyChar.ToString() == settings.P2PowerUp)
                {
                    test(sender, e, settings.P2PowerUp);
                }
                if (e.KeyChar.ToString() == settings.P2Block)
                {
                    test(sender, e, settings.P2Block);
                }
                if (e.KeyChar.ToString() == settings.P2Transform)
                {
                    test(sender, e, settings.P2Transform);
                }
            }
        }
        protected void test(object sender, EventArgs e, string player)
        {
            string character = (sender as Button).Name;

            using (Context _context = new Context())
            {
                settings = _context.Settings.FirstOrDefault();

                //select character
                if (player == settings.P1Attack & P1 == null)
                {
                    if(label7.Text != "Banning Phase")
                    {
                        if(label7.Text == "Player 1 turn")
                        {
                            P1 = _context.Characters.Where(x => x.Name == character).FirstOrDefault();
                            (sender as Button).Image = Properties.Resources.p1;
                            play1 = (sender as Button).Name;
                            P1Port.Image = Image.FromFile(P1.Portrait);
                            label7.Text = "Player 2 turn";
                        }
                        else
                        {
                            MessageBox.Show("Not your turn");
                            //error sound
                        }
                    }
                    else
                    {
                        if (pictureBox3.Image == null)
                        {
                            pictureBox3.Image = Image.FromFile(P3.Portrait);
                            (sender as Button).BackgroundImage = Image.FromFile($"G:\\DBZ\\bannedportraits\\{(sender as Button).Name.ToLower()}Portrait.png");
                            (sender as Button).Enabled = false;
                        }
                        else if (pictureBox4.Image == null)
                        {
                            pictureBox4.Image = Image.FromFile(P3.Portrait);
                            (sender as Button).BackgroundImage = Image.FromFile($"G:\\DBZ\\bannedportraits\\{(sender as Button).Name.ToLower()}Portrait.png");
                            (sender as Button).Enabled = false;
                            if(pictureBox5.Image != null)
                            {
                                label7.Text = Turn;
                            }
                        }
                    }                   
                }
                else if(player == settings.P1Attack & P1 != null & P10 == null & team)
                {
                    if (label7.Text == "Player 1 turn")
                    {
                        if (P1.Name != (sender as Button).Name)
                        {
                            P10 = _context.Characters.Where(x => x.Name == character).FirstOrDefault();
                            (sender as Button).Image = Properties.Resources.p1;
                            play10 = (sender as Button).Name;
                            P10Port.Image = Image.FromFile(P10.Portrait);
                            label7.Text = "Player 2 turn";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not your turn");
                        //error sound
                    }
                }
                else if (player == settings.P1Attack & P1 != null & P10 != null & P30 == null & team)
                {
                    if (label7.Text == "Player 1 turn")
                    {
                        if (P1.Name != (sender as Button).Name & P10.Name != (sender as Button).Name)
                        {
                            P30 = _context.Characters.Where(x => x.Name == character).FirstOrDefault();
                            (sender as Button).Image = Properties.Resources.p1;
                            play30 = (sender as Button).Name;
                            P30Port.Image = Image.FromFile(P30.Portrait);
                            label7.Text = "Player 2 turn";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not your turn");
                        //error sound
                    }
                }
                //select character
                else if (player == settings.P2Attack & P2 == null)
                {
                    if (label7.Text != "Banning Phase")
                    {
                        if (label7.Text == "Player 2 turn")
                        {
                            P2 = _context.Characters.Where(x => x.Name == character).FirstOrDefault();
                            (sender as Button).Image = Properties.Resources.p2;
                            play2 = (sender as Button).Name;
                            P2Port.Image = Image.FromFile(P2.Portrait);
                            label7.Text = "Player 1 turn";
                        }
                        else
                        {
                            MessageBox.Show("Not your turn");
                            //error sound
                        }
                    }
                    else
                    {
                        if (pictureBox6.Image == null)
                        {
                            pictureBox6.Image = Image.FromFile(P3.Portrait);
                            (sender as Button).BackgroundImage = Image.FromFile($"G:\\DBZ\\bannedportraits\\{(sender as Button).Name.ToLower()}Portrait.png");
                            (sender as Button).Enabled = false;
                        }
                        else if (pictureBox5.Image == null)
                        {
                            pictureBox5.Image = Image.FromFile(P3.Portrait);
                            (sender as Button).BackgroundImage = Image.FromFile($"G:\\DBZ\\bannedportraits\\{(sender as Button).Name.ToLower()}Portrait.png");
                            (sender as Button).Enabled = false;
                            if (pictureBox4.Image != null)
                            {
                                label7.Text = Turn;
                            }
                        }
                    }                        
                }
                else if (player == settings.P2Attack & P2 != null & P20 == null & team)
                {
                    if (label7.Text == "Player 2 turn")
                    {
                        if (P2.Name != (sender as Button).Name)
                        {
                            P20 = _context.Characters.Where(x => x.Name == character).FirstOrDefault();
                            (sender as Button).Image = Properties.Resources.p2;
                            play20 = (sender as Button).Name;
                            P20Port.Image = Image.FromFile(P20.Portrait);
                            label7.Text = "Player 1 turn";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not your turn");
                        //error sound
                    }
                }
                else if (player == settings.P2Attack & P2 != null & P20 != null & P40 == null & team)
                {
                    if (label7.Text == "Player 2 turn")
                    {
                        if (P2.Name != (sender as Button).Name & P20.Name != (sender as Button).Name)
                        {
                            P40 = _context.Characters.Where(x => x.Name == character).FirstOrDefault();
                            (sender as Button).Image = Properties.Resources.p2;
                            play40 = (sender as Button).Name;
                            P40Port.Image = Image.FromFile(P40.Portrait);
                            label7.Text = "Player 1 turn";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not your turn");
                        //error sound
                    }
                }
                else if (player == settings.P1PowerUp)
                {
                    if(play1 == (sender as Button).Name | play10 == (sender as Button).Name | play30 == (sender as Button).Name)
                    {
                        if(play2 == (sender as Button).Name)
                        {
                            (sender as Button).Image = Properties.Resources.p2;
                        }
                        else
                        {
                            (sender as Button).Image = Properties.Resources.highlight;
                        }
                        if(P10 == null)
                        {
                            play1 = null;
                            P1 = null;
                            P10Port.Image = null;
                        }
                        else if(P10 != null & P30 == null)
                        {
                            P30Port.Image = null;
                            play10 = null;
                            P10 = null;
                        }
                        else if(P30 != null)
                        {
                            play30 = null;
                            P30 = null;
                        }
                    }
                }
                else if (player == settings.P2PowerUp)
                {
                    if (play2 == (sender as Button).Name | play20 == (sender as Button).Name | play40 == (sender as Button).Name)
                    {
                        if (play1 == (sender as Button).Name)
                        {
                            (sender as Button).Image = Properties.Resources.p1;
                        }
                        else
                        {
                            (sender as Button).Image = Properties.Resources.highlight;
                        }
                        if (P20 == null)
                        {
                            play2 = null;
                            P2 = null;
                            P20Port.Image = null;
                        }
                        else if (P20 != null & P40 == null)
                        {
                            P40Port.Image = null;
                            play20 = null;
                            P20 = null;
                        }
                        else if (P40 != null)
                        {
                            play40 = null;
                            P40 = null;
                        }
                    }
                }
                else if (player == settings.P1Transform)
                {
                    if (P3 != null & P1 == null)
                    {
                        P3 = _context.Characters.Where(x => x.Name == character & x.Form == P3.Form + 1).FirstOrDefault();
                        if (P3 != null)
                        {
                            pictureBox1.Image = Image.FromFile(P3.PortraitLeft);
                        }
                    }
                    else if (P1 != null & P3 != null)
                    {
                        P3 = _context.Characters.Where(x => x.Name == P1.Name & x.Form == P3.Form + 1).FirstOrDefault();
                        if (P3 != null)
                        {
                            pictureBox1.Image = Image.FromFile(P3.PortraitLeft);
                        }
                    }

                }
                else if (player == settings.P2Transform & P2 == null)
                {
                    if (P4 != null)
                    {
                        P4 = _context.Characters.Where(x => x.Name == character & x.Form == P4.Form + 1).FirstOrDefault();
                        if (P4 != null)
                        {
                            pictureBox2.Image = Image.FromFile(P4.PortraitRight);
                        }
                    }
                    else if (P2 != null & P4 != null)
                    {
                        P4 = _context.Characters.Where(x => x.Name == P2.Name & x.Form == P4.Form + 1).FirstOrDefault();
                        if (P4 != null)
                        {
                            pictureBox2.Image = Image.FromFile(P4.PortraitLeft);
                        }
                    }
                }
                else if(player == settings.P1Block & P1 == null | player == settings.P1Block & P10 == null | player == settings.P1Block & P30 == null)
                {
                    if(P1 == null)
                    {
                        List<Button> buttonlist = this.Controls.OfType<Button>().ToList();
                        Button b = buttonlist[r.Next(0, 24)];
                        if(b.Name != "Zeno")
                        {
                            character = b.Name;
                            P1 = _context.Characters.Where(x => x.Name == character).FirstOrDefault();
                            b.Select();
                            b.Image = Properties.Resources.p1;
                            play1 = b.Name;
                            pictureBox1.Image = Image.FromFile(P1.PortraitRight);
                        }
                    }
                    else if (P10 == null & team)
                    {
                        List<Button> buttonlist = this.Controls.OfType<Button>().ToList();
                        Button b = buttonlist[r.Next(0, 21)];
                        if(P1.Name != b.Name & b.Name != "Zeno")
                        {
                            character = b.Name;
                            P10 = _context.Characters.Where(x => x.Name == character).FirstOrDefault();
                            b.Select();
                            b.Image = Properties.Resources.p1;
                            play10 = b.Name;
                            pictureBox1.Image = Image.FromFile(P10.PortraitRight);
                        }
                    }
                    else if(P30 == null  & team)
                    {
                        List<Button> buttonlist = this.Controls.OfType<Button>().ToList();
                        Button b = buttonlist[r.Next(0, 24)];
                        if (P1.Name != b.Name & P10.Name != b.Name & b.Name != "Zeno")
                        {
                            character = b.Name;
                            P30 = _context.Characters.Where(x => x.Name == character).FirstOrDefault();
                            b.Select();
                            b.Image = Properties.Resources.p1;
                            play30 = b.Name;
                            pictureBox1.Image = Image.FromFile(P30.PortraitRight);
                        }
                    }
                }
                else if (player == settings.P2Block & P2 == null | player == settings.P2Block & P20 == null | player == settings.P2Block & P40 == null)
                {
                    if (P2 == null)
                    {
                        List<Button> buttonlist = this.Controls.OfType<Button>().ToList();
                        Button b = buttonlist[r.Next(0, 24)];
                        if (b.Name != "Zeno")
                        {
                            character = b.Name;
                            P2 = _context.Characters.Where(x => x.Name == character).FirstOrDefault();
                            b.Select();
                            b.Image = Properties.Resources.p2;
                            play2 = b.Name;
                            pictureBox2.Image = Image.FromFile(P2.PortraitRight);
                        }
                    }
                    else if (P20 == null & team)
                    {
                        List<Button> buttonlist = this.Controls.OfType<Button>().ToList();
                        Button b = buttonlist[r.Next(0, 21)];
                        if (P2.Name != b.Name & b.Name != "Zeno")
                        {
                            character = b.Name;
                            P20 = _context.Characters.Where(x => x.Name == character).FirstOrDefault();
                            b.Select();
                            b.Image = Properties.Resources.p2;
                            play20 = b.Name;
                            pictureBox2.Image = Image.FromFile(P20.PortraitRight);
                        }
                    }
                    else if(P40 == null & team)
                    {
                        List<Button> buttonlist = this.Controls.OfType<Button>().ToList();
                        Button b = buttonlist[r.Next(0, 24)];
                        if (P2.Name != b.Name & P20.Name != b.Name & b.Name != "Zeno")
                        {
                            character = b.Name;
                            P40 = _context.Characters.Where(x => x.Name == character).FirstOrDefault();
                            b.Select();
                            b.Image = Properties.Resources.p2;
                            play40 = b.Name;
                            pictureBox2.Image = Image.FromFile(P40.PortraitRight);
                        }
                    }
                }
                if (team)
                {
                    if (play1 != null & play2 != null & play10 != null & play20 != null & play30 != null & play40 != null)
                    {
                        Arena arena = new Arena(P1, P2, settings);
                        this.Hide();
                        arena.team = true;
                        arena.P10 = P10;
                        arena.P20 = P20;
                        arena.P30 = P30;
                        arena.P40 = P40;
                        arena.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    if (play1 != null & play2 != null)
                    {
                        Arena arena = new Arena(P1, P2, settings);
                        this.Hide();
                        arena.p1win = p1win;
                        arena.p2win = p2win;
                        arena.ShowDialog();
                        this.Close();
                    }
                }
            }
        }
        public void lmao(Character player)
        {
            axWindowsMediaPlayer1.Visible = true;
            axWindowsMediaPlayer1.settings.volume = 0;
            axWindowsMediaPlayer1.uiMode = "None";
            if (P1 != null | P10 != null | P30 != null)
            {
                if(P30 != null)
                {
                    if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P30.Name}Vs{player.Name}.mp4") | File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P30.Name}.mp4"))
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P30.Name}Vs{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{P30.Name}Vs{player.Name}.mp4";
                        }
                        else
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P30.Name}.mp4";
                        }
                    }
                    else if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P10.Name}Vs{player.Name}.mp4") | File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P10.Name}.mp4"))
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P10.Name}Vs{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{P10.Name}Vs{player.Name}.mp4";
                        }
                        else
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P10.Name}.mp4";
                        }
                    }
                    else if(File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P1.Name}Vs{player.Name}.mp4") | File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P1.Name}.mp4"))
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P1.Name}Vs{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{P1.Name}Vs{player.Name}.mp4";
                        }
                        else
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P1.Name}.mp4";
                        }
                    }
                    else
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}.mp4";
                        }
                    }
                }
                else if(P10 != null)
                {
                    if(File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P10.Name}Vs{player.Name}.mp4") | File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P10.Name}.mp4"))
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P10.Name}Vs{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{P10.Name}Vs{player.Name}.mp4";
                        }
                        else
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P10.Name}.mp4";
                        }
                    }
                    else if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P1.Name}Vs{player.Name}.mp4") | File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P1.Name}.mp4"))
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P1.Name}Vs{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{P1.Name}Vs{player.Name}.mp4";
                        }
                        else
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P1.Name}.mp4";
                        }
                    }
                    else
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}.mp4";
                        }
                    }
                }
                else if (P1 != null)
                {
                    if(File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P1.Name}Vs{player.Name}.mp4") | File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P1.Name}.mp4"))
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P1.Name}Vs{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{P1.Name}Vs{player.Name}.mp4";
                        }
                        else
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P1.Name}.mp4";
                        }
                    }
                    else
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}.mp4";
                        }
                    }
                }
            }
            else if (P2 != null | P20 != null | P40 != null)
            {
                if (P40 != null)
                {
                    if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P40.Name}Vs{player.Name}.mp4") | File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P40.Name}.mp4"))
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P40.Name}Vs{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{P40.Name}Vs{player.Name}.mp4";
                        }
                        else
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P40.Name}.mp4";
                        }
                    }
                    else if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P20.Name}Vs{player.Name}.mp4") | File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P20.Name}.mp4"))
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P20.Name}Vs{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{P20.Name}Vs{player.Name}.mp4";
                        }
                        else
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P20.Name}.mp4";
                        }
                    }
                    else if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P2.Name}Vs{player.Name}.mp4") | File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P2.Name}.mp4"))
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P2.Name}Vs{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{P2.Name}Vs{player.Name}.mp4";
                        }
                        else
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P2.Name}.mp4";
                        }
                    }
                    else
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}.mp4";
                        }
                    }
                }
                else if (P20 != null)
                {
                    if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P20.Name}Vs{player.Name}.mp4") | File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P20.Name}.mp4"))
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P20.Name}Vs{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{P20.Name}Vs{player.Name}.mp4";
                        }
                        else
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P20.Name}.mp4";
                        }
                    }
                    else if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P2.Name}Vs{player.Name}.mp4") | File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P2.Name}.mp4"))
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P2.Name}Vs{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{P2.Name}Vs{player.Name}.mp4";
                        }
                        else
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P2.Name}.mp4";
                        }
                    }
                    else
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}.mp4";
                        }
                    }
                }
                else if (P2 != null)
                {
                    if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P2.Name}Vs{player.Name}.mp4") | File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P2.Name}.mp4"))
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{P2.Name}Vs{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{P2.Name}Vs{player.Name}.mp4";
                        }
                        else
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}Vs{P2.Name}.mp4";
                        }
                    }
                    else
                    {
                        if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}.mp4"))
                        {
                            axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}.mp4";
                        }
                    }
                }
            }
            else
            {
                if (File.Exists($"G:\\DBZ\\Videos\\vid-gif\\{player.Name}.mp4") == false)
                {
                    axWindowsMediaPlayer1.Visible = false;
                }
                axWindowsMediaPlayer1.URL = $"G:\\DBZ\\Videos\\vid-gif\\{player.Name}.mp4";
            }
        }
        public void draft()
        {
            panel1.Visible = true;
        }        
    }
}