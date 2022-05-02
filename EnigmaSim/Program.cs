using System;
using System.Collections.Generic;
using System.Linq;

namespace EnigmaSim {

    class Program {
        static void Main(string[] args) {
            char[] rotorDescr10 = new char[26] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] rotorDescr1 = new char[26] { 'E', 'K', 'M', 'F', 'L', 'G', 'D', 'Q', 'V', 'Z', 'N', 'T', 'O', 'W', 'Y', 'H', 'X', 'U', 'S', 'P', 'A', 'I', 'B', 'R', 'C', 'J' };
            char[] rotorDescr2 = new char[26] { 'A', 'J', 'D', 'K', 'S', 'I', 'R', 'U', 'X', 'B', 'L', 'H', 'W', 'T', 'M', 'C', 'Q', 'G', 'Z', 'N', 'P', 'Y', 'F', 'V', 'O', 'E' };
            char[] rotorDescr3 = new char[26] { 'B', 'D', 'F', 'H', 'J', 'L', 'C', 'P', 'R', 'T', 'X', 'V', 'Z', 'N', 'Y', 'E', 'I', 'W', 'G', 'A', 'K', 'M', 'U', 'S', 'Q', 'O' };
            char[] rotorDescr4 = new char[26] { 'E', 'S', 'O', 'V', 'P', 'Z', 'J', 'A', 'Y', 'Q', 'U', 'I', 'R', 'H', 'X', 'L', 'N', 'F', 'T', 'G', 'K', 'D', 'C', 'M', 'W', 'B' };
            char[] rotorDescr5 = new char[26] { 'V', 'Z', 'B', 'R', 'G', 'I', 'T', 'Y', 'U', 'P', 'S', 'D', 'N', 'H', 'L', 'X', 'A', 'W', 'M', 'J', 'Q', 'O', 'F', 'E', 'C', 'K' };

            var rotorDescriptions = new List<KeyValuePair<int, char[]>>() {
                new KeyValuePair<int, char[]>(1, rotorDescr1),
                new KeyValuePair<int, char[]>(2, rotorDescr2),
                new KeyValuePair<int, char[]>(3, rotorDescr3),
                new KeyValuePair<int, char[]>(4, rotorDescr4),
                new KeyValuePair<int, char[]>(5, rotorDescr5)
            };

            var rotorNotches = new List<KeyValuePair<int, char>>() {
                new KeyValuePair<int, char>(1, 'Q'),
                new KeyValuePair<int, char>(2, 'E'),
                new KeyValuePair<int, char>(3, 'V'),
                new KeyValuePair<int, char>(4, 'J'),
                new KeyValuePair<int, char>(5, 'Z')
            };

            char[] reflectorA = new char[26] { 'E', 'J', 'M', 'Z', 'A', 'L', 'Y', 'X', 'V', 'B', 'W', 'F', 'C', 'R', 'Q', 'U', 'O', 'N', 'T', 'S', 'P', 'I', 'K', 'H', 'G', 'D' };
            char[] reflectorB = new char[26] { 'Y', 'R', 'U', 'H', 'Q', 'S', 'L', 'D', 'P', 'X', 'N', 'G', 'O', 'K', 'M', 'I', 'E', 'B', 'F', 'Z', 'C', 'W', 'V', 'J', 'A', 'T' };

            string[] pluckboardDescr1 = { "AB", "HE", "JS", "NT", "RV", "CO", "QL" };


            // Create enigma machine object
            EnigmaMachine enigma1 = new EnigmaMachine();

            //try {
            //    enigma1.FeedInRotor("I", rotorDescriptions[0].Value, 'A', 'A' - 65);
            //    enigma1.FeedInRotor("I", rotorDescriptions[1].Value, 'A', 'A' - 65);
            //    enigma1.FeedInRotor("I", rotorDescriptions[2].Value, 'A', 'A' - 65);
            //} catch (Exception ex) {
            //    Console.WriteLine(ex.ToString());
            //}


            int index = 3;
            while (index > 0) {
                Console.Write("Feed in the rotor (1-5): ");
                int rotorNumber = int.Parse(Console.ReadLine());

                var result = rotorDescriptions.Find(x => x.Key == rotorNumber);

                // If rotor not in the list
                if (result.Equals(default(KeyValuePair<int, char>))) {
                    Console.WriteLine("Rotor already taken or doesn't exist");
                } else {
                    char notchPos = rotorNotches.Find(x => x.Key == rotorNumber).Value;

                    Console.Write("Enter the ring position: ");
                    char ringPos = char.ToUpper(Console.ReadLine()[0]);
                    // feed in the rotor to the machine
                    try {
                        enigma1.FeedInRotor("I", result.Value, ringPos, notchPos);
                    } catch (Exception ex) {
                        Console.WriteLine(ex.ToString());
                    }
                    // Remove from the list
                    rotorDescriptions.Remove(result);
                    index--;
                }
            }

            try {
                enigma1.FeedInReflector(reflectorB);
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            // AB NF JR
            Console.Write("Set up the pluckboard: ");
            string pluckboardDescr = Console.ReadLine();
            if (pluckboardDescr == "") {
                enigma1.PluckboardExists = false;
            } else {
                enigma1.PluckboardExists = true;
                Console.WriteLine(enigma1.PluckboardExists);
                PluckBoard pluckBoard = new PluckBoard(pluckboardDescr);
                enigma1.SetPluckBoard(pluckBoard);
            }

            string plaintext;
            string encrypted;

            Console.Write("Type the text that you want to encode/decode (spaces will be substituted with X): ");
            plaintext = Console.ReadLine();
            plaintext = plaintext.ToUpper();
            plaintext = plaintext.Replace(' ', 'X');


            while (true) {
                encrypted = enigma1.Encrypt(plaintext);
                Console.WriteLine("Encrypted: " + encrypted);

                Console.Write("Do you want to continue to decode/encode? (y/n)");
                string answer = Console.ReadLine();
                if (answer == "n" || answer == "N") break;

                //Reset the rotors
                enigma1.Reset();

                Console.Write("Type the text that you want to encode/decode: ");
                plaintext = Console.ReadLine();
                plaintext = plaintext.ToUpper();
                plaintext = plaintext.Replace(' ', 'X');
            }
        }
    }
}
