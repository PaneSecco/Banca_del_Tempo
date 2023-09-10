using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Banca_del_Tempo
{
    internal class Utente
    {
        //attributi
        private string nome_cognome;
        private string telefono;
        private int ore;

        //costruttore
        public Utente(string _nome_cognome, string _telefono, int _ore) {
            Nome_Cognome = _nome_cognome;
            Telefono = _telefono;
            Ore = _ore;
        }

        //properties

        [JsonProperty("nome e cognome")]
        public string Nome_Cognome{

            get { return nome_cognome;} 
            private set { nome_cognome = value;}
        }

        [JsonProperty("telefono")]
        public  string Telefono
        {

            get { return telefono; }
            private set { telefono = value; }
        }

        [JsonProperty("ore")]
        public  int Ore
        {
            get { return ore; }
            private set { ore = value; }
        }

        //metodi

        public void Aggiungi_Ore(int aggiunta)
        {
            Ore = Ore + aggiunta;
        }

        public void Diminuisci_Ore(int debito)
        {
            Ore = Ore - debito;
        }


    }
}
