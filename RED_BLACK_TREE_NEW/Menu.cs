using System;
using System.Collections.Generic;

namespace Red_BlackTree
{

    class Menu
    {
        RB tree = new RB();

        public WhatToDo Action(ref WhatToDo action, List<int> temp)//ГЛАВНАЯ ФУНКЦИЯ
        {
            FileData input = new FileData();
            if (action == WhatToDo.INSERT) ///////////////////////////////// ВСТАВКА
            {
                InputType inputType = AskForInput();
                if (inputType == InputType.FILE)//////////////ВСТАВКА ИЗ ФАЙЛА
                {
                    bool x = input.FileInput(tree, temp);
                    while (x == false)
                    {
                        x = input.FileInput(tree, temp);
                    }
                    action = ChoiceAction();
                    Action(ref action, temp);
                    return action;
                }
                else if (inputType == InputType.RANDOM)////////ВСТАВКА РАНДОМОМ
                {
                    Input.Random(tree, temp);
                    MenuAnswer toSaveInput = AskForSavingInput();
                    if (toSaveInput == MenuAnswer.YES)
                    {
                        input.SaveInput(temp);
                    }
                    action = ChoiceAction();
                    Action(ref action, temp);
                    return action;
                }
                else///////////////////////////////////////////ВСТАВКА С КЛАВЫ
                {
                    Input.Keyboard(tree, temp);
                    MenuAnswer toSaveInput = AskForSavingInput();
                    if (toSaveInput == MenuAnswer.YES)
                    {
                        input.SaveInput(temp);
                    }
                    action = ChoiceAction();
                    Action(ref action, temp);
                    return action;
                }
            }
            else if (action == WhatToDo.DELETE)///////////////////////////////////УДАЛЕНИЕ
            {
                if (tree.GetRoot() != null)
                {
                    Console.WriteLine("Введите узел, который необходимо удалить:");
                    int key = Input.GetInt();
                    tree.Delete( tree, key);
                }
                else 
                {
                    Console.WriteLine("Дерево пустое");
                }
                action = ChoiceAction();
                Action(ref action, temp);
                return action;
            }
            else if (action == WhatToDo.PRINT)/////////////////////////////////////НАРИСОВАТЬ ДЕРЕВО
            {
                tree.Print();
                action = ChoiceAction();
                return Action(ref action,temp);
            }
            else if (action == WhatToDo.SAVERESULT)////////////////////////////////СОХРАНИТЬ РЕЗУЛЬТАТ
            {
                if (tree.GetRoot() != null) 
                {
                    Console.WriteLine("Результат будет сохранен в виде прямой обходки");
                    MenuAnswer toSaveInput = AskForSavingOutput();
                    if (toSaveInput == MenuAnswer.YES)
                    {
                        input.SaveResult(tree.GetRoot());
                    }
                }
                else
                {
                    Console.WriteLine("Дерево пустое");
                }
                action = ChoiceAction();
                Action(ref action, temp);
                return action;
            }
            else //////////////////////////////////////////////////////////////////ЗАВЕРШИТЬ
            {
                return action;
            }
        }
        public void MyProgram()
        {
            Greeting();
            List<int> toFile = new List<int>();
            Menu m = new Menu();
            WhatToDo action = m.ChoiceAction();
            while (m.Action(ref action, toFile) != WhatToDo.COMPLEATE) ;
        }
        public MenuAnswer GetChoice()
        {
            MenuAnswer a = 0;
            while (!MenuAnswer.TryParse(Console.ReadLine(), out a))
            {
                Console.WriteLine("Ошибка ввода! Введите нужное число");
            }
            return a;
        }
        public InputType GetInput()
        {
            InputType a = 0;
            while (!InputType.TryParse(Console.ReadLine(), out a))
            {
                Console.WriteLine("Ошибка ввода! Введите нужный вариант");
            }
            return a;
        }
        public WhatToDo GetAction()
        {
            WhatToDo a = 0;
            while (!WhatToDo.TryParse(Console.ReadLine(), out a))
            {
                Console.WriteLine("Ошибка ввода! Введите нужный вариант");
            }
            return a;
        }
        public void Greeting()
        {
            Console.WriteLine($"Бовчурова Ирина группа 403 {Environment.NewLine} Необходимо реализовать заданную структуру данных, продемонстрировать характерные особенности,{Environment.NewLine} реализовать возможность добавления и удаления элементов,визуализировать красно-черное дерево.В программе должны быть{Environment.NewLine} предусмотрены три варианта заполнения: пользователем с клавиатуры, из файла и случайными числами.{Environment.NewLine}");
        }
        bool  RightMenuChoice(MenuAnswer choice)
        {
            if ((choice == MenuAnswer.NO) || (choice == MenuAnswer.YES))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Ошибка ввода, введите 1 или 2");
                return false;
            }
        }
        bool RightInputChoice(InputType choice)
        {
            if ((choice == InputType.RANDOM) || (choice == InputType.KEYBOARD) || (choice == InputType.FILE))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Ошибка ввода, введите 1 или 2 или 3");
                return false;
            }
        }
        bool RightActionChoice(WhatToDo choice)
        {
            if ((choice == WhatToDo.INSERT) || (choice == WhatToDo.DELETE) || (choice == WhatToDo.PRINT) || (choice == WhatToDo.COMPLEATE) || (choice == WhatToDo.SAVERESULT))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Ошибка ввода, введите 1, 2, 3, 4 или 5");
                return false;
            }
        }
        public MenuAnswer AskForModularTests()
        {
            Console.WriteLine("Желаете провести модульные тесты?");
            Console.WriteLine($" 1 - да {Environment.NewLine} 2 - нет");
            Console.WriteLine("Ваш выбор:");
            MenuAnswer decision = GetChoice();
            while (!RightMenuChoice(decision))
            {
                decision = GetChoice(); ;
            }
            return decision;
        }
        public WhatToDo ChoiceAction()
        {
            Console.WriteLine("Какое действие необходимо выполнить?");
            Console.WriteLine($" 1 - Вставить элемент {Environment.NewLine} 2 - Удалить элемент {Environment.NewLine} 3 - Вывести на экран дерево {Environment.NewLine} 4 - Сохранить результат {Environment.NewLine} 5 - Завершить");
            Console.WriteLine("Ваш выбор:");
            WhatToDo decision = GetAction();
            while (!RightActionChoice(decision))
            {
                decision = GetAction(); ;
            }
            return decision;
        }
        public InputType AskForInput()
        {
            Console.WriteLine("Каким способом вы хотите ввести данные?");
            Console.WriteLine($" 1 - с клавиатуры {Environment.NewLine} 2 - из файла {Environment.NewLine} 3 - заполнить рандомными числами");
            Console.WriteLine("Ваш выбор:");
            InputType decision = GetInput();
            while (!RightInputChoice(decision))
            {
                decision = GetInput(); ;
            }
            return decision;
        }
        public MenuAnswer AskForSavingInput()
        {
            Console.WriteLine("Желаете сохранить исходные данные?");
            Console.WriteLine($" 1 - да {Environment.NewLine} 2 - нет");
            Console.WriteLine("Ваш выбор:");
            MenuAnswer decision = GetChoice();
            while (!RightMenuChoice(decision))
            {
                decision = GetChoice(); ;
            }
            return decision;
        }
        public MenuAnswer AskForSavingOutput()
        {
            Console.WriteLine("Желаете сохранить результат?");
            Console.WriteLine($" 1 - да {Environment.NewLine} 2 - нет");
            Console.WriteLine("Ваш выбор:");
            MenuAnswer decision = GetChoice();
            while (!RightMenuChoice(decision))
            {
                decision = GetChoice(); ;
            }
            return decision;
        }
        public MenuAnswer AskForRewriting()
        {
            Console.WriteLine("Файл уже существует, желаете перезаписать?");
            Console.WriteLine($" 1 - да {Environment.NewLine} 2 - нет");
            Console.WriteLine("Ваш выбор:");
            MenuAnswer decision = GetChoice();
            while (!RightMenuChoice(decision))
            {
                decision = GetChoice(); ;
            }
            return decision;
        }
    }
}