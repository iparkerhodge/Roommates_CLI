using System;
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

            //RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            //Console.WriteLine("Getting all roommates: ");

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

            //List<Roommate> roommates = roommateRepo.GetAllWithRoom();

            //foreach (Roommate rm in roommates)
            //{
            //    Console.WriteLine(@$"
            //        Id: {rm.Id}
            //        {rm.Firstname} {rm.Lastname}
            //        Rent Portion: {rm.RentPortion}
            //        Move In Date: {rm.MovedInDate}
            //        Room: {rm.Room.Name}
            //    ");
            //}

            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            bool app = true;

            while (app == true)
            {

                int choice = -1;

                List<int> allRoomIds = roomRepo.GetAllIds();
                List<int> allRoommateIds = roommateRepo.GetAllIds();

                while (true)
                {
                    Console.WriteLine(@"
                Welcome to Chore Manager!
                -------------------------
                Select an option:
                0 List all rooms
                1 List room by Id
                2 Add a room
                3 Delete a room
                4 Edit a room
                5 List all roommates
                6 List roommate by Id
                7 Add a roommate
                8 Edit a roommate
                9 Delete a roommate
                ");

                    string resp = Console.ReadLine();

                    if (resp == "")
                    {
                        app = false;
                        break;
                    }
                    else
                    {
                        bool allowed = int.TryParse(resp, out choice);

                        if (allowed && choice >= 0 && choice < 10)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Not a valid choice.");
                        }
                    }

                }

                switch (choice)
                {
                    case 0:
                        List<Room> allRooms = roomRepo.GetAll();
                        foreach (Room room in allRooms)
                        {
                            Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
                        }
                        break;
                    case 1:
                        int roomId = -1;
                        while (true)
                        {
                            Console.WriteLine("Input Room Id: ");
                            bool allowed = int.TryParse(Console.ReadLine(), out roomId);
                            if (allowed && allRoomIds.Contains(roomId))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Id. Choice is not an interger or Id does not exist.");
                            }
                        }
                        Console.WriteLine($"Getting Room with Id {roomId}");
                        Room singleRoom = roomRepo.GetById(roomId);
                        Console.WriteLine($"{singleRoom.Id} {singleRoom.Name} {singleRoom.MaxOccupancy}");
                        break;
                    case 2:
                        Console.WriteLine("Room name:");
                        string roomName = Console.ReadLine();
                        int maxOcc = -1;
                        while (true)
                        {
                            Console.WriteLine("Maximum occupancy: ");
                            bool allowed = int.TryParse(Console.ReadLine(), out maxOcc);
                            if (allowed && maxOcc > 0)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Value must be a postive number");
                            }
                        }
                        Room newRoom = new Room
                        {
                            Name = roomName,
                            MaxOccupancy = maxOcc
                        };
                        roomRepo.Insert(newRoom);
                        Console.WriteLine($"Added {newRoom.Name} with id {newRoom.Id}");
                        break;
                    case 3:
                        int roomToDelete = -1;
                        while (true)
                        {
                            Console.WriteLine("Input Id of room to be deleted: ");
                            string response = Console.ReadLine();
                            if (response == "")
                            {
                                break;
                            }
                            else
                            {
                                bool allowed = int.TryParse(response, out roomToDelete);
                                if (allowed && allRoomIds.Contains(roomToDelete))
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Room does not exist. Please enter a valid room Id.");
                                }
                            }
                        }
                        if (roomToDelete == -1)
                        {
                            break;
                        }
                        else
                        {
                            roomRepo.Delete(roomToDelete);
                            Console.WriteLine($"Deleted room with Id {roomToDelete}");
                            break;
                        }
                    case 4:
                        int roomToEditId = -1;
                        while (true)
                        {
                            Console.WriteLine("Enter Id of room to edit");
                            bool allowed = int.TryParse(Console.ReadLine(), out roomToEditId);
                            if(allowed && allRoomIds.Contains(roomToEditId))
                            {
                                Room roomToEdit = roomRepo.GetById(roomToEditId);
                                Console.WriteLine($"{roomToEdit.Id} {roomToEdit.Name} {roomToEdit.MaxOccupancy}");
                                Console.WriteLine("Room name: ");
                                string newName = Console.ReadLine();
                                if (newName == "")
                                {
                                    newName = roomToEdit.Name;
                                }
                                int newOcc = -1;
                                while(true)
                                {
                                    Console.WriteLine("Max occupancy: ");
                                    bool permitted = int.TryParse(Console.ReadLine(), out newOcc);
                                    if(permitted && newOcc > 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Value must be a number > 0.");
                                    }
                                }

                                Room editedRoom = new Room { };
                                editedRoom.Id = roomToEditId;
                                editedRoom.Name = newName;
                                editedRoom.MaxOccupancy = newOcc;
                                roomRepo.Update(editedRoom);
                                Console.WriteLine($"Edited room: {editedRoom.Id} {editedRoom.Name} {editedRoom.MaxOccupancy}");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Room Id invalid. Please enter a valid Id.");
                            }
                        }
                        break;
                    case 5:
                        List<Roommate> allRoommates = roommateRepo.GetAllWithRoom();
                        foreach(Roommate rm in allRoommates)
                        {
                            Console.WriteLine(@$"
                                Id: {rm.Id}
                                {rm.Firstname} {rm.Lastname}
                                Rent Portion: {rm.RentPortion}
                                Move In Date: {rm.MovedInDate}
                            ");
                        }
                        break;
                    case 6:
                        int roommateId = -1;
                        while (true)
                        {
                            Console.WriteLine("Input Roommate Id: ");
                            bool allowed = int.TryParse(Console.ReadLine(), out roommateId);
                            if (allowed && allRoommateIds.Contains(roommateId))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Id. Choice is not an interger or Id does not exist.");
                            }
                        }
                        Console.WriteLine($"Getting Roommate with Id {roommateId}");
                        Roommate singleRoommate = roommateRepo.GetById(roommateId);
                        Console.WriteLine(@$"
                                {singleRoommate.Firstname} {singleRoommate.Lastname}
                                Rent Portion: {singleRoommate.RentPortion}
                                Move In Date: {singleRoommate.MovedInDate},
                                Room: {singleRoommate.Room.Name}
                            ");
                        break;
                    case 7:
                        Console.WriteLine("Roommate first name:");
                        string roommateFirstName = Console.ReadLine();
                        Console.WriteLine("Roommate last name:");
                        string roommateLastName = Console.ReadLine();
                        int RentPortion = -1;
                        while (true)
                        {
                            Console.WriteLine("Rent Portion: ");
                            bool allowed = int.TryParse(Console.ReadLine(), out RentPortion);
                            if (allowed && RentPortion > 0 && RentPortion < 100)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Value must be a number between 0 and 100");
                            }
                        }
                        int RoomId = -1;
                        while (true)
                        {
                            Console.WriteLine("Room Id: ");
                            bool allowed = int.TryParse(Console.ReadLine(), out RoomId);
                            if (allowed && allRoomIds.Contains(RoomId))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Room does not exist. Enter a valid room Id.");
                            }
                        }
                        Roommate newRoommate = new Roommate
                        {
                            Firstname = roommateFirstName,
                            Lastname = roommateLastName,
                            RentPortion = RentPortion,
                            MovedInDate = DateTime.Now,
                            Room = new Room
                            {
                                Id = RoomId
                            }
                        };
                        roommateRepo.Insert(newRoommate);
                        Console.WriteLine($"Added {newRoommate.Firstname} {newRoommate.Lastname} with id {newRoommate.Id}");
                        break;
                };
            }
        }
    }
}