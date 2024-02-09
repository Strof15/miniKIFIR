using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniKIFIR
{
    public class Students : IFelvetelizo
    {
        string oM_Azonosito;
        string neve;
        string email; 
        DateTime szuletesiDatum;
        string ertesitesiCime;
        int matematika;
        int magyar;

        public Students(string oM_Azonosito, string neve, string email, DateTime szuletesiDatum, string ertesitesiCime, int matematika, int magyar)
        {
            this.oM_Azonosito = oM_Azonosito;
            this.neve = neve;
            this.email = email;
            this.szuletesiDatum = szuletesiDatum;
            this.ertesitesiCime = ertesitesiCime;
            this.matematika = matematika;
            this.magyar = magyar;
        }

        public string OM_Azonosito { get => oM_Azonosito; set => oM_Azonosito = value; }
        public string Neve { get => neve; set => neve = value; }
        public string Email { get => email; set => email = value; }
        public DateTime SzuletesiDatum { get => szuletesiDatum; set => szuletesiDatum = value; }
        public string ErtesitesiCime { get => ertesitesiCime; set => ertesitesiCime = value; }
        public int Matematika { get => matematika; set => matematika = value; }
        public int Magyar { get => magyar; set => magyar = value; }

        public string CSVSortAdVissza()
        {
            string line = $"{oM_Azonosito};{neve};{email};{szuletesiDatum.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)};{ertesitesiCime};{matematika};{magyar}";
            return line;
        }

        public void ModositCSVSorral(string csvString)
        {
            string[] studentArray = csvString.Split(',');
            if (studentArray.Length == 7)
            {
                oM_Azonosito = studentArray[0];
                neve = studentArray[1];
                email = studentArray[2];
                szuletesiDatum = DateTime.ParseExact(studentArray[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                ertesitesiCime = studentArray[4];
                matematika = int.Parse(studentArray[5]);
                magyar = int.Parse(studentArray[6]);
            }
        }
    }
}
