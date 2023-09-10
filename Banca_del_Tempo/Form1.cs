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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Banca_del_Tempo
{
    public partial class Form1 : Form
    {
        Elenco_Associati lista_associati;
        Banca Banca1;
        public Form1()
        {
            InitializeComponent();
            lista_associati=new Elenco_Associati();

            string fileName = "lista_associati.json";
            var filetext = File.ReadAllText(fileName);
            var NEWfiletext = JsonConvert.DeserializeObject<List<Utente>>(filetext);

            foreach (Utente persona in NEWfiletext)
            {
                lista_associati.Aggiungi_associato(persona);
                //MessageBox.Show(persona.Nome_Cognome);
            }

            //string json = JsonConvert.SerializeObject(NEWfiletext.ToArray());

            Banca1= new Banca(lista_associati);
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

            //posiziona come default la scelta "vedi tute le persone iscritte a questa segreteria" e "fai transizione"

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            /*
            Button button1 = new Button();
            Controls.Add(button1);
            button1.Text = "OK";
            button1.Bottom = 120;
            */
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem=="Elimina utente")
            {
                textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
                textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
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
            //MessageBox.Show("è stato selezionato " + comboBox1.SelectedItem.ToString());

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
            listView1.Columns.Add("Nome & Cognome", 100);
            listView1.Columns.Add("Telefono", 100);
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
            listView1.Columns.Add("Nome & Cognome", 100);
            listView1.Columns.Add("Telefono", 100);
            listView1.Columns.Add("Bilancio", 100);

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
            listView1.Columns.Add("Categoria", 100);
            listView1.Columns.Add("Bilancio", 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "Esegui transizione")
            {
                Banca1.Transizione(textBox1.Text, textBox2.Text, int.Parse(textBox3.Text));
                cambia_listview_Segreteria();
            }
            if (comboBox2.SelectedItem.ToString() == "Ricarica")
            {
                Banca1.Ricarica(textBox1.Text, int.Parse(textBox3.Text));
                cambia_listview_Segreteria();
            }
            if (comboBox2.SelectedItem.ToString() == "Aggiungi utente")
            {
                Banca1.Aggiungi_utente(textBox1.Text, textBox2.Text, int.Parse(textBox3.Text));
                cambia_listview_Segreteria();
            }
            if (comboBox2.SelectedItem.ToString() == "Elimina utente")
            {
                foreach (Utente persona in Banca1.Segreteria.Associati)
                {
                    if (persona.Nome_Cognome == textBox1.Text)
                    {
                        Banca1.Elimina_utente(persona);
                        cambia_listview_Segreteria();
                        return;
                    }
                }
            }
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
            label4.Text = "Aumento ore di prestazioni (le ore verranno addizionate):";

            textBox1.Visible = true;
            textBox1.Text = null;
            textBox1.Enabled = true;

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
            label4.Text = "Tempo attuale (se meno di 0 si ha un debito da saldare prima dell'uscita):";

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
    }
}
