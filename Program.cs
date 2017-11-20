using System;
using banktesterforms;


namespace CloudBankTester
{
    class Program
    {

        /* INSTANCE VARIABLES */
        public static KeyboardReader reader = new KeyboardReader();
        //  public static String rootFolder = System.getProperty("user.dir") + File.separator +"bank" + File.separator ;
        public static String rootFolder = AppDomain.CurrentDomain.BaseDirectory;
        public static String prompt = "Start Mode> ";
        public static String[] commandsAvailable = new String[] { "Load Bank Keys", "Show Coins", "Deposite Coin", "Withdraw Coins","Look at Receipt", "quit" };
   //public static int timeout = 10000; // Milliseconds to wait until the request is ended. 
       // public static FileUtils fileUtils = new FileUtils(rootFolder, bank);
        public static Random myRandom = new Random();
        public static string publicKey = "";
        public static string privateKey = "";
        public static string email = "";
        public static BankKeys myKeys;




        /* MAIN METHOD */
        public static void Main(String[] args)
        {
            printWelcome();
            run(); // Makes all commands available and loops
            Console.Out.WriteLine("Thank you for using CloudBank Tester. Goodbye.");
        } // End main

        /* STATIC METHODS */
        public static async void run()
        {
            bool restart = false;
            while (!restart)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Out.WriteLine("");
                //  Console.Out.WriteLine("========================================");
                Console.Out.WriteLine("");
                Console.Out.WriteLine("Commands Available:");
                Console.ForegroundColor = ConsoleColor.White;
                int commandCounter = 1;
                foreach (String command in commandsAvailable)
                {
                    Console.Out.WriteLine(commandCounter + (". " + command));
                    commandCounter++;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Out.Write(prompt);
                Console.ForegroundColor = ConsoleColor.White;
                int commandRecieved = reader.readInt(1,5);


                switch (commandRecieved)
                {
                    case 1:
                        loadKeys();
                        break;
                    case 2:
                        showCoins();
                        break;
                    case 3:
                        await depositAsync();
                        break;
                    case 4:
                        withdraw();
                        break;
                    case 5:
                        receipt();
                        break;
                    case 6:
                        Console.Out.WriteLine("Goodbye!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Out.WriteLine("Command failed. Try again.");
                        break;
                }// end switch
            }// end while
        }// end run method


        public static void printWelcome()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.Out.WriteLine("║                   CloudBank Tester v.11.19.17                    ║");
            Console.Out.WriteLine("║          Used to Authenticate Test CloudBank                     ║");
            Console.Out.WriteLine("║      This Software is provided as is with all faults, defects    ║");
            Console.Out.WriteLine("║          and errors, and without warranty of any kind.           ║");
            Console.Out.WriteLine("║                Free from the CloudCoin Consortium.               ║");
            Console.Out.WriteLine("╚══════════════════════════════════════════════════════════════════╝");
            Console.ForegroundColor = ConsoleColor.White;
        } // End print welcome



        static void loadKeys()
        {
            publicKey = "preston.CloudCoin.global";
            privateKey = "0DECE3AF-43EC-435B-8C39-E2A5D0EA8676";
            email = "Preston@what.com";
            Console.Out.WriteLine("Public key is " + publicKey );
            Console.Out.WriteLine("Private key is " + privateKey);
            Console.Out.WriteLine("Email is " + email);
            myKeys = new BankKeys(publicKey, privateKey, email);
        }

        /* Show coins will populate the CloudBankUtils with the totals of each denominations
         These totals are public properties that can be accessed */
        static async void showCoins()
        {
            CloudBankUtils cbu = new CloudBankUtils(myKeys);
            await cbu.showCoins();
            Console.Out.WriteLine("Ones in our bank:" + cbu.onesInBank  );
            Console.Out.WriteLine("Five in our bank:" + cbu.fivesInBank);
            Console.Out.WriteLine("Twenty Fives in our bank:" + cbu.twentyFivesInBank);
            Console.Out.WriteLine("Hundreds in our bank:" + cbu.hundresInBank );
            Console.Out.WriteLine("Two Hundred Fifties in our bank:" + cbu.twohundredfiftiesInBank );
        }//end show coins


        /* Deposit allow you to send a stack file to the CloudBank */
        static async System.Threading.Tasks.Task depositAsync()
        {
            CloudBankUtils sender = new CloudBankUtils( myKeys);
            Console.Out.WriteLine("What is the path to your stack file?");
            //string path = reader.readString();
            string path = AppDomain.CurrentDomain.BaseDirectory ;
            path += reader.readString();
            Console.Out.WriteLine("Loading " + path);
            sender.loadStackFromFile(path);
            await sender.sendStackToCloudBank(publicKey);
            await sender.getReceipt(publicKey);
        }//end deposit


        static void withdraw() { }//end deposit
        static void receipt() { }//end deposit
    }
}
