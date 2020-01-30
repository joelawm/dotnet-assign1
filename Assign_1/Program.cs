/*
 * Names: Joe Meyer, Huajian Huang
 * 
 * Assignment: Assign1
 * 
 * Function: The main fucntion of this program is to create a piece of software that allows the managment of properties in DeKalb.
 * 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Assign_1
{
    internal class Program
    {
        // the input data declares here
        private const string PersonFile = "../../p.txt";
        private const string HouseFile = "../../h.txt";
        private const string ApartmentFile = "../../a.txt";

        public static void Main(string[] args)
        {
            //start a new community for dekalb
            Community community = new Community(99999, "DeKalb", 0);

            // if PersonFile exists
            if (File.Exists(PersonFile))
            {
                // PersonFile declares here
                using (StreamReader sr = File.OpenText(PersonFile))
                {
                    // Split the data by '\n' and save them as 1d array
                    string[] input = sr.ReadToEnd().Split('\n');
                    int i = 0;
                    
                    do
                    {
                        // Split the data from input[] and save them in iInput[]
                        string[] iInput = input[i].Split('\t');

                        var id = UInt32.Parse(iInput[0]);
                        var lName = iInput[1];
                        var fName = iInput[2];
                        var occ = iInput[3];
                        var year = Int32.Parse(iInput[4]);
                        var month = Int32.Parse(iInput[5]);
                        var day = Int32.Parse(iInput[6]);
                        var dt = new DateTime(year, month, day);
                        var resId = iInput[7];

                        community.Residents.Add(new Person(id, dt, lName, fName, occ, resId));
                        i++;
                    } while (i < input.Length); // if i less than input[]'s length

                    sr.Close();
                }
            }
             
            // if HouseFile exists
            if (File.Exists(HouseFile))
            {
                using (StreamReader sr = File.OpenText(HouseFile))
                {
                    // split data by '\n' and save them in input array
                    string[] input = sr.ReadToEnd().Split('\n');
                    int i = 0;

                    do
                    {
                        // split data by '\t' and save them in iInput array
                        string[] iInput = input[i].Split('\t');
                        var id = UInt32.Parse(iInput[0]);
                        var oId = UInt32.Parse(iInput[1]);
                        var x = UInt32.Parse(iInput[2]);
                        var y = UInt32.Parse(iInput[3]);
                        var stAddr = iInput[4];
                        var city = iInput[5];
                        var state = iInput[6];
                        var zip = iInput[7];
                        var forSale = iInput[8].Equals("T");
                        var bedRoom = UInt32.Parse(iInput[9]);
                        var bath = UInt32.Parse(iInput[10]);
                        var sqft = UInt32.Parse(iInput[11]);
                        var garage = iInput[12].Equals("T");
                        var aGarage = iInput[13].Equals("T");
                        var floor = UInt32.Parse(iInput[14]);

                        House house = new House(id, x, y, oId, stAddr, city, state,
                            zip, forSale, bedRoom, bath, sqft, garage, aGarage, floor);
                        community.Props.Add(house);
                        i++;
                    } while (i < input.Length); // if i less than input array's length

                    sr.Close();
                }
            }

            // if ApartmentFile exists
            if (File.Exists(ApartmentFile))
            {
                using (StreamReader sr = File.OpenText(ApartmentFile))
                {                    
                    // split data by '\n' and save them in input array
                    string[] input = sr.ReadToEnd().Split('\n');
                    int i = 0;

                    do
                    {
                        // split data by '\t' and save them in input array
                        string[] iInput = input[i].Split('\t');
                        var id = UInt32.Parse(iInput[0]);
                        var oId = UInt32.Parse(iInput[1]);
                        var x = UInt32.Parse(iInput[2]);
                        var y = UInt32.Parse(iInput[3]);
                        var stAddr = iInput[4];
                        var city = iInput[5];
                        var state = iInput[6];
                        var zip = iInput[7];
                        var forSale = iInput[8].Equals("T");
                        var bedRoom = UInt32.Parse(iInput[9]);
                        var bath = UInt32.Parse(iInput[10]);
                        var sqft = UInt32.Parse(iInput[11]);
                        var unit = iInput[12];

                        Apartment apartment = new Apartment(id, x, y, oId, stAddr, city, state, zip, forSale, bedRoom,
                            bath, sqft, unit);
                        community.Props.Add(apartment);
                        i++;
                    } while (i < input.Length); // do if i less than input array's length

                    sr.Close();
                }
            }

            //if option does not equal 10 countinue the program
            int option = 0;
            while (option != 10)
            {
               //Options menu giving you access to the programs UI
               Console.WriteLine("1. Full property list.");
               Console.WriteLine("2. List addresses of either House or Apartment-type properties.");
               Console.WriteLine("3. List addresses of all for-sale properties");
               Console.WriteLine("4. List all residents of a community.");
               Console.WriteLine("5. List all residents of a property, by street address.");
               Console.WriteLine("6. Toggle a property, by street address, as being for-sale or not.");
               Console.WriteLine("7. Buy a for-sale property, by street address.");
               Console.WriteLine("8. Add yourself as ana occupant to property.");
               Console.WriteLine("9. Remove yourself as ans occupant to a property.");
               Console.WriteLine("10. Quit.\n");

               string inputOption = Console.ReadLine();

               // more option for the user to quit the program
               if (inputOption == "q" || inputOption == "Q" || inputOption == "e" || inputOption == "E"
                   || inputOption == "quit" || inputOption == "QUIT" || inputOption == "exit" || inputOption == "EXIT")
               {
                   option = 10;
               }
               // if the user type something other than option given
               else if (!Int32.TryParse(inputOption, out option))
               {
                   option = 0;
               }
               
               switch (option)
               {
                    //case one handles the Full property List
                   case 1:
                       Console.WriteLine($"<{community.Id}> {community.Name}. Population ({community.Population}). Mayor: {community.MayorID}");
                       

                        //itterate through all the propeties
                       foreach (var p in community.Props)
                       {
                           Console.WriteLine($"Property Address: {p.StreetAddr} / {p.City} / {p.State} / {p.Zip}");
                           
                           foreach (var r in community.Residents)
                           {
                               // if property matches that person
                               if (r.Id == p.OwnerId)
                               {
                                   Console.WriteLine($"\tOwned By {r.FullName} Age ({DateTime.Now.Year - r.Birthday.Year}) Occupation {r.Occupation}");
                                   break;
                               }
                           }

                           // if true return "for sale", else return "Not for sale"
                           string sale = p.ForSale ? "For sale" : "Not for sale";
                           Console.Write($"\t({sale}) {((Residential) p).Bedrooms} \\ {((Residential) p).Baths} \\ " +
                                             $"{((Residential) p).Sqft} sq.ft.");
                           
                           // if the property is not a house
                           if((p as House) != null)
                           {
                               string garage = ((House) p).Garage ? "With Garage" 
                                   : ((bool) ((House) p).AttatchedGarage ? "With attach garage" : "With no garage");
                               Console.WriteLine($"\n\t...{garage} : {((House) p).Flood} floor\n");
                           }
                           else
                           {
                               Console.WriteLine($" Apt.# {((Apartment)p).Unit}\n");
                           }
                       }
                       
                       Console.WriteLine("Press any key to continues.");
                       Console.ReadKey();
                       break;
                   // This case 2 list addresses of either house or apartment-type propeties
                   case 2:
                        //output for the UI
                       Console.WriteLine("Enter property type (House/Apartment):");
                        //take in input
                       string houseOrApart = Console.ReadLine();
                       Console.WriteLine($"List addresses of {houseOrApart} property in {community.Name} community.");
                       Console.WriteLine("---------------------------------------------------------------------------\n");

                        //check wether its a house or a apartmnet
                       switch (houseOrApart)
                       {
                            //house case
                           case "House":
                               foreach (var property in community.Props)
                               {
                                   // if the property is not a house
                                   if ((property as House) != null)
                                   {
                                       Console.WriteLine($"{property.StreetAddr} {property.City} {property.State}");
                                   }
                               }

                               break;
                            //apoatment case
                           case "Apartment":
                               foreach (var property in community.Props)
                               {
                                   // if the property is not a apartment
                                   if ((property as Apartment) != null)
                                   {
                                       Console.WriteLine($"{property.StreetAddr} Apt.# {((Apartment)property).Unit} {property.City} {property.State}");
                                   }
                               }

                               break;
                           default:
                                //error out if no option
                               Console.WriteLine($"Error: \"{houseOrApart}\" is not in option...");
                               break;
                       }   
                       
                       Console.WriteLine("Press any key to continues.");
                       Console.ReadKey();
                       break;
                    //List addressesof all for-sale properties
                   case 3:
                       //output
                       Console.WriteLine($"List addresses all FOR SALE property in {community.Name} community.");
                       Console.WriteLine("---------------------------------------------------------------------------\n");
                       

                        //iterate throught eh community properites
                       foreach (var property in community.Props)
                       {
                           // if the property is for sale
                           if (property.ForSale)
                           {
                               //main output
                               Console.WriteLine((property as Apartment) != null
                                   ? $"{property.StreetAddr} Apt.# {((Apartment) property).Unit} {property.City} {property.State}"
                                   : $"{property.StreetAddr} {property.City} {property.State}");
                           }
                       }
                       
                       Console.WriteLine("Press any key to continues.");
                       Console.ReadKey();
                       break;
                   //List all residents of a community
                   case 4:
                       //output for UI
                       Console.WriteLine($"List of all RESIDENTS in {community.Name} community.");
                       Console.WriteLine("---------------------------------------------------------------------------\n");
                       
                        //iterate throught each item and display the information
                       foreach (var resident in community.Residents)
                       {
                           Console.WriteLine($"{resident.FullName} Age({DateTime.Now.Year - resident.Birthday.Year}) Occupation: {resident.Occupation}\n");
                       }
                       
                       Console.WriteLine("Press any key to continues.");
                       Console.ReadKey();
                       break;
                   //List all residents of a property, by street address
                   case 5:
                        //output for UI
                       Console.WriteLine("Enter the street address: ");
                       string streetAddr = Console.ReadLine();
                       Console.WriteLine($"List of all residents live in {streetAddr}");
                       Console.WriteLine("---------------------------------------------------------------------------\n");

                        //iterate through each community properties
                       foreach (var property in community.Props)
                       {
                           // if the street name exists;
                           if (property.StreetAddr != streetAddr) continue;
                           foreach (var resident in community.Residents)
                           {
                               // if the person id matches the peoperty id
                               if (resident.Id == property.OwnerId)
                               {
                                   Console.WriteLine($"{resident.FullName} Age({DateTime.Now.Year - resident.Birthday.Year}) Occupation: {resident.Occupation}");
                               }
                           }
                       }
                       
                       Console.WriteLine("Press any key to continues.");
                       Console.ReadKey();
                       break;
                   //Toggle a property, by street address, as being for-sale or not
                   case 6:
                        //output for UI
                       Console.WriteLine("Enter the street address: ");
                       string notForSale = Console.ReadLine();
                       
                       //iterate throught the comunity properties
                       foreach (var property in community.Props)
                       {
                           // if the property is for sale, then skip
                           if (property.StreetAddr != notForSale) continue;
                           //set to false if not for sale
                           property.ForSale = false;
                           Console.WriteLine($"Now {notForSale} is NOT for sale.");
                       }
                       
                       Console.WriteLine("Press any key to continues.");
                       Console.ReadKey();
                       break;
                   //Buy a for-sale propety, by street address
                   case 7:
                        //output for UI
                       Console.WriteLine("Enter the street address: ");
                       string purchase = Console.ReadLine();
                       
                       foreach (var property in community.Props)
                       {
                           // if the street address not exist, skip
                           if (property.StreetAddr != purchase) continue;
                           if (property.ForSale != true)
                           {
                               Console.WriteLine($"{purchase} This property is NOT FOR SALE");
                               break;
                           }
                           Console.WriteLine("in continue");
                           //set false if not for sale
                           property.ForSale = false;
                            //general output
                           Console.WriteLine($"Congrats! You have successfully purchased {purchase} this property");
                           Console.WriteLine($"Property Address: {property.StreetAddr} / {property.City} / {property.State} / {property.Zip}");
                           string sale = property.ForSale ? "ForSale" : "Not for sale";
                           Console.Write($"\t({sale}) {((Residential) property).Bedrooms} \\ {((Residential) property).Baths} \\ " +
                                         $"{((Residential) property).Sqft} sq.ft.");
                           
                           // if the property is not a House
                           if((property as House) != null)
                           {
                               // return whether the house has a garage or not
                               string garage = ((House) property).Garage ? "With Garage" 
                                   : ((bool) ((House) property).AttatchedGarage ? "With attach garage" : "With no garage");
                               Console.WriteLine($"\n\t...{garage} : {((House) property).Flood} floor\n");
                           }
                           else
                           {
                               Console.WriteLine($" Apt.# {((Apartment)property).Unit}\n");
                           } 
                       }

                       Console.WriteLine("Press any key to continues.");
                       Console.ReadKey();
                       break;
                     //Add yourself as an occupant for a property.
                     case 8:
                        Console.WriteLine("Enter the street address to lookup:");
                        string lookup = Console.ReadLine();
                        foreach (var property in community.Props)
                        {
                            //start going through each property listed
                            if (property.StreetAddr == lookup)
                            {

                                bool isFound = false;
                                foreach (var r in community.Residents)
                                {
                                    if (r.Id == 0) //if the resident is the mayor id 0
                                    {
                                        foreach (var e in r.Residencelds)
                                        {
                                            if (e == property.Id)
                                            {
                                                //output
                                                Console.WriteLine("You are already a resident at this property.");
                                                isFound = true;
                                                break;
                                            }
                                        }

                                        //if not found then its good
                                        if (!isFound)
                                        {
                                            r.Add(property.Id);
                                            Console.WriteLine("Success! You have been added as a resident at this property.");
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        Console.WriteLine("Press any key to continues.");
                        Console.ReadKey();
                        break;
                    //Remove yourself as an occpuant from a propety.
                    case 9:
                        //output for UI
                        Console.WriteLine("Enter the street address to lookup:");
                        string lookup2 = Console.ReadLine();
                        bool notAddress = true;
                        foreach (var property in community.Props)
                        {
                            //start going through each property listed
                            if (property.StreetAddr == lookup2)
                            {

                                bool isFound = false;
                                foreach (var r in community.Residents)
                                {
                                    if (r.Id == 0) //if the resident is the mayor id 0
                                    {
                                        foreach (var e in r.Residencelds)
                                        {
                                            //success option
                                            if (e == property.Id)
                                            {
                                                Console.WriteLine("Success! You have been removed as a resident from this property.");
                                                r.Remove(property.Id);
                                                isFound = true;
                                                notAddress = false;
                                                break;
                                            }
                                        }

                                        //if not found the output error
                                        if (!isFound)
                                        {
                                            Console.WriteLine("You do not currently reside at this property.");
                                            notAddress = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        //if address doesnt exist
                        if (notAddress == true)
                        {
                            Console.WriteLine("I'm sorry, I don't recognize this address: " + lookup2);
                        }
                        Console.WriteLine("Press any key to continues.");
                        Console.ReadKey();
                        break;
                    //quit option
                    case 10:
                        Console.WriteLine("Quitting program...");
                        break;
                   // Standard error out
                   default:
                       Console.WriteLine("Please select available option!");
                       Console.WriteLine("Press any key to continues.");
                       Console.ReadKey();
                       break;
               }
            }
        }
    }
    
    /*
    * Class: Person
    * 
    * Function: Provide a object instance for pulling in the person data from the text file
    * 
    */
    public class Person : IComparable
    {
        //variables
        private readonly uint _id;
        private readonly DateTime _birthday;

        private string lastName;
        private string firstName;
        private string occupation;
        private List<uint> residencelds = new List<uint>();
        private string fullName;

        //person object for base
        public Person()
        {
            _id = 0;
            LastName = "";
            FirstName = "";
            fullName = "";
            Occupation = "";
            _birthday = DateTime.Now;
            residencelds.Add(0);
        }

        //for creating a person object
        public Person(uint id, DateTime bd, string l, string f, string o, string resId)
        {
            _id = id;
            _birthday = bd;
            LastName = l;
            FirstName = f;
            Occupation = o;
            fullName = FirstName + ", " + LastName;
            
            // if fail to convert the residence id
            if (Int32.TryParse(resId, out var result))
            {
                residencelds.Add((uint)result);
            }
            else
            {
                throw new ArgumentException("error: fail to convert residence id, please check again!");
            }
        }

        //all the following are get/set methods
        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }

        public string Occupation
        {
            get => occupation;
            set => occupation = value;
        }

        public uint[] Residencelds => residencelds.ToArray();
        
        //add residence id to array
        public void Add(uint id)
        {
            residencelds.Add(id);
        }

        //remove residence id to array
        public void Remove(uint id)
        {
            residencelds.Remove(id);
        }

        public uint Id => _id;

        public DateTime Birthday => _birthday;

        public string FullName => fullName;


        //compare method for person class
        public int CompareTo(object alpha)
        {
            if (alpha == null)
            {
                return 1;
            }

            Person otherO = alpha as Person;
            if (this.Id == otherO.Id)
                return 0;

            return this.fullName.CompareTo(otherO.fullName);
        }

        //toString method to output the different variables
        public override string ToString()
        {
            return $"ID: {_id} Name: {FullName} Date of birth: {_birthday} Occupation: {Occupation}";
        }
    }

    /*
    * Class: Propety
    * 
    * Function: Allows a user to create a object for the property class based off its variables
    * 
    */

    public class Property : IComparable
    {
        //variables for class
        private readonly uint _id;
        private readonly uint _x;
        private readonly uint _y;

        private uint ownerID;
        private string streetAddr;
        private string city;
        private string state;
        private string zip;
        private bool forSale;

        //Property Object
        protected Property()
        {
            Console.WriteLine("Property() are using...");
            _id = 0;
            _y = 0;
            _x = 0;
            ownerID = 0;
            streetAddr = "";
            city = "";
            state = "";
            zip = "";
            forSale = false;
        }

        //creating the Property Object
        public Property(uint id, uint x, uint y, uint o, string sa, string c, string st, string z, bool fs)
        {
            _id = id;
            _x = x;
            _y = y;
            ownerID = o;
            streetAddr = sa;
            city = c;
            state = st;
            zip = z;
            forSale = fs;
        }


        //All of the GET/SET methods
        public uint OwnerId
        {
            get => ownerID;
            set => ownerID = value;
        }

        public string StreetAddr
        {
            get => streetAddr;
            set => streetAddr = value;
        }

        public string State
        {
            get => state;
            set => state = value;
        }

        public string City
        {
            get => city;
            set => city = value;
        }

        public string Zip
        {
            get => zip;
            set => zip = value;
        }

        public bool ForSale
        {
            get => forSale;
            set => forSale = value;
        }

        public uint Id => _id;

        public uint X => _x;

        public uint Y => _y;

        //compare for the property
        public int CompareTo(object alpha)
        {
            if (alpha == null)
            {
                return 1;
            }

            Property otherP = alpha as Property;

            if (this.OwnerId == otherP.Id)
            {
                return 0;
            }
            
            var stateResult = this.state.CompareTo(otherP.State);
            if (stateResult != 0)
            {
                return stateResult;
            }
            
            var cityResult = this.city.CompareTo(otherP.city);
            if (cityResult != 0)
            {
                    return cityResult;
            }
            
            var streetResult = this.StreetAddr.CompareTo(otherP.StreetAddr);
            if (streetResult != 0)
            {
                return streetResult;
            }

            return 1;
        }
    }

    /*
    * Class: Residential
    * 
    * Function: Residential property class which allws us to specify what type of property it is.
    * 
    */
    public class Residential : Property
    {
        //variables
        private uint bedrooms;
        private uint baths;
        private uint sqft;
        
        //creating the residential object
        protected Residential(uint id, uint x, uint y, uint o,
            string sa, string c, string st, string z, bool fs, uint bedroom, uint bath, uint sq)
            : base(id, x, y, o, sa, c, st, z, fs)
        {
            Bedrooms = bedroom;
            Baths = bath;
            Sqft = sq;
        }


        //all the GET/SET methods for The class.
        public uint Bedrooms
        {
            get => bedrooms;
            set => bedrooms = value;
        }

        public uint Baths
        {
            get => baths;
            set => baths = value;
        }

        public uint Sqft
        {
            get => sqft;
            set => sqft = value;
        }
    }


    //House class which is based off the Residential class but with a few added variables
    class House : Residential
    {
        //variables added over the residential
        private bool garage;
        private bool? attatchedGarage;
        private uint flood;

        //creating the residential house object
        public House(uint id, uint x, uint y, uint o, string sa, string c,
            string st, string z, bool fs, uint bedroom, uint bath, uint sq,
            bool gar, bool aGar, uint fl)
            : base(id, x, y, o, sa, c, st, z, fs, bedroom, bath, sq)
        {
            AttatchedGarage = null;
            Garage = gar;
            AttatchedGarage = aGar;
            Flood = fl;
        }

        //These are the GET/Set methods for the Hosue class
        public bool Garage
        {
            get => garage;
            set => garage = value;
        }

        public uint Flood
        {
            get => flood;
            set => flood = value;
        }

        public bool? AttatchedGarage
        {
            get => attatchedGarage;
            set => attatchedGarage = value;
        }
    }

    //Apartment class based off the Residential class
    public class Apartment : Residential
    {
        //variables
        private string unit;

        //Apartment creating object
        public Apartment(uint id, uint x, uint y, uint o, string sa, string c,
            string st, string z, bool fs, uint bedroom, uint bath, uint sq, string u)
            : base(id, x, y, o, sa, c, st, z, fs, bedroom, bath, sq)
        {
            Unit = u;
        }


        //GET/SET for Apartment
        public string Unit
        {
            get => unit;
            set => unit = value;
        }
    }

    /*
    * Class: Community
    * 
    * Function: This allows a user to create Community objects based off the popultion or Mayor
    * 
    */

    public class Community : IComparable, IEnumerable
    {
        private readonly uint _id;
        private readonly string _name;

        private SortedSet<Property> props;
        private SortedSet<Person> residents;
        private uint mayorID;
        private uint population;

        //Communaity Object
        public Community()
        {
            _id = 0;
            _name = "";
            Props = null;
            Residents = null;
            MayorID = 0;
        }

        //creating the Community Object
        public Community(uint id, string name, uint mId)
        {
            _id = id;
            _name = name;
            props = new SortedSet<Property>();
            residents = new SortedSet<Person>();
            MayorID = mId;
        }
        

        //GET/SET methods for community object
        public SortedSet<Property> Props
        {
            get => props;
            set => props = value;
        }

        public SortedSet<Person> Residents
        {
            get => residents;
            set => residents = value;
        }

        //Compareto method
        public int CompareTo(object alpha)
        {
            if (alpha == null)
            {
                return 1;
            }

            Community otherC = alpha as Community;
            return this._name.CompareTo(otherC._name);
        }

        public uint Id => _id;

        public string Name => _name;

        //GET/SEt for mayor
        public uint MayorID
        {
            get => mayorID;
            set => mayorID = value;
        }

        public uint Population => (uint) residents.Count;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new CommEnum(this);
        }
    }

    /*
    * Class: CommEnum
    * 
    * Function: Allows you to compare certain objects.
    * 
    */

    public class CommEnum : IEnumerator
    {
        private Community cList;
        private int pos = -1;

        internal CommEnum(Community cList)
        {
            this.cList = cList;
        }

        //Move to the next object
        public bool MoveNext()
        {
            if (pos != cList.Residents.Count)
            {
                pos++;
            }
            return pos < cList.Residents.Count;
        }

        //get the current spot in count
        public object Current
        {
            get
            {
                if (pos == -1 || pos == cList.Residents.Count)
                {
                    throw new InvalidOperationException();
                }

                return pos;
            }
        }

        //reset method to reset the pos
        public void Reset()
        {
            pos = -1;
        }
    }
}
