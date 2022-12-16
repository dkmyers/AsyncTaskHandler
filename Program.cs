using System;
using System.Threading.Tasks;

namespace AsyncBotTasks {
    //For learning purposes, a bunch of classes are included so async functions can return something more interesting than blank values
    internal class Toast {
        int sliceCount;
        bool jam;
        public Toast(int givenSliceCount, bool givenJam)
        {
            this.sliceCount = givenSliceCount;
            this.jam = givenJam;
        }

        public int getSliceCount()
        {
            return sliceCount;
        }

        public void changeJam(bool changeJam)
        {
            this.jam = changeJam; 
        }

        public bool getJam() { 
            return jam; 
        }
    }
    internal class Juice { }

    internal class Eggs {
        int count;
        public Eggs(int givenCount) {
            this.count = givenCount;
        }
    }

    //each slice of bacon tracks itself, rather than a collection of bacon
        //This differs from eggs and toast
    //The task will instead create and manage a List<Bacon> object
    internal class Bacon {
        bool side1Fried = false;
        bool side2Fried = false;

        public Bacon() { }
        public void frySide1()
        {
            side1Fried = true;
        }

        public void frySide2()
        {
            side2Fried = true;
        }

    }

    internal class Cereal { }

    internal class Program
    {


        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World! Press the 'Enter' key to begin making breakfast.");
            Console.ReadLine();
            var toastTask = ToastBreadAsync(3);
            var eggsTask = FryEggsAsync(8);
            var juiceTask = PourJuiceAsync();
            var cerealTask = MakeABowlOfCereal();
            var baconTask = FryBacon(12);

            var toast = await toastTask;
            Console.WriteLine("Bread has been toasted!");

            var eggs = await eggsTask;
            Console.WriteLine("Eggs have been fried!");

            var juice = await juiceTask;
            Console.WriteLine("Juice has been poured!");

            var cereal = await cerealTask;
            Console.WriteLine("Cereal and Milk both poured in bowl!");

            var bacon = await baconTask;
            Console.WriteLine($"All {bacon.Count} slices of bacon have been fried!");

            Console.WriteLine("Breakfast is ready!");
        }

        static async Task<Toast> ToastBreadAsync(int slices)
        {
            Toast retVal = new Toast(slices, false);
            int sliceCopy = 0;
            //Iterate down from slicecount to 0
                //Toaster can process two slices of toast at a time
            do
            {
                if (sliceCopy == (retVal.getSliceCount() - 1))
                {
                    Console.WriteLine($"Toasting the last slice of bread...");
                }
                else
                {
                    Console.WriteLine($"Toasting bread #{sliceCopy+1} and {sliceCopy+2}");
                }
                sliceCopy += 2;
                //Each slice of toast gets jam applied
                await Task.Delay(20000);
            } while (sliceCopy < retVal.getSliceCount());
            
            return retVal;
        }


        static async Task<Juice> PourJuiceAsync()
        {
            Console.WriteLine("Pouring a glass of juice...");
            await Task.Delay(200);
            return new Juice();
        }

        static async Task<Eggs> FryEggsAsync(int number)
        {
            //Fry eggs four at a time
            var Eggs = new Eggs(number);
            int friedEggs = 0;
            while (friedEggs < number)
            {
                Console.WriteLine("Frying an egg!");
                await Task.Delay(1000);
                friedEggs++;
            }
            
            return Eggs;
        }

        static async Task<Cereal> MakeABowlOfCereal()
        {
            var retVal = new Cereal();
            Console.WriteLine("Pouring cereal into bowl!");
            await Task.Delay(5000);

            Console.WriteLine("Pouring milk into the bowl of cereal!");
            await Task.Delay(3000);
            return retVal;
        }

        static async Task<List<Bacon>> FryBacon(int number)
        {
            var retVal = new List<Bacon>();
            for(int i = 0; i < number; i++)
            {
                retVal.Add(new Bacon());
            }
            Bacon b;
            for(int i = 0; i < number; i++)
            {
                b = retVal[i];
                Console.WriteLine($"Frying the first side of bacon #{i+1}!");
                await Task.Delay(3000);
                b.frySide1();

                Console.WriteLine($"Frying the second side of bacon #{i+1}!");
                await Task.Delay(2000);
                b.frySide2();
            }



            return retVal;
        }
    }
}
