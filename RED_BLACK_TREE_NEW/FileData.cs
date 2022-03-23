using System;
using System.Collections.Generic;
using System.IO;

namespace Red_BlackTree
{
    class FileData
    {
        public bool FileInput(RB tree, List<int> temp)
        {
            string filePath = GetPath();
            string line;
            int number;
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    int i = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        try
                        {
                            number = Convert.ToInt32(line);
                            temp.Add(number);
                            tree.Insert(number);
                        }
                        catch
                        {
                            Console.WriteLine("ошибка данных");
                            return false;
                        }
                        i++; 
                        Console.WriteLine( i + " число: " + number);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Проверьте путь к файлу");
                return false;
            }
            return true;
        }
        public string GetPath()
        {
            Console.WriteLine("Введите путь к файлу:");
            string path = Console.ReadLine();
            string filePath;
            try
            {
                filePath = Path.GetFullPath(path);
            }
            catch
            {
                Console.WriteLine("Проверьте путь");
                filePath = GetPath();
            }
            return path;
        }
        public static void PreOrder(Node root, string path, List<int> toFile) //breadth-first output to file
        {
            if (!root.isLeaf)
            {
                toFile.Add(root.data);
                PreOrder(root.left, path, toFile);
                PreOrder(root.right, path, toFile);
            }
        }
        public  string CreateFile()
        {
            string path = "";
            bool isSucced = false;
            while (!isSucced)
            {
                path = GetPath();
                FileStream fStream = null;
                try
                {
                    fStream = new FileStream(path, FileMode.CreateNew);
                    isSucced = true;
                }
                catch
                {
                    if (File.Exists(path))
                    {
                        Menu m = new Menu();
                        MenuAnswer rewrite = m.AskForRewriting();
                        if (rewrite == MenuAnswer.YES)
                        {
                            File.Delete(path);
                            fStream = new FileStream(path, FileMode.CreateNew);
                            isSucced = true;
                        }
                        else
                        {
                            isSucced = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некорректный файл или недопустимое имя файла");
                        isSucced = false;
                    }
                }
                if (fStream != null) try { fStream.Close(); } catch (Exception) { Console.WriteLine("Stream not closed!"); isSucced = false; }
            }
            return path;
        }
        public  void SaveInput(List<int> list)
        {
            string path = CreateFile();
            File.WriteAllLines(path, list.ConvertAll(x => x.ToString()));
        }
        public  void SaveResult(Node root)
        {
            string path = CreateFile();
            List<int> toFile = new List<int>();
            PreOrder(root, path, toFile);
            string text = "";
            foreach (int item in toFile)
            {
                text += item.ToString() + " ";
            }
            File.WriteAllText(path, "Дерево в прямой обходке: " + text);
        }
    }
}

