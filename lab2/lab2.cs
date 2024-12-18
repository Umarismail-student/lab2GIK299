using System;

namespace WindchillCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("=== Wind Chill Factor Calculator ===");

                // Input för temperatur
                double temperature = GetValidatedDouble("Enter outdoor temperature in Celsius:");

                // Input för vindhastighet och enhet
                Console.WriteLine("Enter wind speed unit ('m' for m/s or 'k' for km/h):");
                string windSpeedUnit = Console.ReadLine().ToLower();

                double windSpeed = GetValidatedDouble("Enter wind speed value:");

                // Omvandling om vindhastighet är i m/s
                if (windSpeedUnit == "m")
                {
                    windSpeed *= 3.6;
                }

                // Skapa en instans av WindChillCalculator
                WindChillCalculator calc = new WindChillCalculator(temperature, windSpeed);

                // Beräkning och resultat
                double WCT = calc.CalculateWCT();
                string classification = calc.ClassifyWCT();

                Console.WriteLine($"\nWind Chill Factor: {WCT:F1}");
                Console.WriteLine($"Classification: {classification}\n");

                // Meny för att fortsätta eller avsluta
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Calculate again");
                Console.WriteLine("2. Exit");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Fortsätt loopen för ny beräkning
                        break;
                    case "2":
                        Console.WriteLine(" Goodbye!");
                        return; // Avslutar programmet
                    default:
                        Console.WriteLine("Invalid choice.  Please enter 1 or 2.\n");
                        break;
                }
            }
        }

        /// <summary>
        /// Validerar inmatade nummer och hanterar ogiltiga inputs.
        /// </summary>
        static double GetValidatedDouble(string prompt)
        {
            double value;
            while (true)
            {
                Console.WriteLine(prompt);
                if (double.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }
                Console.WriteLine("Invalid input . Please enter a valid nummber.");
            }
        }
    }

    /// <summary>
    /// Klass som hanterar beräkning och klassificering av Wind Chill Factor.
    /// </summary>
    class WindChillCalculator
    {
        private double Temperature { get; }
        private double WindSpeed { get; }

        public WindChillCalculator(double temperature, double windSpeed)
        {
            Temperature = temperature;
            WindSpeed = windSpeed;
        }

        /// <summary>
        /// Beräkna Wind Chill Factor baserat på formeln
        /// </summary>
        public double CalculateWCT()
        {
            return 13.12 + (0.6215 * Temperature) - (11.37 * Math.Pow(WindSpeed, 0.16)) +
                   (0.3965 * Temperature * Math.Pow(WindSpeed, 0.16));
        }

        /// <summary>
        /// Klassificerar WCT enligt specifika gränsvärden.
        /// </summary>
        public string ClassifyWCT()
        {
            double WCT = CalculateWCT();

            if (WCT > -25) return "Cold";
            if (WCT >= -35) return "Very Cold";
            if (WCT >= -60) return "Risk of Frostbite";
            return "Severee Frostbite risk";
            // vi har tagit hjälp från youtube, c sharp boken och chatgpt som inlärningsmedel.
        }
    }
}
