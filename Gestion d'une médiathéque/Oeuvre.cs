using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_d_une_médiathéque
{
    public class Oeuvre
    {
        string titre;
        string auteur;
        string editeur;
        int year;
        public Oeuvre() { }
        public Oeuvre(string titre, string auteur, string editeur, int year)
        {
            this.Titre = titre ?? throw new ArgumentNullException(nameof(titre));
            this.Auteur = auteur ?? throw new ArgumentNullException(nameof(auteur));
            this.Editeur = editeur ?? throw new ArgumentNullException(nameof(editeur));
            this.Year = year;
        }
        public Oeuvre(Oeuvre o)
        {
            this.Titre = o.Titre;
            this.Auteur = o.Auteur; ;
            this.Editeur = o.Editeur;
            this.Year = o.Year;
        }
        public string Titre { get => titre; set => titre = value; }
        public string Auteur { get => auteur; set => auteur = value; }
        public string Editeur { get => editeur; set => editeur = value; }
        public int Year { get => year; set => year = value; }

        public override string ToString()
        {
            return "Titre :" + Titre + "\nAuteur :" + Auteur + "\nEditeur :" + editeur + "\nAnnee de sorie :" + Year;
        }
    }
}
