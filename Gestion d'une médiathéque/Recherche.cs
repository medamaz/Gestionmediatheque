using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_d_une_médiathéque
{
    public partial class Recherche : Form
    {
        Form parent;
        DataTable dt;
        BindingSource bs = new BindingSource();
        Support s;
        public Recherche(Form parent, DataTable dt )
        {
            InitializeComponent();
            this.parent = parent;
            this.dt = dt;
        }

        private void Recherche_Load(object sender, EventArgs e)
        {
            parent.Visible = false;
            s = new Support();
            numericUpDown1.Maximum = DateTime.Now.Year;
            bs.DataSource = this.dt;
            dataGridView1.DataSource = bs;
        }

        private void Recherche_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.Visible = true;
        }

        void filterApply(int[] check)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bs.DataSource = Support.rechercherOeuvres(textBox1.Text, textBox2.Text, textBox3.Text, (int)numericUpDown1.Value);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            numericUpDown1.Value = 1900;
            bs.DataSource = this.dt;
        }
    }
}
