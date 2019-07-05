using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBZFinal
{
    public partial class Arena : Form
    {
        public Character P1, P2, P10, P20, P30, P40, P5, P6;
        Setting Settings;
        int roundcounter = 1, P1W = 0, P2W = 0, RoundCount = 1;
        double time = 15;
        string P1Status = "", P2Status = "", keep1, keep2;
        public WinPercentage p1win, p2win, p10win, p20win, p30win, p40win;
        public bool team = false;
        Context _context = new Context();
        public Arena(Character p1, Character p2, Setting settings)
        {
            InitializeComponent();
            P1 = p1;
            P2 = p2;
            Settings = settings;
            P1Port.BackgroundImage = Image.FromFile(P1.Portrait);
            P2Port.BackgroundImage = Image.FromFile(P2.Portrait);
            keep1 = P1.Name;
            keep2 = P2.Name;
        }
        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar.ToString();
            if (e.KeyChar.ToString() == Settings.P1Attack & button1.Enabled == true & turn.Text == "P1's Turn" | e.KeyChar.ToString() == Settings.P2Attack & button1.Enabled == true & turn.Text != "P1's Turn")
            {
                keymethod(button1);
            }
            if (e.KeyChar.ToString() == Settings.P1Block & turn.Text == "P1's Turn" | e.KeyChar.ToString() == Settings.P2Block & turn.Text != "P1's Turn")
            {
                keymethod(button2);
            }
            if (e.KeyChar.ToString() == Settings.P1PowerUp & button3.Enabled == true & turn.Text == "P1's Turn" | e.KeyChar.ToString() == Settings.P2PowerUp & button3.Enabled == true & turn.Text != "P1's Turn")
            {
                keymethod(button3);
            }
            if (e.KeyChar.ToString() == Settings.P1Transform & button4.Enabled == true & turn.Text == "P1's Turn" | e.KeyChar.ToString() == Settings.P2Transform & button4.Enabled == true & turn.Text != "P1's Turn")
            {
                keymethod(button4);
            }
            if (e.KeyChar.ToString() == Settings.P1Ultimate & button5.Enabled == true & turn.Text == "P1's Turn" | e.KeyChar.ToString() == Settings.P2Ultimate & button5.Enabled == true & turn.Text != "P1's Turn")
            {
                keymethod(button5);
            }
        }
        private void keymethod(Button bt)
        {
            if (turn.Text == "P1's Turn")
            {
                P1Status = bt.Text;
                turn.Text = "P2's Turn";
                grayout(P2, p2power, button1, button3, button4, button5, P2Status);
                time = 15;
                reset.Stop();
                reset.Start();
                PassiveMethod(P1, p1power, p1health, p1powerlabel, p1healthlabel);
            }
            else
            {
                P2Status = bt.Text;
                PassiveMethod(P2, p2power, p2health, p2powerlabel, p2healthlabel);
                lockin();
            }
        }
        private void Arena_Load(object sender, EventArgs e)
        {
            if (team)
            {
                P10Port.Visible = true;
                P10Port.BackgroundImage = Image.FromFile(P10.Portrait);
                P20Port.Visible = true;
                P20Port.BackgroundImage = Image.FromFile(P20.Portrait);
                P30Port.Visible = true;
                P30Port.BackgroundImage = Image.FromFile(P30.Portrait);
                P40Port.Visible = true;
                P40Port.BackgroundImage = Image.FromFile(P40.Portrait);
            }
            else
            {
                P1Port.BackgroundImage = Image.FromFile(P1.Portrait);
                P2Port.BackgroundImage = Image.FromFile(P2.Portrait);
            }
            roundcounter = 1;
            P1pb.Image = Image.FromFile(P1.PortraitLeft);
            P2pb.Image = Image.FromFile(P2.PortraitRight);
            CurrentCharacterStats(P1, p1stats);
            CurrentCharacterStats(P2, p2stats);
            p1healthlabel.Text = "HEALTH: " + p1health.Value.ToString();
            p2healthlabel.Text = "HEALTH: " + p2health.Value.ToString();
            p1powerlabel.Text = "POWER: " + p1power.Value.ToString();
            p2powerlabel.Text = "POWER: " + p2power.Value.ToString();
            roundcount.Text = "Round " + roundcounter.ToString();
            turn.Text = "P1's Turn";
            p1name.Text = P1.Name;
            p2name.Text = P2.Name;
            grayout(P1, p1power, button1, button3, button4, button5, P1Status);
            if (p1rb2.Checked & p2rb2.Checked)
            {
                battledata.Text += "                               Final Round \r\n";
            }
            else
            {
                battledata.Text += "                                Round " + RoundCount + "\r\n";
            }
            battledata.Text += "_______________________________________\r\n";
        }
        private void buttonClick(object sender, EventArgs e)
        {
            if (turn.Text == "P1's Turn")
            {
                if (button6.Enabled == true)
                {
                    grayout(P1, p1power, button1, button3, button4, button5, P1Status);
                }
                else
                {
                    P1Status = (sender as Button).Text;
                    enabler((sender as Button), button6, button5, button2, button3, button4, button1);
                }
            }
            else
            {
                if (button6.Enabled == true)
                {
                    grayout(P2, p2power, button1, button3, button4, button5, P2Status);
                }
                else
                {
                    P1Status = (sender as Button).Text;
                    enabler((sender as Button), button6, button5, button2, button3, button4, button1);
                }
            }
        }
        private void CurrentCharacterStats(Character CurrentPlayer, TextBox lb) //display stats
        {
            lb.Text = "Name: " + CurrentPlayer.Name + "\r\nDamage: " + CurrentPlayer.AttackDamage + "\r\nAttack cost: " + CurrentPlayer.PowerCost
                + "\r\nPassive chance: " + CurrentPlayer.PassiveChance + "\r\nTransform cost: " + CurrentPlayer.UpgradeCost + "\r\nUlt: " +
                CurrentPlayer.UltDamage + "\r\nUlt cost: " + CurrentPlayer.UltCost;
        }
        private void reset_Tick(object sender, EventArgs e)
        {
            if (turn.Text == "P1's Turn")
            {
                grayout(P2, p2power, button1, button3, button4, button5, P2Status);
                turn.Text = "P2's Turn";
                reset.Stop();
                reset.Start();
                time = 15;
                P1Status = "";
            }
            else
            {
                P2Status = "";
                lockin();
            }
        }
        private void lockin()
        {
            if (P1Status == "Transform")
            {
                if (P2.PassiveActive & P2.Passives.Name == "Skip")
                {
                    battledata.Text += $"{P1.Name} was frozen! \r\n";
                }
                else
                {
                    TransformMethod(P1, P1pb, p1power, p1powerlabel);
                }
            }
            if (P2Status == "Transform")
            {
                if (P1.PassiveActive & P1.Passives.Name == "Skip")
                {
                    battledata.Text += $"{P2.Name} was frozen! \r\n";
                }
                else
                {
                    TransformMethod(P2, P2pb, p2power, p2powerlabel);
                }
            }
            if (P1Status == "Attack")
            {
                if (P2.PassiveActive & P2.Passives.Name == "Skip")
                {
                    battledata.Text += $"{P1.Name} was frozen! \r\n";
                }
                else
                {
                    AttackMethod(P1, P2, p1power, p2health, p2power, p1powerlabel, p1healthlabel, p2healthlabel, p2powerlabel, P2Status);
                }
            }
            if (P2Status == "Attack")
            {
                if (P1.PassiveActive & P1.Passives.Name == "Skip")
                {
                    battledata.Text += $"{P2.Name} was frozen! \r\n";
                }
                else
                {
                    AttackMethod(P2, P1, p2power, p1health, p1power, p2powerlabel, p1healthlabel, p1healthlabel, p1powerlabel, P1Status);
                }
            }
            if (P1Status == "Ultimate")
            {
                if (P2.PassiveActive & P2.Passives.Name == "Skip")
                {
                    battledata.Text += $"{P1.Name} was frozen! \r\n";
                }
                else
                {
                    UltimateMethod(P1, P2, p1power, p2health, p2power, p1powerlabel, p2healthlabel, p2powerlabel, P2Status, P1pb);
                }
            }
            if (P2Status == "Ultimate")
            {
                if (P1.PassiveActive & P1.Passives.Name == "Skip")
                {
                    battledata.Text += $"{P2.Name} was frozen! \r\n";
                }
                else
                {
                    UltimateMethod(P2, P1, p2power, p1health, p1power, p2powerlabel, p1healthlabel, p1powerlabel, P1Status, P2pb);
                }
            }
            if (P1Status == "Powerup")
            {
                if (P2.PassiveActive & P2.Passives.Name == "Skip")
                {
                    battledata.Text += $"{P1.Name} was frozen! \r\n";
                }
                else
                {
                    PowerupMethod(P1, p1power, p1powerlabel, p2power, p2powerlabel);
                }
            }
            if (P2Status == "Powerup")
            {
                if (P1.PassiveActive & P1.Passives.Name == "Skip")
                {
                    battledata.Text += $"{P2.Name} was frozen! \r\n";
                }
                else
                {
                    PowerupMethod(P2, p2power, p2powerlabel, p1power, p1powerlabel);
                }
            }
            turn.Text = "P1's Turn";
            time = 15;
            reset.Stop();
            reset.Start();
            roundcounter += 1;
            P1.PassiveActive = false;
            P2.PassiveActive = false;
            roundcount.Text = "Round " + roundcounter.ToString();
            grayout(P1, p1power, button1, button3, button4, button5, P1Status);
            CurrentCharacterStats(P1, p1stats);
            CurrentCharacterStats(P2, p2stats);
            battledata.SelectionStart = battledata.Text.Length;
            battledata.ScrollToCaret();
            wincheck();
        }
        private void wincheck()
        {
            if (p1health.Value == 0 | p2health.Value == 0)
            {
                if (p1health.Value == 0 & p2health.Value == 0)
                {
                    P2W++;
                    P1W++;
                    battledata.Text += "Round ended in a draw \r\n";
                    battledata.Text += "_______________________________________\r\n";
                }
                else if (p1health.Value == 0)
                {
                    P2W++;
                    battledata.Text += P2.Name + " won the battle in round " + RoundCount + "\r\n";
                    battledata.Text += "_______________________________________\r\n";
                    P5 = P2;
                    P6 = P1;
                }
                else
                {
                    P1W++;
                    battledata.Text += P1.Name + " won the battle in round " + RoundCount + "\r\n";
                    battledata.Text += "_______________________________________\r\n";
                    P5 = P1;
                    P6 = P2;
                }
                RoundCount++;
                if (P1W == 3 | P2W == 3)
                {
                    timer.Enabled = false;
                    countDown.Enabled = false;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    p1win = _context.WinPercentages.Where(x => x.Player == "P1" & x.Character == keep1).FirstOrDefault();
                    p2win = _context.WinPercentages.Where(x => x.Player == "P2" & x.Character == keep2).FirstOrDefault();
                    if (team)
                    {
                        p10win = _context.WinPercentages.Where(x => x.Player == "P1" & x.Character == P10.Name).FirstOrDefault();
                        p20win = _context.WinPercentages.Where(x => x.Player == "P2" & x.Character == P20.Name).FirstOrDefault();
                        p30win = _context.WinPercentages.Where(x => x.Player == "P1" & x.Character == P30.Name).FirstOrDefault();
                        p40win = _context.WinPercentages.Where(x => x.Player == "P2" & x.Character == P40.Name).FirstOrDefault();
                    }

                    if (P1W == 3 & P2W == 3)
                    {
                        panel2.Visible = true;
                        win.Text = "Game ended in a draw";
                        battledata.Text += "_______________________________________\r\n";
                        if (team)
                        {
                            p10win.Loses++;
                            p30win.Loses++;
                            p20win.Wins++;
                            p40win.Wins++;
                            p10win.Wins++;
                            p30win.Wins++;
                            p20win.Loses++;
                            p40win.Loses++;
                            p10win.Percentage = (Convert.ToDecimal(p10win.Wins) / (Convert.ToDecimal(p10win.Wins) + Convert.ToDecimal(p10win.Loses))) * 100;
                            p20win.Percentage = (Convert.ToDecimal(p20win.Wins) / (Convert.ToDecimal(p20win.Wins) + Convert.ToDecimal(p20win.Loses))) * 100;
                            p30win.Percentage = (Convert.ToDecimal(p30win.Wins) / (Convert.ToDecimal(p30win.Wins) + Convert.ToDecimal(p30win.Loses))) * 100;
                            p40win.Percentage = (Convert.ToDecimal(p40win.Wins) / (Convert.ToDecimal(p40win.Wins) + Convert.ToDecimal(p40win.Loses))) * 100;
                        }
                        p2win.Wins++;
                        p1win.Loses++;
                        p1win.Wins++;
                        p2win.Loses++;
                        p1win.Percentage = (Convert.ToDecimal(p1win.Wins) / (Convert.ToDecimal(p1win.Wins) + Convert.ToDecimal(p1win.Loses))) * 100;
                        p2win.Percentage = (Convert.ToDecimal(p2win.Wins) / (Convert.ToDecimal(p2win.Wins) + Convert.ToDecimal(p2win.Loses))) * 100;
                        reset.Stop();
                        countDown.Stop();
                        _context.SaveChanges();
                        return;
                    }
                    else if (P1W == 3)
                    {
                        if (team)
                        {
                            p10win.Wins++;
                            p30win.Wins++;
                            p20win.Loses++;
                            p40win.Loses++;
                            p10win.Percentage = (Convert.ToDecimal(p10win.Wins) / (Convert.ToDecimal(p10win.Wins) + Convert.ToDecimal(p10win.Loses))) * 100;
                            p20win.Percentage = (Convert.ToDecimal(p20win.Wins) / (Convert.ToDecimal(p20win.Wins) + Convert.ToDecimal(p20win.Loses))) * 100;
                            p30win.Percentage = (Convert.ToDecimal(p30win.Wins) / (Convert.ToDecimal(p30win.Wins) + Convert.ToDecimal(p30win.Loses))) * 100;
                            p40win.Percentage = (Convert.ToDecimal(p40win.Wins) / (Convert.ToDecimal(p40win.Wins) + Convert.ToDecimal(p40win.Loses))) * 100;
                        }
                        panel2.Visible = true;
                        win.Text = "GAME OVER: " + P1.Name + " Won";
                        p1win.Wins++;
                        p2win.Loses++;
                        p1win.Percentage = (Convert.ToDecimal(p1win.Wins) / (Convert.ToDecimal(p1win.Wins) + Convert.ToDecimal(p1win.Loses))) * 100;
                        p2win.Percentage = (Convert.ToDecimal(p2win.Wins) / (Convert.ToDecimal(p2win.Wins) + Convert.ToDecimal(p2win.Loses))) * 100;
                        reset.Stop();
                        countDown.Stop();
                        _context.SaveChanges();
                        return;

                    }
                    else if (P2W == 3)
                    {
                        if (team)
                        {
                            p10win.Loses++;
                            p30win.Loses++;
                            p20win.Wins++;
                            p40win.Wins++;
                            p10win.Percentage = (Convert.ToDecimal(p10win.Wins) / (Convert.ToDecimal(p10win.Wins) + Convert.ToDecimal(p10win.Loses))) * 100;
                            p20win.Percentage = (Convert.ToDecimal(p20win.Wins) / (Convert.ToDecimal(p20win.Wins) + Convert.ToDecimal(p20win.Loses))) * 100;
                            p30win.Percentage = (Convert.ToDecimal(p30win.Wins) / (Convert.ToDecimal(p30win.Wins) + Convert.ToDecimal(p30win.Loses))) * 100;
                            p40win.Percentage = (Convert.ToDecimal(p40win.Wins) / (Convert.ToDecimal(p40win.Wins) + Convert.ToDecimal(p40win.Loses))) * 100;
                        }
                        panel2.Visible = true;
                        win.Text = "GAME OVER: " + P2.Name + " won";
                        p2win.Wins++;
                        p1win.Loses++;
                        p1win.Percentage = (Convert.ToDecimal(p1win.Wins) / (Convert.ToDecimal(p1win.Wins) + Convert.ToDecimal(p1win.Loses))) * 100;
                        p2win.Percentage = (Convert.ToDecimal(p2win.Wins) / (Convert.ToDecimal(p2win.Wins) + Convert.ToDecimal(p2win.Loses))) * 100;
                        reset.Stop();
                        countDown.Stop();
                        _context.SaveChanges();
                        return;
                    }
                }
                if (P1W == 1 & p1rb2.Checked == false)
                {
                    p1rb1.Checked = true;
                }
                if (P1W == 2 & p1rb3.Checked == false)
                {
                    p1rb2.Checked = true;
                }
                if (P2W == 1 & p2rb2.Checked == false)
                {
                    p2rb1.Checked = true;
                }
                if (P2W == 2 & p2rb3.Checked == false)
                {
                    p2rb2.Checked = true;
                }
                restart(P5, P6);
            }
        }
        public void restart(Character winner, Character loser)
        {
            if (team)
            {
                if (loser == null)
                {
                    if (P2.Name == P20.Name)
                    {
                        P2 = _context.Characters.Include("Passives").Where(x => x.Name == P40.Name).FirstOrDefault();
                        P20Port.Image = Image.FromFile("G:\\DBZ\\UI\\test3.png");
                    }
                    else if (P2.Name == P40.Name)
                    {
                        P2 = _context.Characters.Include("Passives").Where(x => x.Name == P40.Name).FirstOrDefault();
                        P40Port.Image = Image.FromFile("G:\\DBZ\\UI\\test3.png");
                    }
                    else
                    {
                        P2 = _context.Characters.Include("Passives").Where(x => x.Name == P20.Name).FirstOrDefault();
                        P2Port.Image = Image.FromFile("G:\\DBZ\\UI\\test3.png");
                    }
                    if (P1.Name == P10.Name)
                    {
                        P1 = _context.Characters.Include("Passives").Where(x => x.Name == P30.Name).FirstOrDefault();
                        P10Port.Image = Image.FromFile("G:\\DBZ\\UI\\test3.png");
                    }
                    else if (P1.Name == P30.Name)
                    {
                        P1 = _context.Characters.Include("Passives").Where(x => x.Name == P30.Name).FirstOrDefault();
                        P30Port.Image = Image.FromFile("G:\\DBZ\\UI\\test3.png");
                    }
                    else
                    {
                        P1 = _context.Characters.Include("Passives").Where(x => x.Name == P10.Name).FirstOrDefault();
                        P1Port.Image = Image.FromFile("G:\\DBZ\\UI\\test3.png");
                    }

                    p2health.Value = 100;
                    p2power.Value = 70;
                    p1health.Value = 100;
                    p1power.Value = 70;
                }
                else
                {
                    if (loser.Name == P2.Name)
                    {
                        if (loser.Name == P20.Name)
                        {
                            P2 = _context.Characters.Include("Passives").Where(x => x.Name == P40.Name).FirstOrDefault();
                            P20Port.Image = Image.FromFile("G:\\DBZ\\UI\\test3.png");
                        }
                        else if (loser.Name == P40.Name)
                        {
                            P2 = _context.Characters.Include("Passives").Where(x => x.Name == P40.Name).FirstOrDefault();
                            P40Port.Image = Image.FromFile("G:\\DBZ\\UI\\test3.png");
                        }
                        else
                        {
                            P2 = _context.Characters.Include("Passives").Where(x => x.Name == P20.Name).FirstOrDefault();
                            P2Port.Image = Image.FromFile("G:\\DBZ\\UI\\test3.png");
                        }
                        p2health.Value = 100;
                        p2power.Value = 70;
                    }
                    else
                    {
                        if (loser.Name == P10.Name)
                        {
                            P1 = _context.Characters.Include("Passives").Where(x => x.Name == P30.Name).FirstOrDefault();
                            P10Port.Image = Image.FromFile("G:\\DBZ\\UI\\test3.png");
                        }
                        else if (loser.Name == P30.Name)
                        {
                            P1 = _context.Characters.Include("Passives").Where(x => x.Name == P30.Name).FirstOrDefault();
                            P30Port.Image = Image.FromFile("G:\\DBZ\\UI\\test3.png");
                        }
                        else
                        {
                            P1 = _context.Characters.Include("Passives").Where(x => x.Name == P10.Name).FirstOrDefault();
                            P1Port.Image = Image.FromFile("G:\\DBZ\\UI\\test3.png");
                        }
                        p1health.Value = 100;
                        p1power.Value = 70;
                    }
                }
            }
            else
            {
                P1 = _context.Characters.Include("Passives").Where(x => x.Name == P1.Name).FirstOrDefault();
                P2 = _context.Characters.Include("Passives").Where(x => x.Name == P2.Name).FirstOrDefault();
                p2health.Value = 100;
                p2power.Value = 70;
                p1health.Value = 100;
                p1power.Value = 70;
            }

            Arena_Load(this, null);
            battledata.SelectionStart = battledata.Text.Length;
            battledata.ScrollToCaret();
            P5 = null;
            P6 = null;
        }
        public void AttackMethod(Character player, Character opponent, ProgressBar playerpower, ProgressBar opponenthealth, ProgressBar opponentpower, Label playerpwlb, Label HealthLabel, Label ophplb, Label oppwlb, string status)
        {
            if (status == "Block")
            {
                if (opponent.Passives.Name == "HalfDamage" & opponent.PassiveActive == true)
                {
                    if (player.Passives.Name == "DoubleDamage" & opponent.PassiveActive == true)
                    {
                        if (opponenthealth.Value < player.AttackDamage / 2)
                        {
                            opponenthealth.Value = player.AttackDamage / 2;
                        }
                        opponenthealth.Value -= player.AttackDamage / 2;

                        if (opponentpower.Value > 90)
                        {
                            opponentpower.Value = 90;
                        }
                        opponentpower.Value += 10;
                        battledata.Text += $"{opponent.Name} blocked with half damage passive against {player.Name}'s double damage attack and took {player.AttackDamage / 2} damage \r\n";
                    }
                    else
                    {
                        if (opponenthealth.Value < player.AttackDamage / 4)
                        {
                            opponenthealth.Value = player.AttackDamage / 4;
                        }
                        opponenthealth.Value -= player.AttackDamage / 4;

                        if (opponentpower.Value > 90)
                        {
                            opponentpower.Value = 90;
                        }
                        opponentpower.Value += 10;
                        battledata.Text += $"{opponent.Name} blocked with half damage passive against {player.Name}'s Attack and took {player.AttackDamage / 4} damage \r\n";
                    }
                }
                else if (opponent.Passives.Name == "Dodge" & opponent.PassiveActive == true)
                {
                    battledata.Text += $"{opponent.Name} dodged {player.Name}'s Attack \r\n";
                }
                else if (opponent.Passives.Name == "Absorb" & opponent.PassiveActive == true)
                {
                    if (opponent.Form >= 2)
                    {
                        if (opponenthealth.Value > 100 - 10)
                        {
                            opponenthealth.Value = 100 - 10;
                        }
                        opponenthealth.Value = opponenthealth.Value + 10;
                        HealthLabel.Text = opponenthealth.Value.ToString();
                        battledata.Text += opponent.Name + " has absorbed " + player.Name + "'s attack and gained " + 10 + " health \r\n";
                    }
                    else
                    {
                        if (opponenthealth.Value > 100 - 5)
                        {
                            opponenthealth.Value = 100 - 5;
                        }
                        opponenthealth.Value = opponenthealth.Value + 5;
                        HealthLabel.Text = opponenthealth.Value.ToString();
                        battledata.Text += opponent.Name + " has absorbed " + player.Name + "'s attack and gained " + 5 + " health \r\n";
                    }
                }
                else if (player.Passives.Name == "DoubleDamage" & player.PassiveActive == true)
                {
                    if (opponenthealth.Value < player.AttackDamage)
                    {
                        opponenthealth.Value = player.AttackDamage;
                    }
                    opponenthealth.Value -= player.AttackDamage;

                    if (opponentpower.Value > 90)
                    {
                        opponentpower.Value = 90;
                    }
                    opponentpower.Value += 10;
                    battledata.Text += $"{opponent.Name} blocked {player.Name}'s Attack and took {player.AttackDamage} damage \r\n";
                }
                else
                {
                    if (opponenthealth.Value < player.AttackDamage / 2)
                    {
                        opponenthealth.Value = player.AttackDamage / 2;
                    }
                    opponenthealth.Value -= player.AttackDamage / 2;
                    if (opponentpower.Value > 90)
                    {
                        opponentpower.Value = 90;
                    }
                    opponentpower.Value += 10;
                    battledata.Text += $"{opponent.Name} blocked {player.Name}'s Attack and took {player.AttackDamage / 2} damage \r\n";
                }
            }
            else
            {
                if (opponent.Passives.Name == "HalfDamage" & opponent.PassiveActive == true)
                {
                    if (player.Passives.Name == "DoubleDamage" & opponent.PassiveActive == true)
                    {
                        if (opponenthealth.Value < player.AttackDamage)
                        {
                            opponenthealth.Value = player.AttackDamage;
                        }
                        opponenthealth.Value -= player.AttackDamage;

                        if (opponentpower.Value > 90)
                        {
                            opponentpower.Value = 90;
                        }
                        opponentpower.Value += 10;
                        battledata.Text += $"{opponent.Name} blocked with half damage passive against {player.Name}'s attack and took {player.AttackDamage} damage \r\n";
                    }
                    else
                    {
                        if (opponenthealth.Value < player.AttackDamage / 2)
                        {
                            opponenthealth.Value = player.AttackDamage / 2;
                        }
                        opponenthealth.Value -= player.AttackDamage / 2;

                        if (opponentpower.Value > 90)
                        {
                            opponentpower.Value = 90;
                        }
                        opponentpower.Value += 10;
                        battledata.Text += $"{opponent.Name} blocked with half damage passive against {player.Name}'s Attack and took {player.AttackDamage / 2} damage \r\n";
                    }
                }
                else if (opponent.Passives.Name == "Dodge" & opponent.PassiveActive == true)
                {
                    battledata.Text += $"{opponent.Name} dodged {player.Name}'s Attack \r\n";
                }
                else if (opponent.Passives.Name == "Absorb" & opponent.PassiveActive == true)
                {
                    if (opponent.Form == 2)
                    {
                        if (opponenthealth.Value > 100 - 10)
                        {
                            opponenthealth.Value = 100 - 10;
                        }
                        opponenthealth.Value = opponenthealth.Value + 10;
                        HealthLabel.Text = opponenthealth.Value.ToString();
                        battledata.Text += opponent.Name + " has absorbed " + player.Name + "'s attack and gained " + 10 + " health \r\n";
                    }
                    else
                    {
                        if (opponenthealth.Value > 100 - 5)
                        {
                            opponenthealth.Value = 100 - 5;
                        }
                        opponenthealth.Value = opponenthealth.Value + 5;
                        HealthLabel.Text = opponenthealth.Value.ToString();
                        battledata.Text += opponent.Name + " has absorbed " + player.Name + "'s attack and gained " + 5 + " health \r\n";
                    }
                }
                else if (player.Passives.Name == "DoubleDamage" & player.PassiveActive == true)
                {
                    if (opponenthealth.Value < player.AttackDamage * 2)
                    {
                        opponenthealth.Value = player.AttackDamage * 2;
                    }
                    opponenthealth.Value -= player.AttackDamage * 2;

                    if (opponentpower.Value > 90)
                    {
                        opponentpower.Value = 90;
                    }
                    opponentpower.Value += 10;
                    battledata.Text += $"{opponent.Name} blocked {player.Name}'s Attack and took {player.AttackDamage * 2} damage \r\n";
                }
                else
                {
                    if (opponenthealth.Value < player.AttackDamage)
                    {
                        opponenthealth.Value = player.AttackDamage;
                    }
                    opponenthealth.Value -= player.AttackDamage;
                    battledata.Text += $"{player.Name} has successfully attacked {opponent.Name} dealing {player.AttackDamage} damage \r\n";
                }
            }
            playerpower.Value -= player.PowerCost;
            ophplb.Text = "Health: " + opponenthealth.Value.ToString();
            oppwlb.Text = "POWER: " + opponentpower.Value.ToString();
            playerpwlb.Text = "POWER: " + playerpower.Value.ToString();
        }
        public void PowerupMethod(Character player, ProgressBar playerpower, Label playerpowerlabel, ProgressBar opponentpower, Label opponentpowerlabel)
        {
            if (player.Passives.Name == "TriplePowerUp")
            {
                playerpower.Value = 100;
            }
            else if (player.Passives.Name == "Steal")
            {
                if (playerpower.Value > 50)
                {
                    playerpower.Value = 50;
                }
                playerpower.Value += 50;

                if (opponentpower.Value < 10)
                {
                    opponentpower.Value = 10;
                }
                opponentpower.Value -= 10;
            }
            else
            {
                if (playerpower.Value > 70)
                {
                    playerpower.Value = 70;
                }
                playerpower.Value += 30;
            }
            playerpowerlabel.Text = "POWER: " + playerpower.Value.ToString();
            battledata.Text += player.Name + " has powered up to " + playerpower.Value + "% \r\n";
        }
        private void TransformMethod(Character CurrentPlayer, PictureBox NewPicture, ProgressBar PowerBar, Label PowerLabel)
        {
            PowerBar.Value = PowerBar.Value - CurrentPlayer.UpgradeCost;
            if (CurrentPlayer == P1)
            {
                P1 = _context.Characters.Include("Passives").Where(x => x.Name == P1.Name & x.Form == P1.Form + 1).FirstOrDefault();
                NewPicture.Image = Image.FromFile(P1.PortraitLeft);
                CurrentPlayer = P1;
            }
            if (CurrentPlayer == P2)
            {
                P2 = _context.Characters.Include("Passives").Where(x => x.Name == P2.Name & x.Form == P2.Form + 1).FirstOrDefault();
                NewPicture.Image = Image.FromFile(P2.PortraitRight);
                CurrentPlayer = P2;
            }
            PowerLabel.Text = $"POWER: {PowerBar.Value.ToString()}";
            if (CurrentPlayer.Form == 2)
            {
                battledata.Text += $"{CurrentPlayer.Name} transformed into thier second form\r\n";
            }
            else if (CurrentPlayer.Form == 3)
            {
                battledata.Text += $"{CurrentPlayer.Name} transformed into thier third form\r\n";
            }
            else if (CurrentPlayer.Form == 4)
            {
                battledata.Text += $"{CurrentPlayer.Name} transformed into thier fourth form\r\n";
            }
            else
            {
                battledata.Text += $"{CurrentPlayer.Name} transformed into thier fifth form\r\n";
            }
        }
        private Character UltimateMethod(Character player, Character enemy, ProgressBar playerpower, ProgressBar enemyhealth, ProgressBar enemypower, Label playerpowerlabel, Label enemyhealthlabel, Label enemypowerlabel, string enemystatus, PictureBox playerpb)
        {
            if (player.Name == "Vegito" | player.Name == "Whis")
            {
                if (enemyhealth.Value < player.UltDamage)
                {
                    enemyhealth.Value = player.UltDamage;
                }
                enemyhealth.Value -= player.UltDamage;
                if (enemystatus == "Block")
                {
                    battledata.Text += player.Name + " ult was unblockable dealing " + player.UltDamage + " damage \r\n";
                }
                else
                {
                    battledata.Text += player.Name + " has successfully ulted " + enemy.Name + " dealing " + player.UltDamage + " damage \r\n";
                }
            }
            else if (enemystatus == "Block")
            {
                if (enemyhealth.Value < player.UltDamage / 2)
                {
                    enemyhealth.Value = player.UltDamage / 2;
                }
                enemyhealth.Value -= player.UltDamage / 2;
                if (enemypower.Value > 90)
                {
                    enemypower.Value = 90;
                }
                enemypower.Value += 10;
                battledata.Text += enemy.Name + " has blocked " + player.Name + "'s ultimate and took " + player.UltDamage / 2 + " damage \r\n";
            }
            else
            {
                if (enemyhealth.Value < player.UltDamage)
                {
                    enemyhealth.Value = player.UltDamage;
                }
                enemyhealth.Value -= player.UltDamage;
                battledata.Text += player.Name + " has successfully ulted " + enemy.Name + " dealing " + player.UltDamage + " damage \r\n";
            }
            enemyhealthlabel.Text = "Health: " + enemyhealth.Value.ToString();
            enemypowerlabel.Text = "POWER: " + enemypower.Value.ToString();
            playerpower.Value -= player.UltCost;
            playerpowerlabel.Text = "POWER: " + playerpower.Value.ToString();
            if (player.Name == "Sasuke" & player.Form == 2)
            {
                player = _context.Characters.Include("Passives").Where(x => x.Name == player.Name & x.Form == 3).FirstOrDefault();
                playerpb.Image = Image.FromFile(player.PortraitLeft);
                return player;
            }
            return null;
        }
        private void countDown_Tick(object sender, EventArgs e)
        {
            time -= 0.1;
            string time2 = time.ToString("0.00");
            timer.Text = time2;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (turn.Text == "P1's Turn")
            {
                turn.Text = "P2's Turn";
                grayout(P2, p2power, button1, button3, button4, button5, P2Status);
                time = 15;
                reset.Stop();
                reset.Start();
                PassiveMethod(P1, p1power, p1health, p1powerlabel, p1healthlabel);
            }
            else
            {
                PassiveMethod(P2, p2power, p2health, p2powerlabel, p2healthlabel);
                lockin();
            }
        }
        public void grayout(Character player, ProgressBar playerpower, Button attack, Button powerup, Button transform, Button ultimate, string status)
        {
            if (player.CanUlt == true & playerpower.Value >= player.UltCost)
            {
                ultimate.Enabled = true;
            }
            else
            {
                ultimate.Enabled = false;
            }
            if (playerpower.Value >= player.PowerCost)
            {
                attack.Enabled = true;
            }
            else
            {
                attack.Enabled = false;
            }
            if (player.Upgradable == true & playerpower.Value >= player.UpgradeCost)
            {
                transform.Enabled = true;
            }
            else
            {
                transform.Enabled = false;
            }
            if (playerpower.Value <= 99)
            {
                powerup.Enabled = true;
            }
            else
            {
                powerup.Enabled = false;
            }
            button2.Enabled = true;
            button6.Enabled = false;

            status = "";
        }
        private void enabler(Button enable1, Button enable2, Button disable1, Button disable2, Button disable3, Button disable4, Button disable5)
        {
            disable1.Enabled = false;
            disable2.Enabled = false;
            disable3.Enabled = false;
            disable4.Enabled = false;
            disable5.Enabled = false;
            enable1.Enabled = true;
            enable2.Enabled = true;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            P1 = _context.Characters.Include("Passives").Where(x => x.Name == keep1).FirstOrDefault();
            P2 = _context.Characters.Include("Passives").Where(x => x.Name == keep2).FirstOrDefault();
            Arena arena = new Arena(P1, P2, Settings);
            if (team)
            {
                arena.P10 = P10;
                arena.P20 = P20;
                arena.P30 = P30;
                arena.P40 = P40;
                arena.p1win = p1win;
                arena.p2win = p2win;
                arena.team = true;
            }
            arena.ShowDialog();
            this.Close();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            CharacterSelect cs = new CharacterSelect();
            this.Hide();
            cs.ShowDialog();
            this.Close();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            Form1 cs = new Form1();
            this.Hide();
            cs.ShowDialog();
            this.Close();
        }
        public void PassiveMethod(Character CurrentPlayer, ProgressBar CurrentPlayerPowerBar, ProgressBar CurrentPlayerHealthBar, Label CurrentPlayerPowerLabel, Label CurrentPlayerHealthLabel)
        {
            Random rnd = new Random();
            int random = rnd.Next(1, 100);
            if (random <= CurrentPlayer.PassiveChance)
            {
                CurrentPlayer.PassiveActive = true;
            }
            if (CurrentPlayer.Name == "Goku" & random <= CurrentPlayer.PassiveChance)
            {
                if (CurrentPlayerPowerBar.Value > 80)
                {
                    CurrentPlayerPowerBar.Value = 80;
                }
                battledata.Text += CurrentPlayer.Name + " gained 20 energy from his passive \r\n";
                CurrentPlayerPowerBar.Value += 20;
                CurrentPlayerPowerLabel.Text = "POWER:" + CurrentPlayerPowerBar.Value.ToString();
            }
            if (CurrentPlayer.Name == "Vegeta" & random <= CurrentPlayer.PassiveChance | CurrentPlayer.Name == "Vegito" & random <= CurrentPlayer.PassiveChance)
            {
                if (CurrentPlayerHealthBar.Value > 75)
                {
                    CurrentPlayerHealthBar.Value = 75;
                }
                if (CurrentPlayer.Form > 3)
                {
                    if (CurrentPlayerHealthBar.Value <= 40)
                    {
                        CurrentPlayer.PassiveChance = 40;
                    }
                }
                battledata.Text += CurrentPlayer.Name + " gained 25 health from his passive \r\n";
                CurrentPlayerHealthBar.Value += 25;
                CurrentPlayerHealthLabel.Text = "HEALTH:" + CurrentPlayerHealthBar.Value.ToString();
            }
            if (CurrentPlayer.Name == "Gohan" & CurrentPlayerHealthBar.Value < 30 & CurrentPlayer.Form >= 3)
            {
                CurrentPlayer.AttackDamage = 45;
                if (CurrentPlayerPowerBar.Value >= 85)
                {
                    CurrentPlayerPowerBar.Value = 85;
                }
                CurrentPlayerPowerBar.Value += 15;
                CurrentPlayerPowerLabel.Text = "Power:" + CurrentPlayerPowerBar.Value.ToString();
                battledata.Text += "Gohan gained 10 energy from his passive and entered rage \r\n";
            }
            if (CurrentPlayer.Name == "Jiren" & CurrentPlayerHealthBar.Value <= 35 & CurrentPlayer.Form >= 2 | CurrentPlayer.Name == "Toppo" & CurrentPlayerHealthBar.Value <= 35 & CurrentPlayer.Form >= 2)
            {
                CurrentPlayer.PassiveChance = 75;
                CurrentPlayer.UltDamage = 80;
            }
        }
    }
}