using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_11
{
    class Program
    {
        static void Main(string[] args)
        {
            var seatMap = System.IO.File.ReadAllLines(@$"input.txt").ToList();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            CreateChaos(seatMap);
            stopWatch.Stop();
            Console.WriteLine($@"{stopWatch.Elapsed}");


            stopWatch = new Stopwatch();
            stopWatch.Start();
            CreateChaos2(seatMap);
            stopWatch.Stop();
            Console.WriteLine($@"{stopWatch.Elapsed}");
        }

        static void CreateChaos(List<string> seatMap)
        {
            var maxRow = seatMap.Count() - 1;
            var maxCol = seatMap[0].Length - 1;
            var mapState = seatMap.ToList();
            var currentMap = mapState.ToList();
            var stateChange = true;

            while (stateChange == true)
            {
                stateChange = false;

                for (var currentRow = 0; currentRow <= maxRow; currentRow++)
                {
                    for (var currentCol = 0; currentCol <= maxCol; currentCol++)
                    {
                        if (currentMap[currentRow][currentCol] == '.') { continue; }
                        else
                        {
                            var seatState = currentMap[currentRow][currentCol];
                            seatState = ChangeState(mapState, currentRow, currentCol, maxRow, maxCol);
                            if (seatState != currentMap[currentRow][currentCol])
                            {
                                stateChange = true;
                                currentMap[currentRow] = currentMap[currentRow].Remove(currentCol, 1).Insert(currentCol, seatState.ToString());
                            }
                        }
                    }
                }

                mapState = currentMap.ToList();
            }

            var collapsed = string.Join("", mapState.Select(row => new string(row)));

            Console.WriteLine($@"{collapsed.Where(c => c == '#').Count()}");
        }

        static char ChangeState(List<string> mapState, int currentRow, int currentCol, int maxRow, int maxCol)
        {
            var seatState = mapState[currentRow][currentCol];
            var occupiedCount = 0;

            for (var checkRow = Math.Max(currentRow-1,0); checkRow <= Math.Min(currentRow+1,maxRow); checkRow++)
            {
                for (var checkCol = Math.Max(currentCol-1,0); checkCol <= Math.Min(currentCol+1,maxCol); checkCol++)
                {
                    if ((checkRow != currentRow || checkCol != currentCol) && mapState[checkRow][checkCol] == '#')
                    {
                        occupiedCount++;
                    }
                }
            }

            if (seatState == 'L' && occupiedCount == 0)
            {
                return '#';
            }
            else if (seatState == '#' && occupiedCount >= 4)
            {
                return 'L';
            }

            return seatState;
        }

        static void CreateChaos2(List<string> seatMap)
        {
            var maxRow = seatMap.Count() - 1;
            var maxCol = seatMap[0].Length - 1;
            var mapState = seatMap.ToList();
            var currentMap = mapState.ToList();
            var stateChange = true;

            while (stateChange == true)
            {
                stateChange = false;

                for (var currentRow = 0; currentRow <= maxRow; currentRow++)
                {
                    for (var currentCol = 0; currentCol <= maxCol; currentCol++)
                    {
                        if (currentMap[currentRow][currentCol] == '.') { continue; }
                        else
                        {
                            var seatState = currentMap[currentRow][currentCol];
                            seatState = ChangeState2(mapState, currentRow, currentCol, maxRow, maxCol);
                            if (seatState != currentMap[currentRow][currentCol])
                            {
                                stateChange = true;
                                currentMap[currentRow] = currentMap[currentRow].Remove(currentCol, 1).Insert(currentCol, seatState.ToString());
                            }
                        }
                    }
                }

                mapState = currentMap.ToList();
            }

            var collapsed = string.Join("", mapState.Select(row => new string(row)));

            Console.WriteLine($@"{collapsed.Where(c => c == '#').Count()}");
        }

        static char ChangeState2(List<string> mapState, int currentRow, int currentCol, int maxRow, int maxCol)
        {
            var seatState = mapState[currentRow][currentCol];
            int occupiedCount = 0, multiplier = 0;
            bool foundSeat = false;

            for (var checkRow = Math.Max(currentRow-1,0); checkRow <= Math.Min(currentRow+1,maxRow); checkRow++)
            {
                for (var checkCol = Math.Max(currentCol-1,0); checkCol <= Math.Min(currentCol+1,maxCol); checkCol++)
                {
                    if (checkRow == currentRow && checkCol == currentCol) { continue; }

                    var rowDiff = checkRow - currentRow;
                    var colDiff = checkCol - currentCol;

                    multiplier = 1;
                    foundSeat = false;
                    while (foundSeat == false)
                    {
                        var adjustedRow = currentRow+(rowDiff*multiplier);
                        var adjustedCol = currentCol+(colDiff*multiplier);
                        if (adjustedRow >= 0 && adjustedCol >= 0 && adjustedRow <= maxRow && adjustedCol <= maxCol)
                        {
                            if (mapState[adjustedRow][adjustedCol] != '.')
                            {
                                foundSeat = true;
                                if (mapState[adjustedRow][adjustedCol] == '#')
                                {
                                    occupiedCount++;
                                }
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }

                        multiplier++;
                    }
                }
            }

            if (seatState == 'L' && occupiedCount == 0)
            {
                return '#';
            }
            else if (seatState == '#' && occupiedCount >= 5)
            {
                return 'L';
            }

            return seatState;
        }
    }
}
