
declare @PLN_ID int

--glob�l eredm�ny
--T�ra sorsz�ma	Rendsz�m	Indul�si id�	�rkez�si id�	Megrendel�sek sz�ma	�sszes mennyis�g I.	�sszes mennyis�g II.	Futott t�vols�g	K�lts�g
--1	AUCH1	6:45	13:10	8	55	1200	600	61000

select DENSE_RANK ( ) OVER ( ORDER BY TPL.ID ) as T�ra_sorsz�ma, * 
from PLN_PUBLICATEDPLAN PLN
inner join TPL_TRUCKPLAN TPL on TPL.PLN_ID = PLN.ID

inner join PTP_PLANTOURPOINT PTP on PTP.TPL_ID = PLN.ID
where PLN.ID = 644