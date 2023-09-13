using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Net.Mime.MediaTypeNames;


namespace Banca_del_Tempo
{
    public partial class Form1 : Form
    {
        Elenco_Associati lista_associati;
        Banca Banca1;
        List<Ricevuta> elenco_ricevute;

        string[] tipi_di_categorie = { "Piccoli lavori" , "Trasporto", "Cura di una persona", "Scolastico"};
        string[] tipi_di_viste = { "Segreteria", "Persone indebitate","Bilancio categorie" };
        string[] tipi_di_azioni = { "Esegui transizione", "Ricarica" , "Aggiungi utente" , "Elimina utente" };
        public Form1()
        {
            InitializeComponent();

            label5.Parent = pictureBox1;
            label5.BackColor = Color.Transparent;

            this.pictureBox1.SendToBack();

            lista_associati =new Elenco_Associati();
            elenco_ricevute=new List<Ricevuta>();

            string fileName = "lista_associati.json";
            var filetext = File.ReadAllText(fileName);
            var NEWfiletext1 = JsonConvert.DeserializeObject<List<Utente>>(filetext);

            foreach (Utente persona in NEWfiletext1)
            {
                lista_associati.Aggiungi_associato(persona);
            }

            Banca1= new Banca(lista_associati);

            fileName = "elenco_ricevute.json";
            filetext = File.ReadAllText(fileName);
            var NEWfiletext2 = JsonConvert.DeserializeObject<List<Ricevuta>>(filetext);

            foreach (Ricevuta persona in NEWfiletext2)
            {
                elenco_ricevute.Add(persona);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //oggetti della combobox (1) utili per cambiare la "vista" della listview

            comboBox1.Items.Add("Segreteria");
            comboBox1.Items.Add("Persone indebitate");
            comboBox1.Items.Add("Bilancio categorie");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            //oggetti della combobox (2) utili per cambiare il tipo di azione che si può compiere

            comboBox2.Items.Add("Esegui transizione");
            comboBox2.Items.Add("Ricarica");
            comboBox2.Items.Add("Aggiungi utente");
            comboBox2.Items.Add("Elimina utente");
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            //oggetti della combobox (3) utili per cambiare il tipo di categoria in cui si può fare una prestazione

            foreach(string sezione in tipi_di_categorie)
            {
                comboBox3.Items.Add(sezione);
            }
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;

            //posiziona come default la scelta "vedi tute le persone iscritte a questa segreteria" e "fai transizione"

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //se è sulle categorie non fare
            if (comboBox2.SelectedItem=="Elimina utente" && listView1.SelectedItems.Count!=0 && comboBox1.SelectedItem!="Bilancio categorie")
            {
                textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
                textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
            }
            if (comboBox2.SelectedItem == "Ricarica" && listView1.SelectedItems.Count != 0 && comboBox1.SelectedItem != "Bilancio categorie")
            {
                textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //creazione dei campi pertinenti alla richiesta

            if (comboBox1.SelectedItem.ToString() == "Segreteria")
            {
                cambia_listview_Segreteria();
            }

            if (comboBox1.SelectedItem.ToString() == "Persone indebitate")
            {
                cambia_listview_Persone_indebitate();
            }

            if (comboBox1.SelectedItem.ToString() == "Bilancio categorie")
            {
                cambia_listview_Bilancio_categorie();
            }
        }

        public void cambia_listview_Segreteria()
        {
            listView1.Clear();
            listView1.Columns.Add("Nome & Cognome", 150);
            listView1.Columns.Add("Telefono", 150);
            listView1.Columns.Add("Bilancio", 100);

            int volte = Banca1.Segreteria.Associati.Count;

            for (int i = 0; i < volte; i++)
            {
                string[] row = { Banca1.Segreteria.Associati[i].Nome_Cognome, Banca1.Segreteria.Associati[i].Telefono, Banca1.Segreteria.Associati[i].Ore.ToString() };
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
            }
        }
        public void cambia_listview_Persone_indebitate()
        {
            listView1.Clear();
            listView1.Columns.Add("Nome & Cognome", 150);
            listView1.Columns.Add("Telefono", 150);
            listView1.Columns.Add("Bilancio", 150);

            int volte = Banca1.Segreteria.Associati.Count;

            for (int i = 0; i < volte; i++)
            {
                if (Banca1.Segreteria.Associati[i].Ore < 0)
                {
                    string[] row = { Banca1.Segreteria.Associati[i].Nome_Cognome, Banca1.Segreteria.Associati[i].Telefono, Banca1.Segreteria.Associati[i].Ore.ToString() };
                    var listViewItem = new ListViewItem(row);
                    listView1.Items.Add(listViewItem);
                }
            }

        }
        public void cambia_listview_Bilancio_categorie()
        {
            listView1.Clear();
            listView1.Columns.Add("Categoria", 250);
            listView1.Columns.Add("Bilancio", 150);

            Dictionary<string, int> tendenze= new Dictionary<string, int>();

            foreach(string categoria in tipi_di_categorie)
            {
                tendenze.Add(categoria, 0);
            }

            foreach(Ricevuta singolo in elenco_ricevute)
            {
                singolo.Restituisci_resoconto(tendenze);
            }

            int volte=tendenze.Count;
            for(int i=0; i<volte; i++)
            {
                string[] row = { tipi_di_categorie[i], tendenze[tipi_di_categorie[i]].ToString()};
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "Esegui transizione")
            { 
                if (Controlla_dati() == true)
                {
                    Banca1.Transizione(textBox1.Text, textBox2.Text, int.Parse(textBox3.Text));
                    Ricevuta ricevuta1 = new Ricevuta(textBox1.Text, textBox2.Text, comboBox3.SelectedItem.ToString(), int.Parse(textBox3.Text));
                    elenco_ricevute.Add(ricevuta1);

                    string fileName = "elenco_ricevute.json";
                    string json = JsonConvert.SerializeObject(elenco_ricevute);
                    System.IO.File.WriteAllText(fileName, json);

                    cambia_listview_Segreteria();

                    //svuoto quello che era stato precedentemente inserito
                    textBox1.Text = null;
                    textBox2.Text = null;
                    textBox3.Text = null;
                }
            }
            if (comboBox2.SelectedItem.ToString() == "Ricarica")
            {
                if (Controlla_dati() == true)
                {
                    Banca1.Ricarica(textBox1.Text, int.Parse(textBox3.Text));
                    cambia_listview_Segreteria();
                    textBox1.Text = null;
                    textBox2.Text = null;
                    textBox3.Text = null;
                }
            }
            if (comboBox2.SelectedItem.ToString() == "Aggiungi utente")
            {
                if (Controlla_dati() == true)
                {
                    Banca1.Aggiungi_utente(textBox1.Text, textBox2.Text, int.Parse(textBox3.Text));
                    cambia_listview_Segreteria();
                    textBox1.Text = null;
                    textBox2.Text = null;
                    textBox3.Text = null;
                }
            }
            if (comboBox2.SelectedItem.ToString() == "Elimina utente")
            {
                if (Controlla_dati() == true)
                {
                    string nominativo=textBox1.Text;
                    textBox1.Text = null;
                    textBox2.Text = null;
                    textBox3.Text = null;
                    foreach (Utente persona in Banca1.Segreteria.Associati)
                    {
                        if (persona.Nome_Cognome == nominativo)
                        {
                            Banca1.Elimina_utente(persona);
                            cambia_listview_Segreteria();
                            return;
                        }
                    }
                }
            }
        }

        public bool Controlla_dati()
        {
            if (comboBox2.SelectedItem.ToString() == "Esegui transizione")
            {
                cambia_listview_Segreteria();

                if(textBox1.Text== textBox2.Text)
                {
                    MessageBox.Show("non è possibile effettuare un pagamento tra un utente e il medesimo");
                    return false;
                }

                string richiedente=textBox1.Text;
                string accettatore=textBox2.Text;
                int ore=int.Parse(textBox3.Text);

                //sistemazione del nome del richiedente
                char[] spearator = { ' ' };
                string[] strlist = richiedente.Split(spearator);

                bool maiuscola = true;
                string finale_richi=null;

                int contatore = 0;

                foreach (string s in strlist)
                {
                    contatore++;
                    finale_richi= finale_richi + s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
                    if (contatore != strlist.Length)
                    {
                        finale_richi = finale_richi + " ";
                    }
                }

                textBox1.Text = finale_richi;

                //sistemazione del nome dell'accettatore

                strlist = accettatore.Split(spearator);
                maiuscola = true;
                string finale_acc = null;

                contatore = 0;

                foreach (string s in strlist)
                {
                    contatore++;
                    finale_acc = finale_acc + s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
                    if (contatore != strlist.Length)
                    {
                        finale_acc = finale_acc + " ";
                    }
                }

                textBox2.Text = finale_acc; 

                if (ore<=0)
                {
                    MessageBox.Show("Le ore inserite devono essere superiori a 0");
                    return false;
                }
                return true;

            }
            if (comboBox2.SelectedItem.ToString() == "Ricarica")
            {
                int ore = int.Parse(textBox3.Text);
                if(ore<=0)
                {
                    MessageBox.Show("Le ore inserite non possono essere negative");
                    return false;
                }
                return true;
            }
            if (comboBox2.SelectedItem.ToString() == "Aggiungi utente")
            {
                string nominativo = textBox1.Text;
                string telefono= textBox2.Text;
                int ore = int.Parse(textBox3.Text);

                //sistemazione del nome del richiedente
                char[] spearator = { ' ' };
                string[] strlist = nominativo.Split(spearator);

                bool maiuscola = true;
                string finale = null;

                int contatore = 0;

                foreach (string s in strlist)
                {
                    contatore++;
                    finale = finale + s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
                    if(contatore!=strlist.Length)
                    {
                        finale = finale +" ";
                    }
                }

                textBox1.Text = finale;

                //controllo telefono

                string regex = ".*[a-zA-Z].*"; // regex to check if string contains any letters

                bool result = Regex.IsMatch(telefono, regex);

                if (result == true || telefono.Length!= 10 || telefono.Length != 11)
                {
                    MessageBox.Show("il numero di telefono deve contenere solo numeri e deve essere da 10 o 11 cifre");
                    return false;
                }

                //controllo ore

                if (ore < 3)
                {
                    MessageBox.Show("Le ore inserite devono essere superiori o uguali a 3");
                    return false;
                }

                return true;
            }
            if (comboBox2.SelectedItem.ToString() == "Elimina utente")
            {
                return true;
            }
            return false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //creazione dei campi pertinenti alla richiesta

            if (comboBox2.SelectedItem.ToString() == "Esegui transizione")
            {
                cambia_menu_Esegui_transizione();
            }
            if (comboBox2.SelectedItem.ToString() == "Ricarica")
            {
                cambia_menu_Ricarica();
            }
            if (comboBox2.SelectedItem.ToString() == "Aggiungi utente")
            {
                cambia_menu_Aggiungi_utente();
            }
            if (comboBox2.SelectedItem.ToString() == "Elimina utente")
            {
                cambia_menu_Elimina_utente();
            }

        }

        public void cambia_menu_Esegui_transizione()
        {
            label1.Visible = true; 
            label1.Text = "Richiedente (nome e cognome)";
            label2.Visible = true;
            label2.Text = "Accettatore (nome e cognome)";
            label3.Visible = true;
            label3.Text = "Richiesta per:";
            label4.Visible = true;
            label4.Text = "Tempo necessario:";

            textBox1.Visible = true;
            textBox1.Text = null;
            textBox1.Enabled = true;

            textBox2.Visible = true;
            textBox2.Text = null;
            textBox2.Enabled = true;

            textBox3.Visible = true;
            textBox3.Enabled = true;
            textBox3.Text = null;

            comboBox3.Visible = true;
        }
        public void cambia_menu_Ricarica()
        {
            label1.Visible = true;
            label1.Text = "Richiedente (nome e cognome)";
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = true;
            label4.Text = "Aumento ore (le ore verranno addizionate):";

            textBox1.Visible = true;
            textBox1.Text = null;
            textBox1.Enabled = false;

            textBox2.Visible = false;

            textBox3.Visible = true;
            textBox3.Enabled = true;
            textBox3.Text = null;

            comboBox3.Visible = false;
        }

        public void cambia_menu_Aggiungi_utente()
        {
            label1.Visible = true;
            label1.Text = "Nome e cognome";
            label2.Visible = true;
            label2.Text = "Telefono";
            label3.Visible = false;
            label4.Visible = true;
            label4.Text = "Tempo default (minimo 3 ore):";

            textBox1.Visible = true;
            textBox1.Text = null;
            textBox1.Enabled = true;

            textBox2.Visible = true;
            textBox2.Text = null;
            textBox2.Enabled = true;

            textBox3.Visible = true;
            textBox3.Enabled = true;
            textBox3.Text = null;

            comboBox3.Visible = false;
        }

        public void cambia_menu_Elimina_utente()
        {
            label1.Visible = true;
            label1.Text = "Nome utente";
            label2.Visible = true;
            label2.Text = "Telefono";
            label3.Visible = false;
            label4.Visible = true;
            label4.Text = "Tempo attuale (non possibile se meno di 0):";

            textBox1.Visible = true;
            textBox1.Text = null;
            textBox1.Enabled = false;

            textBox2.Visible = true;
            textBox2.Text = null;
            textBox2.Enabled = false;

            textBox3.Visible = true;
            textBox3.Enabled = false;
            textBox3.Text = null;

            comboBox3.Visible = false;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string elenco_completo = null;

            foreach(Ricevuta singola in elenco_ricevute)
            {
                elenco_completo+= "\n" + singola.Ricevuta_transizione() + "\n"+ "------------------------------" + "\n";
            }

            MessageBox.Show(elenco_completo);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 secondForm = new Form2(tipi_di_viste, tipi_di_azioni);

            secondForm.Show();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
