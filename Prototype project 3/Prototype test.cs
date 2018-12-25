using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Prototype_project_3
{
    public partial class Main : Form
    {
        Dictionary<int, string> gemeenteDictionary = new Dictionary<int, string>()
        {
            // Gebieden buiten de map kleur 
            { 0x000000, ""  },
            { 0xD5F2F6, ""  },
            { 0xf0f8eb, ""  },
            // normaal paars 0x is hekje 
            { 0xD5BBDC, "Westland" },                   //20
            { 0xD5BBDB, "s-Gravenhage" },               //14
            { 0xD5BBDA, "Wassenaar" },                  //13
            { 0xD5BBEB, "Leidschendam-Voorburg" },      //15
            { 0xD5BBEC, "Zoetermeer" },                 //16
            { 0xD5BBEA, "Pijnacker-Nootdorp" },         //17
            { 0xD5BBDF, "Delft" },                      //22
            { 0xD5BBDD, "Midden-Delfland" },            //21
            { 0xD5BBDE, "Rijswijk" },                   //19
            // opgelichte paars
            { 0xdfcce5, "Westland" },                         //20
            { 0xdfcce4, "s-Gravenhage" },                     //14
            { 0xdfcce3, "Wassenaar" },                        //13
            { 0xdfccf0, "Leidschendam-Voorburg" },            //15
            { 0xdfccf1, "Zoetermeer" },                       //16
            { 0xdfccef, "Pijnacker-Nootdorp" },               //17
            { 0xdfcce7, "Delft" },                            //22
            { 0xdfcce1, "Midden-Delfland" },                  //21
            { 0xdfcce6, "Rijswijk" },                         //19
            
            // normaal geel
            { 0xFFF17D, "Noordwijk" },                  //1
            { 0xFFF17E, "Noordwijkerhout" },            //2
            { 0xFFF17F, "Hillegom" },                   //3
            { 0xFFF18A, "Lisse" },                      //4
            { 0xFFF18B, "Teylingen" },                  //5
            { 0xFFF17C, "Katwijk" },                    //6
            { 0xFFF18E, "Oegstgeest" },                 //7
            { 0xFFF18C, "Kaag en Braassem" },           //8
            { 0xFFF18D, "Leiden" },                     //9
            { 0xFFF18F, "Voorschoten" },                //11
            { 0xFFF19A, "Zoeterwoude" },                //12
            // opgelichte geel
            { 0xfff8bd, "Noordwijk" },                          //1
            { 0xfff8be, "Noordwijkerhout" },                    //2
            { 0xfff8bf, "Hillegom" },                           //3
            { 0xfff8c4, "Lisse" },                              //4
            { 0xfff8c5, "Teylingen" },                          //5
            { 0xfff8ba, "Katwijk" },                            //6
            { 0xfff8c6, "Oegstgeest" },                         //7
            { 0xfff8c7, "Kaag en Braassem" },                   //8
            { 0xfff8c8, "Leiden" },                             //9
            { 0xfff8c9, "Voorschoten" },                        //11
            { 0xfff8cc, "Zoeterwoude" },                        //12

            // normaal groen
            { 0x7fc98c, "Alphen aan den Rijn" },         //23
            { 0x7fc98b, "Nieuwkoop" },                   //26
            // opgelichte groen
            { 0x9fd6a7, "Alphen aan den Rijn" },                //23
            { 0x9fd6a8, "Nieuwkoop" },                          //26

            // normaal rood
            { 0xff7f66, "Ridderkerk" },                  //59
            { 0xff7f67, "Barendrecht" },                 //60
            { 0xff7f65, "Krimpen aan den IJssel" },      //61
            { 0xff7f64, "Capelle aan den IJssel" },      //62
            { 0xff7f74, "Lansingerland" },               //63
            { 0xff7f73, "Rotterdam" },                   //64
            { 0xff7f68, "Albrandswaard" },               //65
            { 0xff7f76, "Nissewaard" },                  //66
            { 0xff7f75, "Maassluis" },                   //69
            { 0xff7f71, "Brielle" },                     //72
            { 0xff7f72, "Westvoorne" },                  //73
            { 0xff7f70, "Hellevoetsluis" },              //74
            // opgelichte rood
            { 0xff9f8c, "Ridderkerk" },                          //59
            { 0xff9f8d, "Barendrecht" },                         //60
            { 0xff9f8b, "Krimpen aan den IJssel" },              //61
            { 0xff9f8a, "Capelle aan den IJssel" },              //62
            { 0xff9f97, "Lansingerland" },                       //63
            { 0xff9f96, "Rotterdam" },                           //64
            { 0xff9f8e, "Albrandswaard" },                       //65
            { 0xff9f98, "Nissewaard" },                          //66
            { 0xff9f95, "Maassluis" },                           //69
            { 0xff9f92, "Brielle" },                             //72
            { 0xff9f94, "Westvoorne" },                          //73
            { 0xff9f93, "Hellevoetsluis" },                      //74

            // normaal oranje
            { 0xffcc7c, "Goeree-Overflakkee" },         //75
            // opgelichte oranje
            { 0xffd99c, "Goeree-Overflakkee" },                  //75

            // normaal roze
            { 0xffbfab, "Waddinxveen" },                //29
            { 0xffbfad, "Zuidplas" },                   //30
            { 0xffbfaa, "Bodegraven-Reeuwijk" },        //31
            { 0xffbfac, "Gouda" },                      //32
            { 0xffbfae, "Krimpenerwaard" },             //35
            // opgelichte roze
            { 0xffcfc0, "Waddinxveen" },                        //29
            { 0xffcfc1, "Zuidplas" },                           //30
            { 0xffcfbf, "Bodegraven-Reeuwijk" },                //31
            { 0xffcfc3, "Gouda" },                              //32
            { 0xffcfc2, "Krimpenerwaard" },                     //35

            // normaal bruin
            { 0xdfc695, "Binnenmaas" },                //54
            { 0xdfc696, "Strijen" },                   //55
            { 0xdfc697, "Cromstrijen" },               //56
            { 0xdfc699, "Oud-Beijerland" },            //57
            { 0xdfc698, "Korendijk" },                 //58
            // opgelichte bruin
            { 0xe7d4af, "Binnenmaas" },                         //54
            { 0xe7d4b0, "Strijen" },                            //55
            { 0xe7d4b2, "Cromstrijen" },                        //56
            { 0xe7d4b3, "Oud-Beijerland" },                     //57
            { 0xe7d4b1, "Korendijk" },                          //58

            // normaal grijs
            { 0xcac7c5, "Sliedrecht" },                 //48
            { 0xcac7c3, "Dordrecht" },                  //49
            { 0xcac7c4, "Papendrecht" },                //50
            { 0xcac7c1, "Alblasserdam" },               //51
            { 0xcac7c2, "Zwijndrecht" },                //53
            // opgelichte grijs
            { 0xdad8d2, "Sliedrecht" },                          //48
            { 0xdad8d5, "Dordrecht" },                           //49
            { 0xdad8d6, "Papendrecht" },                         //50
            { 0xdad8d3, "Alblasserdam" },                        //51
            { 0xdad8d4, "Zwijndrecht" },                         //53

            // normaal blauw
            { 0x71ccda, "Molenwaard" },                 //40
            { 0x71ccde, "Zederik" },                    //43
            { 0x71ccdc, "Giessenlanden" },              //44
            { 0x71ccdf, "Leerdam" },                    //45
            { 0x71ccdd, "Gorinchem" },                  //46
            { 0x71ccdb, "Hardinxveld-Giessendam" },     //47
            // opgelichte blauw
            { 0x94d9e3, "Molenwaard" },                           //40
            { 0x94d9e6, "Zederik" },                              //43
            { 0x94d9e5, "Giessenlanden" },                        //44
            { 0x94d9e7, "Leerdam" },                              //45
            { 0x94d9e2, "Gorinchem" },                            //46
            { 0x94d9e4, "Hardinxveld-Giessendam" },               //47

        };

        public Main()
        {
            InitializeComponent();
            pb_achtergrond.Image = Properties.Resources.hele_map_met_juiste_hex;
            chart1.Series["Gemeente1"].IsVisibleInLegend = false;
            chart1.Series["Gemeente2"].IsVisibleInLegend = false;
            lbl_testwaarde1.Text = "#0";
            lbl_testwaarde2.Text = "#0";




        }

        private void pb_achtergrond_MouseClick(object sender, MouseEventArgs e)
        {
            // periode is variabel met de tekst uit de radiobuttons
            string periode = "";
            if (radioButton1.Checked)
                periode = "'"+radioButton1.Text+ "'";
            if (radioButton2.Checked)
                periode = "'"+radioButton2.Text+ "'";
            if (radioButton3.Checked)
                periode = "'"+radioButton3.Text+ "'";
            if (radioButton4.Checked)
                periode = "'"+radioButton4.Text+ "'";
            if (radioButton5.Checked)
                periode = "'"+radioButton5.Text+ "'";
            if (radioButton6.Checked)
                periode = "'"+radioButton6.Text+ "'";
            if (radioButton7.Checked)
                periode = "'"+radioButton7.Text+ "'";
            if (radioButton8.Checked)
                periode = "'"+radioButton8.Text+ "'";
            if (radioButton9.Checked)
                periode = "'"+radioButton9.Text+"'";
            if (radioButton10.Checked)
                periode = "perioden.naam";

            // Selecteer eerste gebied        
            if (e.Button == MouseButtons.Left)
            {
                Bitmap bm = (Bitmap)pb_achtergrond.Image;
                Point p = new Point((Cursor.Position.X), (Cursor.Position.Y));
                Point newPoint = pb_achtergrond.PointToClient(p);
                lbl_mouseX.Text = newPoint.X.ToString();
                lbl_mouseY.Text = newPoint.Y.ToString();
                var pixel = bm.GetPixel(newPoint.X, newPoint.Y);
                int hexvalue = pixel.R * 256 * 256 + pixel.G * 256 + pixel.B;

                if (lbl_getGebied.Text == (""))
                    MessageBox.Show("Dit is geen gemeente");

                if (lbl_getGebied.Text != lbl_currentSelected2.Text && lbl_getGebied.Text !=(""))
                {
                    lbl_currentSelected1.Text = lbl_getGebied.Text;
                    string conn = "Server = 127.0.0.1; port = 5432; User Id = postgres; Password = postgres; Database = Retake project 3";
                    NpgsqlConnection connection = new NpgsqlConnection(conn);
                    string sql = "select regios.naam, COUNT(herkomstgroepering.naam) from dataproject" +
                                        " join regios on regios.code = dataproject.regios" +
                                        " join herkomstgroepering on herkomstgroepering.code = dataproject.herkomstgroepering" +
                                        " join perioden on perioden.code = dataproject.perioden" +
                                        " where herkomstgroepering.naam = '" + lbl_test_herkomst.Text + "' and regios.naam='" + lbl_currentSelected1.Text + "' and perioden.naam = "+periode+" group by regios.naam";
                    NpgsqlDataAdapter dataGrabber = new NpgsqlDataAdapter(sql, connection);
                    DataTable dataStore = new DataTable();
                    dataGrabber.Fill(dataStore);
                    connection.Open();

                    // als het niet in database staat dan geeft het error message
                    if (dataStore.Rows.Count != 0)
                    {
                        lbl_Aantal1.Text = lbl_test_aantal1.Text + dataStore.Rows[0][1];
                        lbl_testwaarde1.Text = "#" + dataStore.Rows[0][1];

                        chart1.Series[0].Points.Clear();
                        chart1.Series[0].LegendText = lbl_currentSelected1.Text;
                        chart1.Series[0].Color = Color.FromArgb(pixel.R, pixel.G, pixel.B);
                        gb_gemeente1.Text = lbl_currentSelected1.Text;
                        chart1.Series[0].IsVisibleInLegend = true;
                        chart1.Visible = true;

                        if(periode == "perioden.naam")
                        {
                            chart1.Series[0].Points.AddXY("Alle perioden", (dataStore.Rows[0][1]));
                        }
                        else
                        {
                            chart1.Series[0].Points.AddXY(periode, (dataStore.Rows[0][1]));
                        }
                        
                        lbl_uitkomsts_verschil.Text = "";
                        
                    }
                    else
                    {
                        lbl_Aantal1.Text = lbl_test_aantal1.Text;
                        lbl_uitkomsts_verschil.Text = "";
                        MessageBox.Show("Deze gemeente heeft geen gegevens voor deze periode");
                    }
                    connection.Close();
                }
            };

            // Selecteer tweede gebied        
            if (e.Button == MouseButtons.Right)
            {
                Bitmap bm = (Bitmap)pb_achtergrond.Image;
                Point p = new Point((Cursor.Position.X), (Cursor.Position.Y));
                Point newPoint = pb_achtergrond.PointToClient(p);
                lbl_mouseX.Text = newPoint.X.ToString();
                lbl_mouseY.Text = newPoint.Y.ToString();
                var pixel = bm.GetPixel(newPoint.X, newPoint.Y);
                int hexvalue = pixel.R * 256 * 256 + pixel.G * 256 + pixel.B;

                if (lbl_getGebied.Text == (""))
                    MessageBox.Show("Dit is geen gemeente");

                if (lbl_getGebied.Text != lbl_currentSelected1.Text && lbl_getGebied.Text !=(""))

                {
                    lbl_currentSelected2.Text = lbl_getGebied.Text;
                    string conn = "Server = 127.0.0.1; port = 5432; User Id = postgres; Password = postgres; Database = Retake project 3";
                    NpgsqlConnection connection = new NpgsqlConnection(conn);
                    string sql = "select regios.naam, COUNT(herkomstgroepering.naam) from dataproject" +
                                        " join regios on regios.code = dataproject.regios" +
                                        " join herkomstgroepering on herkomstgroepering.code = dataproject.herkomstgroepering" +
                                        " join perioden on perioden.code = dataproject.perioden" +
                                        " where herkomstgroepering.naam = '" + lbl_test_herkomst.Text + "' and regios.naam='" + lbl_currentSelected2.Text + "' and perioden.naam = " +periode+ " group by regios.naam";
                    NpgsqlDataAdapter dataGrabber = new NpgsqlDataAdapter(sql, connection);
                    DataTable dataStore = new DataTable();
                    dataGrabber.Fill(dataStore);
                    connection.Open();

                    if (dataStore.Rows.Count != 0)
                    {
                        lbl_Aantal2.Text = lbl_test_aantal2.Text + dataStore.Rows[0][1];
                        lbl_testwaarde2.Text = "#"+dataStore.Rows[0][1]; 
                        
                        chart1.Series[1].Points.Clear();
                        chart1.Series[1].LegendText = lbl_currentSelected2.Text;
                        chart1.Series[1].Color = Color.FromArgb(pixel.R, pixel.G, pixel.B);
                        gb_gemeente2.Text = lbl_currentSelected2.Text;
                        chart1.Series[1].IsVisibleInLegend = true;
                        chart1.Visible = true;

                        if (periode == "perioden.naam")
                        {
                            chart1.Series[1].Points.AddXY("Alle perioden", (dataStore.Rows[0][1]));
                        }
                        else
                        {
                            chart1.Series[1].Points.AddXY(periode, (dataStore.Rows[0][1]));
                        }
                        lbl_uitkomsts_verschil.Text = "";
                    }
                    else
                    {
                        lbl_Aantal2.Text = lbl_test_aantal2.Text;
                        lbl_uitkomsts_verschil.Text = "";
                        MessageBox.Show("Deze gemeente heeft geen gegevens voor deze periode");
                    }
                    connection.Close();
                }
            };
        }

        private void pb_achtergrond_MouseMove(object sender, MouseEventArgs e)
        {
            // check kleurcode onder muis en of het in dictionary is en houdt muis positie bij
            Bitmap bm = (Bitmap)pb_achtergrond.Image;
            Point p = new Point((Cursor.Position.X), (Cursor.Position.Y));
            Point newPoint = pb_achtergrond.PointToClient(p);
            lbl_mouseX.Text = newPoint.X.ToString();
            lbl_mouseY.Text = newPoint.Y.ToString();
            var pixel = bm.GetPixel(newPoint.X, newPoint.Y);
            int hexvalue = pixel.R * 256 * 256 + pixel.G * 256 + pixel.B;
            lbl_colorCode.Text = string.Format("{0:X}", hexvalue);

            if (lbl_getGebied.Text == (""))
                pb_achtergrond.Image = Properties.Resources.hele_map_met_juiste_hex;

            // if in dictionary
            if (gemeenteDictionary.ContainsKey(hexvalue))
            {
                string gemeenteFromDictionary = gemeenteDictionary[hexvalue];
                lbl_getGebied.Text = gemeenteFromDictionary;
            }

            //Gebieden hovers
            // paars
            if (lbl_getGebied.Text == ("Westland"))
            {pb_achtergrond.Image = Properties.Resources.Westland;}
            if (lbl_getGebied.Text == ("s-Gravenhage"))
            { pb_achtergrond.Image = Properties.Resources.s_Gravenhage;}
            if (lbl_getGebied.Text == ("Wassenaar"))
            { pb_achtergrond.Image = Properties.Resources.Wassenaar; }
            if (lbl_getGebied.Text == ("Leidschendam-Voorburg"))
            { pb_achtergrond.Image = Properties.Resources.Leidschendam_Voorburg; }
            if (lbl_getGebied.Text == ("Zoetermeer"))
            { pb_achtergrond.Image = Properties.Resources.Zoetermeer; }
            if (lbl_getGebied.Text == ("Pijnacker-Nootdorp"))
            { pb_achtergrond.Image = Properties.Resources.Pijnacker_Nootdorp; }
            if (lbl_getGebied.Text == ("Delft"))
            { pb_achtergrond.Image = Properties.Resources.Delft; }
            if (lbl_getGebied.Text == ("Midden-Delfland"))
            { pb_achtergrond.Image = Properties.Resources.Midden_Delfland; }
            if (lbl_getGebied.Text == ("Rijswijk"))
            { pb_achtergrond.Image = Properties.Resources.Rijswijk; }
            // geel
            if (lbl_getGebied.Text == ("Noordwijk"))
            { pb_achtergrond.Image = Properties.Resources.Noordwijk; }
            if (lbl_getGebied.Text == ("Noordwijkerhout"))
            { pb_achtergrond.Image = Properties.Resources.Noordwijkerhout; }
            if (lbl_getGebied.Text == ("Hillegom"))
            { pb_achtergrond.Image = Properties.Resources.Hillegom; }
            if (lbl_getGebied.Text == ("Lisse"))
            { pb_achtergrond.Image = Properties.Resources.Lisse; }
            if (lbl_getGebied.Text == ("Teylingen"))
            { pb_achtergrond.Image = Properties.Resources.Teylingen; }
            if (lbl_getGebied.Text == ("Katwijk"))
            { pb_achtergrond.Image = Properties.Resources.Katwijk; }
            if (lbl_getGebied.Text == ("Oegstgeest"))
            { pb_achtergrond.Image = Properties.Resources.Oegstgeest; }
            if (lbl_getGebied.Text == ("Kaag en Braassem"))
            { pb_achtergrond.Image = Properties.Resources.Kaag_en_Braassem; }
            if (lbl_getGebied.Text == ("Leiden"))
            { pb_achtergrond.Image = Properties.Resources.Leiden; }
            if (lbl_getGebied.Text == ("Voorschoten"))
            { pb_achtergrond.Image = Properties.Resources.Voorschoten; }
            if (lbl_getGebied.Text == ("Zoeterwoude"))
            { pb_achtergrond.Image = Properties.Resources.Zoeterwoude; }
            // groen
            if (lbl_getGebied.Text == ("Alphen aan den Rijn"))
            { pb_achtergrond.Image = Properties.Resources.Alphen_aan_den_Rijn; }
            if (lbl_getGebied.Text == ("Nieuwkoop"))
            { pb_achtergrond.Image = Properties.Resources.Nieuwkoop; }
            // rood
            if (lbl_getGebied.Text == ("Ridderkerk"))
            { pb_achtergrond.Image = Properties.Resources.Ridderkerk; }
            if (lbl_getGebied.Text == ("Barendrecht"))
            { pb_achtergrond.Image = Properties.Resources.Barendrecht; }
            if (lbl_getGebied.Text == ("Krimpen aan den IJssel"))
            { pb_achtergrond.Image = Properties.Resources.Krimpen_aan_den_IJssel; }
            if (lbl_getGebied.Text == ("Capelle aan den IJssel"))
            { pb_achtergrond.Image = Properties.Resources.Capelle_aan_den_IJssel; }
            if (lbl_getGebied.Text == ("Lansingerland"))
            { pb_achtergrond.Image = Properties.Resources.Lansingerland; }
            if (lbl_getGebied.Text == ("Rotterdam"))
            { pb_achtergrond.Image = Properties.Resources.Rotterdam; }
            if (lbl_getGebied.Text == ("Albrandswaard"))
            { pb_achtergrond.Image = Properties.Resources.Albrandswaard; }
            if (lbl_getGebied.Text == ("Nissewaard"))
            { pb_achtergrond.Image = Properties.Resources.Nissewaard; }
            if (lbl_getGebied.Text == ("Maassluis"))
            { pb_achtergrond.Image = Properties.Resources.Maassluis; }
            if (lbl_getGebied.Text == ("Brielle"))
            { pb_achtergrond.Image = Properties.Resources.Brielle; }
            if (lbl_getGebied.Text == ("Westvoorne"))
            { pb_achtergrond.Image = Properties.Resources.Westvoorne; }
            if (lbl_getGebied.Text == ("Hellevoetsluis"))
            { pb_achtergrond.Image = Properties.Resources.Hellevoetsluis; }
            // oranje
            if (lbl_getGebied.Text == ("Goeree-Overflakkee"))
            { pb_achtergrond.Image = Properties.Resources.Goeree_Overflakkee; }
            // roze
            if (lbl_getGebied.Text == ("Waddinxveen"))
            { pb_achtergrond.Image = Properties.Resources.Waddinxveen; }
            if (lbl_getGebied.Text == ("Zuidplas"))
            { pb_achtergrond.Image = Properties.Resources.Zuidplas; }
            if (lbl_getGebied.Text == ("Bodegraven-Reeuwijk"))
            { pb_achtergrond.Image = Properties.Resources.Bodegraven_Reeuwijk; }
            if (lbl_getGebied.Text == ("Gouda"))
            { pb_achtergrond.Image = Properties.Resources.Gouda; }
            if (lbl_getGebied.Text == ("Krimpenerwaard"))
            { pb_achtergrond.Image = Properties.Resources.Krimpenerwaard; }
            // bruin
            if (lbl_getGebied.Text == ("Binnenmaas"))
            { pb_achtergrond.Image = Properties.Resources.Binnenmaas; }
            if (lbl_getGebied.Text == ("Strijen"))
            { pb_achtergrond.Image = Properties.Resources.Strijen; }
            if (lbl_getGebied.Text == ("Cromstrijen"))
            { pb_achtergrond.Image = Properties.Resources.Cromstrijen; }
            if (lbl_getGebied.Text == ("Oud-Beijerland"))
            { pb_achtergrond.Image = Properties.Resources.Oud_Beijerland; }
            if (lbl_getGebied.Text == ("Korendijk"))
            { pb_achtergrond.Image = Properties.Resources.Korendijk; }
            // grijs
            if (lbl_getGebied.Text == ("Sliedrecht"))
            { pb_achtergrond.Image = Properties.Resources.Sliedrecht; }
            if (lbl_getGebied.Text == ("Dordrecht"))
            { pb_achtergrond.Image = Properties.Resources.Dordrecht; }
            if (lbl_getGebied.Text == ("Papendrecht"))
            { pb_achtergrond.Image = Properties.Resources.Papendrecht; }
            if (lbl_getGebied.Text == ("Alblasserdam"))
            { pb_achtergrond.Image = Properties.Resources.Alblasserdam; }
            if (lbl_getGebied.Text == ("Zwijndrecht"))
            { pb_achtergrond.Image = Properties.Resources.Zwijndrecht; }
            // blauw
            if (lbl_getGebied.Text == ("Molenwaard"))
            { pb_achtergrond.Image = Properties.Resources.Molenwaard; }
            if (lbl_getGebied.Text == ("Zederik"))
            { pb_achtergrond.Image = Properties.Resources.Zederik; }
            if (lbl_getGebied.Text == ("Giessenlanden"))
            { pb_achtergrond.Image = Properties.Resources.Giessenlanden; }
            if (lbl_getGebied.Text == ("Leerdam"))
            { pb_achtergrond.Image = Properties.Resources.Leerdam; }
            if (lbl_getGebied.Text == ("Gorinchem"))
            { pb_achtergrond.Image = Properties.Resources.Gorinchem; }
            if (lbl_getGebied.Text == ("Hardinxveld-Giessendam"))
            { pb_achtergrond.Image = Properties.Resources.Hardinxveld_Giessendam; }
        }

        private void button2_Click(object sender, EventArgs e)
        {tabControl1.SelectedTab = Page_Vraag;}
        
        private void button4_Click(object sender, EventArgs e)
        {tabControl1.SelectedTab = Page_Menu;}
        
        private void button1_Click_1(object sender, EventArgs e)
        {tabControl1.SelectedTab = Page_Menu;}

        private void VisualToMenu_Click(object sender, EventArgs e)
        {tabControl1.SelectedTab = Page_Menu;}
        
        private void button1_Click_2(object sender, EventArgs e)
        {tabControl1.SelectedTab = Page_Menu;}
        
        private void Knop_AboutToVoorbeeld_Click(object sender, EventArgs e)
        {tabControl1.SelectedTab = Page_Vraag;}

        private void Knop_FilterToVoorbeeld_Click(object sender, EventArgs e)
        {tabControl1.SelectedTab = Page_Vraag;}
        
        private void button18_Click(object sender, EventArgs e)
        {tabControl1.SelectedTab = Page_Menu;}
                                      
        private void button2_Click_1(object sender, EventArgs e)
        {tabControl1.SelectedTab = Page_Vraag;}
                
        private void button18_Click_1(object sender, EventArgs e)
        {tabControl1.SelectedTab = Page_Menu;}
                        
        private void Knop_vb1TOvb2_Click(object sender, EventArgs e)
        {tabControl1.SelectedTab = Page_Vraag;}

        // wanneer je verandert van radiobutton dan worden de teksten gereset en de charts ook
        // allemaal radio buttons met dezelfde functie die ze moeten uitvoeren :( kan dit niet makkelijker?

        public void ResetStuff()
        {
            lbl_currentSelected1.Text = "";
            lbl_currentSelected2.Text = "";
            lbl_Aantal1.Text = "Aantal             op school: ";
            lbl_Aantal2.Text = "Aantal             op school: ";
            lbl_test_aantal1.Text = lbl_test_aantal1.Text;
            lbl_test_aantal2.Text = lbl_test_aantal2.Text;
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[0].IsVisibleInLegend = false;
            chart1.Series[1].IsVisibleInLegend = false;
            chart1.Visible = false;
            lbl_uitkomsts_verschil.Text = "";
            gb_gemeente2.Text = "Gemeente 2";
            gb_gemeente1.Text = "Gemeente 1";


        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {ResetStuff();}

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {ResetStuff();}

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {ResetStuff();}

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {ResetStuff();}

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {ResetStuff();}

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {ResetStuff();}

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {ResetStuff();}

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {ResetStuff();}

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {ResetStuff();}

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        { ResetStuff(); }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((lbl_Aantal1.Text != lbl_test_aantal1.Text && lbl_Aantal2.Text != lbl_test_aantal2.Text) &&(lbl_Aantal1.Text != "Aantal             op school: " && lbl_Aantal2.Text != "Aantal             op school: ")) {
                int verschil = 0;
                int waarde1 = Int32.Parse(lbl_testwaarde1.Text.Remove(0, 1));
                int waarde2 = Int32.Parse(lbl_testwaarde2.Text.Remove(0, 1));


                if (waarde1 >= waarde2)
                {
                    verschil = waarde1 - waarde2;
                    lbl_uitkomsts_verschil.Text = verschil.ToString();
                };

                if (waarde2 >= waarde1)
                {
                    verschil = waarde2 - waarde1;
                    lbl_uitkomsts_verschil.Text = verschil.ToString();

                }
            }
                
        }

        private void herkomst_SelectedValueChanged(object sender, EventArgs e)
        {
            ResetStuff();
            lbl_test_herkomst.Text = herkomst.SelectedItem.ToString();
            lbl_test_aantal1.Text= "Aantal " + cb_herkomst_mensen.Items[herkomst.SelectedIndex].ToString() + " op school: ";
            lbl_test_aantal2.Text = "Aantal "+ cb_herkomst_mensen.Items[herkomst.SelectedIndex].ToString() + " op school: ";
            chart1.Titles.Add("Aantal "+cb_herkomst_mensen.Items[herkomst.SelectedIndex].ToString()) ;
            chart1.Titles.RemoveAt(0);            
        }

        
    }
}
