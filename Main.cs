using Oracle.ManagedDataAccess.Client;
using System;
using System.IO;
using System.Windows.Forms;

namespace ru.kemoms.Sp.ANTONIO
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog() { InitialDirectory = @"c:\", Multiselect = true ,Filter="XML|*.xml" };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    object pl;
                    foreach (string value in dlg.FileNames)
                    {
                        using (FileStream file = new FileStream(value, FileMode.Open, FileAccess.Read))
                            pl = XML.SpClass.FromXml(file, typeof(ANTONIO.Файл), Properties.Resources.Файл, null, null);
                        GC.Collect();
                        Ins(pl);
                        pl = null;
                        GC.Collect();
                    }
                    MessageBox.Show(string.Format("Загрузка {0} файлов завершена.", dlg.FileNames.Length));
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        void Ins(object ob)
        {
            ANTONIO.Файл obj = ob as ANTONIO.Файл;
            using (OracleConnection con = new OracleConnection(string.Format("User Id= {0}; Password ={1}; DATA SOURCE= {2}", textBox2.Text, textBox1.Text, textBox4.Text)))
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = textBox5.Text;

                int kolPaket = 10000;
                int kolIter = obj.Документ.Length / kolPaket;

                for (int i = 0; i <= kolIter; i++)
                {

                    int kol = i == kolIter ? obj.Документ.Length - kolPaket * kolIter : kolPaket;
                    cmd.ArrayBindCount = kol;
                    if (i == kolIter)
                    {

                    }
                    cmd.Parameters.Clear();

                    cmd.Parameters.Add("ИдФайл", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("ВерсФорм", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("ТипИнф", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("ВерсПрог", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("КолДок", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("ДолжОтв", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("Тлф", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("ФИООтв_Фамилия", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("ФИООтв_Имя", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("ФИООтв_Отчество", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("ИдДок", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("ДатаДок", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("СНИЛС", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("Гражд", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("ДатаРожд", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("Пол", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("Фамилия", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("Имя", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("Отчество", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("КодВидДок", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];
                    cmd.Parameters.Add("СерНомДок", OracleDbType.Varchar2, System.Data.ParameterDirection.Input).Value = new object[kol];

                    for (int j = 0; j < kol; j++)
                    {
                        ((object[])(cmd.Parameters["ИдФайл"].Value))[j] = obj.ИдФайл;
                        ((object[])(cmd.Parameters["ВерсФорм"].Value))[j] = obj.ВерсФорм;
                        ((object[])(cmd.Parameters["ТипИнф"].Value))[j] = obj.ТипИнф;
                        ((object[])(cmd.Parameters["ВерсПрог"].Value))[j] = obj.ВерсПрог;
                        ((object[])(cmd.Parameters["КолДок"].Value))[j] = obj.КолДок;
                        ((object[])(cmd.Parameters["ДолжОтв"].Value))[j] = obj.ИдОтпр?.ДолжОтв;
                        ((object[])(cmd.Parameters["Тлф"].Value))[j] = obj.ИдОтпр?.Тлф;
                        ((object[])(cmd.Parameters["ФИООтв_Фамилия"].Value))[j] = obj.ИдОтпр?.ФИООтв?.Фамилия;
                        ((object[])(cmd.Parameters["ФИООтв_Имя"].Value))[j] = obj.ИдОтпр?.ФИООтв?.Имя;
                        ((object[])(cmd.Parameters["ФИООтв_Отчество"].Value))[j] = obj.ИдОтпр?.ФИООтв?.Отчество;
                        ((object[])(cmd.Parameters["ИдДок"].Value))[j] = obj.Документ[kolPaket * i + j].ИдДок;
                        ((object[])(cmd.Parameters["ДатаДок"].Value))[j] = obj.Документ[kolPaket * i + j].ДатаДок;
                        ((object[])(cmd.Parameters["СНИЛС"].Value))[j] = obj.Документ[kolPaket * i + j].СвЗастрахЛиц?.СНИЛС;
                        ((object[])(cmd.Parameters["Гражд"].Value))[j] = obj.Документ[kolPaket * i + j].СвЗастрахЛиц?.Гражд;
                        ((object[])(cmd.Parameters["ДатаРожд"].Value))[j] = obj.Документ[kolPaket * i + j].СвЗастрахЛиц?.ДатаРожд;
                        ((object[])(cmd.Parameters["Пол"].Value))[j] = obj.Документ[kolPaket * i + j].СвЗастрахЛиц?.Пол;
                        ((object[])(cmd.Parameters["Фамилия"].Value))[j] = obj.Документ[kolPaket * i + j].СвЗастрахЛиц?.ФИОФЛ?.Фамилия;
                        ((object[])(cmd.Parameters["Имя"].Value))[j] = obj.Документ[kolPaket * i + j].СвЗастрахЛиц?.ФИОФЛ?.Имя;
                        ((object[])(cmd.Parameters["Отчество"].Value))[j] = obj.Документ[kolPaket * i + j].СвЗастрахЛиц?.ФИОФЛ?.Отчество;
                        ((object[])(cmd.Parameters["КодВидДок"].Value))[j] = obj.Документ[kolPaket * i + j].СвЗастрахЛиц?.УдЛичнФЛ?.КодВидДок;
                        ((object[])(cmd.Parameters["СерНомДок"].Value))[j] = obj.Документ[kolPaket * i + j].СвЗастрахЛиц?.УдЛичнФЛ?.СерНомДок;
                    }
                    cmd.ExecuteNonQuery();
                    GC.Collect();
                }
                con.Close();
                con.Dispose();



            }
        }

    }
}