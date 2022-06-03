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
    public partial class Accueil : Form
    {
        public Accueil()
        {
            InitializeComponent();
        }
        private async Task loadInformation()
        {
            Support s =new Support();
            string type = "SupportCD";
            Oeuvre oeuvre;
            Adherent ad;
            for (int i = 0; i < 40; i++)
            {
                if(i >=20)
                {
                    type = "SupportLivre";
                }
                oeuvre = null;
                ad = null;
                oeuvre = new Oeuvre(Faker.Name.FullName(), Faker.Name.FullName(), Faker.Name.FullName(), Faker.RandomNumber.Next(1980, 2022));
                ad = new Adherent(Faker.Name.Last(), Faker.Name.First(), Faker.Address.StreetAddress(), Faker.Internet.Email());
                s.ajouterOeuvres(type, oeuvre);
                Adherent.ajouterAdherent(ad);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new GsOeuvres(this).ShowDialog();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await Task.Run(() => loadInformation());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new GsAdherent(this).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new GsPrets(this).ShowDialog();
        }
    }
}
