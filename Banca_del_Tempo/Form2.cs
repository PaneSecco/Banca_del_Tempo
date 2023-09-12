using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Banca_del_Tempo
{
    public partial class Form2 : Form
    {
        private string[] tipi_di_viste;
        private string[] tipi_di_azioni;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string[] _tipi_di_viste, string[] _tipi_di_azioni)
        {
            this.tipi_di_viste= _tipi_di_viste;
            this.tipi_di_azioni = _tipi_di_azioni;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //immetto le sezioni nella combobox (1)

            foreach(string element in tipi_di_viste)
            {
                comboBox1.Items.Add(element);
            }

            //faccio la stessa cosa per la combobox (2)

            foreach (string element in tipi_di_azioni)
            {
                comboBox2.Items.Add(element);
            }

            //non le rendo scrivibili

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            textBox1.Text = "Scegliere una sezione per poter vedere la guida";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Segreteria")
            {
                textBox1.Text = "Una volta selezionata la segreteria saranno fatti vedere tutti gli utenti iscritti ad essa, mostrando il loro nome e cognome, o in alcuni casi nomignolo, numero di telefono e bilancio attuale.";
            }
            if (comboBox1.SelectedItem.ToString() == "Persone indebitate")
            {
                textBox1.Text = "Una volta selezionata la vista 'Persone indebitate' verrà fatta comparire una lista identica alla segreteria ma che comprende solo le persone indebitate, quindi coloro che hanno un bilancio negativo";
            }
            if (comboBox1.SelectedItem.ToString() == "Bilancio categorie")
            {
                textBox1.Text = "Una volta selezionato il bilancio delle categorie verranno mostrate le ore che sono state usate dagli utenti per ricevere ogni prestazione";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "Esegui transizione")
            {
                textBox1.Text = "L'azione 'Esegui transizione' permetterà di azionare una transizione tra due utenti, con richiesta del loro nome e cognome, categoria dello scambio e le ore che l'accettatore richiede per la prestazione offerta, se uno dei due nomi non coincide con quelli presenti nella sergreteria non sarà fatta aluna transizione, inoltre le ore inserite non possono essere nulle o negative, per ovvie ragioni.";
            }
            if (comboBox2.SelectedItem.ToString() == "Ricarica")
            {
                textBox1.Text = "La 'Ricarica' permette ad un'utente di mettere più ore a disposizione del suo account, basterà selezionare il nome dalla listview (se è posizionata su visualizzazione 'segreteria' o 'Persone indebitate'), ed aggiungere le ore, ovviamente non da 0 in negativo";
            }
            if (comboBox2.SelectedItem.ToString() == "Aggiungi utente")
            {
                textBox1.Text = "L'aggiunta utente è autoesplicativa, indicando i campi di nome e cognome (le maiuscole verranno automaticamente sistemate dal programma in base agli spazi), telefono personale (se inserite lettere o numeri con cifre che non sono in totale 9 non verrà accettato) e il bilancio iniziale che deve essere come minimo di 3 ore";
            }
            if (comboBox2.SelectedItem.ToString() == "Elimina utente")
            {
                textBox1.Text = "L'elimina utente si può facilmente dedurre cosa fa, permette la selezione di un utente che verrà cancellato una volta finita l'operazione, le sue 'azioni', se fatte, saranno comunque salvate nella sezione fatture";
            }
        }
    }
}
