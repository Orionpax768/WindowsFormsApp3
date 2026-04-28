using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Dictionary<string, List<string>> emails = new Dictionary<string, List<string>>();

        private void FillDefaultData()
        {
            emails.Clear();
            emails.Add("mail.ru", new List<string> { "andrei_boiko", "alexander_lipov", "elena_belova", "kirill_stepanov" });
            emails.Add("gmail.com", new List<string> { "alena.polekhina", "ivan.pohomov", "marina_abrabova" });
            emails.Add("yandex.ru", new List<string> { "artem.dotovich", "aleksander_kssovich", "baraka_abama" });
        }
        private void AddEmailToDictionary(string username, string domain)
        {
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(domain))
            {
                throw new ArgumentException("Имя пользователя и домен не могут быть пустыми!");
            }

            if (emails.ContainsKey(domain))
            {
                if (!emails[domain].Contains(username))
                {
                    emails[domain].Add(username);
                }
            }
            else
            {
                emails.Add(domain, new List<string> { username });
            }
        }
        private List<string> GetSortedEmails()
        {
            List<string> fullEmails = new List<string>();

            foreach (var pair in emails)
            {
                string domain = pair.Key;
                foreach (var user in pair.Value)
                {
                    fullEmails.Add($"{user}@{domain}");
                }
            }
            fullEmails.Sort();
            return fullEmails;
        }
        private void OutputToInterface(List<string> emailList)
        {
            listBox1.Items.Clear();
            foreach (var email in emailList)
            {
                listBox1.Items.Add(email);
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                FillDefaultData();
                GenerateDomen.Items.Clear();
                GenerateDomen.Items.Add("mail.ru");
                GenerateDomen.Items.Add("gmail.com");
                GenerateDomen.Items.Add("yandex.ru");
                if (GenerateDomen.Items.Count > 0)
                    GenerateDomen.SelectedIndex = 0;
                List<string> sortedList = GetSortedEmails();
                OutputToInterface(sortedList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string user = textBoxUser.Text.Trim();
                string dom = GenerateDomen.Text.Trim();
                AddEmailToDictionary(user, dom);
                List<string> sortedList = GetSortedEmails();
                OutputToInterface(sortedList);
            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show(argEx.Message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            emails.Clear();
            listBox1.Items.Clear();
            textBoxUser.Clear();
        }
    }
}