using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SweeftDigital
{
  internal class Program
  {
    //1
    static bool isPalindrom(string text)
    {
      text = text.ToLower();

      for (int i = 0; i < (text.Length / 2); i++)
      {
        if (text[i] != text[(text.Length - 1) - i])
          return false;
      }

      return true;
    }

    //2
    static int minSplit(int amount)
    {
      int count = 0, temp;
      int[] coins = new int[] { 1, 5, 10, 20, 50 };

      for (int i = coins.Length - 1; i >= 0; i--)
      {
        temp = amount / coins[i];
        if (temp != 0)
        {
          count += temp;
          amount -= temp * coins[i];
          if (amount == 0) { break; }
        }

      }

      return count;
    }

    //
    static int notContains(int[] array)
    {
      bool check = false;

      for (int i = 1; i < array.Max(); i++)
      {
        for (int j = 0; j < array.Length; j++)
        {
          if (i == array[j])
          {
            check = true;
            break;
          }
        }
        if (!check) { return i; }

        check = false;
      }

      return -1;
    }

    //4
    static bool isProperly(string sequence)
    {
      int countLeft = 0, countRight = 0;
      char left = '(', right = ')';

      if (sequence[0] == right || sequence[sequence.Length - 1] == left || sequence.Length % 2 != 0)      //sequence == "" || sequence == null ||
        return false;

      for (int i = 1; i < sequence.Length - 1; i++)
      {
        if (sequence[i] == left)
          countLeft++;
        else if (sequence[i] == right)
          countRight++;
      }

      return countLeft == countRight;
    }

    //5
    static private int Counter { get; set; }

    static int countVariants(int stearsCount)
    {
      if (stearsCount < 0) return -1;

      if (stearsCount - 1 > 0)
        countVariants(stearsCount - 1);
      else if (stearsCount - 1 == 0)
        return ++Counter;

      if (stearsCount - 2 > 0)
        countVariants(stearsCount - 2);
      else if (stearsCount - 2 == 0)
        return ++Counter;


      return Counter;
    }

    static void Main(string[] args)
    {

      //// 1. Boolean isPalindrome(String text);

      //string name = "Hannah";
      //if (isPalindrom(name))
      //  Console.WriteLine($"Palindrome is \"{name}\".");
      //else
      //  Console.WriteLine($"Palindrome is not \"{name}\".");


      /* 
       * 2. გვაქვს 1,5,10,20 და 50 თეთრიანი მონეტები. დაწერეთ ფუნქცია, რომელსაც გადაეცემა თანხა (თეთრებში)
       *   და აბრუნებს მონეტების მინიმალურ რაოდენობას, რომლითაც შეგვიძლია ეს თანხა დავახურდაოთ. Int minSplit(Int amount);
       */

      //Console.WriteLine($"Number of coins: {minSplit(121)}.");  //4


      /*
       * 3. მოცემულია მასივი, რომელიც შედგება მთელი რიცხვებისგან. დაწერეთ ფუნქცია რომელსაც გადაეცემა ეს მასივი
       *   და აბრუნებს მინიმალურ მთელ რიცხვს, რომელიც 0-ზე მეტია და ამ მასივში არ შედის. Int notContains(Int[] array);
       */

      //int[] numbers = { 9, -5, -2, 1, 3, 4, 8, 10 };  //2
      //Console.WriteLine(notContains(numbers));


      /*
       * 4. მოცემულია String რომელიც შედგება „(„ და „)“ ელემენტებისგან. დაწერეთ ფუნქცია რომელიც აბრუნებს 
       *   ფრჩხილები არის თუ არა მათემატიკურად სწორად დასმული. Boolean isProperly(String sequence);
       *   მაგ: (()()) სწორი მიმდევრობაა,  ())() არასწორია        
       */

      //string sequence = "(()())";      // false: ()))(()  )(   ()))()     

      //if (isProperly(sequence))
      //  Console.WriteLine($"\"{sequence}\" is true.");
      //else
      //  Console.WriteLine($"\"{sequence}\" is false.");


      /*
       * 5. გვაქვს n სართულიანი კიბე, ერთ მოქმედებაში შეგვიძლია ავიდეთ 1 ან 2 საფეხურით.
       *   დაწერეთ ფუნქცია რომელიც დაითვლის n სართულზე ასვლის ვარიანტების რაოდენობას.
       *   Int countVariants(Int stearsCount);
      */

      //int stears = 5;  //5 => 8
      //Console.WriteLine($"Variants: {countVariants(stears)}");


      /*
       *  იხილეთ "MyHashTable" კლასი.
       * 6. დაწერეთ საკუთარი მონაცემთა სტრუქტურა, რომელიც საშუალებას მოგვცემს O(1) დროში წავშალოთ ელემენტი.
       */

      //MyHashTable<string, string> myHashTable = new MyHashTable<string, string>(20);

      //myHashTable.Add("1", "SweeftDigital");
      //Console.WriteLine(myHashTable.Find("1"));

      //myHashTable.Remove("1");
      //string text = myHashTable.Find("1");
      //if (text != null)
      //  Console.WriteLine(text);
      //else
      //  Console.WriteLine("Not found");


      /*
       * 7. იხილეთ "Task_7" საქაღალდეში.
       */


      /*
       *  იხილეთ "MyExchangeRate" კლასი.
       * 8. მოცემულია საქართველოს ეროვნული ბანკის RSS feed-ის მისამართი: http://www.nbg.ge/rss.php , 
       *   რომელიც არბუნებს მიმდინარე ვალუტის კურსებს. დაწერეთ ფუნქცია, რომელსაც გადაეცემა ორი ვალუტის 
       *   იდენტიფიკატორი(USD, GEL, EUR…) და აბრუნებს ვალუტებს შორის გაცვლის კურსს. 
       *   Double exchangeRate (String from, String to);
       */

      //double result = MyExchangeRate.exchangeRate("USD", "AED");
      //Console.WriteLine($"{MyExchangeRate.QuantityInfo} {MyExchangeRate.NameInfoFrom} = {result} {MyExchangeRate.NameInfoTo}");

    }

  }
}
