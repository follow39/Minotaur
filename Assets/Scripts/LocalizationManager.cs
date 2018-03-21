using UnityEngine;
using System.Collections;

public static class LocalizationManager
{
    public static string Localization(string key)
    {
        switch (key)
        {
            case "Gold":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Gold";
                    case SystemLanguage.Russian:
                        return "Золото";
                }

            case "Time":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Time";
                    case SystemLanguage.Russian:
                        return "Время";
                }

            case "StartGame":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Start Game";
                    case SystemLanguage.Russian:
                        return "Начать игру";
                }

            case "Shop":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Shop";
                    case SystemLanguage.Russian:
                        return "Магазин";
                }

            case "Records":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Records";
                    case SystemLanguage.Russian:
                        return "Рекорды";
                }

            case "Current":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Current";
                    case SystemLanguage.Russian:
                        return "Текущий";
                }

            case "Next":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Next";
                    case SystemLanguage.Russian:
                        return "Следующий";
                }

            case "Count":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Count";
                    case SystemLanguage.Russian:
                        return "Количество";
                }

            case "Cooldown":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Cooldown";
                    case SystemLanguage.Russian:
                        return "Перезарядка";
                }


            case "Fail":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Fail!";
                    case SystemLanguage.Russian:
                        return "Ошибка!";
                }


            case "Success":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Success!";
                    case SystemLanguage.Russian:
                        return "Успех!";
                }


            case "Easy":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Easy";
                    case SystemLanguage.Russian:
                        return "Легко";
                }

            case "Normal":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Normal";
                    case SystemLanguage.Russian:
                        return "Средне";
                }

            case "Hard":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Hard";
                    case SystemLanguage.Russian:
                        return "Сложно";
                }

            case "Hardcore":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Hardcore";
                    case SystemLanguage.Russian:
                        return "Очень сложно";
                }

            case "Resume":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Resume";
                    case SystemLanguage.Russian:
                        return "Продолжить";
                }

            case "Restart":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Restart";
                    case SystemLanguage.Russian:
                        return "Заново";
                }

            case "Menu":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Menu";
                    case SystemLanguage.Russian:
                        return "Меню";
                }

            case "Exit":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Exit";
                    case SystemLanguage.Russian:
                        return "Выход";
                }

            case "PanelHitPoints":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Your hit points increases";
                    case SystemLanguage.Russian:
                        return "Увеличивает уровень здоровья персонажа";
                }

            case "PanelEnergy":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Increases the energy to run";
                    case SystemLanguage.Russian:
                        return "Увеличивает количество энергии для бега";
                }

            case "PanelBackpack":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Increases the capacity of your backpack";
                    case SystemLanguage.Russian:
                        return "Увеличивает вместимость вашего рюкзака";
                }

            case "PanelIllumination":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Increases the range of illumination";
                    case SystemLanguage.Russian:
                        return "Увеличивает расстояние освещения";
                }

            case "PanelPath":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Using this object you leave traces in 25 seconds";
                    case SystemLanguage.Russian:
                        return "Используя этот предмет вы оставляете следы в течении 25 секунд";
                }

            case "PanelTrap":
                switch (Application.systemLanguage)
                {
                    default:
                        return "When injected into the trap of this monster can not perform actions";
                    case SystemLanguage.Russian:
                        return "При попадании в этот капкан монстр не может совершать действия";
                }

            case "PanelEnergyAdd":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Restores some amount of energy";
                    case SystemLanguage.Russian:
                        return "Восстанавливает некоторое количество энергии";
                }

            case "PanelVoodoo":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Using magic voodoo you neutralization monster for a while";
                    case SystemLanguage.Russian:
                        return "Используя магию вуду вы обезвреживаете монстра на некоторое время";
                }

            case "PanelAdvertising1":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Displays ads in 2 times less";
                    case SystemLanguage.Russian:
                        return "Показывать рекламу в 2 раза реже";
                }

            case "PanelAdvertising2":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Displays ads in 3 times less";
                    case SystemLanguage.Russian:
                        return "Показывать рекламу в 3 раза реже";
                }

            case "PanelAdvertising3":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Displays ads in 4 times less";
                    case SystemLanguage.Russian:
                        return "Показывать рекламу в 4 раза реже";
                }

            case "PanelAdvertising4":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Displays ads in 5 times less";
                    case SystemLanguage.Russian:
                        return "Показывать рекламу в 5 раз реже";
                }

            case "PanelGold1":
                switch (Application.systemLanguage)
                {
                    default:
                        return "gold - 1$";
                    case SystemLanguage.Russian:
                        return "золота - 1$";
                }

            case "PanelGold2":
                switch (Application.systemLanguage)
                {
                    default:
                        return "gold - 2$";
                    case SystemLanguage.Russian:
                        return "золота - 2$";
                }

            case "PanelGold3":
                switch (Application.systemLanguage)
                {
                    default:
                        return "gold - 3$";
                    case SystemLanguage.Russian:
                        return "золота - 3$";
                }

            case "PanelGold4":
                switch (Application.systemLanguage)
                {
                    default:
                        return "gold - 4$";
                    case SystemLanguage.Russian:
                        return "золота - 4$";
                }

            case "TextMoveButtons":
                switch (Application.systemLanguage)
                {
                    default:
                        return "These buttons are moved your character";
                    case SystemLanguage.Russian:
                        return "Эти кнопки передвигают вашего персонажа";
                }

            case "TextTimePanel":
                switch (Application.systemLanguage)
                {
                    default:
                        return "This shows the elapsed time and the amount of gold taken";
                    case SystemLanguage.Russian:
                        return "Здесь показывается прошедшее время и количество собранного золота";
                }

            case "TextPauseButton":
                switch (Application.systemLanguage)
                {
                    default:
                        return "This button pauses the game";
                    case SystemLanguage.Russian:
                        return "Эта кнопка приостанавливает игру";
                }

            case "TextFinishButton":
                switch (Application.systemLanguage)
                {
                    default:
                        return "With this button, you will be able to complete the game, if you have a several amount of gold in a backpack";
                    case SystemLanguage.Russian:
                        return "С помощью этой кнопки вы сможете закончить игру, если у вас есть некоторое количество золота в рюкзаке";
                }

            case "TextRunningButton":
                switch (Application.systemLanguage)
                {
                    default:
                        return "This button activates the run";
                    case SystemLanguage.Russian:
                        return "Эта кнопка активирует режим бега";
                }

            case "TextCollectButton":
                switch (Application.systemLanguage)
                {
                    default:
                        return "With this button you can collect gold";
                    case SystemLanguage.Russian:
                        return "С помощью этой кнопки вы можете собирать золото";
                }

            case "Mission":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Your mission - to find a very valuable treasures and bring them out of the maze .. If you can survive.";
                    case SystemLanguage.Russian:
                        return "Ваша миссия - найти очень ценные сокровища и вынести их из лабиринта.. Если вы сможете выжить.";
                }

            case "Tap":
                switch (Application.systemLanguage)
                {
                    default:
                        return "Tap on the screen.";
                    case SystemLanguage.Russian:
                        return "Коснитесь экрана";
                }
        }
        return key;
    }
}
