using System;

namespace SusApi.Models
{
    public class SusFilter
    {
        protected SusFilter()
        {

        }

        public SusFilter(string modulo, string[] anos, string[] ufs, string[] meses = null)
        {
            Modulo = modulo;
            Anos = anos;
            Meses = meses ?? Array.Empty<string>();
            Ufs = ufs;
        }

        public string Modulo { get; set; }
        public string[] Anos { get; set; }
        public string[] Meses { get; set; }
        public string[] Ufs { get; set; }


    }
}
