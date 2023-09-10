using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Banca_del_Tempo
{
    internal class Banca
    {
        private Dictionary<string, int> categorie= new Dictionary<string, int>();
        private Elenco_Associati segreteria;
        public Banca(Elenco_Associati _segreteria) { 
            Segreteria = _segreteria;
        }

        public Elenco_Associati Segreteria
        {
            get { return segreteria; }
            set { segreteria = value; }
        }

        public void Transizione(string richiedente, string accettatore, int pagamento)
        {
            foreach(Utente persona in segreteria.Associati){
                if (persona.Nome_Cognome == richiedente)
                {
                    persona.Diminuisci_Ore(pagamento);
                }
                if (persona.Nome_Cognome == accettatore)
                {
                    persona.Aggiungi_Ore(pagamento);
                }
            }
            Salva_su_JSON();
        }

        public void Ricarica(string richiedente, int pagamento)
        {
            foreach (Utente persona in segreteria.Associati)
            {
                if (persona.Nome_Cognome == richiedente)
                {
                    persona.Aggiungi_Ore(pagamento);
                }
            }
            Salva_su_JSON();
        }

        public void Aggiungi_utente(string nome_cognome, string telefono, int bilancio_iniziale)
        {
            Utente NuovoUtente = new Utente(nome_cognome, telefono, bilancio_iniziale);
            segreteria.Aggiungi_associato(NuovoUtente);
            Salva_su_JSON();
        }

        public void Elimina_utente(Utente DaEliminare)
        {
            segreteria.Associati.Remove(DaEliminare);
            Salva_su_JSON();
        }

        public void Salva_su_JSON()
        {
            string fileName = "lista_associati.json";
            string json = JsonConvert.SerializeObject(segreteria.Associati);

            //write string to file
            System.IO.File.WriteAllText(fileName, json);
        }
    }
}
