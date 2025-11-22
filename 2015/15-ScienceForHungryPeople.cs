using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class ScienceForHungryPeople
    {
        public static void SolvePart1()
        {
            var sprinkles = new Ingredient("Sprinkles: capacity 2, durability 0, flavor -2, texture 0, calories 3");
            var butterscotch = new Ingredient("Butterscotch: capacity 0, durability 5, flavor -3, texture 0, calories 3");
            var chocolate = new Ingredient("Chocolate: capacity 0, durability 0, flavor 5, texture -1, calories 8");
            var candy = new Ingredient("Candy: capacity 0, durability -1, flavor 0, texture 5, calories 8");
            var best = 0L;
            for (var sprinklesQuantity=0;sprinklesQuantity<=100;sprinklesQuantity++)
                for (var butterscotchQuantity=0;butterscotchQuantity<=100-sprinklesQuantity;butterscotchQuantity++)
                    for (var chocolateQuantity=0;chocolateQuantity<=100-sprinklesQuantity-butterscotchQuantity;chocolateQuantity++)
                    {
                        var candyQuantity = 100-sprinklesQuantity-butterscotchQuantity-chocolateQuantity;
                        var capacity = Math.Max(0,sprinkles.Capacity*sprinklesQuantity + butterscotch.Capacity*butterscotchQuantity + chocolate.Capacity*chocolateQuantity + candy.Capacity*candyQuantity);
                        var durability = Math.Max(0,sprinkles.Durability*sprinklesQuantity + butterscotch.Durability*butterscotchQuantity + chocolate.Durability*chocolateQuantity + candy.Durability*candyQuantity);
                        var flavor = Math.Max(0,sprinkles.Flavor*sprinklesQuantity + butterscotch.Flavor*butterscotchQuantity + chocolate.Flavor*chocolateQuantity + candy.Flavor*candyQuantity);
                        var texture = Math.Max(0,sprinkles.Texture*sprinklesQuantity + butterscotch.Texture*butterscotchQuantity + chocolate.Texture*chocolateQuantity + candy.Texture*candyQuantity);
                        var score = capacity*durability*flavor*texture;
                        best = Math.Max(best, score);
                    }

            Console.WriteLine($"Part 1: The best cookie has a score of {best}");            
        }

        public static void SolvePart2()
        {
            var sprinkles = new Ingredient("Sprinkles: capacity 2, durability 0, flavor -2, texture 0, calories 3");
            var butterscotch = new Ingredient("Butterscotch: capacity 0, durability 5, flavor -3, texture 0, calories 3");
            var chocolate = new Ingredient("Chocolate: capacity 0, durability 0, flavor 5, texture -1, calories 8");
            var candy = new Ingredient("Candy: capacity 0, durability -1, flavor 0, texture 5, calories 8");
            var best = 0L;
            for (var sprinklesQuantity=0;sprinklesQuantity<=100;sprinklesQuantity++)
                for (var butterscotchQuantity=0;butterscotchQuantity<=100-sprinklesQuantity;butterscotchQuantity++)
                    for (var chocolateQuantity=0;chocolateQuantity<=100-sprinklesQuantity-butterscotchQuantity;chocolateQuantity++)
                    {
                        var candyQuantity = 100-sprinklesQuantity-butterscotchQuantity-chocolateQuantity;
                        var calories = sprinkles.Calories*sprinklesQuantity + butterscotch.Calories*butterscotchQuantity + chocolate.Calories*chocolateQuantity + candy.Calories*candyQuantity;
                        if (calories != 500) continue;
                        var capacity = Math.Max(0,sprinkles.Capacity*sprinklesQuantity + butterscotch.Capacity*butterscotchQuantity + chocolate.Capacity*chocolateQuantity + candy.Capacity*candyQuantity);
                        var durability = Math.Max(0,sprinkles.Durability*sprinklesQuantity + butterscotch.Durability*butterscotchQuantity + chocolate.Durability*chocolateQuantity + candy.Durability*candyQuantity);
                        var flavor = Math.Max(0,sprinkles.Flavor*sprinklesQuantity + butterscotch.Flavor*butterscotchQuantity + chocolate.Flavor*chocolateQuantity + candy.Flavor*candyQuantity);
                        var texture = Math.Max(0,sprinkles.Texture*sprinklesQuantity + butterscotch.Texture*butterscotchQuantity + chocolate.Texture*chocolateQuantity + candy.Texture*candyQuantity);
                        var score = capacity*durability*flavor*texture;
                        best = Math.Max(best, score);
                    }

            Console.WriteLine($"Part 1: The best cookie with 500 calories has a score of {best}");            
        }


        public class Ingredient
        {
            public string Name;
            public long Capacity;
            public long Durability;
            public long Flavor;
            public long Texture;
            public long Calories;

            public Ingredient(string input)
            {
                var data = input.Replace(":", "").Replace(",", "").Split(' ');
                Name = data[0];
                Capacity = int.Parse(data[2]);
                Durability = int.Parse(data[4]);
                Flavor = int.Parse(data[6]);
                Texture = int.Parse(data[8]);
                Calories = int.Parse(data[10]);
            }
        }    
    }
}
