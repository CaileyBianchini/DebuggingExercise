using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Game
    {
        //Cailey Bianchini
        //this is the debugging exercise however I accidently replaced it with Hello World, I copy and pasted the original so its alright



        bool _gameOver = false;
        string _playerName = "Hero";
        int _playerHealth = 120;
        int playerDamage = 20;
        int _playerDefense = 10;
        int _levelScaleMax = 5;

        int _turnCount = 0;

        Random random;


        //Run the game
        public void Run()
        {

            while (_gameOver == false)
            {
                Start();
                Update();
                End();

            }

        }
        //This function handles the battles for our ladder. roomNum is used to update the our opponent to be the enemy in the current room. 
        //turnCount is used to keep track of how many turns it took the player to beat the enemy
        bool StartBattle(int roomNum, ref int turnCount)
        {
            //initialize default enemy stats
            int enemyHealth = 0;
            int enemyAttack = 0;
            int enemyDefense = 0;
            string enemyName = "";
            //Changes the enemy's default stats based on our current room number. 
            //This is how we make it seem as if the player is fighting different enemies
            switch (roomNum)
            {
                case 0:
                    {
                        enemyHealth = 100;
                        enemyAttack = 20;
                        enemyDefense = 5;
                        enemyName = "Wizard";
                        break;
                    }
                case 1:
                    {
                        enemyHealth = 80;
                        enemyAttack = 30;
                        enemyDefense = 5;
                        enemyName = "Troll";
                        break;
                    }
                case 2:
                    {

                        enemyHealth = 200;
                        enemyAttack = 40;
                        enemyDefense = 10;
                        enemyName = "Giant";
                        break;
                    }
            }
  

            //Loops until the player or the enemy is dead
            while (_playerHealth > 0 && enemyHealth > 0)
            {
                //Displays the stats for both charactersa to the screen before the player takes their turn
                PrintStats(_playerName, _playerHealth, playerDamage, _playerDefense);
                PrintStats(enemyName, enemyHealth, enemyAttack, enemyDefense);

                //Get input from the player
                char input = ' ';
                input = GetInput("Attack", "Defend", "What would you like to do?");
                //If input is 1, the player wants to attack. By default the enemy blocks any incoming attack < that sucks so no
                if (input == '1')
                {
                    enemyHealth -= playerDamage;
                    Console.WriteLine("\nYou dealt " + playerDamage + " damage.");
                    Console.Write("> ");
                    Console.ReadKey();
                    Console.Clear();

                }
                //If the player decides to defend the enemy just takes their turn. However this time the block attack function is
                //called instead of simply decrementing the health by the enemy's attack value.
                if (input == '2')
                {
                    int attackVal = playerDamage;
                    int damage = attackVal - enemyDefense;
                    BlockAttack(ref enemyHealth, ref attackVal, ref enemyDefense, ref damage);
                    Console.WriteLine(enemyName + " dealt " + enemyAttack + " damage.");

                    Console.Write("> ");
                    Console.ReadKey();
                    Console.Clear();

                    
                }

                if (enemyHealth > 0)
                { 
                    //After the player attacks, the enemy takes its turn. Since the player decided not to defend, the block attack function is not called.
                    _playerHealth -= enemyAttack;
                    Console.WriteLine(enemyName + " dealt " + enemyAttack + " damage.");
                }
                Console.Write("> ");
                Console.ReadKey();
                Console.Clear();
                turnCount++;

                if (_playerHealth < enemyAttack)
                {
                    return _playerHealth <= enemyAttack;
                }

            }
            //Return whether or not our player died
            roomNum++;
            return _playerHealth <= 0;


        }
        //Decrements the health of a character. The attack value is subtracted by that character's defense
        int BlockAttack(ref int enemyHealth, ref int attackVal,  ref int enemyDefense, ref int damage)
        {

            attackVal = playerDamage;
            damage = attackVal - enemyDefense;
            if (damage < 0)
            {
                damage = 0;
            }
            enemyHealth -= damage;
            return damage;
        }

        void PotionChoices(int _playerHealth, int _playerDefense)
        {
            char input = ' ';
            Console.WriteLine("Since this is your first dungeon I will let you get these for free.");
            GetInput("Blue Potion", "Red Potion", "Random", "Which potion do you want?");
            if (input =='3')
            {
                random = new Random();
                int randomNumber = random.Next(1, 2);
            }
            if (input == '1')
            {
                Console.WriteLine("Oh? So you chose the Blue Potion? That is 10 to you defense.");
                _playerDefense = _playerDefense + 10;
            }
            else if (input == '2')
            {
                Console.WriteLine("Oh? So you chose the Red Potion? That is 30 to you defense.");
                _playerHealth = _playerHealth + 30;
            }
            else
            {
                Console.WriteLine("Too bad, maybe next time!");
                return;
            }
     
            return;
        }


        //Scales up the player's stats based on the amount of turns it took in the last battle
        //can also upgrade stats
        public void UpgradeStats(int turnCount)
        {

            //Shop
            Console.WriteLine("you walk to the shawdowy corner where you spot a susspicious man." + 
                " He smiles at you and opens up his jacket and you see experince bottles!");
            Console.WriteLine("In a scratchy voice he spoke 'Would you like to level up your stats?'");
            char input = ' ';
            GetInput("Yes", "No", "So? Whats your choice?");
            if (input == '1')
            {
                PotionChoices(_playerHealth, _playerDefense);
            }
            else
            {
                Console.WriteLine("The old man shook his head 'Such a shame, maybe next time.'");
            }

            //Subtract the amount of turns from our maximum level scale to get our current level scale
            int scale = _levelScaleMax - turnCount;
            if (scale <= 0)
            {
                scale = 1;
            }
            _playerHealth += 10 * scale;
            playerDamage *= scale;
            _playerDefense *= scale;


        }
        //Gets input from the player
        //Out's the char variable given. This variables stores the player's input choice.
        //The parameters option1 and option 2 displays the players current chpices to the screen
        char GetInput(string option1, string option2)
        {
            //Initialize input
            char input = ' ';
            //Loop until the player enters a valid input
            while (input != '1' && input != '2')
            {
                Console.WriteLine("1." + option1);
                Console.WriteLine("2." + option2);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
            }
            return input;
        }
        char GetInput(string option1, string option2, string quiry)
        {
            Console.WriteLine(quiry);
            //Initialize input
            char input = ' ';
            //Loop until the player enters a valid input
            while (input != '1' && input != '2')
            {
                Console.WriteLine("1." + option1);
                Console.WriteLine("2." + option2);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
            }
            return input;
        }
        char GetInput(string option1, string option2, string option3, string quiry)
        {
            Console.WriteLine(quiry);
            //Initialize input
            char input = ' ';
            //Loop until the player enters a valid input
            while (input != '1' && input != '2')
            {
                Console.WriteLine("1." + option1);
                Console.WriteLine("2." + option2);
                Console.WriteLine("3. " + option3);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
            }
            return input;
        }
        char GetInput(string option1, string option2, string option3, string option4, string quiry)
        {
            Console.WriteLine(quiry);
            //Initialize input
            char input = ' ';
            //Loop until the player enters a valid input
            while (input != '1' && input != '2')
            {
                Console.WriteLine("1." + option1);
                Console.WriteLine("2." + option2);
                Console.WriteLine("3. " + option3);
                Console.WriteLine("4. " + option4);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
            }
            return input;
        }

        //Prints the stats given in the parameter list to the console
        void PrintStats(string name, int health, int damage, int defense)
        {
            Console.WriteLine("\n" + name);
            Console.WriteLine("Health: " + health);
            Console.WriteLine("Damage: " + playerDamage);
            Console.WriteLine("Defense: " + defense);
        }

        //This is used to progress through our game. A recursive function meant to switch the rooms and start the battles inside them.
        void ClimbLadder(int roomNum)
        {
            if (_playerHealth > 0)
            {
                Console.WriteLine("You are in room " + roomNum);
                char input = ' ';
                input = GetInput("Go Forward", "Stay", "Go to the shadowed man", "What would you like to do?");
                if (input == '1')
                {
                    roomNum++;
                }
                else if(input == '3')
                {
                    UpgradeStats(_levelScaleMax); 
                    return;
                }

            }
            else
            {
                _gameOver = true;
                return;
            }


            //Displays context based on which room the player is in
            switch (roomNum)
            {
                case 0:
                    {
                        Console.WriteLine("\nA wizard blocks your path");
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("\nA troll stands before you");
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("\nA giant has appeared!");
                        break;
                    }
                default:
                    {
                        _gameOver = true;
                        break;
                    }
            }
            int turnCount = 0;
            //Starts a battle. If the player survived the battle, level them up and then proceed to the next room.
            if (StartBattle(roomNum, ref turnCount))
            {
                UpgradeStats(turnCount);
                ClimbLadder(roomNum + 1);
            }
                _gameOver = true;
        }

        //Displays the character selection menu. 
        void SelectCharacter()
        {
            char input = ' ';
            //Loops until a valid option is choosen
            while (input != '1' && input != '2' && input != '3')
            {
                //Prints options
                Console.WriteLine("Welcome! Please select a character.");
                Console.WriteLine("1.Sir Kibble");
                Console.WriteLine("2.Gnojoel");
                Console.WriteLine("3.Joedazz");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                //Sets the players default stats based on which character was picked
                switch (input)
                {
                    case '1':
                        {
                            _playerName = "Sir Kibble";
                            int _playerHealth = 120;
                            int _playerDefense = 10;
                            int playerDamage = 40;
                            break;
                        }
                    case '2':
                        {
                            _playerName = "Gnojoel";
                            int _playerHealth = 40;
                            int _playerDefense = 2;
                            int playerDamage = 70;
                            break;
                        }
                    case '3':
                        {
                            _playerName = "Joedazz";
                            int _playerHealth = 200;
                            int _playerDefense = 5;
                            int playerDamage = 25;
                            break;
                        }
                    //If an invalid input is selected display and input message and input over again.
                    default:
                        {
                            Console.WriteLine("Invalid input. Press any key to continue.");
                            Console.Write("> ");
                            Console.ReadKey();
                            break;
                        }
                }
                Console.Clear();
            }
            //Prints the stats of the choosen character to the screen before the game begins to give the player visual feedback
            PrintStats(_playerName, _playerHealth, playerDamage, _playerDefense);
            Console.WriteLine("Press any key to continue.");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();
        }
        //Performed once when the game begins
        public void Start()
        {
            SelectCharacter();
        }

        //Repeated until the game ends
        public void Update()
        {
            ClimbLadder(0);
        }

        //Performed once when the game ends
        public void End()
        {
            //If the player died print death message
            if (_playerHealth <= 0)
            {
                Console.WriteLine("Failure");
                return;
            }
            else
            {
                //Print game over message
                Console.WriteLine("Congrats");
            }
        }
    }
}