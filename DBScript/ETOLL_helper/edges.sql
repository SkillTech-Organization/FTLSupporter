select PTP.ID, TOD.ID, PTP_ORDER, PTP.NOD_ID, PTP_DISTANCE, WHS.WHS_NAME, WHS.EDG_ID, DEP.DEP_NAME, DEP.EDG_ID, DST.ID, DST_DISTANCE, DST_EDGES
from PTP_PLANTOURPOINT PTP
inner join TPL_TRUCKPLAN TPL on TPL.ID = PTP.TPL_ID
inner join TRK_TRUCK TRK on TRK.ID = TPL.TRK_ID
inner join TTP_TRUCKTYPE TTP on TTP.ID = TRK.TTP_ID
left outer join WHS_WAREHOUSE WHS on WHS.ID = PTP.WHS_ID
left outer join TOD_TOURORDER TOD on TOD.ID = PTP.TOD_ID
left outer join DEP_DEPOT DEP on DEP.ID = TOD.DEP_ID
left outer join DST_DISTANCE DST on DST.SPP_ID = TTP.SPP_ID and 
				DST.NOD_ID_FROM = (select PTPF.NOD_ID from PTP_PLANTOURPOINT PTPF where PTPF.TPL_ID = PTP.TPL_ID and PTPF.PTP_ORDER=PTP.PTP_ORDER-1) and
				DST.NOD_ID_TO = PTP.NOD_ID
where PTP.TPL_ID = 12292 and PTP_DISTANCE>0
order by PTP_ORDER 