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

            int option = 0;
            while (option != 10)
            {
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
                   case 1:
                       Console.WriteLine($"<{community.Id}> {community.Name}. Population ({community.Population}). Mayor: {community.MayorID}");
                       
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
                   case 2:
                       Console.WriteLine("Enter property type (House/Apartment):");
                       string houseOrApart = Console.ReadLine();
                       Console.WriteLine($"List addresses of {houseOrApart} property in {community.Name} community.");
                       Console.WriteLine("---------------------------------------------------------------------------\n");

                       switch (houseOrApart)
                       {
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
                               Console.WriteLine($"Error: \"{houseOrApart}\" is not in option...");
                               break;
                       }   
                       
                       Console.WriteLine("Press any key to continues.");
                       Console.ReadKey();
                       break;
                   case 3:
                       Console.WriteLine($"List addresses all FOR SALE property in {community.Name} community.");
                       Console.WriteLine("---------------------------------------------------------------------------\n");
                       
                       foreach (var property in community.Props)
                       {
                           // if the property is for sale
                           if (property.ForSale)
                           {
                               Console.WriteLine((property as Apartment) != null
                                   ? $"{property.StreetAddr} Apt.# {((Apartment) property).Unit} {property.City} {property.State}"
                                   : $"{property.StreetAddr} {property.City} {property.State}");
                           }
                       }
                       
                       Console.WriteLine("Press any key to continues.");
                       Console.ReadKey();
                       break;
                   case 4:
                       Console.WriteLine($"List of all RESIDENTS in {community.Name} community.");
                       Console.WriteLine("---------------------------------------------------------------------------\n");

                       foreach (var resident in community.Residents)
                       {
                           Console.WriteLine($"{resident.FullName} Age({DateTime.Now.Year - resident.Birthday.Year}) Occupation: {resident.Occupation}\n");
                       }
                       
                       Console.WriteLine("Press any key to continues.");
                       Console.ReadKey();
                       break;
                   case 5:
                       Console.WriteLine("Enter the street address: ");
                       string streetAddr = Console.ReadLine();
                       Console.WriteLine($"List of all residents live in {streetAddr}");
                       Console.WriteLine("---------------------------------------------------------------------------\n");

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
                   case 6:
                       Console.WriteLine("Enter the street address: ");
                       string notForSale = Console.ReadLine();
                       
                       foreach (var property in community.Props)
                       {
                           // if the property is for sale, then skip
                           if (property.StreetAddr != notForSale) continue;
                           property.ForSale = false;
                           Console.WriteLine($"Now {notForSale} is NOT for sale.");
                       }
                       
                       Console.WriteLine("Press any key to continues.");
                       Console.ReadKey();
                       break;
                   case 7:
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
                           property.ForSale = false;
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
                                                Console.WriteLine("You are already a resident at this property.");
                                                isFound = true;
                                                break;
                                            }
                                        }

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
                    case 9:
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
                                            if (e == property.Id)
                                            {
                                                Console.WriteLine("Success! You have been removed as a resident from this property.");
                                                r.Remove(property.Id);
                                                isFound = true;
                                                notAddress = false;
                                                break;
                                            }
                                        }

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

                        if (notAddress == true)
                        {
                            Console.WriteLine("I'm sorry, I don't recognize this address: " + lookup2);
                        }
                        Console.WriteLine("Press any key to continues.");
                        Console.ReadKey();
                        break;
                    case 10:
                        Console.WriteLine("Quitting program...");
                        break;
                   default:
                       Console.WriteLine("Please select available option!");
                       Console.WriteLine("Press any key to continues.");
                       Console.ReadKey();
                       break;
               }
            }
        }
    }
    
    public class Person : IComparable
    {
        private readonly uint _id;
        private readonly DateTime _birthday;

        private string lastName;
        private string firstName;
        private string occupation;
        private List<uint> residencelds = new List<uint>();
        private string fullName;

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
        
        public void Add(uint id)
        {
            residencelds.Add(id);
        }

        public void Remove(uint id)
        {
            residencelds.Remove(id);
        }

        public void Add(uint id)
        {
            residencelds.Add(id);
        }

        public void Remove(uint id)
        {
            residencelds.Remove(id);
        }

        public uint Id => _id;

        public DateTime Birthday => _birthday;

        public string FullName => fullName;

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

        public override string ToString()
        {
            return $"ID: {_id} Name: {FullName} Date of birth: {_birthday} Occupation: {Occupation}";
        }
    }

    public class Property : IComparable
    {
        private readonly uint _id;
        private readonly uint _x;
        private readonly uint _y;

        private uint ownerID;
        private string streetAddr;
        private string city;
        private string state;
        private string zip;
        private bool forSale;

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

    public class Residential : Property
    {
        private uint bedrooms;
        private uint baths;
        private uint sqft;
        
        protected Residential(uint id, uint x, uint y, uint o,
            string sa, string c, string st, string z, bool fs, uint bedroom, uint bath, uint sq)
            : base(id, x, y, o, sa, c, st, z, fs)
        {
            Bedrooms = bedroom;
            Baths = bath;
            Sqft = sq;
        }

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

    class House : Residential
    {
        private bool garage;
        private bool? attatchedGarage;
        private uint flood;

    
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

    public class Apartment : Residential
    {
        private string unit;

        public Apartment(uint id, uint x, uint y, uint o, string sa, string c,
            string st, string z, bool fs, uint bedroom, uint bath, uint sq, string u)
            : base(id, x, y, o, sa, c, st, z, fs, bedroom, bath, sq)
        {
            Unit = u;
        }

        public string Unit
        {
            get => unit;
            set => unit = value;
        }
    }

    public class Community : IComparable, IEnumerable
    {
        private readonly uint _id;
        private readonly string _name;

        private SortedSet<Property> props;
        private SortedSet<Person> residents;
        private uint mayorID;
        private uint population;

        public Community()
        {
            _id = 0;
            _name = "";
            Props = null;
            Residents = null;
            MayorID = 0;
        }

        public Community(uint id, string name, uint mId)
        {
            _id = id;
            _name = name;
            props = new SortedSet<Property>();
            residents = new SortedSet<Person>();
            MayorID = mId;
        }
        
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

    public class CommEnum : IEnumerator
    {
        private Community cList;
        private int pos = -1;

        internal CommEnum(Community cList)
        {
            this.cList = cList;
        }

        public bool MoveNext()
        {
            if (pos != cList.Residents.Count)
            {
                pos++;
            }
            return pos < cList.Residents.Count;
        }

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

        public void Reset()
        {
            pos = -1;
        }
    }
}
