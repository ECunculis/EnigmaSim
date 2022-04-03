using System;
using System.Collections.Generic;

namespace EnigmaSim {

    class Program {
        static void Main(string[] args) {
            char[] rotorDescr10 = new char[26]{ 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] rotorDescr1 = new char[26] { 'E', 'K', 'M', 'F', 'L', 'G', 'D', 'Q', 'V', 'Z', 'N', 'T', 'O', 'W', 'Y', 'H', 'X', 'U', 'S', 'P', 'A', 'I', 'B', 'R', 'C', 'J' };
            char[] rotorDescr2 = new char[26] { 'A', 'J', 'D', 'K', 'S', 'I', 'R', 'U', 'X', 'B', 'L', 'H', 'W', 'T', 'M', 'C', 'Q', 'G', 'Z', 'N', 'P', 'Y', 'F', 'V', 'O', 'E' };
            char[] rotorDescr3 = new char[26] { 'B', 'D', 'F', 'H', 'J', 'L', 'C', 'P', 'R', 'T', 'X', 'V', 'Z', 'N', 'Y', 'E', 'I', 'W', 'G', 'A', 'K', 'M', 'U', 'S', 'Q', 'O' };

            char[] reflectorA = new char[26] { 'E', 'J', 'M', 'Z', 'A', 'L', 'Y', 'X', 'V', 'B', 'W', 'F', 'C', 'R', 'Q', 'U', 'O', 'N', 'T', 'S', 'P', 'I', 'K', 'H', 'G', 'D' };
            char[] reflectorB = new char[26] { 'Y', 'R', 'U', 'H', 'Q', 'S', 'L', 'D', 'P', 'X', 'N', 'G', 'O', 'K', 'M', 'I', 'E', 'B', 'F', 'Z', 'C', 'W', 'V', 'J', 'A', 'T' };

            // Create enigma machine object
            EnigmaMachine enigma1 = new EnigmaMachine();

            try {
                enigma1.FeedInRotor("I", rotorDescr1, 1, 'Q' - 65);
                enigma1.FeedInRotor("II", rotorDescr2, 1, 'E' - 65);
                enigma1.FeedInRotor("III", rotorDescr3, 1, 'V' - 65);
                enigma1.FeedInReflector(reflectorB);
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            string plaintext = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAaAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            string encrypted = enigma1.Encrypt(plaintext);

            Console.WriteLine("Encrypted: " + encrypted);

            //foreach (Rotor rotor in enigma1.rotors) {
            //    Console.WriteLine(rotor.ToString());
            //}

        }
    }

    class EnigmaMachine {
        // Create a list of rotors.
        public List<Rotor> rotors = new List<Rotor>();
        public Reflector reflector;

        public EnigmaMachine() {

        }

        public string Encrypt(string plaintext) {
            string encrypted = "";
            int counter = 0;

            foreach (char letter in plaintext) {
                // Advance the right most rotor by 1
                counter++;
                Console.WriteLine("It's letter number:" + counter);
                // Create the array of rotors which must be rotated next turn:
                bool[] rotorRotate = new bool[rotors.Count];

                // Need to check the position of notches of each rotor,
                // to decide which rotors must be rotated. For instance, if there are 4 rotors
                // positioned in the following way: 1, 2, 3, 4, then if the 3rd rotor's notch
                // is positioned so that the 2nd rotor must be rotated next time,
                // 3rd rotor must be rotated as well.
                for (int i = 1; i < rotors.Count; i++) {
                    if (rotors[i].notchPos == 0) {
                        rotorRotate[i] = rotorRotate[i - 1] = true;
                    }
                }

                rotorRotate[rotors.Count - 1] = true; // should always be rotated

                for (int i = 0; i < rotors.Count; i++) {
                    if (rotorRotate[i]) rotors[i].Rotate();
                }


                char encryptedLetter = letter;
                //Console.WriteLine("It's letter " + letter);
                // Go throught all rotors from right to left
                for (int i = rotors.Count - 1; i >= 0; i--) {
                    //Console.Write("Rotor " + rotors[i].name + "  ");
                    encryptedLetter = rotors[i].Scramble(encryptedLetter, 1);
                    //Console.WriteLine("Now it's letter: " + encryptedLetter);
                }

                // Go throught reflector
                //Console.Write("Reflector ");
                encryptedLetter = reflector.Scramble(encryptedLetter);
                //Console.WriteLine("Now it's letter: " + encryptedLetter);

                // Go back thought rotors from left to right
                for (int i = 0; i < 3; i++) {
                    //Console.Write("Rotor " + rotors[i].name + "  ");
                    encryptedLetter = rotors[i].Scramble(encryptedLetter, 0);
                    //Console.WriteLine("Now it's letter: " + encryptedLetter);
                }

                //Console.WriteLine("Now it's letter: " + encryptedLetter);
                encrypted += encryptedLetter;
                // Advance the rotors one step forward
                // Advance first rotor by 1 step
                //rotors[rotors.Count - 1].position = (rotors[rotors.Count - 1].position + 1) % 26;*/

                Console.WriteLine("");
            }

            return encrypted;
        }

