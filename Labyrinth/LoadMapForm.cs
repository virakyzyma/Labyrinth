using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labyrinth
{
    public partial class LoadMapForm : Form
    {
        private int[,] array2;

        public LoadMapForm()
        {
            (Height, Width) = (400, 400);
            (FormBorderStyle, MaximizeBox) = (FormBorderStyle.FixedDialog, false);
            AddControls();
            InitializeComponent();
        }

        private void AddControls()
        {
            Label label = new Label
            {
                Text = "Choose a map",
                Font = new Font("Vernada", 12),
                AutoSize = true
            };
            Controls.Add(label);
            CenterControl(label);

            Button loadMap = new Button()
            {
                Text = "Browse",
                Font = new Font("Vernada", 12),
                AutoSize = true
            };
            Controls.Add(loadMap);
            CenterControl(loadMap, label);

            Label labelInfo = new Label()
            {
                Font = new Font("Vernada", 12),
                AutoSize = true
            };
            Controls.Add(labelInfo);
            CenterControl(labelInfo, loadMap);

            Button startButton = new Button()
            {
                Enabled = false,
                AutoSize = true,
                Font = new Font("Vernada", 12),
                Text = "Start game"
            };
            Controls.Add(startButton);
            CenterControl(startButton, labelInfo, 5);

            loadMap.Click += (sender, e) =>
            {
                OpenFileDialog dialog = new OpenFileDialog()
                {
                    Filter = "txt files (*.txt)|*txt"
                };
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(dialog.FileName);
                        array2 = new int[lines.Length, lines[0].Split(',').Length];
                        for (int i = 0; i < lines.Length; i++)
                        {
                            string[] values = lines[i].Split(',');
                            for (int j = 0; j < values.Length; j++)
                            {
                                array2[i, j] = int.Parse(values[j].Trim());
                            }
                        }
                        labelInfo.Text = $"Map name: {Path.GetFileName(dialog.FileName)}";
                        CenterControl(labelInfo, loadMap);
                        startButton.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }                    
                }
            };
            startButton.Click += (sender, e) =>
            {
                GameForm gameForm = new GameForm(array2);
                gameForm.ShowDialog();
            };
            
        }
       private void CenterControl(Control? currentControl, Control? lastControl=null, int koeff = 1)
        {
            int centerX = (this.ClientSize.Width - currentControl.Width) / 2;
            int centerY = lastControl != null ? lastControl.Bottom : currentControl.Height;

            if(lastControl != null )
            {
                currentControl.Location = new Point(centerX, centerY + lastControl.Height + koeff);
            }
            else
            {
                currentControl.Location = new Point(centerX, centerY);
            }
        }
        
    }
}
