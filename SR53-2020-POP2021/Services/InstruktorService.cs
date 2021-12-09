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
   public class InstruktorService : IEntitet
    {

        public void IzbrisiEntitet(string jmbg)
        {
            Instruktor instruktor = Util.Instance.Instruktori.ToList().Find(i => i.Korisnik.JMBG.Equals(jmbg));
            if (instruktor == null)
            {
                throw new UserNotFoundException($"Ne postoji korisnik sa JMBG: {jmbg}");
            }
            using (SqlConnection connection = new SqlConnection(Util.CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"update instruktori set Aktivan = 0 where JMBG = @JMBG";

                command.Parameters.Add(new SqlParameter("JMBG", instruktor.Korisnik.JMBG));

                command.ExecuteNonQuery();
            }
        }

        public void UcitajEntitet(string filename)
        {
            Util.Instance.Instruktori = new ObservableCollection<Instruktor>();
            using (SqlConnection connection = new SqlConnection(Util.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"select * from instruktori";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    RegistrovaniKorisnik registrovaniKorisnik = Util.Instance.Korisnici.ToList().Find(korisnik => korisnik.JMBG.Equals(reader.GetString(2)));

                    Instruktor instruktor = new Instruktor
                    {

                        Korisnik = registrovaniKorisnik,
                    };

                    Util.Instance.Instruktori.Add(instruktor);
                }
                reader.Close();
            }
        }

        public void SacuvajEntitet(Object obj)
        {
            Instruktor instruktor = obj as Instruktor;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into instruktori (Ime, Prezime, JMBG, Pol, Adresa_ID, Email,
                                        Lozinka, Tip_Korisnika, Aktivan)
                                        VALUES (@Ime, @Prezime, @JMBG, @Pol, @Adresa_ID, @Email, @Lozinka,
                                        @Tip_Korisnika, @Aktivan)";
                command.Parameters.Add(new SqlParameter("Ime", instruktor.Korisnik.Ime));
                command.Parameters.Add(new SqlParameter("Prezime", instruktor.Korisnik.Prezime));
                command.Parameters.Add(new SqlParameter("JMBG", instruktor.Korisnik.JMBG));
                command.Parameters.Add(new SqlParameter("Pol", instruktor.Korisnik.Pol.ToString()));
                command.Parameters.Add(new SqlParameter("Adresa_ID", instruktor.Korisnik.Adresa.ID));
                command.Parameters.Add(new SqlParameter("Email", instruktor.Korisnik.Email));
                command.Parameters.Add(new SqlParameter("Lozinka", instruktor.Korisnik.Lozinka));
                command.Parameters.Add(new SqlParameter("Tip_Korisnika", instruktor.Korisnik.TipKorisnika.ToString()));
                command.Parameters.Add(new SqlParameter("Aktivan", instruktor.Korisnik.Aktivan));

                command.ExecuteNonQuery();
            }
        }

        public void IzmeniEntitet(Object obj)
        {
            Instruktor instruktor = obj as Instruktor;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update instruktori set Ime = @Ime, Prezime = @Prezime, Pol = @Pol , Adresa_ID = @Adresa_ID, Email = @Email,
                                        Lozinka = @Lozinka, Tip_Korisnika = @Tip_Korisnika, Aktivan = @Aktivan where JMBG = @JMBG";
                command.Parameters.Add(new SqlParameter("Ime", instruktor.Korisnik.Ime));
                command.Parameters.Add(new SqlParameter("Prezime", instruktor.Korisnik.Prezime));
                command.Parameters.Add(new SqlParameter("JMBG", instruktor.Korisnik.JMBG)); // JMBG = @JMBG, zbog unique key u bazi ne updatujem kolonu jer proveravam jedinstvenost u aplikaciji.
                command.Parameters.Add(new SqlParameter("Pol", instruktor.Korisnik.Pol.ToString()));
                command.Parameters.Add(new SqlParameter("Adresa_ID", instruktor.Korisnik.Adresa.ID));
                command.Parameters.Add(new SqlParameter("Email", instruktor.Korisnik.Email));
                command.Parameters.Add(new SqlParameter("Lozinka", instruktor.Korisnik.Lozinka));
                command.Parameters.Add(new SqlParameter("Tip_Korisnika", instruktor.Korisnik.TipKorisnika.ToString()));
                command.Parameters.Add(new SqlParameter("Aktivan", instruktor.Korisnik.Aktivan));

                command.ExecuteNonQuery();
            }
        }
    }
}
