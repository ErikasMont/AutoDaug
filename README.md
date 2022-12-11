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

![image_test](https://user-images.githubusercontent.com/79359651/191044984-36640eb8-814b-4dd7-bfd1-330ed6a38f1d.png)

# API specifikacija
## Naudotojų API metodai
### GET /users
Gražina sąrašą sistemos naudotojų, prieinamas tik administratoriams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/users`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
|Unauthorized  |401     |
#### Užklausos pavyzdys
`GET https://autodaug.azurewebsites.net/api/users`
#### Atsakymo pavyzdys
```
[
  {
    "id": 0,
    "username": "string",
    "password": "stringst",
    "phoneNumber": "stringstring",
    "accountState": "string",
    "isAdmin": true
  }
]
```
### GET /users/{id}
Gražina naudotoją su id, kuris buvo nurodytas užklausos metu, kartu su URL
#### Metodo URL
`https://autodaug.azurewebsites.net/api/users/{id}`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
|Unauthorized  |401     |
|Not found     |404     |
#### Parametrai
|Pavadinimas   |Ar būtinas?   |Apibūdinimas   |Pavyzdys   |
| ------------ | ------------ | ------------- | --------- |
|id            |Taip          | Naudotojo id  | `5`       |
#### Užklausos pavyzdys
`GET https://autodaug.azurewebsites.net/api/users/5`
#### Atsakymo pavyzdys
```
{
  "id": 0,
  "username": "string",
  "password": "stringst",
  "phoneNumber": "stringstring",
  "accountState": "string",
  "isAdmin": true
}
```
### PUT /users/{id}
Atnaujiną naudotoją su duotais parametrais, kurie buvo nurodyti užklausos metu, id kartu su URL, o kiti parametrai perudodami kartu su užklausos body 
#### Metodo URL
`https://autodaug.azurewebsites.net/api/users/{id}`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
|Bad request   |400     |
|Unauthorized  |401     |
#### Parametrai
|Pavadinimas   |Ar būtinas?   |Apibūdinimas   |Pavyzdys   |
| ------------ | ------------ | ------------- | --------- |
|id            |Taip          |Naudotojo id  | `5`       |
|username      |Taip          |Nautotojo vardas   | `demovardas`   |
|phoneNumber   |Taip          |Naudotojo telefono numeris   | `+37061234567`   |
#### Užklausos pavyzdys
`PUT https://autodaug.azurewebsites.net/api/users/5`
```
{
  "username": "demovardas",
  "phoneNumber": "+37061234567"
}
```
#### Atsakymo pavyzdys
```
Tuščias body su statuso kodu 200 Success
```
### DELETE /users/{id}
Ištrina naudotoją su nurodytu id per URL, funckija prieinama tik administratoriams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/users/{id}`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|No Content    |204     |
|Unauthorized  |401     |
|Not found     |404     |
#### Parametrai
|Pavadinimas   |Ar būtinas?   |Apibūdinimas   |Pavyzdys   |
| ------------ | ------------ | ------------- | --------- |
|id            |Taip          |Naudotojo id   | `5`       |
#### Užklausos pavyzdys
`DELETE https://autodaug.azurewebsites.net/api/users/5`
#### Atsakymo pavyzdys
```
Tuščias body su statuso kodu 204 No content
```
### POST /users/register
Sukuria naują naudotoją su nurodytais parametrais
#### Metodo URL
`https://autodaug.azurewebsites.net/api/users/register`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|No Content    |201     |
|Bad request   |400     |
#### Parametrai
|Pavadinimas   |Ar būtinas?   |Apibūdinimas   |Pavyzdys   |
| ------------ | ------------ | ------------- | --------- |
|username      |Taip          |Naudotojo vardas   | `demovardas`   |
|password      |Taip          |Naudotojo slaptažodis   | `slaptazodis`   |
|phoneNumber   |Taip          |Naudotojo telefono numeris   | `+37061234567`   |
#### Užklausos pavyzdys
`POST https://autodaug.azurewebsites.net/api/users/register`
```
{
  "username": "demovardas",
  "password": "slaptazodis",
  "phoneNumber": "+37061234567"
}
```
#### Atsakymo pavyzdys
```
{
  "id": 0,
  "username": "demovardas",
  "password": "hashedpass", 
  "phoneNumber": "+37061234567",
  "accountState": "Not Confirmed",
  "isAdmin": false
}
```
### POST /users/confirm/{id}
Patvirtina naudotojo registraciją pagal duotą naudotojo id, kuris perduodamas per URL. Funkcija prieinama tik administratoriui
#### Metodo URL
`https://autodaug.azurewebsites.net/api/users/confirm/{id}`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
|Bad request   |400     |
|Unauthorized  |401     |
#### Parametrai
|Pavadinimas   |Ar būtinas?   |Apibūdinimas   |Pavyzdys   |
| ------------ | ------------ | ------------- | --------- |
|id            |Taip          |Naudotojo id   | `5`       |
#### Užklausos pavyzdys
`POST https://autodaug.azurewebsites.net/api/users/confirm/5`
#### Atsakymo pavyzdys
```
Tuščias body su statuso kodu 200 Success
```
### POST /users/token
Gražina naudotojo informaciją kartu su sugeneruotu žetonu, kuris vėliau yra naudojamas atpažinti naudotojo rolei
#### Metodo URL
`https://autodaug.azurewebsites.net/api/users/token`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
|Bad request   |400     |
#### Parametrai
|Pavadinimas   |Ar būtinas?   |Apibūdinimas   |Pavyzdys   |
| ------------ | ------------ | ------------- | --------- |
|username      |Taip          |Naudotojo vardas   | `demovardas`     |
|password      |Taip          |Naudotojo slaptažodis   | `slaptazodis`   |
#### Užklausos pavyzdys
`POST https://autodaug.azurewebsites.net/api/users/token`
```
{
  "username": "string",
  "password": "string"
}
```
#### Atsakymo pavyzdys
```
{
  "id": 5,
  "username": "demovardas",
  "password": "hashedpass",
  "isAdmin": false,
  "token": "token"
}
```
### POST /users/logout
Atjungia vartotoją nuo sistemos išvalydamas slapukus
#### Metodo URL
`https://autodaug.azurewebsites.net/api/users/logout`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
#### Užklausos pavyzdys
`POST https://autodaug.azurewebsites.net/api/users/logout`
#### Atsakymo pavyzdys
```
Tuščias body su statuso kodu 200 Success
```
## Skelbimų tipų API metodai
### GET /advertTypes
Gražina sąrašą esamų skelbimų tipų
#### Metodo URL
`https://autodaug.azurewebsites.net/api/advertTypes`, funkcija prieinama prisijungusiems naudotojams
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
|Unauthorized  |401     |
#### Užklausos pavyzdys
`GET https://autodaug.azurewebsites.net/api/advertTypes`
#### Atsakymo pavyzdys
```
[
  {
    "id": 5,
    "name": "type of advert",
    "description": "description of the type"
  }
]
```
### GET /advertTypes/{id}
Gražina skelbimo tipą, pagal id, kuris perduodamas per URL, funkcija prieinama prisijungusiems naudotojams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/advertTypes/{id}`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
|Unauthorized  |401     |
|Not found     |404     |
#### Užklausos pavyzdys
`GET https://autodaug.azurewebsites.net/api/advertTypes/5`
#### Atsakymo pavyzdys
```
{
  "id": 5,
  "name": "type of advert",
  "description": "description of the type"
}
```
### POST /advertTypes
Sukuria naują skelbimo tipą su nurodytais parametrais, funckija prieinama tik administratoriams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/advertTypes`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|No Content    |201     |
|Bad request   |400     |
|Unauthorized  |401     |
#### Parametrai
|Pavadinimas   |Ar būtinas?   |Apibūdinimas   |Pavyzdys   |
| ------------ | ------------ | ------------- | --------- |
|name      |Taip          |Skelbimo tipo pavadinimas   | `pavadinimas`   |
|description   |Taip          |Skelbimo tipo aprašymas   | `aprašymas skelbimo tipui`   |
#### Užklausos pavyzdys
`POST https://autodaug.azurewebsites.net/api/advertTypes`
```
}
  "name": "pavadinimas",
  "description": "aprašymas skelbimo tipui"
}
```
#### Atsakymo pavyzdys
```
{
  "id": 5,
  "name": "pavadinimas",
  "description": "aprašymas skelbimo tipui"
}
```
### PUT /advertTypes/{id}
Atnaujiną skelbimo tipą su duotais parametrais, kurie buvo nurodyti užklausos metu, id kartu su URL, o kiti parametrai perudodami kartu su užklausos body, funckija prieinama tik administratoriams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/advertTypes/{id}`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
|Bad request   |400     |
|Unauthorized  |401     |
#### Parametrai
|Pavadinimas   |Ar būtinas?   |Apibūdinimas   |Pavyzdys   |
| ------------ | ------------ | ------------- | --------- |
|name      |Taip          |Skelbimo tipo pavadinimas   | `pavadinimas`   |
|description   |Taip          |Skelbimo tipo aprašymas   | `aprašymas skelbimo tipui`   |
#### Užklausos pavyzdys
`PUT https://autodaug.azurewebsites.net/api/advertTypes/5`
```
{
  "name": "pavadinimas",
  "description": "aprašymas skelbimo tipui"
}
```
#### Atsakymo pavyzdys
```
Tuščias body su statuso kodu 200 Success
```
### DELETE /advertTypes/{id}
Ištrina skelbimo tipą su nurodytu id per URL, funckija prieinama tik administratoriams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/advertTypes/{id}`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|No Content    |204     |
|Unauthorized  |401     |
|Not found     |404     |
#### Parametrai
|Pavadinimas   |Ar būtinas?   |Apibūdinimas   |Pavyzdys   |
| ------------ | ------------ | ------------- | --------- |
|id            |Taip          |Skelbimo tipo id   | `5`       |
#### Užklausos pavyzdys
`DELETE https://autodaug.azurewebsites.net/api/advertTypes/5`
#### Atsakymo pavyzdys
```
Tuščias body su statuso kodu 204 No content
```
### GET /advertTypes/{id}/adverts
Gražina sąrašą skelbimų pagal nurodytą skelbimo tipą, funkcija prieinama prisijungusiems naudotojams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/advertTypes/{id}/adverts`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
|Unauthorized  |401     |
|Not found     |404     |
#### Užklausos pavyzdys
`GET https://autodaug.azurewebsites.net/api/advertTypes/5/adverts`
#### Atsakymo pavyzdys
```
[
  {
    "id": 1,
    "name": "skelbimas",
    "description": "skelbimo aprašymas",
    "price": 1500,
    "advertType_Id": 5,
    "user_Id": 2
  }
]
```
## Skelbimų API metodai
### GET /adverts
Gražina sąrašą esamų skelbimų, funkcija prieinama prisijungusiems naudotojams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/adverts`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
|Unauthorized  |401     |
#### Užklausos pavyzdys
`GET https://autodaug.azurewebsites.net/api/adverts`
#### Atsakymo pavyzdys
```
[
  {
    "id": 1,
    "name": "pavadinimas",
    "description": "aprašymas",
    "price": 1500,
    "advertType_Id": 2,
    "user_Id": 3
  }
]
```
### GET /adverts/{id}
Gražina skelbimą, pagal id, kuris perduodamas per URL, funkcija prieinama prisijungusiems naudotojams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/adverts/{id}`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
|Unauthorized  |401     |
|Not found     |404     |
#### Užklausos pavyzdys
`GET https://autodaug.azurewebsites.net/api/adverts/5`
#### Atsakymo pavyzdys
```
{
  "id": 5,
  "name": "pavadinimas",
  "description": "aprašymas",
  "price": 1500,
  "advertType_Id": 2,
  "user_Id": 3
}
```
### POST /adverts
Sukuria naują skelbimą su nurodytais parametrais, funkcija prieinama prisijungusiems naudotojams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/advertTypes`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|No Content    |201     |
|Bad request   |400     |
|Unauthorized  |401     |
#### Parametrai
|Pavadinimas   |Ar būtinas?   |Apibūdinimas   |Pavyzdys   |
| ------------ | ------------ | ------------- | --------- |
|name      |Taip          |Skelbimo pavadinimas   | `pavadinimas`   |
|description   |Taip          |Skelbimo aprašymas   | `aprašymas`   |
|price     |Taip    |Kaina prekės skelbime    | `1500`   |
|advertType_Id    |Taip    |Skelbimo tipo id    | `1`    |
|user_Id     | Taip     |Naudotojo id, kuriam priklauso šis skelbimas     | `3`    |
#### Užklausos pavyzdys
`POST https://autodaug.azurewebsites.net/api/adverts`
```
{
  "name": "pavadinimas",
  "description": "aprašymas",
  "price": 1500,
  "advertType_Id": 1,
  "user_Id": 3
}
```
#### Atsakymo pavyzdys
```
{
  "id": 6,
  "name": "pavadinimas",
  "description": "aprašymas",
  "price": 1500,
  "advertType_Id": 1,
  "user_Id": 3
}
```
### PUT /adverts/{id}
Atnaujiną skelbimą su duotais parametrais, kurie buvo nurodyti užklausos metu, id kartu su URL, o kiti parametrai perudodami kartu su užklausos body, funkcija prieinama prisijungusiems naudotojams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/adverts/{id}`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
|Bad request   |400     |
|Unauthorized  |401     |
#### Parametrai
|Pavadinimas   |Ar būtinas?   |Apibūdinimas   |Pavyzdys   |
| ------------ | ------------ | ------------- | --------- |
|name      |Taip          |Skelbimo pavadinimas   | `pavadinimas`   |
|description   |Taip          |Skelbimo aprašymas   | `aprašymas`   |
|price     |Taip    |Kaina prekės skelbime    | `1500`   |
|advertType_Id    |Taip    |Skelbimo tipo id    | `1`    |
#### Užklausos pavyzdys
`PUT https://autodaug.azurewebsites.net/api/adverts/5`
```
{
  "name": "pavadinimas",
  "description": "aprašymas",
  "price": 1500,
  "advertType_Id": 1
}
```
#### Atsakymo pavyzdys
```
Tuščias body su statuso kodu 200 Success
```
### DELETE /adverts/{id}
Ištrina skelbimą su nurodytu id per URL, funkcija prieinama prisijungusiems naudotojams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/adverts/{id}`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|No Content    |204     |
|Unauthorized  |401     |
|Not found     |404     |
#### Parametrai
|Pavadinimas   |Ar būtinas?   |Apibūdinimas   |Pavyzdys   |
| ------------ | ------------ | ------------- | --------- |
|id            |Taip          |Skelbimo id   | `5`       |
#### Užklausos pavyzdys
`DELETE https://autodaug.azurewebsites.net/api/adverts/5`
#### Atsakymo pavyzdys
```
Tuščias body su statuso kodu 204 No content
```
## Mašinų API metodai
### GET /cars
Gražina sąrašą esamų skelbimų, funkcija prieinama prisijungusiems naudotojams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/cars`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
|Unauthorized  |401     |
#### Užklausos pavyzdys
`GET https://autodaug.azurewebsites.net/api/cars`
#### Atsakymo pavyzdys
```
[
  {
    "id": 2,
    "make": "carmake",
    "model": "carmodel",
    "manufactureDate": "2015-05-05",
    "milage": "64123",
    "gasType": "Benzinas",
    "engine": "1.9",
    "color": "Balta",
    "gearbox": "Automatinė",
    "advert_Id": 3,
    "user_Id": 4
  }
]
```
### GET /cars/{id}
Gražina mašiną, pagal id, kuris perduodamas per URL, funkcija prieinama prisijungusiems naudotojams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/cars/{id}`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
|Unauthorized  |401     |
|Not found     |404     |
#### Užklausos pavyzdys
`GET https://autodaug.azurewebsites.net/api/cars/5`
#### Atsakymo pavyzdys
```
{
  "id": 2,
  "make": "carmake",
  "model": "carmodel",
  "manufactureDate": "2015-05-05",
  "milage": "64123",
  "gasType": "Benzinas",
  "engine": "1.9",
  "color": "Balta",
  "gearbox": "Automatinė",
  "advert_Id": 3,
  "user_Id": 4
}
```
### POST /cars
Sukuria naują mašiną su nurodytais parametrais, funkcija prieinama prisijungusiems naudotojams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/cars`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|No Content    |201     |
|Bad request   |400     |
|Unauthorized  |401     |
#### Parametrai
|Pavadinimas   |Ar būtinas?   |Apibūdinimas   |Pavyzdys   |
| ------------ | ------------ | ------------- | --------- |
|make      |Taip          |Mašinos markė   | `markė`   |
|model   |Taip          |Mašinos modelis   | `modelis`   |
|manufactureDate     |Taip    |Mašinos pagaminimo data    | `2015-05-05`   |
|milage     |Taip    |Mašinos rida    | `64123`   |
|gasType     |Taip    |Mašinos kuro tipas    | `Benzinas`   |
|engine     |Taip    |Mašinos variklio darbinis tūris    | `1.9`   |
|color     |Taip    |Mašinos spalva    | `Balta`   |
|gearbox     |Taip    |Mašinos greičių dėžės tipas    | `Automatinė`   |
|advert_Id    |Taip    |Mašinos seklbimo id    | `3`    |
|user_Id     | Taip     |Naudotojo id, kuriam priklauso ši mašina     | `4`    |
#### Užklausos pavyzdys
`POST https://autodaug.azurewebsites.net/api/adverts`
```
{
  "make": "markė",
  "model": "modelis",
  "manufactureDate": "2015-05-05",
  "milage": "64123",
  "gasType": "Benzinas",
  "engine": "1.9",
  "color": "Balta",
  "gearbox": "Automatinė",
  "advert_Id": 3,
  "user_Id": 4
}
```
#### Atsakymo pavyzdys
```
{
  "id": 0,
  "make": "markė",
  "model": "modelis",
  "manufactureDate": "2015-05-05",
  "milage": "64123",
  "gasType": "Benzinas",
  "engine": "1.9",
  "color": "Balta",
  "gearbox": "Automatinė",
  "advert_Id": 3,
  "user_Id": 4
}
```
### PUT /cars/{id}
Atnaujiną mašiną su duotais parametrais, kurie buvo nurodyti užklausos metu, id kartu su URL, o kiti parametrai perudodami kartu su užklausos body, funkcija prieinama prisijungusiems naudotojams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/cars/{id}`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|OK            |200     |
|Bad request   |400     |
|Unauthorized  |401     |
#### Parametrai
|Pavadinimas   |Ar būtinas?   |Apibūdinimas   |Pavyzdys   |
| ------------ | ------------ | ------------- | --------- |
|make      |Taip          |Mašinos markė   | `markė`   |
|model   |Taip          |Mašinos modelis   | `modelis`   |
|manufactureDate     |Taip    |Mašinos pagaminimo data    | `2015-05-05`   |
|milage     |Taip    |Mašinos rida    | `64123`   |
|gasType     |Taip    |Mašinos kuro tipas    | `Benzinas`   |
|engine     |Taip    |Mašinos variklio darbinis tūris    | `1.9`   |
|color     |Taip    |Mašinos spalva    | `Balta`   |
|gearbox     |Taip    |Mašinos greičių dėžės tipas    | `Automatinė`   |
|advert_Id    |Taip    |Mašinos seklbimo id    | `3`    |
#### Užklausos pavyzdys
`PUT https://autodaug.azurewebsites.net/api/cars/5`
```
{
  "make": "markė",
  "model": "modelis",
  "manufactureDate": "2015-05-05",
  "milage": "64123",
  "gasType": "Benzinas",
  "engine": "1.9",
  "color": "Balta",
  "gearbox": "Automatinė",
  "advert_Id": 3,
  "user_Id": 4
}
```
#### Atsakymo pavyzdys
```
Tuščias body su statuso kodu 200 Success
```
### DELETE /cars/{id}
Ištrina mašiną su nurodytu id per URL, funkcija prieinama prisijungusiems naudotojams
#### Metodo URL
`https://autodaug.azurewebsites.net/api/cars/{id}`
#### Atsakymų kodai
|Pavadinimas   |Kodas   |
| ------------ | ------ |
|No Content    |204     |
|Unauthorized  |401     |
|Not found     |404     |
#### Parametrai
|Pavadinimas   |Ar būtinas?   |Apibūdinimas   |Pavyzdys   |
| ------------ | ------------ | ------------- | --------- |
|id            |Taip          |Skelbimo id   | `5`       |
#### Užklausos pavyzdys
`DELETE https://autodaug.azurewebsites.net/api/cars/5`
#### Atsakymo pavyzdys
```
Tuščias body su statuso kodu 204 No content
```
# Naudotojo sąsaja
## Prisijungimo langas
![image](https://user-images.githubusercontent.com/79359651/206660603-a04ce34a-0db7-4069-8e88-510b06091ea7.png)
Prisijungus nueinama į pagrindinį puslapį priklausomai pagal rolę, taip pat iš šio lango galima ir užsiregistruoti į sistemą

## Registracijos langas
![image](https://user-images.githubusercontent.com/79359651/206661058-dcfb80c3-d902-4c87-8efd-9fc3c1bf9d75.png)
Registracijos lange įvedus informaciją gaunamas pranešimas, jog naudotojas užregistruotas sėkmingai ir reikia laukti administratoriaus patvirtinimo

## Modalinis naudongos informacijos langas
![image](https://user-images.githubusercontent.com/79359651/206661436-e3d2504a-3f2a-4f49-bbed-5b1411498b38.png)
Paspaudus „Useful info“ mygtuką, atidaromas šis modalinis langas, kuriame galite matyti informaciją norint prisiregistruoti

## Administratoriaus pagrindinis langas
![image](https://user-images.githubusercontent.com/79359651/206893331-e9f1d6fe-e7b9-4ec2-9e91-fd6908e9b982.png)
Administratoriui prisijungus matomas šis vaizdas su administratoriaus navigacijos juosta ir pagrindine tinklapio funkcija, naršyti skelbimus pagal tipą

## Visų naudotojų langas
![image](https://user-images.githubusercontent.com/79359651/206893394-af964330-5d31-4172-89bb-f1d949469d5a.png)
Šiame lange administratorius paspaudęs ant Confirm user mygtuko patvirtina naudotojo registraciją ir jo statusas pasikeičia į patvirtintą. Taip pat, administratorius gali šalinti naudotojus iš sistemos šiame lange

## Skelbimų tipų langas
![image](https://user-images.githubusercontent.com/79359651/206893455-5adbddcf-ea56-4832-9a24-4968488fb128.png)
Šiame lange administratorius gali peržiūrėti visus skelbimo tipus, ištrinti skelbimo tipą jei jis neturi skelbimų, redaguoti kurį nors skelbimą arba pridėti naują

## Skelbimo tipo redagavimo langas
![image](https://user-images.githubusercontent.com/79359651/206893513-d1603e9d-4d9d-4d0d-931f-bbd6c8e60914.png)
Šiame lange redaguojama skelbimo tipo informacija, į laukus automatiškai yra užpildoma esama informacija ir ją galima koreguoti. Baigus redagavimą renkamės Confirm data mygtuką ir duomenys bus atnaujinti, priešingu atveju galime grįžti į skelbimų tipu lentelę

## Naujo skelbimo pridėjimo langas
![image](https://user-images.githubusercontent.com/79359651/206893608-094cd80a-481a-4957-885d-beb5ea728844.png)
Šiame lange galime įvesti informaciją naujam skelbimo tipui pridėti

## Visų skelbimų langas
![image](https://user-images.githubusercontent.com/79359651/206893635-72e1bcaf-37f4-487e-94f0-41fd143e292d.png)
Šiame lange administratorius gali matyti visus skelbimus esamus sistemoje, iš čia galima pasirinkti juos redaguoti arba pašalinti, jei skelbimas neturi automobilio

## Visų mašinų langas
![image](https://user-images.githubusercontent.com/79359651/206893690-af9ddf7c-904b-4a20-82c9-3f67414cf6b1.png)
Šiame lange administratorius gali matyti visus automobilius esamus sistemoje, iš čia galima pasirinkti juos redaguoti arba pašalinti

## Naudotojo pagrindinis langas
![image](https://user-images.githubusercontent.com/79359651/206893745-fb0f23ef-d0c4-45ef-acf9-c0cd3b2dbcb2.png)
Šis langas yra matomas paprastam naudotojui, kuris yra užsiregistravęs ir jo registracija buvo patvirtinta, rodomas navigacijos meniu, kuriame naudotojas gali matyti savo automobilius ir savo skelbimus, bei koreguoti savo profilio informaciją

## Profilio informacijos langas
![image](https://user-images.githubusercontent.com/79359651/206893805-5f5cef22-4764-4d01-9af9-6d5b6f039580.png)
Šiame lange naudotojas gali koreguoti savo profilio informaciją, patvirtinus profilio informacija yra pakeičiama ir jei naudotojo vardas buvo pakeistas, reikia prisijungti jau nauju naudotojo vardu

## Naudotojo skelbimų langas
![image](https://user-images.githubusercontent.com/79359651/206893886-ddc62d38-ffaa-4475-9b8a-705da9816126.png)
Šiame lange naudotojas gali pridėti naują skelbimą, matyti jau esamus skelbimus, pasirinkti vieną iš jų redagavimui arba pašalinimui su sąlyga, kad šis neturi jokių automobilių

## Skelbimo redagavimo langas
![image](https://user-images.githubusercontent.com/79359651/206893917-6a3a4263-6409-43f6-8138-1820d8ffe005.png)
Šiame lange naudotojas gali redaguoti savo skelbimą, informacija užpildoma automatiškai

## Naujo skelbimo pridėjimo langas
![image](https://user-images.githubusercontent.com/79359651/206893956-821e6e05-7a91-4344-9bf1-11299028102e.png)
Šiame lange naudotojas gali pridėti naują skelbimą

## Naudotojo automobilių langas
![image](https://user-images.githubusercontent.com/79359651/206893982-dba74605-7018-47f4-9c57-5813829fb4b7.png)
Šiame lange naudotojas gali matyti visus savo automobilius, pridėti naują automobilį, pasirinkti vieną iš jų redaguoti arba pašalinti

## Automobilio redagavimo langas
![image](https://user-images.githubusercontent.com/79359651/206894028-86adcd8f-b528-4c3e-9e1f-6bdc6fc5ee7b.png)
Šiame lange naudotojas gali redaguoti savo automobilio informaciją, ji yra užpildoma automatiškai

## Naujo automobilio langas
![image](https://user-images.githubusercontent.com/79359651/206894051-372212d0-b438-4c01-b4f4-95e41e680ae6.png)
Šiame lange naudotojas gali pridėti naują automobilį į sistemą ir priskirti jam skelbimą

## Skelbimų naršymas
![image](https://user-images.githubusercontent.com/79359651/206894102-4609d865-3657-4bda-b132-ce67c6e69d2e.png)
Pasirinkus skelbimo tipą ir paspaudus mygtuką Search, gaunamas langas skelbimų pagal pasirinktą tipą

![image](https://user-images.githubusercontent.com/79359651/206894127-617f769a-f405-4416-8dd2-2e9506cf816a.png)
Paspaudus ant kurio nors skelbimo galime matyti detalesnę jo informaciją

![image](https://user-images.githubusercontent.com/79359651/206894163-59a7a243-a96e-41e7-b77f-066cd4124f87.png)
Čia yra skelbimo su automobiliu langas

![image](https://user-images.githubusercontent.com/79359651/206894181-80f3e6e8-2bb8-4aa2-a9d7-806b6cb2add2.png)
Čia yra skelbimo be automobilio langas

## Tinklapio pritaikymas mobiliems įrenginiams
![image](https://user-images.githubusercontent.com/79359651/206894214-4bf0b5c3-1f3d-4425-9680-ed5c591717e8.png)
![image](https://user-images.githubusercontent.com/79359651/206894232-a9595c3e-5bc4-423e-b0a9-cae2958ff1aa.png)

Navigacijos meniu pasikeičia, jei yra tinklapis yra prieinamas iš mobilaus įrenginio

# Išvados
Šiame modulyje pavyko realizuoti skelbimų sistemą, bei geriau išmokti front-end technologijas ir kaip patalpinti sistemą į debesis, kad pastaroji būtų prieinama visiems. Kadangi .NET karkasą žinau gerai ir turiu patirties su tuo, back-end pusę implementuoti nebuvo sudėtinga, tačiau daugiau darbo ir pastangų reikėjo įdėti ties front-end puse, nes šis darbas man mažiau patinka ir nematau savęs front-end'o srityje. Bet baigiant projektą tos žinios buvo pagerintos ir einant į pabaigą buvo šiek tiek lengviau.
