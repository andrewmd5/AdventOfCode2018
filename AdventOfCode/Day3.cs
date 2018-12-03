using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    internal class Claim
    {
        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Rectangle Rect { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Width/Height: {Width}x{Height}, Coords: {X}x{Y}";
        }

        public static Claim Parse(string input)
        {
            var claim = new Claim();
            claim.Id = int.Parse(input.Substring(1, input.IndexOf('@') - 1).Trim());
            var coords = input.Substring(input.IndexOf('@') + 1, input.IndexOf(':') - input.IndexOf('@') - 1).Trim()
                .Split(',');
            claim.X = int.Parse(coords[0]);
            claim.Y = int.Parse(coords[1]);
            var size = input.Substring(input.IndexOf(':') + 1).Split('x');
            claim.Width = int.Parse(size[0]);
            claim.Height = int.Parse(size[1]);
            claim.Rect = new Rectangle(claim.X, claim.Y, claim.Width, claim.Height);
            return claim;
        }
    }


    public class Day3
    {
        public static void Solve()
        {
            Console.WriteLine("Solving Day 3...");
            var claims = File.ReadAllLines("E:\\Dev\\AdventOfCode2018\\AdventOfCode\\day-3.txt").Where(c => !string.IsNullOrWhiteSpace(c)).Select(Claim.Parse).ToList();
            var grid = new Dictionary<int, Dictionary<int, int>>();
            foreach (var claim in claims)
            {
                for (var x = claim.X; x < claim.X + claim.Width; ++x)
                {
                    for (var y = claim.Y; y < claim.Y + claim.Height; ++y)
                    {
                        if (!grid.TryGetValue(x, out var gridDictY))
                        {
                            gridDictY = new Dictionary<int, int>();
                            grid[x] = gridDictY;
                        }
                        if (!gridDictY.TryGetValue(y, out var gridAtLocation))
                        {
                            gridAtLocation = 0;
                        }
                        gridDictY[y] = ++gridAtLocation;
                    }
                }
            }
            var collisions = 0;
            for (var x = 0; x < 1000; ++x)
            {
                for (var y = 0; y < 1000; ++y)
                {
                    if (!grid.TryGetValue(x, out var gridDictY)) continue;
                    if (!gridDictY.TryGetValue(y, out var gridAtLocation)) continue;
                    if (gridAtLocation > 1)
                    {
                        collisions++;
                    }
                }
            }
            Console.WriteLine($"Total collisions: {collisions}");

            foreach (var claim in claims)
            {
                var isUnclaimed = true;

                for (var x = claim.X; x < claim.X + claim.Width; ++x)
                {
                    for (var y = claim.Y; y < claim.Y + claim.Height; ++y)
                    {
                        if (!grid.TryGetValue(x, out var gridDictY)) continue;
                        if (!gridDictY.TryGetValue(y, out var gridAtLocation)) continue;
                        if (gridAtLocation <= 1) continue;
                        isUnclaimed = false;
                        break;
                    }
                }
                if (isUnclaimed)
                {
                    Console.WriteLine($"Santa suit fabric ID: {claim.Id}");
                    Console.WriteLine("Day 3 solved!");
                    break;
                }

            }

        }
    }
}