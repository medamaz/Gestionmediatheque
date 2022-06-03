using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_d_une_médiathéque
{
    public class Prets
    {
        DateTime dtepr;
        Adherent ad;
        Oeuvre o;

        public Prets()
        {
        }

        public Prets(DateTime dtepr, Adherent ad,Oeuvre o)
        {
            this.Dtepr = dtepr;
            this.Ad = ad;
            this.O = o;
        }
        public DateTime Dtepr { get => dtepr; set => dtepr = value; }
        public Adherent Ad { get => ad; set => ad = value; }
        public Oeuvre O { get => o; set => o = value; }
    }
}
