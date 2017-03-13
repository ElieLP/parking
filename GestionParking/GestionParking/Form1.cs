using Parking;
using Parking.Controllers;
using Parking.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Parking.Interfaces;

namespace Parking
{
    public partial class Form1 : Form, IView
    {
        //Constante pour les PictureBox
        public const int Width = 75;
        public const int Height = 50;

        //Création des Controllers
        IFloorController floorController;
        ISlotController slotController;
        public event ViewHandler<IView> changed;

        //Création des Etages
        Floor floor1;
        Floor floor2;
        Floor floor3;
        Floor floor4;
        Floor floor5;

        //Création de la liste des slots par etage
        List<Slot> listFloor1;
        List<Slot> listFloor2;
        List<Slot> listFloor3;
        List<Slot> listFloor4;
        List<Slot> listFloor5;

        //Création de la liste des PictureBox par étage
        List<PictureBox> listPictFloor1;
        List<PictureBox> listPictFloor2;
        List<PictureBox> listPictFloor3;
        List<PictureBox> listPictFloor4;
        List<PictureBox> listPictFloor5;

        //Selection de PictureBox
        PictureBox actualPict;
        int actualFloor;

        public void setController(FloorController cont)
        {
            floorController = cont;
        }

        public Form1()
        {
            InitializeComponent();
            grp_parking.Controls.Clear();
            actualFloor = 1;
            

            //Instancation des Etages
            listFloor1 = new List<Slot>();
            listFloor2 = new List<Slot>();
            listFloor3 = new List<Slot>();
            listFloor4 = new List<Slot>();
            listFloor5 = new List<Slot>();

            //Instanciation des List de PictureBox
            listPictFloor1 = new List<PictureBox>();
            listPictFloor2 = new List<PictureBox>();
            listPictFloor3 = new List<PictureBox>();
            listPictFloor4 = new List<PictureBox>();
            listPictFloor5 = new List<PictureBox>();

            //Instancation des listes pas Etages
            floor1 = new Floor(1, listFloor1);
            floor2 = new Floor(2, listFloor2);
            floor3 = new Floor(3, listFloor3);
            floor4 = new Floor(4, listFloor4);
            floor5 = new Floor(5, listFloor5);

            //Affectation des places des Etages
            floor1.generateFloor1();
            floor2.generateFloor2();
            floor3.generateFloor2();
            floor4.generateFloor3();
            floor5.generateFloor3();

            //Affecation des pictureBox
            generatePictFloor(listPictFloor1, listFloor1);
            generatePictFloor(listPictFloor2, listFloor2);
            generatePictFloor(listPictFloor3, listFloor3);
            generatePictFloor(listPictFloor4, listFloor4);
            generatePictFloor(listPictFloor5, listFloor5);

            //Affichage du premier Etage
            displayFloor(listPictFloor1);
            grp_detail_etage.Show();
            grp_detail_place.Hide();

            int nbTotal = floor1.slotList.Count;
            int nbDispo = floor1.statsDisponible();
            int nbOccupee = floor1.statsOccupee();
            int nbReservee = floor1.statsReservee();
            int totalPlace = listFloor1.Count + listFloor2.Count + listFloor3.Count + listFloor4.Count + listFloor5.Count;
            int totalDispo = floor1.statsDisponible() + floor2.statsDisponible() + floor3.statsDisponible() + floor4.statsDisponible() + floor5.statsDisponible();
            int totalOccupee = floor1.statsOccupee() + floor2.statsOccupee() + floor3.statsOccupee() + floor4.statsOccupee() + floor5.statsOccupee();
            int totalReservee = floor1.statsReservee() + floor2.statsReservee() + floor3.statsReservee() + floor4.statsReservee() + floor5.statsReservee();

            lbl_etage_nb_places.Text = nbTotal.ToString();
            lbl_etage_nb_places_dispo.Text = nbDispo.ToString();
            lbl_etage_nb_places_occupees.Text = nbOccupee.ToString();
            lbl_etage_nb_places_reservees.Text = nbReservee.ToString();
            lbl_nb_stats_places.Text = totalPlace.ToString();
            lbl_nb_stats_places_dispo.Text = totalDispo.ToString();
            lbl_nb_stats_places_occupees.Text = totalOccupee.ToString();
            lbl_nb_stats_places_reservees.Text = totalReservee.ToString();

        }

