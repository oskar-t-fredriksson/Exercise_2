using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Inlämningsuppgift_2
{
    /*Class: Person
     *Purpose: Is used in all methods to access contact list 
     */
    class Person
    {
        public string name, address, phone, email;
        /*Constructor: Person
         *Purpose: Creates object Person
         *Parameters: N = set name, A = set address, T = set phone, E = set email
         */
        public Person(string N, string A, string T, string E)
        {
            name = N; address = A; phone = T; email = E;
        }
        /*Constructor: Person
         *Purpose: To change existing contacts
         */
        public Person()
        {
            Console.WriteLine("Lägger till ny person");
            Console.Write("  1. ange namn:    ");
            name = Console.ReadLine();
            Console.Write("  2. ange adress:  ");
            address = Console.ReadLine();
            Console.Write("  3. ange telefon: ");
            phone = Console.ReadLine();
            Console.Write("  4. ange email:   ");
            email = Console.ReadLine();
        }
        public void Print()
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", name, address, phone, email);
        }
        /*Method: ChangeValue
         *Purpose: To change existing contacts
         *Parameters: 'Value' contains current value and 'newValue' the new value input
         *Return value: Sends back input to the object 'Person'
         */
        public void ChangeValue(string value, string newValue)
        {
            switch (value)
            {
                case "namn":
                    name = newValue;
                    break;
                case "adress":
                    address = newValue;
                    break;
                case "telefon":
                    phone = newValue;
                    break;
                case "email":
                    email = newValue;
                    break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> Dict = LoadFile();
            Console.WriteLine("Hej och välkommen till adresslistan");
            Console.WriteLine("Skriv 'sluta' för att sluta!");
            string command;
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                if (command == "sluta")
                {
                    Console.WriteLine("Hej då!");
                }
                else if (command == "ny")
                {
                    Dict.Add(new Person());
                }
                else if (command == "ta bort")
                {
                    DeleteMethod(Dict);
                }
                else if (command == "visa")
                {
                    for (int i = 0; i < Dict.Count(); i++)
                    {
                        Dict[i].Print();
                    }
                }
                else if (command == "ändra")
                {
                    ChangeMethod(Dict);
                }
                else
                {
                    Console.WriteLine("Okänt kommando: {0}", command);
                }
            } while (command != "sluta");
        }
        /*Method: ChangeMethod (static)
         *Purpose: To change existing contacts
         *Parameters: Dict is containging all the contacts
         *Return value: Sends back input to the object Person
         */
        private static void ChangeMethod(List<Person> Dict)
        {
            Console.Write("Vem vill du ändra (ange namn): ");
            string wantToChange = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].name == wantToChange) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", wantToChange);
            }
            else
            {
                Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
                string fieldToChange = Console.ReadLine();
                Console.Write("Vad vill du ändra {0} på {1} till: ", fieldToChange, wantToChange);
                string newValue = Console.ReadLine();
                Dict[found].ChangeValue(fieldToChange, newValue);
            }
        }
        /*Method: ShowMethod (static)
         *Purpose: To show existing contacts
         *Parameters: Dict is containging all the contacts
         *Return value: Returns all the contacts listed under the indexes
         */
        private static void ShowMethod(List<Person> Dict)
        {
            for (int i = 0; i < Dict.Count(); i++)
            {
                Person P = Dict[i];
                Console.WriteLine("{0}, {1}, {2}, {3}", P.name, P.address, P.phone, P.email);
            }
        }
        /*Method: DeleteMethod (static)
         *Purpose: To delete a specific existing contact
         *Parameters: Dict is containging all the contacts
         *Return value: Returns which index is supposed to be deleted
         */
        private static void DeleteMethod(List<Person> Dict)
        {
            Console.Write("Vem vill du ta bort (ange namn): ");
            string toRemove = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].name == toRemove) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", toRemove);
            }
            else
            {
                Dict.RemoveAt(found);
            }
        }
        /*Method: LoadFile (static)
         *Purpose: Reads in all contacts from file
         *Return value: Returns all contacts listed in file
         */
        static List<Person> LoadFile()
        {
            List<Person> Dict = new List<Person>();
            Console.Write("Laddar adresslistan ... ");
            using (StreamReader fileStream = new StreamReader(@"..\..\address.lis"))
            {
                while (fileStream.Peek() >= 0)
                {
                    string line = fileStream.ReadLine();
                    string[] word = line.Split('#');
                    Person P = new Person(word[0], word[1], word[2], word[3]);
                    Dict.Add(P);
                }
            }
            Console.WriteLine("klart!");
            return Dict;
        }
    }
}
