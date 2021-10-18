using System;
using System.Collections.Generic;

namespace FerrisWheel
{
    class MainClass
    {
        // The chair that is down
        static int down = 0;

        // History with the persons who left the wheel
        static List<int> left = new List<int>();

        // Last chair that was emptied
        static int lastChair = -1;

        // The current person that should do something
        static int cPerson = -1;

        /// <summary>
        /// Inits the arr with the value sent in the second argument.
        /// </summary>
        /// <param name="arr">Arr.</param>
        /// <param name="v">V.</param>
        static void initArr(int[] arr, int v)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                arr[i] = v;
            }
        }

        /// <summary>
        /// Returns `true` if the wheel is empty. Otherwise `false`.
        /// </summary>
        /// <returns><c>true</c>, if is empty was wheeled, <c>false</c> otherwise.</returns>
        /// <param name="wheel">Wheel.</param>
        static bool wheelIsEmpty(int[] wheel)
        {
            for (int i = 0; i < wheel.Length; ++i)
            {
                if (wheel[i] != -1)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Rotate the wheel to the next chair.
        /// </summary>
        /// <param name="c">C.</param>
        /// <param name="wheel">Wheel.</param>
        /// <param name="pIds">P identifiers.</param>
        static void nextChair(int[] c, int[] wheel, int[] pIds)
        {
            down %= wheel.Length;

            switch (wheel[down])
            {
                // Not occuped
                case -1:
                    ++cPerson;
                    if (cPerson >= c.Length)
                        break;
                    wheel[down] = c[cPerson] - 1;
                    pIds[down] = cPerson;
                    break;
                    // Someone have to leave the game
                    case 0:
                    if (pIds[down] != -1)
                    {
                        left.Add(pIds[down]);
                        lastChair = down;
                    }
                    wheel[down] = -1;
                    break;
                    // One more time for this person!
                    default:
                    --wheel[down];
                    break;
            }

            ++down;
        }


        public static void Main(string[] args)
        {
            // Input data
            int n = ReadInteger("How many chairs? n = ");

            int p = ReadInteger("How many persons? p = ");

            // Tickets
            int[] c = new int[p];

            // The magic wheel
            int[] wheel = new int[n];

            // Person ids
            int[] pIds = new int[n];

            // Initialize wheel and pIds with -1 values
            initArr(wheel, -1);
            initArr(pIds, -1);

            int sum = 0;

            Console.WriteLine("Rotations:");
            for (var i = 0; i < c.Length; ++i)
            {
                c[i] = ReadInteger(string.Format("> c({0}) = ", i + 1));
                sum += c[i];
            }

            // a)
            Console.WriteLine("Sum: {0} euro", sum);

            // Start the wheel rotating
            bool started = false;

            // Is NOT empty or was not started yet
            while (cPerson <= c.Length || wheelIsEmpty(wheel) == false || started == false)
            {
                nextChair(c, wheel, pIds);
                started = true;
            }

            // Output results
            Console.Write("Order of persons that left the wheel: ");
            foreach (var item in left)
            {
                Console.Write("{0} ", item + 1);
            }
            Console.WriteLine("\nLast chair that was emptied: {0}", lastChair + 1);
        }

        private static int ReadInteger(string caption)
        {
            Console.Write(caption);
            int value;
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Invalid integer. Please specify a valid integer.");
                Console.Write(caption);
            }
            return value;
        }
    }
}