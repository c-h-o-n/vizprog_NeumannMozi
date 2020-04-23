
# Általános	útmutató
**Felület	megkötések**
- A	felületet	létrehozásához	Windows	Presentation	Foundation	(WPF)	technológiát	
használjon.
- Program	legyen	menüvezérelt,	az	egyes	funkciókat	pedig	a	menüelemek és	eszköztár
segítségével	érje	el	a	felhasználó.
- A	különböző	funkciókat	valósítsa	meg	külön	ablakban	vagy UserControl-ok	segítségével	
egyetlen	ablakon	belül.

**Adatbázis	megkötések**
- Adatbázist	hozza	létre	MS	SQL	LocalDB	segítségével	úgy,	hogy	az	adatbázis	állomány	a	
projekt	könyvtárában	legyen.	Legalább	harmadik	normálformában	legyen	(3NF)	az	
adatbázis.
- Használjon	Entity	Framework-öt	(Code	first	vagy	Database	first).
- Lekérdezéseket	LINQ-val	valósítsa	meg.
# Mozijegy
**Feladatleírás**
- Készítsen	 egy	 olyan	 grafikus	 felületű asztali	 alkalmazást,	 amely	 egy	 mozi	 jegyeladásifoglalási	rendszerét	valósítja	meg.	A	mozi	több	teremmel	rendelkezzen,	aminek	leírását	
(termek,	sorok	és	soronkénti	székek	száma,	stb.)	egy	XML	állományban	lehessen	megadni	
(új	 helyszín	 esetén	 ne	 kelljen	 átírni	 a	 programot	 csak	 az XML	 állományt).	 Lehessen	
megadni	 és	 áttekinteni	 a	 műsort.	 A	 műsor	 nyomtatható	 legyen	 PDF-be.	 Készítsen	
kimutatást	 a	 termek	 kihasználtságáról	 és	 idősávok,	 valamint	 az	 egyes	 filmek	
látogatottságáról.
