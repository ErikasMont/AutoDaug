# AutoDaug

# Sprendžiamo uždavinio aprašymas 
Projekto tikslas – sukurti patogią sistemą automobilių pardavimų ar kitiems su automobiliais susijusiems skelbimams talpinti ir palengvinti jų paiešką.

Veikimo principas – sistemą sudaro:

•	Internetinė aplikacija, kurią klientas matys ir gales naudotis

•	Taikomoji programa, skirta logikai įgyvendinti, klientas šios dalies nematys

•	Duomenų bazė, skirta saugoti duomenis apie pardavėjus, automobilius ir skelbimus

#

Pardavėjas, norėdamas naudotis šia sistema, turės prisiregistruoti prie internetinės aplikacijos, galės pridėti savo automobilį ir sukurti jam skelbimą, taip pat gales pašalinti skelbimus, bei automobilius. Taip pat, registruotas naudotojas visada galės atnaujinti automobilio arba skelbimo informaciją, taip pat juos pašalinti. Svečias galės ieškoti skelbimų pagal tam tikrus kriterijus arba peržiūrėti visus skelbimus. Administratorius galės pašalinti skelbimus, kuriuose yra akivaizdžiai neteisinga informacija arba publikai netinkami aprašymai. Taip pat, administatorius galės patvirtinti naudotojų registracijas, patvirtinti norimus paskelbti skelbimus.

# Funkciniai reikalavimai 
Neregistruotas naudotojas galės: 
1.	 Peržiūrėti svetainės pagrindinį puslapį 
2.	 Užsiregistruoti į internetinės aplikaciją
3.	Prisijungti prie internetinės aplikacijos 

Registruotas naudotojas galės: 
1.	Atsijungti nuo internetinės aplikacijos 
2.	Atnaujinti savo asmeninę informaciją
3.	Pridėti automobilį
4.	Redaguoti pridėto automobilio informaciją
5.	Pašalinti pridėtą automobilį
6.	Matyti visus savo automobilius
7.	Peržiūreti vieno automobilio informaciją
8.	Sukurti skelbimą pasirinktam automobiliui
9.	Sukurti skelbimą ne automobiliui. 
10.	Redaguoti skelbimo informaciją 
11.	Pašalinti skelbimą
12.	Matyti visus savo skelbimus 

Administratorius galės: 
1.	Patvirtinti naudotojo registraciją. 
2.	Patvirtinti norimus skelbti skelbimus. 
3.	Šalinti naudotojus. 
4.	Šalinti netinkamus skelbimus. 
 
# Sistemos architektūra 
Sistemos sudedamosios dalys: 

•	Kliento pusė (ang. Front-End) – naudojant React.js.

•	Serverio pusė (angl. Back-End) – naudojant .NET 6.0. 

•	Duomenų bazė – SQL Server (MSSQL).
 
Paveikslėlyje pavaizduota kuriamos sistemos diegimo diagrama. Naudotojas turėdamas kompiuterį su naršykle, gali pasiekti internetinę aplikaciją HTTP protokolu. Sugeneruotos užklausos yra perduodamos į sistemos taikomąją programą, kurioje užklausa yra apdorojama, jei reikia ji kreipiasi į SQL Server duomenų bazę JDBC ryšiu, bei naudojantis ORM sąsajom, ir sugeneruojamas atsakas. Visa sistema yra talpianama Azure serveryje.


