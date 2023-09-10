using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Banca_del_Tempo
{
    internal class Elenco_Associati
    {
        private List<Utente> associati=new List<Utente>();

        public Elenco_Associati() {
            //List<Utente> _associati
            //Associati = _associati;
        }

        public List<Utente> Associati
        {
            get { return associati; }
            set { associati = value; }
        }

        public void Aggiungi_associato(Utente utente)
        {
            Associati.Add(utente);
        }

        public void Rimuovi_associato(Utente utente) {

            if (Associati.IndexOf(utente) == -1)
            {
                throw new Exception("L'utente non esiste");
            }
            else
            {
                Associati.Remove(utente);
            }
        }

    }
}
