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
    public class CentarService : IEntitet
    {
        public void IzbrisiEntitet(string id)
        {
            int.TryParse(id, out int sifraCentra);
            Centar centar = Util.Instance.Centri.ToList().Find(c => c.ID.Equals(sifraCentra));
            if (centar == null)
            {
                throw new UserNotFoundException($"Ne postoji centar sa ID: {id}");
            }
            using (SqlConnection connection = new SqlConnection(Util.CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"update centri set Aktivan = 0 where ID = @ID";

                command.Parameters.Add(new SqlParameter("ID", centar.ID));

                command.ExecuteNonQuery();
            }
        }
        public void UcitajEntitet(string filename)
        {
            Util.Instance.Centri = new ObservableCollection<Centar>();

            using (SqlConnection connection = new SqlConnection(Util.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"select * from centri";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Centar centar = new Centar
                    {

                        ID = reader.GetInt32(0),
                        NazivCentra = reader.GetString(1),
                        AdresaCentra = Util.Instance.PronadjiAdresu(reader.GetInt32(2)),
                        Aktivan = reader.GetBoolean(3)
                    };

                    Util.Instance.Centri.Add(centar);
                }
            }
        }
        public void SacuvajEntitet(Object obj)
        {
            Centar centar = obj as Centar;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into centri (ID, Naziv, Adresa_ID, Aktivan)
                                        VALUES (@ID, @Naziv, @Adresa_ID, @Aktivan)";
                command.Parameters.Add(new SqlParameter("ID", centar.ID));
                command.Parameters.Add(new SqlParameter("Naziv", centar.NazivCentra));
                command.Parameters.Add(new SqlParameter("Adresa_ID", centar.AdresaCentra.ID));
                command.Parameters.Add(new SqlParameter("Aktivan", centar.Aktivan));

                command.ExecuteNonQuery();
            }
        }

        public void IzmeniEntitet(Object obj)
        {
            Centar centar = obj as Centar;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update centri set ID = @ID, Naziv = @Naziv, Adresa_ID = @Adresa_ID, Aktivan = @Aktivan where ID = @ID";

                command.Parameters.Add(new SqlParameter("ID", centar.ID));
                command.Parameters.Add(new SqlParameter("Naziv", centar.NazivCentra));
                command.Parameters.Add(new SqlParameter("Adresa_ID", centar.AdresaCentra.ID));
                command.Parameters.Add(new SqlParameter("Aktivan", centar.Aktivan));

                command.ExecuteNonQuery();
            }
        }
    }
}
