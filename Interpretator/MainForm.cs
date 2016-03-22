using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interpretator
{
    public partial class Form_interpretator : Form
    {
        MyInterpretator interpretator = null;
        string Code;
        public Form_interpretator()
        {
            InitializeComponent();
        }
        private void Run_button_Click(object sender, EventArgs e)
        {
            Result.Text = "";
            Errors.Text = "";
            dataGrid.Rows.Clear();
            Info.Clear();
            if (interpretator != null) interpretator = null;
            Code = Code_info.Text;
            interpretator = new MyInterpretator(Result,Errors, dataGrid,Info);
            interpretator.Execute(ref Code,null);
        }

        public class MyInterpretator : IInterpretator // наслідування від інтерфейсу
        {
            private RichTextBox output;
            private RichTextBox err;
            private RichTextBox info;
            private DataGridView grid;
            Tokens token_class;
            public MyInterpretator(RichTextBox outputBox, RichTextBox errors,DataGridView grid, RichTextBox INFO) // конструктор
            {
                this.output = outputBox;
                this.err = errors;
                this.grid = grid;
                this.info = INFO;
                token_class = new Tokens(output, err, grid, info);//об'єкт класу токенів
            }
            public bool Execute(ref string Code, object[] args)
            {
                bool res = token_class.ParseTokens(Code, true,false,false);//розбиття на токени та виконання
                return res;
            }
        }

        internal interface IInterpretator
        {
            bool Execute(ref string Code, object[] args);
        }

        private int index = 0;
        private int blockStart;
        private int blockLength;
        private void Form_interpretator_Load(object sender, EventArgs e)
        {
            //Container2.Panel2Collapsed = true;
        }
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var HelpForm = new Help();
            HelpForm.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
    {
                try
                {
					
                    Code_info.Text = Encoding.Default.GetString(File.ReadAllBytes(openFileDialog1.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk.");
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
            StreamWriter sw = new StreamWriter(saveFileDialog.FileName);
            sw.Write(Code_info.Text);
            sw.Close();
            }
        }

        private void Code_panel_Click(object sender, EventArgs e)
        {
            if (Code_panel.Checked == true)
            {
                splitContainer1.Panel1Collapsed = false;
                Container2.Panel1Collapsed = false;
            }  
            else
            if (Detailed_panel.Checked == false)
            {
                Container2.Panel2Collapsed = true;
                splitContainer1.Panel1Collapsed = true;
            }
            else Container2.Panel1Collapsed = true;
        }

        private void Detailed_panel_Click(object sender, EventArgs e)
        {
            if(Detailed_panel.Checked==true)
            {
                Container2.Panel2Collapsed = false;
                splitContainer1.Panel1Collapsed = false;
            }
            else
                if(Code_panel.Checked==false)
                {
                splitContainer1.Panel1Collapsed = true;
                Container2.Panel2Collapsed = true;
               }
            else
                Container2.Panel2Collapsed = true;
        }

        private void Variables_panel_Click(object sender, EventArgs e)
        {
            if (Variables_panel.Checked == true)
                splitContainer1.Panel2Collapsed = false;
            else splitContainer1.Panel2Collapsed = true;
        }

        private void Info_TextChanged(object sender, EventArgs e)
        {
            Info.Select(Info.Text.Length, 0);
            Info.ScrollToCaret();
        }

        private void Result_TextChanged(object sender, EventArgs e)
        {
            Result.Select(Result.Text.Length, 0);
            Result.ScrollToCaret();
        }

        private void Errors_TextChanged(object sender, EventArgs e)
        {
            Errors.Select(Errors.Text.Length , 0);
            Errors.ScrollToCaret();
        }

		private void fileToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void saveAsDocumentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				StreamWriter sw = new StreamWriter(saveFileDialog.FileName);
				sw.Write(Info.Text);
				sw.Close();
			}
		}

		private void saveInDocumentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				StreamWriter sw = new StreamWriter(saveFileDialog.FileName);
				sw.Write(Result.Text);
				sw.Close();
			}
		}
	}
}

