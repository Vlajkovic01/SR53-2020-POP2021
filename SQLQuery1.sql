 -- Pravljenje baze podataka
CREATE DATABASE SR53_2020_POP2021;

-- Pravljenje tabela
CREATE TABLE adrese(
	ID integer,
	Ulica varchar(25),
	Broj varchar(10),
	Grad varchar(15),
	Drzava varchar(15),
	Aktivna bit,
	constraint adresa_pk primary key (ID)
);

CREATE TABLE korisnici(
	Ime varchar(15),
	Prezime varchar(25),
	JMBG varchar(13),
	Pol varchar(10),
	Adresa_ID integer,
	Email varchar(30),
	Lozinka varchar(15),
	Tip_Korisnika varchar(15),
	Aktivan bit,
	constraint korisnik_jmbg_uq unique (JMBG),
	constraint korisnik_adresa_fk foreign key (Adresa_ID) references adrese(ID)
);

CREATE TABLE instruktori(
	Ime varchar(15),
	Prezime varchar(25),
	JMBG varchar(13),
	Pol varchar(10),
	Adresa_ID integer,
	Email varchar(30),
	Lozinka varchar(15),
	Tip_Korisnika varchar(15),
	Aktivan bit,
	constraint instruktor_jmbg_uq unique (JMBG),
	constraint instruktor_adresa_fk foreign key (Adresa_ID) references adrese(ID)
);

CREATE TABLE polaznici(
	Ime varchar(15),
	Prezime varchar(25),
	JMBG varchar(13),
	Pol varchar(10),
	Adresa_ID integer,
	Email varchar(30),
	Lozinka varchar(15),
	Tip_Korisnika varchar(15),
	Aktivan bit,
	constraint polaznik_jmbg_uq unique (JMBG),
	constraint polaznik_adresa_fk foreign key (Adresa_ID) references adrese(ID)
);

CREATE TABLE centri(
	ID integer,
	Naziv varchar(25),
	Adresa_ID integer,
	Aktivan bit,
	constraint centar_pk primary key (ID)
);

CREATE TABLE treninzi(
	ID integer,
	Datum varchar(25),
	Vreme varchar(5),
	Trajanje_Min integer,
	Status_Treninga varchar(15),
	Instruktor_JMBG varchar(13),
	Polaznik_JMBG varchar(13),
	Aktivan bit,
	constraint trening_pk primary key (ID),
	constraint trening_instruktor_fk foreign key (Instruktor_JMBG) references instruktori(JMBG),
	constraint trening_polaznik_fk foreign key (Polaznik_JMBG) references polaznici(JMBG),
);