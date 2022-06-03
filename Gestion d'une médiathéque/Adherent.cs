using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_d_une_médiathéque
{
    public class Adherent
    {

        

        int num;
        string nom;
        string prenom;
        string adress;
        string email;

        public Adherent()
        {
        }

        public Adherent(string nom, string prenom, string adress, string email)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Adress = adress;
            this.Email = email;
        }
        public Adherent(Adherent ad)
        {
            this.num = ad.Num;
            this.Nom = ad.Nom;
            this.Prenom = ad.prenom;
            this.Adress = ad.Adress;
            this.Email = ad.Email;
        }

        public int Num { get => num; set => num = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Adress { get => adress; set => adress = value; }
        public string Email { get => email; set => email = value; }

        public static DataTable afficherAdherent()
        {
            string cinstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moami\source\repos\DB\GestionMediatheque.mdf;Integrated Security = True";

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                adapter = new SqlDataAdapter("select * from Adherents", cinstr)
                {
                    MissingSchemaAction = MissingSchemaAction.AddWithKey
                };
                adapter.Fill(dt);
                SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        public static void ajouterAdherent(params Adherent[] ad)
        {
            string cinstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moami\source\repos\DB\GestionMediatheque.mdf;Integrated Security = True";
            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            foreach (Adherent a in ad)
            {
                try
                {

                    cn.ConnectionString = cinstr;
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "INSERT INTO  Adherents (nom,prenom,adresse,email) VALUES (@nom,@prenom,@adresse,@email)";
                    cmd.Parameters.AddWithValue("@nom", a.Nom);
                    cmd.Parameters.AddWithValue("@prenom", a.Prenom);
                    cmd.Parameters.AddWithValue("@adresse", a.Adress);
                    cmd.Parameters.AddWithValue("@email", a.Email);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                catch
                {
                    return;
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public static void supprimerAdherent(int id)
        {
            string cinstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moami\source\repos\DB\GestionMediatheque.mdf;Integrated Security = True";
            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cn.ConnectionString = cinstr;
                cn.Open();
                cmd.Connection = cn;

                cmd.CommandText = "DELETE FROM Adherents WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch
            {
                return;
            }
            finally
            {
                cn.Close();
            }
        }

        public static void modifierAdherent(int id,Adherent a)
        {
            string cinstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moami\source\repos\DB\GestionMediatheque.mdf;Integrated Security = True";
            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();


            try
            {
                cn.ConnectionString = cinstr;
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText = "UPDATE Adherents SET nom = @nom, prenom = @prenom, adresse =@adresse, email = @email where id=@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nom", a.Nom);
                cmd.Parameters.AddWithValue("@prenom", a.Prenom);
                cmd.Parameters.AddWithValue("@adresse", a.Adress);
                cmd.Parameters.AddWithValue("@email", a.Email);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch
            {
                return;
            }
            finally
            {
                cn.Close();
            }
        }
            
        public static Adherent rechercherAdherent(int id)
        {
            string cinstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moami\source\repos\DB\GestionMediatheque.mdf;Integrated Security = True";

            try
            {
                Adherent ad = new Adherent();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string rq = "select * from Adherents where";
                rq = String.Format("{0} id = {1}", rq, id);
                adapter = new SqlDataAdapter(rq, cinstr)
                {
                    MissingSchemaAction = MissingSchemaAction.AddWithKey
                };
                adapter.Fill(dt);
                ad.Num = Convert.ToInt32(dt.Rows[0]["Id"]);
                ad.Nom = dt.Rows[0]["nom"].ToString();
                ad.Prenom = dt.Rows[0]["prenom"].ToString();
                ad.Adress = dt.Rows[0]["adresse"].ToString();
                ad.Email = dt.Rows[0]["email"].ToString();

                SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
                return ad;
            }
            catch
            {
                return null;
            }
        }

        public override string ToString()
        {
            return "Numero :"+num +"\nNom :"+nom+"\nPrenom :"+prenom+"\nAdress :"+adress+"\nEmail :"+email;
        }
    }
}
