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

-- Izmena postojecih podataka 
alter table treninzi
alter column vreme varchar(10)

drop table korisnici
drop table adrese
drop table polaznici
drop table instruktori
drop table centri
drop table treninzi

-- Dodavanje vrednosti
insert into centri values
	(1, 'Max Fitness', 2, 'True')

insert into adrese values
	(0, '', '', '','', 'False')
insert into adrese values
	(1, 'Luke Stevica', '111/A', 'Loznica','Srbija', 'True')
insert into adrese values
	(2, 'Novosadska', '115', 'Novi Sad','Srbija', 'True')

insert into korisnici values
	('', '', '0000000000000', 'MUSKI', 0, '@gmail.com', '0', 'POLAZNIK', 'False')
insert into korisnici values
	('Stefan', 'Vlajkovic', '1234567891234', 'MUSKI', 1, 'vlajkovic@gmail.com', '12345', 'ADMINISTRATOR', 'True')
insert into korisnici values
	('Janko', 'Jankovic', '1234567810101', 'MUSKI', 2, 'jankovic@gmail.com', '12345', 'POLAZNIK', 'True')
insert into korisnici values
	('Nikola', 'Nikolic', '9876543219876', 'MUSKI', 1, 'nikolic@gmail.com', '12345', 'INSTRUKTOR', 'True')
insert into korisnici values
	('Marko', 'Markovic', '1122334455667', 'MUSKI', 2, 'markovic@gmail.com', '12345', 'INSTRUKTOR', 'True')
insert into korisnici values
	('Darko', 'Panic', '1234567899999', 'MUSKI', 2, 'panic@gmail.com', '12345', 'POLAZNIK', 'True')
-- Test
insert into korisnici values
	('Test', 'Test', '1111122222333', 'MUSKI', 2, 'panic@gmail.com', '12345', 'ADMINISTRATOR', 'True')

insert into instruktori values
	('Marko', 'Markovic', '1122334455667', 'MUSKI', 2, 'markovic@gmail.com', '12345', 'INSTRUKTOR', 'True')
insert into instruktori values
	('Nikola', 'Nikolic', '9876543219876', 'MUSKI', 1, 'nikolic@gmail.com', '12345', 'INSTRUKTOR', 'True')

insert into polaznici values
	('', '', '0000000000000', 'MUSKI', 0, '@gmail.com', '0', 'POLAZNIK', 'False')
insert into polaznici values
	('Janko', 'Jankovic', '1234567810101', 'MUSKI', 2, 'jankovic@gmail.com', '12345', 'POLAZNIK', 'True')
insert into polaznici values
	('Darko', 'Panic', '1234567899999', 'MUSKI', 2, 'panic@gmail.com', '12345', 'POLAZNIK', 'True')

insert into treninzi values
	(1, '20.11.2021', '14:00', 60, 'REZERVISAN', '9876543219876', '1234567810101', 'True')
insert into treninzi values
	(2, '21.11.2021', '15:00', 120, 'REZERVISAN', '9876543219876', '1234567810101', 'True')
insert into treninzi values
	(3, '22.11.2021', '16:00', 60, 'REZERVISAN', '1122334455667', '1234567899999', 'True')
insert into treninzi values
	(4, '23.11.2021', '17:00', 60, 'SLOBODAN', '9876543219876', '0000000000000', 'True')
insert into treninzi values
	(5, '24.11.2021', '18:00', 60, 'SLOBODAN', '1122334455667', '0000000000000', 'True')

-- Pregled
select *
from korisnici
select *
from polaznici
select *
from instruktori
select *
from adrese
select *
from treninzi