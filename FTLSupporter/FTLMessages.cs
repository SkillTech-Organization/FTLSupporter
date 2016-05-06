using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTLSupporter
{
    public static class FTLMessages
    {
        public const string M_CALCROUTES = "Útvonalak számítása";
        public const string E_TRKTYPE = "Járműtípus miatt nem teljesítheti a túrát:";
        public const string E_TRKCARGOTYPE = "Árutípus miatt nem teljesítheti a túrát:";
        public const string E_TRKCAPACITY = "Kapacitás miatt nem teljesítheti a túrát:";
        public const string E_TRKCLOSETP = "Valamelyik túrapont már zárva:";
        public const string E_TRKWRONGCOMPLETED = "Helytelen teljesített túrapont érték !";
        public const string E_WRONGCOORD = "Helytelen koordináta!";
        public const string E_T1MISSROUTE = "Aktuális túra teljesítésénél hiányzó szakasz:";
        public const string E_RELMISSROUTE = "Átálásnál hiányzó szakasz:";
        public const string E_T2MISSROUTE = "Beosztandó túra teljesítésénél hiányzó szakasz:";
        public const string E_RETMISSROUTE = "Visszetérés teljesítésénél hiányzó szakasz:";

    }
}
