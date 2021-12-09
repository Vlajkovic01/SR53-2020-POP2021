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
    public class TreningService : IEntitet
    {
        public void IzbrisiEntitet(string id)
        {
            int.TryParse(id, out int sifraTreninga);
            Trening trening = Util.Instance.Treninzi.ToList().Find(t => t.ID == sifraTreninga);
            if (trening == null)
            {
                throw new UserNotFoundException($"Ne postoji trening sa ID: {id}");
            }
            using (SqlConnection connection = new SqlConnection(Util.CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"update treninzi set Aktivan = 0 where ID = @ID";

                command.Parameters.Add(new SqlParameter("ID", trening.ID));

                command.ExecuteNonQuery();
            }
        }
        public void UcitajEntitet(string filename)
        {
            Util.Instance.Treninzi = new ObservableCollection<Trening>();

            using (SqlConnection connection = new SqlConnection(Util.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"select * from treninzi";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Enum.TryParse(reader.GetString(4), out EStatusTreninga statusTreninga);
                    Trening trening = new Trening
                    {

                        ID = reader.GetInt32(0),
                        DatumTreninga = reader.GetString(1),
                        VremePocetkaTreninga = reader.GetString(2),
                        TrajanjeTreninga = reader.GetInt32(3),
                        StatusTreninga = statusTreninga,
                        Instruktor = Util.Instance.PronadjiInstruktora(reader.GetString(5)),
                        Polaznik = Util.Instance.PronadjiPolaznika(reader.GetString(6)),
                        Aktivan = reader.GetBoolean(7)
                    };

                    Util.Instance.Treninzi.Add(trening);
                }
                reader.Close();
            }
        }
        public void SacuvajEntitet(Object obj)
        {
            Trening trening = obj as Trening;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into treninzi (ID, Datum, Vreme, Trajanje_Min, Status_Treninga, Instruktor_JMBG,
                                        Polaznik_JMBG, Aktivan)
                                        VALUES (@ID, @Datum, @Vreme, @Trajanje_Min, @Status_Treninga, @Instruktor_JMBG,
                                        @Polaznik_JMBG, @Aktivan)";
                command.Parameters.Add(new SqlParameter("ID", trening.ID));
                command.Parameters.Add(new SqlParameter("Datum", trening.DatumTreninga));
                command.Parameters.Add(new SqlParameter("Vreme", trening.VremePocetkaTreninga));
                command.Parameters.Add(new SqlParameter("Trajanje_Min", trening.TrajanjeTreninga));
                command.Parameters.Add(new SqlParameter("Status_Treninga", trening.StatusTreninga.ToString()));
                command.Parameters.Add(new SqlParameter("Instruktor_JMBG", trening.Instruktor.Korisnik.JMBG));
                command.Parameters.Add(new SqlParameter("Polaznik_JMBG", trening.Polaznik.Korisnik.JMBG));
                command.Parameters.Add(new SqlParameter("Aktivan", trening.Aktivan));

                command.ExecuteNonQuery();
            }
        }

        public void IzmeniEntitet(Object obj)
        {
            Trening trening = obj as Trening;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update treninzi set ID = @ID, Datum = @Datum, Vreme = @Vreme, Trajanje_Min = @Trajanje_Min, Status_Treninga = @Status_Treninga, Instruktor_JMBG = @Instruktor_JMBG,
                                        Polaznik_JMBG = @Polaznik_JMBG, Aktivan = @Aktivan where ID = @ID";
                command.Parameters.Add(new SqlParameter("ID", trening.ID));
                command.Parameters.Add(new SqlParameter("Datum", trening.DatumTreninga));
                command.Parameters.Add(new SqlParameter("Vreme", trening.VremePocetkaTreninga));
                command.Parameters.Add(new SqlParameter("Trajanje_Min", trening.TrajanjeTreninga));
                command.Parameters.Add(new SqlParameter("Status_Treninga", trening.StatusTreninga.ToString()));
                command.Parameters.Add(new SqlParameter("Instruktor_JMBG", trening.Instruktor.Korisnik.JMBG));
                command.Parameters.Add(new SqlParameter("Polaznik_JMBG", trening.Polaznik.Korisnik.JMBG));
                command.Parameters.Add(new SqlParameter("Aktivan", trening.Aktivan));

                command.ExecuteNonQuery();
            }
        }
    }
}
