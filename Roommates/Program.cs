﻿using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using Roommates.Models;
using Roommates.Repositories;

namespace Roommates
{
    class Program
    {
        /// <summary>
        ///  This is the address of the database.
        ///  We define it here as a constant since it will never change.
        /// </summary>
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            //RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);

            //Console.WriteLine("Getting All Rooms:");
            //Console.WriteLine();

            //List<Room> allRooms = roomRepo.GetAll();

            //foreach (Room room in allRooms)
            //{
            //    Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
            //}

            //Console.WriteLine("----------------------------");
            //Console.WriteLine("Getting Room with Id 1");

            //Room singleRoom = roomRepo.GetById(1);

            //Console.WriteLine($"{singleRoom.Id} {singleRoom.Name} {singleRoom.MaxOccupancy}");

            //Room bathroom = new Room
            //{
            //    Name = "Bathroom",
            //    MaxOccupancy = 1
            //};

            //roomRepo.Delete(11);

            //roomRepo.Insert(bathroom);

            //Console.WriteLine("-------------------------------");
            //Console.WriteLine($"Added the new Room with id {bathroom.Id}");

            //Room updatedBathroom = new Room
            //{
            //    Id = 7,
            //    Name = "Bathroom",
            //    MaxOccupancy = 2
            //};

            //roomRepo.Update(updatedBathroom);

            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            Console.WriteLine("Getting all roommates: ");

            //List<Roommate> roommates = roommateRepo.GetAll();

            //foreach(Roommate rm in roommates)
            //{
            //    Console.WriteLine(@$"
            //        Id: {rm.Id}
            //        {rm.Firstname} {rm.Lastname}
            //        Rent Portion: {rm.RentPortion}
            //        Move In Date: {rm.MovedInDate}
            //    ");
            //}

            //Roommate id1 = roommateRepo.GetById(1);

            //Console.WriteLine(@$"
            //        {id1.Firstname} {id1.Lastname}
            //        Rent Portion: {id1.RentPortion}
            //        Move In Date: {id1.MovedInDate}
            //    ");

            List<Roommate> roommates = roommateRepo.GetAllWithRoom();

            foreach (Roommate rm in roommates)
            {
                Console.WriteLine(@$"
                    Id: {rm.Id}
                    {rm.Firstname} {rm.Lastname}
                    Rent Portion: {rm.RentPortion}
                    Move In Date: {rm.MovedInDate}
                    Room: {rm.Room.Name}
                ");
            }

        }
    }
}