        public void FeedInRotor(string name, char[] wiring, int position, int notchPos) {
            int[] flag = new int[26];
            // Need to check whether parameters are correct 
            for (int i = 0; i < 26; i++) {
                int letterPos = char.ToUpper(wiring[i]) - 65;
                if (flag[letterPos] == 0) {
                    flag[letterPos] = 1;
                } else {
                    throw (new Exception("Error: Rotor description is not correct"));
                }
            }
            // Also check the position and notchPos values

            // Create rotor and add to the list
            rotors.Add(new Rotor(name, wiring, position, notchPos));
        }

        public void FeedInReflector(char[] wiring) {
            int[] flag = new int[26];
            // Need to check whether parameters are correct 
            for (int i = 0; i < 26; i++) {
                int letterPos = char.ToUpper(wiring[i]) - 65;
                if (flag[letterPos] == 0 && char.ToUpper(wiring[letterPos]) - 65 == i) {
                    flag[letterPos] = 1;
                } else {
                    throw (new Exception("Error: Reflector description is not correct"));
                }
            }

            reflector = new Reflector(wiring);
        }

        public int GetRotorNum() {
            return rotors.Count;
        }

    }

    class Rotor {
        public string name;
        public char[] wiring = new char[26]; // Input to output wiring 
        public int notchPos; // Notch position
        public int ringPosition;   // Rotator's current position
        public char currentPos = 'A';
        public Rotor(string name, char[] wiring, int ringPosition, int notchPos) {  //Parameterize"d constructor
            this.name = name;
            this.wiring = wiring;
            // Ring setting means that if, for example, it is set to B-02 and the 
            // initial position is A, it is the same as if initial position was Z with ring setting A-01
            this.ringPosition = ringPosition;
            this.notchPos = notchPos;
        }

        public static int Mod(int x, int m) {
            return (x % m + m) % m;
        }

        // 1 - from right to left, 0 - opposite
        public char Scramble(char letter, int direction) {
            if (direction == 1) {
                // Get the letter
                letter = wiring[char.ToUpper(letter) - 65]; 
            } else {
                // Find the corresponding letter position
                int letterPosition = Array.IndexOf(wiring, letter);
                letter = (char)(letterPosition + 65); // Convert to char
            }
            return letter;
        }

        // Don't forget about double stepping anomaly
        public void Rotate() {
            currentPos = currentPos - 64 > 25 ? 'A' : (char)(currentPos + 1);

            Console.WriteLine("Rotating the rotor " + name);
            Console.WriteLine("Now it is in position " + currentPos);

            notchPos--;
            if (notchPos < 0) notchPos = 25;

            char[] tempWiring = new char[26];
            //Array.Copy(wiring, tempWiring, wiring.Length);

            //Console.WriteLine("Array before rotation: ");
            char temp = 'A';
            foreach (char letter in wiring) {
                //Console.Write(temp + " ");
                temp = (char)(temp + 1);
            }
            //Console.WriteLine("");
            foreach (char letter in wiring) {
                //Console.Write(letter + " ");
            }
            //Console.WriteLine("");
            //Console.WriteLine("Array after rotation: ");

            // Shift all letters to the right and advance each by 1 letter further  
            for (int i = 0; i < 26; i++) {
                // Shift
                int sourceIndex = i + 1;
                sourceIndex = Mod(sourceIndex, 26);
                // Advance
                tempWiring[i] = (char)(wiring[sourceIndex] - 1);
                //if (tempWiring[i] > 90) tempWiring[i] = (char)65;

                tempWiring[i] = (char)(Mod(tempWiring[i] - 65, 26) + 65);

                //Console.Write(tempWiring[i] + " ");
            }
            //Console.WriteLine("");

            wiring = tempWiring;
        }

        public string ToString() {
            return "This is rotor " + name;
        }

    }
    class Reflector {
        char[] wiring = new char[26]; // Input to output wiring 

        public Reflector(char[] wiring) {  //Parameterized constructor
            this.wiring = wiring;
        }

        public char Scramble(char letter) {
            letter = wiring[char.ToUpper(letter) - 65];
            return letter;
        }

        public string toString() {
            return "This is reflector";
        }
    }
}
