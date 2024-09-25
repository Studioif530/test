namespace 테스트3
{
    using System.IO;

    internal class Program
    {
        static int gold = 1500; // 초기 Gold
        static int attackPower = 10; // 기본 공격력
        static int defensePower = 5; // 기본 방어력
        static int health = 50; // 초기 체력
        static int level = 1; // 초기 레벨
        static int experience = 0; // 초기 경험치
        static int maxExperience = 100; // 레벨업에 필요한 경험치

        // 인벤토리 저장용 리스트 (아이템명, 장착여부)
        static Dictionary<string, bool> inventory = new Dictionary<string, bool>();

        static void Main(string[] args)
        {
            
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("스파르타 마을에 오신걸 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");

                Console.WriteLine("1. 상태보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 휴식");
                Console.WriteLine("5. 던전 탐험");
                

                Console.WriteLine("원하시는 행동을 입력해주세요: ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    ShowStatus();
                }
                else if (input == "2")
                {
                    ShowInventory();
                }
                else if (input == "3")
                {
                    EnterShop();
                }
                else if (input == "4")
                {
                    Rest();
                }
                else if (input == "5")
                {
                    EnterDungeon();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }

                
            }
        }

        static void ShowStatus()
        {
            bool showStatus = true;
            while (showStatus)
            {
                // 상태보기 화면 출력
                Console.WriteLine("상태보기.");
                Console.WriteLine($"Lv. {level}");
                Console.WriteLine($"경험치 : {experience}/{maxExperience}");
                Console.WriteLine($"Chad (전사)");
                Console.WriteLine($"공격력 : {attackPower} {(inventory.ContainsKey("초보자 단검") && inventory["초보자 단검"] ? "(+5)" : "")}");
                Console.WriteLine($"방어력 : {defensePower} {(inventory.ContainsKey("초보자 갑옷") && inventory["초보자 갑옷"] ? "(+5)" : "")} {(inventory.ContainsKey("초보자 신발") && inventory["초보자 신발"] ? "(+2)" : "")}");
                Console.WriteLine($"체력 : {health}");
                Console.WriteLine($"Gold : {gold} G");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("원하시는 행동을 입력해주세요: ");

                string statusInput = Console.ReadLine();

                if (statusInput == "0")
                {
                    showStatus = false; // 상태보기 화면 종료, 메인 메뉴로 복귀
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                }
            }
        }

        static void EnterDungeon()
        {
            bool inDungeon = true;

            while (inDungeon)
            {
                Console.WriteLine("던전을 선택하세요:");
                Console.WriteLine("1. Easy (권장 방어력: 5)");
                Console.WriteLine("2. Normal (권장 방어력: 10)");
                Console.WriteLine("3. Hard (권장 방어력: 20)");
                Console.WriteLine("0. 나가기");
                string dungeonChoice = Console.ReadLine();

                // 던전 선택에 따른 처리
                switch (dungeonChoice)
                {
                    case "1":
                        DungeonBattle(5, 500); // Easy 던전
                        break;
                    case "2":
                        DungeonBattle(10, 1000); // Normal 던전
                        break;
                    case "3":
                        DungeonBattle(20, 2000); // Hard 던전
                        break;
                    case "0":
                        inDungeon = false; // 던전 나가기
                        break;
                    default:
                        Console.WriteLine("잘못된 선택입니다. 다시 입력해주세요.");
                        break;
                }
            }
        }

        // 던전 전투 및 보상 로직 함수
        static void DungeonBattle(int requiredDefense, int reward)
        {
            const int healthLoss = 5; // 던전에 들어갈 때 기본 체력 소모

            // 던전 입장 전에 체력 확인
            if (health <= healthLoss)
            {
                Console.WriteLine("체력이 부족하여 던전에 입장할 수 없습니다. 체력을 회복하세요.");
                return; // 체력이 부족하면 함수 종료
            }

            // 체력 감소 처리
            health -= healthLoss;
            Console.WriteLine($"던전에 입장하여 체력이 {healthLoss}만큼 감소했습니다. 현재 체력: {health}");

            // 방어력에 따른 성공/실패 처리 (난이도에 따라)
            if (defensePower < requiredDefense)
            {
                Console.WriteLine($"방어력이 부족합니다. 권장 방어력은 {requiredDefense}입니다.");
                // 추가 실패 로직이나 페널티 처리 가능
            }
            else
            {
                Console.WriteLine("던전을 클리어했습니다!");

                // 보상 지급
                gold += reward;
                Console.WriteLine($"던전을 클리어하고 {reward} G를 얻었습니다! 현재 골드: {gold} G");

                // 경험치 획득 (현재 레벨의 1/10만큼 증가)
                int gainedExperience = level * 10;
                experience += gainedExperience;
                Console.WriteLine($"경험치 {gainedExperience}를 획득했습니다! 현재 경험치: {experience}/{maxExperience}");

                // 레벨업 조건 체크
                if (experience >= maxExperience)
                {
                    LevelUp();
                }
            }
        }

        static void LevelUp()
        {
            level++; // 레벨 증가
            attackPower++; // 레벨 상승마다 공격력 1 증가
            defensePower++; // 레벨 상승마다 방어력 1 증가
            experience = 0; // 레벨 상승 후 경험치는 다시 0으로
            maxExperience = 100 + (level - 1) * 50; // 레벨업에 필요한 경험치 증가

            Console.WriteLine($"축하합니다! 레벨 {level}로 상승하였습니다!");
            Console.WriteLine($"공격력과 방어력이 1씩 증가하였습니다. 현재 공격력: {attackPower}, 방어력: {defensePower}");
        }

        static void ShowInventory()
        {
            bool showInventory = true;
            while (showInventory)
            {
                // 인벤토리 화면 출력
                Console.WriteLine("인벤토리를 확인합니다.");
                if (inventory.Count == 0)
                {
                    Console.WriteLine("아이템이 없습니다.");
                }
                else
                {
                    foreach (var item in inventory)
                    {
                        string equipped = item.Value ? "(장착중)" : "";
                        Console.WriteLine($"{item.Key} {equipped}");
                    }
                }
                Console.WriteLine("0. 나가기");
                Console.WriteLine("E. 아이템 장착/해제");
                Console.WriteLine("원하시는 행동을 입력해주세요: ");

                string inventoryInput = Console.ReadLine();

                if (inventoryInput == "0")
                {
                    showInventory = false; // 인벤토리 종료, 메인 메뉴로 복귀
                }
                else if (inventoryInput.ToUpper() == "E")
                {
                    EquipItem();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                }
            }
        }

        static void EquipItem()
        {
            Console.WriteLine("장착하거나 해제할 아이템을 선택하세요: ");
            foreach (var item in inventory)
            {
                string equipped = item.Value ? "(장착중)" : "";
                Console.WriteLine($"{item.Key} {equipped}");
            }

            string itemToEquip = Console.ReadLine();

            if (inventory.ContainsKey(itemToEquip))
            {
                if (inventory[itemToEquip])
                {
                    // 이미 장착된 상태라면 해제
                    inventory[itemToEquip] = false;
                    Console.WriteLine($"{itemToEquip}을(를) 해제하였습니다.");
                    if (itemToEquip == "초보자 단검") attackPower -= 5;
                    else if (itemToEquip == "초보자 갑옷") defensePower -= 5;
                    else if (itemToEquip == "초보자 신발") defensePower -= 2;
                }
                else
                {
                    // 장착하지 않은 상태라면 장착
                    inventory[itemToEquip] = true;
                    Console.WriteLine($"{itemToEquip}을(를) 장착하였습니다.");
                    if (itemToEquip == "초보자 단검") attackPower += 5;
                    else if (itemToEquip == "초보자 갑옷") defensePower += 5;
                    else if (itemToEquip == "초보자 신발") defensePower += 2;
                }
            }
            else
            {
                Console.WriteLine("해당 아이템이 인벤토리에 없습니다.");
            }
        }

        static void EnterShop()
        {
            bool inShop = true;
            while (inShop)
            {
                // 상점 화면 출력
                Console.WriteLine("상점에 들어갑니다.");
                Console.WriteLine($"1. 초보자 갑옷 | 방어력 5 | 초보자를 위한 갑옷입니다. {GetPurchaseStatus("초보자 갑옷")} 가격: 1000 G");
                Console.WriteLine($"2. 초보자 단검 | 공격력 5 | 초보자를 위한 단검입니다. {GetPurchaseStatus("초보자 단검")} 가격: 500 G");
                Console.WriteLine($"3. 초보자 신발 | 방어력 2 | 초보자를 위한 신발입니다. {GetPurchaseStatus("초보자 신발")} 가격: 200 G");
                Console.WriteLine("4. 아이템 판매"); // 아이템 판매 기능 추가
                Console.WriteLine("0. 나가기");
                Console.WriteLine("Gold: " + gold + " G");
                Console.WriteLine("원하시는 행동을 입력해주세요: ");

                string shopInput = Console.ReadLine();

                if (shopInput == "1")
                {
                    PurchaseItem("초보자 갑옷", 1000, 5); // 가격 1000
                }
                else if (shopInput == "2")
                {
                    PurchaseItem("초보자 단검", 500, 5); // 가격 500
                }
                else if (shopInput == "3")
                {
                    PurchaseItem("초보자 신발", 200, 2);
                }
                else if (shopInput == "4")
                {
                    SellItem(); // 아이템 판매 호출
                }
                else if (shopInput == "0")
                {
                    inShop = false; // 상점 종료, 메인 메뉴로 복귀
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                }
            }
        }

        static void SellItem()
        {
            Console.WriteLine("판매할 아이템을 선택하세요: ");
            foreach (var item in inventory)
            {
                if (item.Value) // 장착된 아이템만 판매 가능
                {
                    Console.WriteLine($"{item.Key} (장착중)");
                }
                else
                {
                    Console.WriteLine(item.Key);
                }
            }

            string itemToSell = Console.ReadLine();

            if (inventory.ContainsKey(itemToSell) && !inventory[itemToSell])
            {
                // 판매 가격 계산 (85%)
                int salePrice = 0;
                if (itemToSell == "초보자 갑옷") salePrice = (int)(1000 * 0.85);
                else if (itemToSell == "초보자 단검") salePrice = (int)(500 * 0.85);
                else if (itemToSell == "초보자 신발") salePrice = (int)(200 * 0.85);

                // 인벤토리에서 아이템 제거
                inventory.Remove(itemToSell);
                gold += salePrice; // 금액 추가
                Console.WriteLine($"{itemToSell}을(를) 판매하였습니다. 판매 금액: {salePrice} G");
            }
            else
            {
                Console.WriteLine("판매할 수 없는 아이템입니다.");
            }
        }

        static string GetPurchaseStatus(string itemName)
        {
            // 이미 인벤토리에 있는 아이템이라면 "(구매 완료)"를 표시
            return inventory.ContainsKey(itemName) ? "(구매 완료)" : "";
        }

        static void PurchaseItem(string itemName, int cost, int statBonus)
        {
            if (!inventory.ContainsKey(itemName))
            {
                if (gold >= cost)
                {
                    inventory.Add(itemName, false); // 장착 여부는 false로 초기화
                    gold -= cost;
                    Console.WriteLine($"{itemName}을(를) 구매하였습니다.");
                }
                else
                {
                    Console.WriteLine("Gold가 부족합니다.");
                }
            }
            else
            {
                Console.WriteLine("이미 소지하고 있는 아이템입니다.");
            }
        }



        static void Rest()
        {
            const int restCost = 300; // 휴식 비용
            const int healthRecovery = 10; // 회복할 체력
            const int maxHealth = 100; // 최대 체력

            if (gold >= restCost)
            {
                gold -= restCost; // 금액 차감
                health += healthRecovery; // 체력 회복

                // 체력이 최대값을 초과하지 않도록 제한
                if (health > maxHealth)
                {
                    health = maxHealth; // 최대 체력으로 설정
                }

                Console.WriteLine($"휴식을 완료했습니다. 현재 체력: {health}");
            }
            else
            {
                Console.WriteLine("Gold가 부족합니다.");
            }
        }

        
    }
}
