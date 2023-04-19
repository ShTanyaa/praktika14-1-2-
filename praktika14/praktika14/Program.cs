using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.IO;

namespace praktika14
{
    class Program
    {
        static void zad1(int n)
        {
            Stack<int> stak = new Stack<int>();
            for (int i = 1; i <= n; i++)
            {
                stak.Push(i);
            }
            Console.WriteLine($"размерность стека {stak.Count()}");
            Console.WriteLine($"верхний элемент стека {stak.Peek()}");
            Console.WriteLine($"содержимое стека: ");
            while (stak.Count > 0)
            {
                Console.WriteLine($"{stak.Pop()}");
            }
            stak.Clear();
            Console.WriteLine($"новая расмерность стека {stak.Count()}");
        }

        static string zad2_a(string expression_get)
        {
            StreamWriter wr = new StreamWriter("text.txt");
            wr.Write(expression_get);
            wr.Close();
            Console.WriteLine("записано в файл");
            string expression_set = File.ReadAllText("text.txt");
            Stack<char> stak = new Stack<char>();
            bool balance = true;
            int i1 = 0;
            while(i1<expression_set.Length&&balance)
            {
                char ch = expression_set[i1];
                if (ch == '(') stak.Push(ch);
                else if(ch==')')
                {
                    if (stak.Count == 0) balance = false;
                    else stak.Pop();
                }
                i1++;
            }
            if (balance && stak.Count == 0) return "скобки сбалансированы";
            else if (stak.Count == 0) return $"возможно лишняя скобка в позиции {i1}";
            else return $"возможна лишняя скобка в позиции {expression_set.Length - stak.Count}";
        }

        static string zad2_b(string expression)
        {
            Stack<char> stack1 = new Stack<char>();

            for (int i = 0; i < expression.Length; i++)
            {
                char ch = expression[i];
                if (ch == '(') { stack1.Push(ch); }
                else if (ch == ')')
                {
                    if (stack1.Count > 0 && stack1.Peek() == '(') { stack1.Pop(); }
                    else { stack1.Push(ch); }
                }
            }
            while (expression.Length > 0 && expression[0] == ')')
            {
                expression = expression.Remove(0, 1);
            }
            while (expression.Length > 0 && expression[expression.Length - 1] == '(')
            {
                expression = expression.Remove(expression.Length - 1, 1);
            }
            while (stack1.Count > 0)
            {
                char ch = stack1.Pop();
                if (ch == '(') { expression += ')'; }
                else if (ch == ')') { expression = expression.Remove(expression.LastIndexOf(')'), 1); }
            }

            File.WriteAllText("text.txt", expression);
            return $"Новое выражение: {expression} - записано в файл.";
        }

        static void Main(string[] args)
        {
          
            //1
            try
            {
                Console.WriteLine("введите кол-во элементов");
                int n = int.Parse(Console.ReadLine());
                zad1(n);
            }
            catch  { Console.WriteLine("error"); }

            //2a
            Console.WriteLine("Введите математическое выражение: ");
            string expressionGet = Console.ReadLine();
            Console.WriteLine(zad2_a(expressionGet));
            // 2b
            string expression = File.ReadAllText("text.txt");
            Console.WriteLine(zad2_b(expression));


            Console.ReadKey();
        }
    }
}
