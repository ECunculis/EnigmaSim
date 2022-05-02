using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaSim {
    class Rotor {
        public string name;
        public char[] wiring = new char[26]; // Input to output wiring 
        public int notchPos; // Notch position
        public char ringPosition;   // Rotator's current position
        public char currentPos = 'A';
        public Rotor(string name, char[] wiring, char ringPosition, int notchPos) {  //Parameterized constructor
            this.name = name;
            this.wiring = wiring;
            // Ring setting means that if, for example, it is set to B-02 and the 
            // initial position is A, it is the same as if initial position was Z with ring setting A-01
            this.ringPosition = ringPosition;
            this.notchPos = notchPos - 65;

            // If ring is set up to A, nothing changes
            // but if we change the ring to B, it is the same as rotating the 
            // rotor backwards. Notch are fixed to the ring. 
            char[] tempWiring = new char[26];

            // Shift all letters to the right and advance each by 1 letter further  
            for (int i = 0; i < 26; i++) {
                // Shift
                int sourceIndex = i - (ringPosition - 65);
                sourceIndex = Mod(sourceIndex, 26);
                // Advance
                tempWiring[i] = (char)(wiring[sourceIndex] + (ringPosition - 65));
                tempWiring[i] = (char)(Mod(tempWiring[i] - 65, 26) + 65);

            }
            this.wiring = tempWiring;
        }

        public Rotor(Rotor obj) {
            this.name = obj.name;
            this.wiring = obj.wiring;
            this.ringPosition = obj.ringPosition; 
            this.currentPos = obj.currentPos;
            this.notchPos = obj.notchPos;
            this.wiring = obj.wiring;
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

            //Console.WriteLine("Rotating the rotor " + name);
            //Console.WriteLine("Now it is in position " + currentPos);

            notchPos--;
            if (notchPos < 0) notchPos = 25;

            char[] tempWiring = new char[26];

            char temp = 'A';
            foreach (char letter in wiring) {
                temp = (char)(temp + 1);
            }

            // Shift all letters to the right and advance each by 1 letter further  
            for (int i = 0; i < 26; i++) {
                // Shift
                int sourceIndex = i + 1;
                sourceIndex = Mod(sourceIndex, 26);
                // Advance
                tempWiring[i] = (char)(wiring[sourceIndex] - 1);
                tempWiring[i] = (char)(Mod(tempWiring[i] - 65, 26) + 65);
            }
            wiring = tempWiring;
        }

        public string ToString() {
            return "This is rotor " + name;
        }

    }
}