        public void Picture_Click(object sender, EventArgs e)
        {
            PictureBox pict = sender as PictureBox;
            actualPict = sender as PictureBox;

            Slot slot = listFloor1[Convert.ToInt32(pict.Tag)];

            grp_detail_place.Text = "Détails : Place " + pict.Tag;
            grp_detail_etage.Hide();
            grp_detail_place.Show();
            

            if (slot.state == SlotState.Empty)
            {//A changer
                TimeSpan dispoTime = DateTime.Now - slot.lastTimeTaken;
                String dispoString = dispoTime.Hours.ToString() + "h" + dispoTime.Minutes.ToString();
                rdb_place_disponible.Checked = true;
                rdb_place_occupee.Checked = false;
                rdb_place_reservee.Checked = false;
                lbl_place_value_disponible.Text = dispoString;
                lbl_place_value_occupee.Text = "-";
                //lbl_place_value_timeUse.Text = "";
                //lbl_place_value_sortie_proche.Text = "";
                //lbl_place_value_sortie_rapide.Text = "";
            }
            else if(slot.state == SlotState.Used)
            {//A changer
                TimeSpan usedTime = DateTime.Now - slot.lastTimeTaken;
                String usedString = usedTime.Hours.ToString() + "h" + usedTime.Minutes.ToString();
                rdb_place_disponible.Checked = false;
                rdb_place_occupee.Checked = false;
                rdb_place_reservee.Checked = true;
                lbl_place_value_disponible.Text = "-";
                lbl_place_value_occupee.Text = usedString;
                //lbl_place_value_timeUse.Text = "";
                //lbl_place_value_sortie_proche.Text = "";
                //lbl_place_value_sortie_rapide.Text = "";          
            }
            else
            {//A changer
                TimeSpan reservedTime = DateTime.Now - slot.lastTimeReserved;
                String reservedString = reservedTime.Hours.ToString() + "h" + reservedTime.Minutes.ToString();
                rdb_place_disponible.Checked = false;
                rdb_place_occupee.Checked = false;
                rdb_place_reservee.Checked = true;
                lbl_place_value_disponible.Text = "-";
                lbl_place_value_occupee.Text = reservedString;
                //lbl_place_value_timeUse.Text = "";
                //lbl_place_value_sortie_proche.Text = "";
                //lbl_place_value_sortie_rapide.Text = "";
            }
        }

        private void btn_floor1_Click(object sender, EventArgs e)
        {
            //Change l'affichage de l'étage en cours par celui de l'étage 1
            grp_parking.Controls.Clear();
            displayFloor(listPictFloor1);
            grp_detail_place.Hide();
            grp_detail_etage.Show();
            grp_detail_etage.Text = "Détails : Etage 1";
            actualFloor = 1;

            int nbTotal = floor1.slotList.Count;
            int nbDispo = floor1.statsDisponible();
            int nbOccupee = floor1.statsOccupee();
            int nbReservee = floor1.statsReservee();

            lbl_etage_nb_places.Text = nbTotal.ToString();
            lbl_etage_nb_places_dispo.Text = nbDispo.ToString();
            lbl_etage_nb_places_occupees.Text = nbOccupee.ToString();
            lbl_etage_nb_places_reservees.Text = nbReservee.ToString();

        }

        private void btn_floor2_Click(object sender, EventArgs e)
        {
            //Change l'affichage de l'étage en cours par celui de l'étage 2
            grp_parking.Controls.Clear();
            displayFloor(listPictFloor2);
            grp_detail_place.Hide();
            grp_detail_etage.Show();
            grp_detail_etage.Text = "Détails : Etage 2";
            actualFloor = 2;

            int nbTotal = floor2.slotList.Count;
            int nbDispo = floor2.statsDisponible();
            int nbOccupee = floor2.statsOccupee();
            int nbReservee = floor2.statsReservee();

            lbl_etage_nb_places.Text = nbTotal.ToString();
            lbl_etage_nb_places_dispo.Text = nbDispo.ToString();
            lbl_etage_nb_places_occupees.Text = nbOccupee.ToString();
            lbl_etage_nb_places_reservees.Text = nbReservee.ToString();
        }

