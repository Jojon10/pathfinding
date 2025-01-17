﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace pathfinding
{
    public class AStar : PathFindAlgorithm
    {

        public AStar(Tilemap tilemap) : base(tilemap) { }

        protected override bool SearchPath(Spot start, Spot end)
        {
            
            List<Spot> OpenSet = new List<Spot>();
            List<Spot> ClosedSet = new List<Spot>();

            OpenSet.Add(start);
            Iterations.Add(new List<Spot> { start }); // nur für Visualisierung

            while (OpenSet.Count > 0)
            {
                List<Spot> iteration = new List<Spot>(); // nur für Visualisierung

                //Find shortest step distance in the direction of your goal within the open set
                int winner = 0;
                for (int i = 0; i < OpenSet.Count; i++)
                    if (OpenSet[i].F < OpenSet[winner].F)
                        winner = i;
                    else if (OpenSet[i].F == OpenSet[winner].F && OpenSet[i].H < OpenSet[winner].H) //tie breaking for faster routing
                            winner = i;

                var current = OpenSet[winner];

                //Found the path, creates and returns the path
                if (OpenSet[winner].Equals(end))
                {
                    Iterations.Add(new List<Spot> { end }); // nur für Visualisierung
                    return true;
                }

                OpenSet.Remove(current);
                ClosedSet.Add(current);


                //Finds the next closest step on the grid
                var neighbors = current.Neighbors;
                for (int i = 0; i < neighbors.Count; i++)//look through our current spots neighbors (current spot is the shortest F distance in openSet)
                {
                    var n = neighbors[i];
                    if (!ClosedSet.Contains(n) && n.IsWalkable)//Checks to make sure the neighbor of our current tile is not within closed set, and has a height of less than 1
                    {
                        iteration.Add(n); // nur für Visualisierung

                        var tempG = current.G + n.Cost;//gets a temp comparison integer for seeing if a route is shorter than our current path

                        bool newPath = false;
                        if (OpenSet.Contains(n)) //Checks if the neighboor we are checking is within the openset
                        {
                            if (tempG < n.G)//The distance to the end goal from this neighboor is shorter so we need a new path
                            {
                                n.G = tempG;
                                newPath = true;
                            }
                        }
                        else//if its not in openSet or closed set, then it IS a new path and we should add it too openset
                        {
                            n.G = tempG;
                            newPath = true;
                            OpenSet.Add(n);
                        }
                        if (newPath)//if it is a newPath caclulate the H and F and set current to the neighboors previous
                        {
                            n.H = Heuristic(n, end);
                            n.Previous = current;
                        }
                    }
                }

                Iterations.Add(iteration);

            }

            Debug.Log("Keinen Pfad gefunden!");
            return false;
        }

        private int Heuristic(Spot a, Spot b)
        {
            // manhattan (schlechte heuristik)
            // var dx = Math.Abs(a.X - b.X);
            // var dy = Math.Abs(a.Y - b.Y);
            // return 1 * (dx + dy);

            var dx = Math.Abs(a.X - b.X);
            var dy = Math.Abs(a.Y - b.Y);
            return Math.Max(dx, dy); // nicht perfekt funktioniert aber
        }
    }
}