using System.ComponentModel.Design;
using System.Threading.Tasks.Dataflow;

namespace Assignment1_txtRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int MaxInventorySize = 5;
            const string HpPotion = "HP 포션";
            const int PotionUp = 30;
            const int MonsterType = 3;

            bool inGame = true;

            string playerName;
            string playerJob = "누락"; //초기화
            int playerMaxHp = 0;
            int playerHp = 0;
            int playerMaxMp = 0;
            int playerMp = 0;
            int playerAtk = 0;
            int playerDef = 0;
            string playerWeapon = "";



            string[] inventory = new string[MaxInventorySize];
            inventory[1] = HpPotion;
            inventory[2] = "";
            inventory[3] = "";
            inventory[4] = "";


            Console.WriteLine("========================================");
            Console.WriteLine("        텍스트 RPG - 캐릭터 관리");
            Console.WriteLine("========================================");
            Console.Write("이름을 입력하세요:");
            playerName = Console.ReadLine();

            //직업선택
            while (playerJob == "누락")
            {
                Console.Write("직업을 선택하세요 (1.전사 / 2.마법사 / 3.궁수):");
                playerJob = Console.ReadLine();
                switch (playerJob)
                {
                    case "1":
                        playerJob = "전사";
                        playerMaxHp = 120;
                        playerHp = 120;
                        playerMaxMp = 60;
                        playerMp = 60;
                        playerAtk = 30;
                        playerDef = 15;
                        playerWeapon = "낡은 검";
                        inventory[0] = playerWeapon;
                        break;

                    case "2":
                        playerJob = "마법사";
                        playerMaxHp = 80;
                        playerMaxMp = 120;
                        playerHp = 80;
                        playerMp = 120;
                        playerAtk = 22;
                        playerDef = 8;
                        playerWeapon = "낡은 지팡이";
                        inventory[0] = playerWeapon;
                        break;
                    case "3":
                        playerJob = "궁수";
                        playerMaxHp = 100;
                        playerMaxMp = 80;
                        playerHp = 100;
                        playerMp = 80;
                        playerAtk = 26;
                        playerDef = 10;
                        playerWeapon = "낡은 활";
                        inventory[0] = playerWeapon;
                        break;
                    default:
                        Console.WriteLine("직업을 다시 선택해주세요.");
                        playerJob = "누락"; //다시 묻게 만들기 위해
                        continue;
                }
            }
            //시작화면
            ShowCharater(playerName, playerJob, playerMaxHp, playerHp, playerMaxMp, playerMp, playerAtk, playerDef);
            ShowInventory(MaxInventorySize, inventory);


            while (inGame == true && playerHp > 0)
            {
                ShowMenu();
                switch (GetInput())
                {
                    case 0:
                        Console.Clear();
                        inGame = false;
                        break;

                    case 1:
                        Console.Clear();
                        playerHp = HuntMonster(MonsterType, playerAtk, playerHp, playerDef, ref inventory, MaxInventorySize);
                        break;

                    case 2:
                        Console.Clear();
                        playerHp = UsePotion(MaxInventorySize, inventory, HpPotion, playerHp, playerMaxHp, PotionUp);
                        continue;

                    case 3:
                        Console.Clear();
                        ShowInventory(MaxInventorySize, inventory);
                        continue;

                    case 4:
                        Console.Clear();
                        ShowCharater(playerName, playerJob, playerMaxHp, playerHp, playerMaxMp, playerMp, playerAtk, playerDef);
                        continue;

                    default:
                        Console.Clear();
                        Console.WriteLine("행동 선택이 잘 못 되었습니다. 다시 선택해주세요.(숫자만 입력가능)");
                        continue;
                }
            }
            if (playerHp <= 0)
            {
                Console.WriteLine("체력이 0이하가 되어 사망했습니다.");
            }
            Console.WriteLine("게임이 종료되었습니다.");

        }

        static void ShowCharater(string name, string job, int maxHp, int hp, int maxMp, int mp, int atk, int def)
        {
            Console.WriteLine();
            Console.WriteLine($"[ {name} / {job} ]");
            Console.WriteLine($"HP: {hp} / {maxHp}\tMP:{mp} / {maxMp}\t공격력: {atk}\t방어력: {def}");
        }

        static void ShowInventory(int invensize, string[] inven)
        {

            for (int i = 0; i < invensize; i++)
            {
                if (inven[i] == "")
                {
                    inven[i] = "(빈 슬롯)";
                }
            }
            Console.WriteLine();
            Console.WriteLine("----------인벤토리----------");
            Console.WriteLine($"[0] {inven[0]}");
            Console.WriteLine($"[1] {inven[1]}");
            Console.WriteLine($"[2] {inven[2]}");
            Console.WriteLine($"[3] {inven[3]}");
            Console.WriteLine($"[4] {inven[4]}");
            Console.WriteLine("-----------------------------");
        }

        static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("========== 행동을 선택하세요 ==========");
            Console.WriteLine("1.몬스터 사냥");
            Console.WriteLine("2. 포션 사용");
            Console.WriteLine("3. 인벤토리 확인");
            Console.WriteLine("4. 캐릭터 정보");
            Console.WriteLine("0. 종료");
            Console.WriteLine(">>>");
        }

        static int GetInput()
        {
            int input = 0;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out input)) { break; }
                else
                {
                    Console.Clear();
                    Console.WriteLine("행동 선택이 잘 못 되었습니다. 다시 선택해주세요.(숫자만 입력가능)");
                    ShowMenu();
                    continue;
                }
            }
            return input;
        }

        static int UsePotion(int invensize, string[] inventory, string potion, int hp, int maxHp, int potionUp)
        {
            potion = "HP 포션";
            bool hasPotion = false;
            int potionLocation = 0; //초기화

            for (int i = 0; i < invensize; i++)
            {
                if (inventory[i] == potion)
                {
                    hasPotion = true;
                    potionLocation = i;
                    break;
                }
            }

            if (hasPotion == true)
            {
                if (hp == maxHp) //풀피일때
                {
                    Console.WriteLine("현재 체력이 가득차있습니다. 포션을 사용하지 않습니다.");
                }
                else
                {
                    if (hp + potionUp > maxHp) //초과한다면
                    {
                        hp = maxHp;
                    }
                    else
                    {
                        hp += potionUp;
                    }
                    inventory[potionLocation] = "";
                    Console.WriteLine("포션을 사용하여 회복하였습니다.");
                }
                Console.WriteLine($"현재 HP: {hp}/{maxHp}");
            }
            else //포션이 없을 때
            {
                Console.WriteLine("사용할 포션이 없습니다. 인벤토리를 확인하세요.");
            }
            return hp;
        }

        static int HuntMonster(int monsterType, int playerAtk, int playerHp, int playerDef, ref string[] inventory, int invensize)
        {
            string monsterName = "";
            int monsterHp = 0;
            int monsterAtk = 0;
            int monsterDef = 0;


            //몬스터 생성
            Random random = new Random();
            int monsterLocation = random.Next(0, monsterType);
            string[] monster = new string[monsterType];

            switch (monsterLocation)
            {
                case 0:
                    monsterName = "슬라임";
                    monsterHp = 30;
                    monsterAtk = 8;
                    monsterDef = 2;
                    break;

                case 1:
                    monsterName = "고블린";
                    monsterHp = 50;
                    monsterAtk = 15;
                    monsterDef = 5;
                    break;

                case 2:
                    monsterName = "오크";
                    monsterHp = 80;
                    monsterAtk = 25;
                    monsterDef = 12;
                    break;
            }

            int playerDamage = playerAtk - monsterDef;
            playerDamage = Math.Max(playerDamage, 1);
            int monsterDamage = monsterAtk - playerDef;
            monsterDamage = Math.Max(monsterDamage, 1);

            Console.WriteLine("전투화면");
            Console.WriteLine($"---{monsterName} 출현!(HP: {monsterHp})---");
            while (monsterHp > 0 && playerHp > 0)
            {
                monsterHp -= playerDamage;
                Console.WriteLine($"내 공격 → {monsterName} -{playerDamage} ({monsterName} 남은 HP: {monsterHp})");
                if (monsterHp <= 0)
                {
                    break;
                }
                playerHp -= monsterDamage;
                Console.WriteLine($"{monsterName} 반격 → 나 -{monsterDamage} (내 남은 HP: {playerHp})");
            }

            if (monsterHp <= 0)
            {
                Console.WriteLine($"{monsterName}을(를) 처치했습니다!");
                int getPotion = random.Next(0, 4);
                if (getPotion == 3)
                { GetPotion(ref inventory, invensize); }

            }

            return playerHp;
        }

        static void GetPotion(ref string[] inventory, int invensize)
        {
            string potion = "HP 포션";

            for (int i = 0; i < invensize; i++)
            {
                if (inventory[i] == "" || inventory[i] == "(빈 슬롯)")
                {
                    inventory[i] = potion;
                    Console.WriteLine("포션을 획득했습니다.");
                    return;
                }
            }
            Console.WriteLine("인벤토리가 가득차 아이템을 획득하지 못했습니다.");
            return;

        }
    }
}