        private void btn_floor3_Click(object sender, EventArgs e)
        {
            //Change l'affichage de l'étage en cours par celui de l'étage 3
            grp_parking.Controls.Clear();
            displayFloor(listPictFloor3);
            grp_detail_place.Hide();
            grp_detail_etage.Show();
            grp_detail_etage.Text = "Détails : Etage 3";
            actualFloor = 3;

            int nbTotal = floor3.slotList.Count;
            int nbDispo = floor3.statsDisponible();
            int nbOccupee = floor3.statsOccupee();
            int nbReservee = floor3.statsReservee();

            lbl_etage_nb_places.Text = nbTotal.ToString();
            lbl_etage_nb_places_dispo.Text = nbDispo.ToString();
            lbl_etage_nb_places_occupees.Text = nbOccupee.ToString();
            lbl_etage_nb_places_reservees.Text = nbReservee.ToString();
        }

        private void btn_floor4_Click(object sender, EventArgs e)
        {
            //Change l'affichage de l'étage en cours par celui de l'étage 4
            grp_parking.Controls.Clear();
            displayFloor(listPictFloor4);
            grp_detail_place.Hide();
            grp_detail_etage.Show();
            grp_detail_etage.Text = "Détails : Etage 4";
            actualFloor = 4;

            int nbTotal = floor4.slotList.Count;
            int nbDispo = floor4.statsDisponible();
            int nbOccupee = floor4.statsOccupee();
            int nbReservee = floor4.statsReservee();

            lbl_etage_nb_places.Text = nbTotal.ToString();
            lbl_etage_nb_places_dispo.Text = nbDispo.ToString();
            lbl_etage_nb_places_occupees.Text = nbOccupee.ToString();
            lbl_etage_nb_places_reservees.Text = nbReservee.ToString();
        }

        private void btn_floor5_Click(object sender, EventArgs e)
        {
            //Change l'affichage de l'étage en cours par celui de l'étage 5
            grp_parking.Controls.Clear();
            displayFloor(listPictFloor5);
            grp_detail_place.Hide();
            grp_detail_etage.Show();
            grp_detail_etage.Text = "Détails : Etage 5";
            actualFloor = 5;

            int nbTotal = floor5.slotList.Count;
            int nbDispo = floor5.statsDisponible();
            int nbOccupee = floor5.statsOccupee();
            int nbReservee = floor5.statsReservee();

            lbl_etage_nb_places.Text = nbTotal.ToString();
            lbl_etage_nb_places_dispo.Text = nbDispo.ToString();
            lbl_etage_nb_places_occupees.Text = nbOccupee.ToString();
            lbl_etage_nb_places_reservees.Text = nbReservee.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void grp_detail_place_Enter(object sender, EventArgs e)
        {

        }

        private void rdb_place_disponible_CheckedChanged(object sender, EventArgs e)
        {

            if (actualFloor == 1)
            {
                //floor1.slotList[].pict = SlotState.Empty;
            }else if (actualFloor == 2)
            {
                //floor2.slotList[actualPict.Tag].pict = SlotState.Empty;
            }
            else if (actualFloor == 3)
            {
                //floor3.slotList[actualPict.Tag].pict = SlotState.Empty;
            }
            else if(actualFloor == 4)
            {
                //floor4.slotList[actualPict.Tag].pict = SlotState.Empty;
            }
            else
            {
               //floor5.slotList[actualPict.Tag].pict = SlotState.Empty;
            }
        }

        public void setController(IFloorController floorCont)
        {
            floorController = floorCont;
        }

        public void setController(ISlotController slotCont)
        {
            slotController = slotCont;
        }


        //Modif
        public void generatePictFloor(List<PictureBox> listPict, List<Slot> listFloor)
        {
            for (int i=0; i<listFloor.Count; i++)
            {
                PictureBox tempPict = new PictureBox();
                tempPict.Location = new Point(listFloor[i].location.x, listFloor[i].location.y);
                tempPict.Height = Height;
                tempPict.Width = Width;
                tempPict.BackColor = Color.Green;
                tempPict.Click += new EventHandler(Picture_Click);
                tempPict.Tag = i;
                listPict.Add(tempPict);
            }
        }

        public void displayFloor(List<PictureBox> listPict)
        {
            for(int i=0; i<listPict.Count; i++)
            {
                grp_parking.Controls.Add(listPict[i]);
            }
        }
    }
}
