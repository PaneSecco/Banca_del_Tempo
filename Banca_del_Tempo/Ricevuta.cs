using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca_del_Tempo
{
    internal class Ricevuta
    {
        private string richiedente;
        private string accettatore;
        private string categoria;
        private int costo;
        public Ricevuta(string _richiedente, string _accettatore, string _categoria, int _costo) 
        { 
            Richiedente = _richiedente;
            Accettatore = _accettatore;
            Categoria = _categoria;
            Costo = _costo;
        }
        public string Richiedente
        {
            get { return richiedente; }
            set { richiedente = value; }
        }

        public string Accettatore
        {
            get { return accettatore; }
            set { accettatore = value; }
        }
        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        public int Costo
        {
            get { return costo; }
            set { costo = value; }
        }

        public string Ricevuta_transizione()
        {
            string fattura = "La transizione effettuata tra " + Richiedente + " per una prestazione di tipo " + Categoria + " accettata da " + Accettatore + " si è conclusa con un pagamento di " + Costo + " ore.";
            return fattura;
        }
        public Dictionary<string, int> Restituisci_resoconto(Dictionary<string, int> resoconto)
        {
            resoconto[Categoria] += Costo;
            return resoconto;
        }
    }
}
