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
`https://autodaug.azurewebsites.net/api/advertTypes`
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
Gražina skelbimo tipą, pagal id, kuris perduodamas per URL
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
### DELETE /advertType/{id}
Ištrina skelbimo tipą su nurodytu id per URL, funckija prieinama tik administratoriams
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
|id            |Taip          |Skelbimo tipo id   | `5`       |
#### Užklausos pavyzdys
`DELETE https://autodaug.azurewebsites.net/api/advertTypes/5`
#### Atsakymo pavyzdys
```
Tuščias body su statuso kodu 204 No content
```
### GET /advertTypes/{id}/adverts
Gražina sąrašą skelbimų pagal nurodytą skelbimo tipą
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

# Išvados
Šiame modulyje pavyko realizuoti skelbimų sistemą, bei geriau išmokti front-end technologijas ir kaip patalpinti sistemą į debesis, kad pastaroji būtų prieinama visiems. Kadangi .NET karkasą žinau gerai ir turiu patirties su tuo, back-end pusę implementuoti nebuvo sudėtinga, tačiau daugiau darbo ir pastangų reikėjo įdėti ties front-end puse, nes šis darbas man mažiau patinka ir nematau savęs front-end'o srityje. Bet baigiant projektą tos žinios buvo pagerintos ir einant į pabaigą buvo šiek tiek lengviau.
