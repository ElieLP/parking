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

        Parking parking;

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
        List<Exit> listExitFloor1;
        List<Exit> listExitFloor2;
        List<Exit> listExitFloor3;
        List<Exit> listExitFloor4;
        List<Exit> listExitFloor5;

        //Création de la liste des PictureBox par étage
        List<PictureBox> listPictFloor1;
        List<PictureBox> listPictFloor2;
        List<PictureBox> listPictFloor3;
        List<PictureBox> listPictFloor4;
        List<PictureBox> listPictFloor5;
        List<PictureBox> listExitPictFloor1;
        List<PictureBox> listExitPictFloor2;
        List<PictureBox> listExitPictFloor3;
        List<PictureBox> listExitPictFloor4;
        List<PictureBox> listExitPictFloor5;

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
            parking = new Parking();

            actualFloor = 1;

            //Instancation des Etages
            listFloor1 = new List<Slot>();
            listFloor2 = new List<Slot>();
            listFloor3 = new List<Slot>();
            listFloor4 = new List<Slot>();
            listFloor5 = new List<Slot>();
            listExitFloor1 = new List<Exit>();
            listExitFloor2 = new List<Exit>();
            listExitFloor3 = new List<Exit>();
            listExitFloor4 = new List<Exit>();
            listExitFloor5 = new List<Exit>();

            //Instanciation des List de PictureBox
            listPictFloor1 = new List<PictureBox>();
            listPictFloor2 = new List<PictureBox>();
            listPictFloor3 = new List<PictureBox>();
            listPictFloor4 = new List<PictureBox>();
            listPictFloor5 = new List<PictureBox>();
            listExitPictFloor1 = new List<PictureBox>();
            listExitPictFloor2 = new List<PictureBox>();
            listExitPictFloor3 = new List<PictureBox>();
            listExitPictFloor4 = new List<PictureBox>();
            listExitPictFloor5 = new List<PictureBox>();

            //Instancation des listes pas Etages
            floor1 = new Floor(1, listFloor1, listExitFloor1);
            floor2 = new Floor(2, listFloor2, listExitFloor2);
            floor3 = new Floor(3, listFloor3, listExitFloor3);
            floor4 = new Floor(4, listFloor4, listExitFloor4);
            floor5 = new Floor(5, listFloor5, listExitFloor5);

            //Affectation des places des Etages
            floor1.generateFloor1();
            floor2.generateFloor2();
            floor3.generateFloor2();
            floor4.generateFloor3();
            floor5.generateFloor3();

            //Affecation des pictureBox
            generatePictFloor(listPictFloor1, listFloor1, listExitPictFloor1, listExitFloor1);
            generatePictFloor(listPictFloor2, listFloor2, listExitPictFloor2, listExitFloor2);
            generatePictFloor(listPictFloor3, listFloor3, listExitPictFloor3, listExitFloor3);
            generatePictFloor2(listPictFloor4, listFloor4, listExitPictFloor4, listExitFloor4);
            generatePictFloor2(listPictFloor5, listFloor5, listExitPictFloor5, listExitFloor5);


            //Affichage du premier Etage
            displayFloor(listPictFloor1,listExitPictFloor1);
            grp_detail_etage.Show();
            displayFloorStats(floor1);
            grp_detail_place.Hide();            
            displayTotalStats();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void grp_detail_place_Enter(object sender, EventArgs e)
        {

        }       

        public void setController(IFloorController floorCont)
        {
            floorController = floorCont;
        }

        public void setController(ISlotController slotCont)
        {
            slotController = slotCont;
        }

        //Génération de PictureBox
        public void generatePictFloor(List<PictureBox> listPict, List<Slot> listFloor, List<PictureBox> listExitPict, List<Exit> listExit)
        {
            for (int i = 0; i < listFloor.Count; i++)
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

            for(int i = 0; i < listExit.Count; i++)
            {
                PictureBox tempPict2 = new PictureBox();
                tempPict2.Location = new Point(listExit[i].location.x, listExit[i].location.y);
                tempPict2.Height = 50;
                tempPict2.Width = 10;
                tempPict2.BackColor = Color.Green;
                tempPict2.Tag = i;
                tempPict2.Click += new EventHandler(Exit_Click);
                listExitPict.Add(tempPict2);
            }
        }

        public void generatePictFloor2(List<PictureBox> listPict, List<Slot> listFloor, List<PictureBox> listExitPict, List<Exit> listExit)
        {
            for (int i = 0; i < listFloor.Count; i++)
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

            for (int i = 0; i < listExit.Count; i++)
            {
                PictureBox tempPict2 = new PictureBox();
                tempPict2.Location = new Point(listExit[i].location.x, listExit[i].location.y);
                tempPict2.Height = 10;
                tempPict2.Width = 50;
                tempPict2.BackColor = Color.Green;
                tempPict2.Tag = i;
                tempPict2.Click += new EventHandler(Exit_Click);
                listExitPict.Add(tempPict2);
            }
        }

        //Affichage
        public void displayFloor(List<PictureBox> listPict, List<PictureBox> listExitPict)
        {
            for (int i = 0; i < listPict.Count; i++)
            {
                grp_parking.Controls.Add(listPict[i]);
            }

            for(int i = 0; i < listExitPict.Count; i++)
            {
                grp_parking.Controls.Add(listExitPict[i]);
            }
        }

        public void displayTotalStats()
        {
            int totalPlace = listFloor1.Count + listFloor2.Count + listFloor3.Count + listFloor4.Count + listFloor5.Count;
            int totalDispo = floor1.statsDisponible() + floor2.statsDisponible() + floor3.statsDisponible() + floor4.statsDisponible() + floor5.statsDisponible();
            int totalOccupee = floor1.statsOccupee() + floor2.statsOccupee() + floor3.statsOccupee() + floor4.statsOccupee() + floor5.statsOccupee();
            int totalReservee = floor1.statsReservee() + floor2.statsReservee() + floor3.statsReservee() + floor4.statsReservee() + floor5.statsReservee();

            lbl_nb_stats_places.Text = totalPlace.ToString();
            lbl_nb_stats_places_dispo.Text = totalDispo.ToString();
            lbl_nb_stats_places_occupees.Text = totalOccupee.ToString();
            lbl_nb_stats_places_reservees.Text = totalReservee.ToString();
            lbl_nb_stats_voiture_today.Text = parking.CarCount.ToString();
            lbl_nb_stats_somme.Text = parking.SommeCollectee.ToString() + "€";
            lbl_nb_tarif.Text = parking.Tarif.ToString() + "€/h";
            lbl_value_ouverture.Text = parking.Ouverture.Hours.ToString() + "h" + parking.Ouverture.Minutes.ToString();
            lbl_value_fermeture.Text = parking.Fermeture.Hours.ToString() + "h" + parking.Ouverture.Minutes.ToString();

        }//Fini

        public void displayFloorStats(Floor p_f)
        {
            int nbTotal = p_f.slotList.Count;
            int nbDispo = p_f.statsDisponible();
            int nbOccupee = p_f.statsOccupee();
            int nbReservee = p_f.statsReservee();

            lbl_etage_nb_places.Text = nbTotal.ToString();
            lbl_etage_nb_places_dispo.Text = nbDispo.ToString();
            lbl_etage_nb_places_occupees.Text = nbOccupee.ToString();
            lbl_etage_nb_places_reservees.Text = nbReservee.ToString();
            lbl_etage_nb_entrees.Text = (p_f.exitList.Count / 2).ToString();
            lbl_etage_nb_sortie.Text = (p_f.exitList.Count / 2).ToString();
            lbl_etage_nb_voitures.Text = p_f.carCount.ToString();
        }//Fini

        public void displayPlaceDispoStats(Slot p_s)
        {
            TimeSpan dispoTime = DateTime.Now - p_s.lastTimeTaken;
            String dispoString = dispoTime.Hours.ToString() + "h" + dispoTime.Minutes.ToString();
            rdb_place_disponible.Checked = true;
            rdb_place_occupee.Checked = false;
            rdb_place_reservee.Checked = false;
            lbl_place_value_disponible.Text = dispoString;
            lbl_place_value_occupee.Text = "-";
            lbl_place_value_timeUse.Text = p_s.NbCar.ToString();

            double minDistance = 10000000000;
            Exit minExit = new Exit(0, new Coord(0, 0));

            double quickDistance = 10000000000;
            Exit quickExit = new Exit(0, new Coord(0, 0));

            switch (actualFloor)
            {
                case 1:
                    for (int i=0;i<listExitFloor1.Count;i++)
                    {
                        if(listExitFloor1[i].State == ExitState.Opened)
                        {
                            double distance = distanceSortie(p_s, listExitFloor1[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minExit = listExitFloor1[i];
                            }
                            //Rapide
                            if (listExitFloor1[i].Sature == true)
                            {
                                distance = distanceSortie2(p_s, listExitFloor1[i]);
                            }
                            if (distance < quickDistance)
                            {
                                quickDistance = distance;
                                quickExit = listExitFloor1[i];
                            }
                        }                        
                    }
                    break;

                case 2:
                    for (int i = 0; i < listExitFloor2.Count; i++)
                    {
                        if(listExitFloor2[i].State == ExitState.Opened)
                        {
                            double distance = distanceSortie(p_s, listExitFloor2[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minExit = listExitFloor2[i];
                            }

                            //Rapide
                            if (listExitFloor1[i].Sature == true)
                            {
                                distance = distanceSortie2(p_s, listExitFloor1[i]);
                            }
                            if (distance < quickDistance)
                            {
                                quickDistance = distance;
                                quickExit = listExitFloor2[i];
                            }
                        }
                    }
                    break;

                case 3:
                    for (int i = 0; i < listExitFloor3.Count; i++)
                    {
                        if(listExitFloor3[i].State == ExitState.Opened)
                        {
                            double distance = distanceSortie(p_s, listExitFloor3[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minExit = listExitFloor3[i];
                            }

                            //Rapide
                            if (listExitFloor1[i].Sature == true)
                            {
                                distance = distanceSortie2(p_s, listExitFloor1[i]);
                            }
                            if (distance < quickDistance)
                            {
                                quickDistance = distance;
                                quickExit = listExitFloor3[i];
                            }
                        }  
                    }
                    break;

                case 4:
                    for (int i = 0; i < listExitFloor4.Count; i++)
                    {
                        if(listExitFloor4[i].State == ExitState.Opened)
                        {
                            double distance = distanceSortie(p_s, listExitFloor4[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minExit = listExitFloor4[i];
                            }

                            //Rapide
                            if (listExitFloor1[i].Sature == true)
                            {
                                distance = distanceSortie2(p_s, listExitFloor1[i]);
                            }
                            if (distance < quickDistance)
                            {
                                quickDistance = distance;
                                quickExit = listExitFloor4[i];
                            }
                        }
                        
                    }
                    break;

                case 5:
                    for (int i = 0; i < listExitFloor5.Count; i++)
                    {
                        if(listExitFloor5[i].State == ExitState.Opened)
                        {
                            double distance = distanceSortie(p_s, listExitFloor5[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minExit = listExitFloor5[i];
                            }

                            //Rapide
                            if (listExitFloor1[i].Sature == true)
                            {
                                distance = distanceSortie2(p_s, listExitFloor1[i]);
                            }
                            if (distance < quickDistance)
                            {
                                quickDistance = distance;
                                quickExit = listExitFloor5[i];
                            }
                        }                        
                    }
                    break;
            }

            lbl_place_value_sortie_proche.Text = "Sortie " + minExit.ID;
            lbl_place_value_sortie_rapide.Text = "Sortie " + quickExit.ID;
        }

        public void displayPlaceReserveStats(Slot p_s)
        {
            TimeSpan reservedTime = DateTime.Now - p_s.lastTimeReserved;
            String reservedString = reservedTime.Hours.ToString() + "h" + reservedTime.Minutes.ToString();
            rdb_place_disponible.Checked = false;
            rdb_place_occupee.Checked = false;
            rdb_place_reservee.Checked = true;
            lbl_place_value_disponible.Text = "-";
            lbl_place_value_occupee.Text = reservedString;
            lbl_place_value_timeUse.Text = p_s.NbCar.ToString();

            double minDistance = 10000000000;
            Exit minExit = new Exit(0, new Coord(0, 0));

            double quickDistance = 10000000000;
            Exit quickExit = new Exit(0, new Coord(0, 0));

            switch (actualFloor)
            {
                case 1:
                    for (int i = 0; i < listExitFloor1.Count; i++)
                    {
                        if(listExitFloor1[i].State == ExitState.Opened)
                        {
                            double distance = distanceSortie(p_s, listExitFloor1[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minExit = listExitFloor1[i];
                            }

                            //Rapide
                            if (listExitFloor1[i].Sature == true)
                            {
                                distance = distanceSortie2(p_s, listExitFloor1[i]);
                            }
                            if (distance < quickDistance)
                            {
                                quickDistance = distance;
                                quickExit = listExitFloor1[i];
                            }
                        }
                        
                    }
                    break;

                case 2:
                    for (int i = 0; i < listExitFloor2.Count; i++)
                    {
                        if(listExitFloor2[i].State == ExitState.Opened)
                        {
                            double distance = distanceSortie(p_s, listExitFloor2[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minExit = listExitFloor2[i];
                            }

                            //Rapide
                            if (listExitFloor1[i].Sature == true)
                            {
                                distance = distanceSortie2(p_s, listExitFloor1[i]);
                            }
                            if (distance < quickDistance)
                            {
                                quickDistance = distance;
                                quickExit = listExitFloor2[i];
                            }
                        }
                        
                    }
                    break;

                case 3:
                    for (int i = 0; i < listExitFloor3.Count; i++)
                    {
                        if(listExitFloor3[i].State == ExitState.Opened)
                        {
                            double distance = distanceSortie(p_s, listExitFloor3[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minExit = listExitFloor3[i];
                            }

                            //Rapide
                            if (listExitFloor1[i].Sature == true)
                            {
                                distance = distanceSortie2(p_s, listExitFloor1[i]);
                            }
                            if (distance < quickDistance)
                            {
                                quickDistance = distance;
                                quickExit = listExitFloor3[i];
                            }
                        }
                        
                    }
                    break;

                case 4:
                    for (int i = 0; i < listExitFloor4.Count; i++)
                    {
                        if(listExitFloor4[i].State == ExitState.Opened)
                        {
                            double distance = distanceSortie(p_s, listExitFloor4[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minExit = listExitFloor4[i];
                            }

                            //Rapide
                            if (listExitFloor1[i].Sature == true)
                            {
                                distance = distanceSortie2(p_s, listExitFloor1[i]);
                            }
                            if (distance < quickDistance)
                            {
                                quickDistance = distance;
                                quickExit = listExitFloor4[i];
                            }
                        }                        
                    }
                    break;

                case 5:
                    for (int i = 0; i < listExitFloor5.Count; i++)
                    {
                        if(listExitFloor5[i].State == ExitState.Opened)
                        {
                            double distance = distanceSortie(p_s, listExitFloor5[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minExit = listExitFloor5[i];
                            }

                            //Rapide
                            if (listExitFloor1[i].Sature == true)
                            {
                                distance = distanceSortie2(p_s, listExitFloor1[i]);
                            }
                            if (distance < quickDistance)
                            {
                                quickDistance = distance;
                                quickExit = listExitFloor5[i];
                            }
                        }
                        
                    }
                    break;
            }

            lbl_place_value_sortie_proche.Text = "Sortie " + minExit.ID;
            lbl_place_value_sortie_rapide.Text = "Sortie " + quickExit.ID;
        }

        public void displayPlaceUsedStats(Slot p_s)
        {
            TimeSpan usedTime = DateTime.Now - p_s.lastTimeTaken;
            String usedString = usedTime.Hours.ToString() + "h" + usedTime.Minutes.ToString();
            rdb_place_disponible.Checked = false;
            rdb_place_occupee.Checked = true;
            rdb_place_reservee.Checked = false;
            lbl_place_value_disponible.Text = "-";
            lbl_place_value_occupee.Text = usedString;
            lbl_place_value_timeUse.Text = p_s.NbCar.ToString();

            double minDistance = 10000000000;
            Exit minExit = new Exit(0, new Coord(0, 0));

            double quickDistance = 10000000000;
            Exit quickExit = new Exit(0, new Coord(0, 0));

            switch (actualFloor)
            {
                case 1:
                    for (int i = 0; i < listExitFloor1.Count; i++)
                    {
                        if(listExitFloor1[i].State == ExitState.Opened)
                        {

                            //Distance
                            double distance = distanceSortie(p_s, listExitFloor1[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minExit = listExitFloor1[i];
                            }

                            //Rapide
                            if (listExitFloor1[i].Sature == true)
                            {
                                distance = distanceSortie2(p_s, listExitFloor1[i]);
                            }
                            if (distance < quickDistance)
                            {
                                quickDistance = distance;
                                quickExit = listExitFloor1[i];
                            }
                        }
                        
                    }
                    break;

                case 2:
                    for (int i = 0; i < listExitFloor2.Count; i++)
                    {
                        if(listExitFloor2[i].State == ExitState.Opened)
                        {
                            double distance = distanceSortie(p_s, listExitFloor2[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minExit = listExitFloor2[i];
                            }

                            //Rapide
                            if (listExitFloor1[i].Sature == true)
                            {
                                distance = distanceSortie2(p_s, listExitFloor1[i]);
                            }
                            if (distance < quickDistance)
                            {
                                quickDistance = distance;
                                quickExit = listExitFloor2[i];
                            }
                        }
                        

                    }
                    break;

                case 3:
                    for (int i = 0; i < listExitFloor3.Count; i++)
                    {
                        if(listExitFloor3[i].State == ExitState.Opened)
                        {
                            double distance = distanceSortie(p_s, listExitFloor3[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minExit = listExitFloor3[i];
                            }

                            //Rapide
                            if (listExitFloor1[i].Sature == true)
                            {
                                distance = distanceSortie2(p_s, listExitFloor1[i]);
                            }
                            if (distance < quickDistance)
                            {
                                quickDistance = distance;
                                quickExit = listExitFloor3[i];
                            }
                        }                        
                    }
                    break;

                case 4:
                    for (int i = 0; i < listExitFloor4.Count; i++)
                    {
                        if(listExitFloor4[i].State == ExitState.Opened)
                        {
                            double distance = distanceSortie(p_s, listExitFloor4[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minExit = listExitFloor4[i];
                            }

                            //Rapide
                            if (listExitFloor1[i].Sature == true)
                            {
                                distance = distanceSortie2(p_s, listExitFloor1[i]);
                            }
                            if (distance < quickDistance)
                            {
                                quickDistance = distance;
                                quickExit = listExitFloor4[i];
                            }
                        }
                        
                    }
                    break;

                case 5:
                    for (int i = 0; i < listExitFloor5.Count; i++)
                    {
                        if(listExitFloor5[i].State == ExitState.Opened)
                        {
                            double distance = distanceSortie(p_s, listExitFloor5[i]);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                minExit = listExitFloor5[i];
                            }

                            //Rapide
                            if (listExitFloor1[i].Sature == true)
                            {
                                distance = distanceSortie2(p_s, listExitFloor1[i]);
                            }
                            if (distance < quickDistance)
                            {
                                quickDistance = distance;
                                quickExit = listExitFloor5[i];
                            }
                        }
                        
                    }
                    break;
            }

            lbl_place_value_sortie_proche.Text = "Sortie " + minExit.ID;
            lbl_place_value_sortie_rapide.Text = "Sortie " + quickExit.ID;   
        }

        //Distance
        public double distanceSortie(Slot p_s, Exit p_e)
        {
            double distance;
            double x = Math.Pow(p_s.location.x - p_e.location.x, 2);
            double y = Math.Pow(p_s.location.y - p_e.location.y, 2);
            distance = Math.Sqrt(x + y);
            return distance;
        }

        public double distanceSortie2(Slot p_s, Exit p_e)
        {
            double distance;
            double x = Math.Pow(p_s.location.x - p_e.location.x, 2);
            double y = Math.Pow(p_s.location.y - p_e.location.y, 2);
            distance = Math.Sqrt(x + y);
            return distance + 30;
        }

        //Events
        public void Exit_Click(object sender, EventArgs e)
        {
            PictureBox pict = sender as PictureBox;
            actualPict = sender as PictureBox;

            Exit exit = new Exit(0,new Coord(0,0));

            if (actualFloor == 1)
            {
                exit = listExitFloor1[Convert.ToInt32(pict.Tag)];
            }
            else if (actualFloor == 2)
            {
                exit = listExitFloor2[Convert.ToInt32(pict.Tag)];
            }
            else if (actualFloor == 3)
            {
                exit = listExitFloor3[Convert.ToInt32(pict.Tag)];
            }
            else if (actualFloor == 4)
            {
                exit = listExitFloor4[Convert.ToInt32(pict.Tag)];
            }
            else if (actualFloor == 5)
            {
                exit = listExitFloor5[Convert.ToInt32(pict.Tag)];
            }

            grp_detail_sortie.Text = "Détails :  Sortie " + pict.Tag;

            if(exit.State == ExitState.Opened)
            {
                rdb_sortie_ouverte.Checked = true;
            }
            else
            {
                rdb_sortie_fermee.Checked = true;
            }

            if(exit.Sature == true)
            {
                chk_stats_sortie_saturee.Checked = true;
            }
            else
            {
                chk_stats_sortie_saturee.Checked = false;
            }

            grp_detail_etage.Hide();
            grp_detail_place.Hide();
            grp_detail_sortie.Show();
            grp_detail_sortie.Show();

            lbl_stats_sortie_nb_voiture.Text = Convert.ToString(exit.nbVoiture);
        }

        public void Picture_Click(object sender, EventArgs e)
        {
            PictureBox pict = sender as PictureBox;
            actualPict = sender as PictureBox;

            Slot slot = new Slot(0, new Coord(0, 0));

            if (actualFloor == 1)
            {
                slot = listFloor1[Convert.ToInt32(pict.Tag)];
            }
            else if (actualFloor == 2)
            {
                slot = listFloor2[Convert.ToInt32(pict.Tag)];
            }
            else if (actualFloor == 3)
            {
                slot = listFloor3[Convert.ToInt32(pict.Tag)];
            }
            else if (actualFloor == 4)
            {
                slot = listFloor4[Convert.ToInt32(pict.Tag)];
            }
            else if (actualFloor == 5)
            {
                slot = listFloor5[Convert.ToInt32(pict.Tag)];
            }


            grp_detail_place.Text = "Détails : Place " + pict.Tag;
            grp_detail_etage.Hide();
            grp_detail_sortie.Hide();
            grp_detail_place.Show();


            if (slot.state == SlotState.Empty)
            {
                displayPlaceDispoStats(slot);
            }
            else if (slot.state == SlotState.Used)
            {
                displayPlaceUsedStats(slot);
            }
            else if (slot.state == SlotState.Reserved)
            {
                displayPlaceReserveStats(slot);   
            }
        }

        //Button
        private void btn_floor1_Click(object sender, EventArgs e)
        {
            //Change l'affichage de l'étage en cours par celui de l'étage 1
            grp_parking.Controls.Clear();
            displayFloor(listPictFloor1, listExitPictFloor1);
            grp_detail_place.Hide();
            grp_detail_etage.Show();
            grp_detail_etage.Text = "Détails : Etage 1";

            actualFloor = 1;

            displayFloorStats(floor1);

        }

        private void btn_floor2_Click(object sender, EventArgs e)
        {
            //Change l'affichage de l'étage en cours par celui de l'étage 2
            grp_parking.Controls.Clear();
            displayFloor(listPictFloor2, listExitPictFloor2);
            grp_detail_place.Hide();
            grp_detail_etage.Show();
            grp_detail_etage.Text = "Détails : Etage 2";
            actualFloor = 2;

            displayFloorStats(floor2);
        }

        private void btn_floor3_Click(object sender, EventArgs e)
        {
            //Change l'affichage de l'étage en cours par celui de l'étage 3
            grp_parking.Controls.Clear();
            displayFloor(listPictFloor3, listExitPictFloor3);
            grp_detail_place.Hide();
            grp_detail_etage.Show();
            grp_detail_etage.Text = "Détails : Etage 3";
            actualFloor = 3;

            displayFloorStats(floor3);
        }

        private void btn_floor4_Click(object sender, EventArgs e)
        {
            //Change l'affichage de l'étage en cours par celui de l'étage 4
            grp_parking.Controls.Clear();
            displayFloor(listPictFloor4,listExitPictFloor4);
            grp_detail_place.Hide();
            grp_detail_etage.Show();
            grp_detail_etage.Text = "Détails : Etage 4";
            actualFloor = 4;

            displayFloorStats(floor4);
        }

        private void btn_floor5_Click(object sender, EventArgs e)
        {
            //Change l'affichage de l'étage en cours par celui de l'étage 5
            grp_parking.Controls.Clear();
            displayFloor(listPictFloor5, listExitPictFloor5);
            grp_detail_place.Hide();
            grp_detail_etage.Show();
            grp_detail_etage.Text = "Détails : Etage 5";
            actualFloor = 5;

            displayFloorStats(floor5);
        }

        //RadioButton
        private void rdb_place_occupee_CheckedChanged(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            if(date.Hour <= parking.Ouverture.Hours)
            {
                if(date.Minute < parking.Ouverture.Minutes)
                {
                    return;
                }
            }
            else if(date.Hour >= parking.Fermeture.Hours)
            {
                if(date.Hour > parking.Fermeture.Hours)
                {
                    return;
                }
            }

            RadioButton rdb = sender as RadioButton;

            if(rdb.Checked == false)
            {
                return;
            }

            SlotState prevState = new SlotState();
            Slot slot;
            PictureBox pict;

            if (actualFloor == 1)
            {
                slot = this.floor1.slotList[Convert.ToInt32(actualPict.Tag)];
                pict = this.listPictFloor1[Convert.ToInt32(actualPict.Tag)];
            }
            else if (actualFloor == 2)
            {
                slot = this.floor2.slotList[Convert.ToInt32(actualPict.Tag)];
                pict = this.listPictFloor2[Convert.ToInt32(actualPict.Tag)];
            }
            else if (actualFloor == 3)
            {
                slot = this.floor3.slotList[Convert.ToInt32(actualPict.Tag)];
                pict = this.listPictFloor3[Convert.ToInt32(actualPict.Tag)];
            }
            else if (actualFloor == 4)
            {
                slot = this.floor4.slotList[Convert.ToInt32(actualPict.Tag)];
                pict = this.listPictFloor4[Convert.ToInt32(actualPict.Tag)];
            }
            else
            {
                slot = this.floor5.slotList[Convert.ToInt32(actualPict.Tag)];
                pict = this.listPictFloor5[Convert.ToInt32(actualPict.Tag)];
            }

            prevState = slot.state;
            slot.NewState(SlotState.Used);
            pict.BackColor = Color.Red;
            displayPlaceUsedStats(slot);

            if (prevState == SlotState.Empty)
            {
                slot.incValue();
                this.parking.incvalue();
                switch (actualFloor)
                {
                    case 1:
                        this.floor1.incvalue();
                        break;
                    case 2:
                        this.floor2.incvalue();
                        break;
                    case 3:
                        this.floor3.incvalue();
                        break;
                    case 4:
                        this.floor4.incvalue();
                        break;
                    case 5:
                        this.floor5.incvalue();
                        break;
                }
            }

            displayTotalStats();
        }

        private void rdb_place_reservee_CheckedChanged(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            if (date.Hour <= parking.Ouverture.Hours)
            {
                if (date.Minute < parking.Ouverture.Minutes)
                {
                    return;
                }
            }
            else if (date.Hour >= parking.Fermeture.Hours)
            {
                if (date.Hour > parking.Fermeture.Hours)
                {
                    return;
                }
            }

            RadioButton rdb = sender as RadioButton;

            if (rdb.Checked == false)
            {
                return;
            }

            SlotState prevState = new SlotState();
            Slot slot;
            PictureBox pict;

            if (actualFloor == 1)
            {
                slot = this.floor1.slotList[Convert.ToInt32(actualPict.Tag)];
                pict = this.listPictFloor1[Convert.ToInt32(actualPict.Tag)];
            }
            else if (actualFloor == 2)
            {
                slot = this.floor2.slotList[Convert.ToInt32(actualPict.Tag)];
                pict = this.listPictFloor2[Convert.ToInt32(actualPict.Tag)];
            }
            else if (actualFloor == 3)
            {
                slot = this.floor3.slotList[Convert.ToInt32(actualPict.Tag)];
                pict = this.listPictFloor3[Convert.ToInt32(actualPict.Tag)];
            }
            else if (actualFloor == 4)
            {
                slot = this.floor4.slotList[Convert.ToInt32(actualPict.Tag)];
                pict = this.listPictFloor4[Convert.ToInt32(actualPict.Tag)];
            }
            else
            {
                slot = this.floor5.slotList[Convert.ToInt32(actualPict.Tag)];
                pict = this.listPictFloor5[Convert.ToInt32(actualPict.Tag)];
            }

            prevState = slot.state;
            slot.NewState(SlotState.Reserved);
            pict.BackColor = Color.Orange;
            displayPlaceReserveStats(slot);

            if(prevState == SlotState.Empty)
            {
                slot.incValue();
                this.parking.incvalue();
                switch (actualFloor)
                {
                    case 1:
                        this.floor1.incvalue();
                        break;
                    case 2:
                        this.floor2.incvalue();
                        break;
                    case 3:
                        this.floor3.incvalue();
                        break;
                    case 4:
                        this.floor4.incvalue();
                        break;
                    case 5:
                        this.floor5.incvalue();
                        break;
                }
            }
            displayTotalStats();
        }

        private void rdb_place_disponible_CheckedChanged(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            if (date.Hour <= parking.Ouverture.Hours)
            {
                if (date.Minute < parking.Ouverture.Minutes)
                {
                    return;
                }
            }
            else if (date.Hour >= parking.Fermeture.Hours)
            {
                if (date.Hour > parking.Fermeture.Hours)
                {
                    return;
                }
            }

            RadioButton rdb = sender as RadioButton;

            if (rdb.Checked == false)
            {
                return;
            }

            SlotState prevState = new SlotState();
            Slot slot;
            PictureBox pict;

            if (actualFloor == 1)
            {
                slot = this.floor1.slotList[Convert.ToInt32(actualPict.Tag)];
                pict = this.listPictFloor1[Convert.ToInt32(actualPict.Tag)];
            }
            else if (actualFloor == 2)
            {
                slot = this.floor2.slotList[Convert.ToInt32(actualPict.Tag)];
                pict = this.listPictFloor2[Convert.ToInt32(actualPict.Tag)];
            }
            else if (actualFloor == 3)
            {
                slot = this.floor3.slotList[Convert.ToInt32(actualPict.Tag)];
                pict = this.listPictFloor3[Convert.ToInt32(actualPict.Tag)];
            }
            else if (actualFloor == 4)
            {
                slot = this.floor4.slotList[Convert.ToInt32(actualPict.Tag)];
                pict = this.listPictFloor4[Convert.ToInt32(actualPict.Tag)];
            }
            else
            {
                slot = this.floor5.slotList[Convert.ToInt32(actualPict.Tag)];
                pict = this.listPictFloor5[Convert.ToInt32(actualPict.Tag)];
            }

            prevState = slot.state;

            if(prevState != SlotState.Empty)
            {
                if(prevState == SlotState.Reserved)
                {
                    TimeSpan usedTime = DateTime.Now - slot.lastTimeReserved;
                    parking.pay(usedTime);
                }
                else if(prevState == SlotState.Used)
                {
                    TimeSpan usedTime = DateTime.Now - slot.lastTimeTaken;
                    parking.pay(usedTime);
                }                
            }

            slot.NewState(SlotState.Empty);
            pict.BackColor = Color.Green;
            displayPlaceDispoStats(slot);

            displayTotalStats();
        }

        private void rdb_sortie_ouverte_CheckedChanged(object sender, EventArgs e)
        {
            
        }//Fail

        private void rdb_sortie_fermee_CheckedChanged(object sender, EventArgs e)
        {
            
        }//Fail

        private void rdb_sortie_ouverte_CheckedChanged_1(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;

            if (rdb.Checked == false)
            {
                return;
            }

            Exit exit;

            if (actualFloor == 1)
            {
                exit = this.floor1.exitList[Convert.ToInt32(actualPict.Tag)];
                if(exit.Sature == false)
                {
                    listExitPictFloor1[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Green;
                }
                else
                {
                    listExitPictFloor1[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Orange;
                }
            }
            else if (actualFloor == 2)
            {
                exit = this.floor2.exitList[Convert.ToInt32(actualPict.Tag)];
                if (exit.Sature == false)
                {
                    listExitPictFloor2[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Green;
                }
                else
                {
                    listExitPictFloor2[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Orange;
                }
            }
            else if (actualFloor == 3)
            {
                exit = this.floor3.exitList[Convert.ToInt32(actualPict.Tag)];
                if (exit.Sature == false)
                {
                    listExitPictFloor3[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Green;
                }
                else
                {
                    listExitPictFloor3[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Orange;
                }
            }
            else if (actualFloor == 4)
            {
                exit = this.floor4.exitList[Convert.ToInt32(actualPict.Tag)];
                if (exit.Sature == false)
                {
                    listExitPictFloor4[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Green;
                }
                else
                {
                    listExitPictFloor4[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Orange;
                }
            }
            else
            {
                exit = this.floor5.exitList[Convert.ToInt32(actualPict.Tag)];
                if (exit.Sature == false)
                {
                    listExitPictFloor5[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Green;
                }
                else
                {
                    listExitPictFloor5[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Orange;
                }
            }

            exit.State = ExitState.Opened;
        }

        private void rdb_sortie_fermee_CheckedChanged_1(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;

            Exit exit;

            if (actualFloor == 1)
            {
                exit = this.floor1.exitList[Convert.ToInt32(actualPict.Tag)];
                listExitPictFloor1[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Red;
            }
            else if (actualFloor == 2)
            {
                exit = this.floor2.exitList[Convert.ToInt32(actualPict.Tag)];
                listExitPictFloor2[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Red;
            }
            else if (actualFloor == 3)
            {
                exit = this.floor3.exitList[Convert.ToInt32(actualPict.Tag)];
                listExitPictFloor3[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Red;
            }
            else if (actualFloor == 4)
            {
                exit = this.floor4.exitList[Convert.ToInt32(actualPict.Tag)];
                listExitPictFloor4[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Red;
            }
            else
            {
                exit = this.floor5.exitList[Convert.ToInt32(actualPict.Tag)];
                listExitPictFloor5[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Red;
            }

            exit.State = ExitState.Closed;
        }

        private void chk_stats_sortie_saturee_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;

            Exit exit = new Exit(0, new Coord(0, 0));

            switch (actualFloor)
            {
                case 1:
                    exit = this.floor1.exitList[Convert.ToInt32(actualPict.Tag)];
                    break;
                case 2:
                    exit = this.floor2.exitList[Convert.ToInt32(actualPict.Tag)];
                    break;
                case 3:
                    exit = this.floor3.exitList[Convert.ToInt32(actualPict.Tag)];
                    break;
                case 4:
                    exit = this.floor4.exitList[Convert.ToInt32(actualPict.Tag)];
                    break;
                case 5:
                    exit = this.floor5.exitList[Convert.ToInt32(actualPict.Tag)];
                    break;
            }

            if(chk.Checked == true)
            {
                exit.Sature = true;
                switch (actualFloor)
                {
                    case 1:
                        if (exit.State == ExitState.Opened)
                        {
                            listExitPictFloor1[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Orange;
                        }
                        else
                        {
                            listExitPictFloor1[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Red;
                        }
                        break;
                    case 2:
                        if (exit.State == ExitState.Opened)
                        {
                            listExitPictFloor2[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Orange;
                        }
                        else
                        {
                            listExitPictFloor2[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Red;
                        }
                        break;
                    case 3:
                        if (exit.State == ExitState.Opened)
                        {
                            listExitPictFloor3[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Orange;
                        }
                        else
                        {
                            listExitPictFloor3[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Red;
                        }
                        break;
                    case 4:
                        if (exit.State == ExitState.Opened)
                        {
                            listExitPictFloor4[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Orange;
                        }
                        else
                        {
                            listExitPictFloor4[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Red;
                        }
                        break;
                    case 5:
                        if (exit.State == ExitState.Opened)
                        {
                            listExitPictFloor5[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Orange;
                        }
                        else
                        {
                            listExitPictFloor5[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Red;
                        }
                        break;
                }
            }
            else
            {
                exit.Sature = false;
                switch(actualFloor)
                {
                    case 1:
                        listExitPictFloor1[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Green;
                        break;
                    case 2:
                        listExitPictFloor2[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Green;
                        break;
                    case 3:
                        listExitPictFloor3[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Green;
                        break;
                    case 4:
                        listExitPictFloor4[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Green;
                        break;
                    case 5:
                        listExitPictFloor5[Convert.ToInt32(actualPict.Tag)].BackColor = Color.Green;
                        break;
                }
            }

            
        }
    }
}