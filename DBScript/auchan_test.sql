
declare @PLN_ID int

--globál eredmény
--Túra sorszáma	Rendszám	Indulási idõ	Érkezési idõ	Megrendelések száma	Összes mennyiség I.	Összes mennyiség II.	Futott távolság	Költség
--1	AUCH1	6:45	13:10	8	55	1200	600	61000

select DENSE_RANK ( ) OVER ( ORDER BY TPL.ID ) as Túra_sorszáma, * 
from PLN_PUBLICATEDPLAN PLN
inner join TPL_TRUCKPLAN TPL on TPL.PLN_ID = PLN.ID

inner join PTP_PLANTOURPOINT PTP on PTP.TPL_ID = PLN.ID
where PLN.ID = 644