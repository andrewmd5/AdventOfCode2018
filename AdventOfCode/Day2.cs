using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day2
    {
        public static void Solve()
        {
            Console.WriteLine($"Solving Day 2...");
            var lines = File.ReadAllLines("E:\\Dev\\AdventOfCode2018\\AdventOfCode\\day-2.txt");
            var twoIndex = 0;
            var threeIndex = 0;
            foreach (var line in lines)
            {

                //https://i.imgur.com/u7rpl9M.png
                var tempDic = new Dictionary<char, byte>();
                foreach (var t in line)
                {
                    if (tempDic.ContainsKey(t))
                    {
                        tempDic[t] += 1;

                    }
                    else
                    {
                        tempDic[t] = 1;
                    }
                }
                if ((tempDic.Values.ToList().Select(x => x).Count(x => x == 2) > 0))
                {
                    twoIndex += 1;
                }
                if ((tempDic.Values.ToList().Select(x => x).Count(x => x == 3) > 0))
                {
                    threeIndex += 1;
                }
            }
            var checkSum = twoIndex * threeIndex;
            Console.WriteLine($"Checksum: {checkSum}");

            //find the correct boxes
            foreach (var line in lines)
            {
                foreach (var subLine in lines)
                {
                    var differCount = 0;
                    var differentIndex = 0;
                    for (var i = 0; i < line.Length; i++)
                    {
                        if (subLine[i] == line[i]) continue;
                        if (++differCount > 1)
                        {
                            break;
                        }
                        differentIndex = i;
                    }
                    if (differCount == 1)
                    {
                        Console.WriteLine($"Correct ID: { line.Remove(differentIndex, 1)}");
                        Console.WriteLine($"Day 2 solved!");
                        return;
                    }
                }
            }

        }
    }
}
