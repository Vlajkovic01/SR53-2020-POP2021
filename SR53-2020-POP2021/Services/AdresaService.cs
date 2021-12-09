using SR53_2020_POP2021.Izuzeci;
using SR53_2020_POP2021.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR53_2020_POP2021.Services
{
    public class AdresaService : IEntitet
    {
        public void IzbrisiEntitet(string id)
        {
            int.TryParse(id, out int sifraAdrese);
            Adresa adresa = Util.Instance.Adrese.ToList().Find(t => t.ID == sifraAdrese);
            if (adresa == null)
            {
                throw new UserNotFoundException($"Ne postoji Adresa sa ID: {id}");
            }
            using (SqlConnection connection = new SqlConnection(Util.CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"update adrese set Aktivna = 0  where ID = @ID";

                command.Parameters.Add(new SqlParameter("ID", adresa.ID));

                command.ExecuteNonQuery();
            }
        }
        public void UcitajEntitet(string filename)
        {
            Util.Instance.Adrese = new ObservableCollection<Adresa>();

            using (SqlConnection connection = new SqlConnection(Util.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"select * from adrese";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Adresa adresa = new Adresa
                    {

                        ID = reader.GetInt32(0),
                        Ulica = reader.GetString(1),
                        Broj = reader.GetString(2),
                        Grad = reader.GetString(3),
                        Drzava = reader.GetString(4),
                        Aktivna = reader.GetBoolean(5)
                    };

                    Util.Instance.Adrese.Add(adresa);
                }
            }
        }
        public void SacuvajEntitet(Object obj)
        {
            Adresa adresa = obj as Adresa;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into adrese (ID, Ulica, Broj, Grad, Drzava, Aktivna)
                                        VALUES (@ID, @Ulica, @Broj, @Grad, @Drzava, @Aktivna)";
                command.Parameters.Add(new SqlParameter("ID", adresa.ID));
                command.Parameters.Add(new SqlParameter("Ulica", adresa.Ulica));
                command.Parameters.Add(new SqlParameter("Broj", adresa.Broj));
                command.Parameters.Add(new SqlParameter("Grad", adresa.Grad));
                command.Parameters.Add(new SqlParameter("Drzava", adresa.Drzava));
                command.Parameters.Add(new SqlParameter("Aktivna", adresa.Aktivna));

                command.ExecuteNonQuery();
            }
        }

        public void IzmeniEntitet(Object obj)
        {
            Adresa adresa = obj as Adresa;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update adrese set ID = @ID, Ulica = @Ulica, Broj = @Broj, Grad = @Grad, Drzava = @Drzava, Aktivna = @Aktivna where ID = @ID";

                command.Parameters.Add(new SqlParameter("ID", adresa.ID));
                command.Parameters.Add(new SqlParameter("Ulica", adresa.Ulica));
                command.Parameters.Add(new SqlParameter("Broj", adresa.Broj));
                command.Parameters.Add(new SqlParameter("Grad", adresa.Grad));
                command.Parameters.Add(new SqlParameter("Drzava", adresa.Drzava));
                command.Parameters.Add(new SqlParameter("Aktivna", adresa.Aktivna));

                command.ExecuteNonQuery();
            }
        }
    }
}
