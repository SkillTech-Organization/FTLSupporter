select * into ETL_ETOLL_2018 from ETL_ETOLL
/*
1 Aut�p�lya
2 Aut��t
3 Fo�tvonal
4 Mell�k�t
5 Egy�b al�rendelt �t
6 V�ros (utca)
7 Fel/lehajt�k, r�mp�k
*/

select RDT_VALUE, * from ETL_ETOLL ETL
inner join EDG_EDGE EDG on EDG.EDG_ETLCODE = ETL.ETL_CODE
where EDG.RDT_VALUE in (7) and ETL_J2_TOLL_KM > 48

update	ETL
set ETL_J2_TOLL_KM = 55.44, ETL_J3_TOLL_KM = 77.78, ETL_J4_TOLL_KM  = 120.4
from ETL_ETOLL ETL
inner join EDG_EDGE EDG on EDG.EDG_ETLCODE = ETL.ETL_CODE
where EDG.RDT_VALUE in (1,2)


update	ETL
set ETL_J2_TOLL_KM = 23.58, ETL_J3_TOLL_KM = 40.83, ETL_J4_TOLL_KM  = 75.10
from ETL_ETOLL ETL
inner join EDG_EDGE EDG on EDG.EDG_ETLCODE = ETL.ETL_CODE
where EDG.RDT_VALUE in (3,4,5,6)

update	ETL_ETOLL 
set ETL_J2_TOLL_FULL = ETL_J2_TOLL_KM*ETL_LEN_KM/1000, 
ETL_J3_TOLL_FULL = ETL_J3_TOLL_KM*ETL_LEN_KM/1000, 
ETL_J4_TOLL_FULL  = ETL_J4_TOLL_KM*ETL_LEN_KM/1000


