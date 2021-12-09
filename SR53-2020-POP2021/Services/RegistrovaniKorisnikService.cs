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
    public class RegistrovaniKorisnikService : IEntitet
    {
        public void IzbrisiEntitet(string jmbg)
        {
            RegistrovaniKorisnik registrovaniKorisnik = Util.Instance.Korisnici.ToList().Find(korisnik => korisnik.JMBG.Equals(jmbg));
            if (registrovaniKorisnik == null)
            {
                throw new UserNotFoundException($"Ne postoji korisnik sa JMBG: {jmbg}");
            }
            using (SqlConnection connection = new SqlConnection(Util.CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"update korisnici set Aktivan = 0 where JMBG = @JMBG";

                command.Parameters.Add(new SqlParameter("JMBG", registrovaniKorisnik.JMBG));

                command.ExecuteNonQuery();
            }

        }

        public void UcitajEntitet(string filename)
        {
            Util.Instance.Korisnici = new ObservableCollection<RegistrovaniKorisnik>();

            using (SqlConnection connection = new SqlConnection(Util.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"select * from korisnici";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Enum.TryParse(reader.GetString(3), out EPol pol);
                    int sifraAdrese = reader.GetInt32(4);
                    Enum.TryParse(reader.GetString(7), out ETipKorisnika tip);
                    RegistrovaniKorisnik registrovaniKorisnik = new RegistrovaniKorisnik
                    {

                        Ime = reader.GetString(0),
                        Prezime = reader.GetString(1),
                        JMBG = reader.GetString(2),
                        Pol = pol,
                        Adresa = Util.Instance.PronadjiAdresu(sifraAdrese),
                        Email = reader.GetString(5),
                        Lozinka = reader.GetString(6),
                        TipKorisnika = tip,
                        Aktivan = reader.GetBoolean(8)
                    };

                    Util.Instance.Korisnici.Add(registrovaniKorisnik);
                }
                reader.Close();
            }
        }

        public void SacuvajEntitet(Object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into korisnici (Ime, Prezime, JMBG, Pol, Adresa_ID, Email,
                                        Lozinka, Tip_Korisnika, Aktivan)
                                        VALUES (@Ime, @Prezime, @JMBG, @Pol, @Adresa_ID, @Email, @Lozinka,
                                        @Tip_Korisnika, @Aktivan)";
                command.Parameters.Add(new SqlParameter("Ime", korisnik.Ime));
                command.Parameters.Add(new SqlParameter("Prezime", korisnik.Prezime));
                command.Parameters.Add(new SqlParameter("JMBG", korisnik.JMBG));
                command.Parameters.Add(new SqlParameter("Pol", korisnik.Pol.ToString()));
                command.Parameters.Add(new SqlParameter("Adresa_ID", korisnik.Adresa.ID));
                command.Parameters.Add(new SqlParameter("Email", korisnik.Email));
                command.Parameters.Add(new SqlParameter("Lozinka", korisnik.Lozinka));
                command.Parameters.Add(new SqlParameter("Tip_Korisnika", korisnik.TipKorisnika.ToString()));
                command.Parameters.Add(new SqlParameter("Aktivan", korisnik.Aktivan));

                command.ExecuteNonQuery();
            }
        }

        public void IzmeniEntitet(Object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update korisnici set Ime = @Ime, Prezime = @Prezime, Pol = @Pol , Adresa_ID = @Adresa_ID, Email = @Email,
                                        Lozinka = @Lozinka, Tip_Korisnika = @Tip_Korisnika, Aktivan = @Aktivan where JMBG = @JMBG";
                command.Parameters.Add(new SqlParameter("Ime", korisnik.Ime));
                command.Parameters.Add(new SqlParameter("Prezime", korisnik.Prezime));
                command.Parameters.Add(new SqlParameter("JMBG", korisnik.JMBG)); // JMBG = @JMBG, zbog unique key u bazi ne updatujem kolonu jer proveravam jedinstvenost u aplikaciji.
                command.Parameters.Add(new SqlParameter("Pol", korisnik.Pol.ToString()));
                command.Parameters.Add(new SqlParameter("Adresa_ID", korisnik.Adresa.ID));
                command.Parameters.Add(new SqlParameter("Email", korisnik.Email));
                command.Parameters.Add(new SqlParameter("Lozinka", korisnik.Lozinka));
                command.Parameters.Add(new SqlParameter("Tip_Korisnika", korisnik.TipKorisnika.ToString()));
                command.Parameters.Add(new SqlParameter("Aktivan", korisnik.Aktivan));

                command.ExecuteScalar();
            }
        }
    }
}
