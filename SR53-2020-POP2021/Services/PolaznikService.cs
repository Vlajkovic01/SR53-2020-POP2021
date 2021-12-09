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
    public class PolaznikService : IEntitet
    {
        public void IzbrisiEntitet(string jmbg)
        {
            Polaznik polaznikPronadjen = Util.Instance.Polaznici.ToList().Find(p => p.Korisnik.JMBG.Equals(jmbg));
            if (polaznikPronadjen == null)
            {
                throw new UserNotFoundException($"Ne postoji korisnik sa JMBG: {jmbg}");
            }
            using (SqlConnection connection = new SqlConnection(Util.CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"update polaznici set Aktivan = 0 where JMBG = @JMBG";

                command.Parameters.Add(new SqlParameter("JMBG", polaznikPronadjen.Korisnik.JMBG));

                command.ExecuteNonQuery();
            }
        }

        public void UcitajEntitet(string filename)
        {
            Util.Instance.Polaznici = new ObservableCollection<Polaznik>();
            using (SqlConnection connection = new SqlConnection(Util.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"select * from polaznici";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    RegistrovaniKorisnik registrovaniKorisnik = Util.Instance.Korisnici.ToList().Find(korisnik => korisnik.JMBG.Equals(reader.GetString(2)));
                    Polaznik polaznik = new Polaznik
                    {

                        Korisnik = registrovaniKorisnik,
                    };

                    Util.Instance.Polaznici.Add(polaznik);
                }
                reader.Close();
            }
        }
        public void SacuvajEntitet(Object obj)
        {
            Polaznik polaznik = obj as Polaznik;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into polaznici (Ime, Prezime, JMBG, Pol, Adresa_ID, Email,
                                        Lozinka, Tip_Korisnika, Aktivan)
                                        VALUES (@Ime, @Prezime, @JMBG, @Pol, @Adresa_ID, @Email, @Lozinka,
                                        @Tip_Korisnika, @Aktivan)";
                command.Parameters.Add(new SqlParameter("Ime", polaznik.Korisnik.Ime));
                command.Parameters.Add(new SqlParameter("Prezime", polaznik.Korisnik.Prezime));
                command.Parameters.Add(new SqlParameter("JMBG", polaznik.Korisnik.JMBG));
                command.Parameters.Add(new SqlParameter("Pol", polaznik.Korisnik.Pol.ToString()));
                command.Parameters.Add(new SqlParameter("Adresa_ID", polaznik.Korisnik.Adresa.ID));
                command.Parameters.Add(new SqlParameter("Email", polaznik.Korisnik.Email));
                command.Parameters.Add(new SqlParameter("Lozinka", polaznik.Korisnik.Lozinka));
                command.Parameters.Add(new SqlParameter("Tip_Korisnika", polaznik.Korisnik.TipKorisnika.ToString()));
                command.Parameters.Add(new SqlParameter("Aktivan", polaznik.Korisnik.Aktivan));

                command.ExecuteNonQuery();
            }
        }

        public void IzmeniEntitet(Object obj)
        {
            Polaznik polaznik = obj as Polaznik;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update polaznici set Ime = @Ime, Prezime = @Prezime, Pol = @Pol , Adresa_ID = @Adresa_ID, Email = @Email,
                                        Lozinka = @Lozinka, Tip_Korisnika = @Tip_Korisnika, Aktivan = @Aktivan where JMBG = @JMBG";
                command.Parameters.Add(new SqlParameter("Ime", polaznik.Korisnik.Ime));
                command.Parameters.Add(new SqlParameter("Prezime", polaznik.Korisnik.Prezime));
                command.Parameters.Add(new SqlParameter("JMBG", polaznik.Korisnik.JMBG)); // JMBG = @JMBG, zbog unique key u bazi ne updatujem kolonu jer proveravam jedinstvenost u aplikaciji.
                command.Parameters.Add(new SqlParameter("Pol", polaznik.Korisnik.Pol.ToString()));
                command.Parameters.Add(new SqlParameter("Adresa_ID", polaznik.Korisnik.Adresa.ID));
                command.Parameters.Add(new SqlParameter("Email", polaznik.Korisnik.Email));
                command.Parameters.Add(new SqlParameter("Lozinka", polaznik.Korisnik.Lozinka));
                command.Parameters.Add(new SqlParameter("Tip_Korisnika", polaznik.Korisnik.TipKorisnika.ToString()));
                command.Parameters.Add(new SqlParameter("Aktivan", polaznik.Korisnik.Aktivan));

                command.ExecuteNonQuery();
            }
        }
    }
}
