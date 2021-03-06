﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2018
{
    public class Vehicle
    {
        #region Public Properties

        public int Id { get; set; }

        #endregion Public Properties

        #region Public Constructors

        public Vehicle(int maxSteps, int id)
        {
            Position = new Location { Column = 0, Row = 0 };
            _maxSteps = maxSteps;
            _id = id;
        }

        #endregion Public Constructors



        #region Public Properties

        public Location Position { get; private set; }
        public int CompliteSteps { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public List<Ride> RidesList { get; set; } = new List<Ride>();

        public bool CanDoIt(Ride ride)
        {
            var pasosACompletar = ((ride.From - ride.To) + (this.Position - ride.From));
            var llegaATiempo = (ride.LatestFinish - 1) >= pasosACompletar + CompliteSteps;
            var restSteps = _maxSteps - CompliteSteps - ((ride.From - ride.To) + (this.Position - ride.From));
            return llegaATiempo && restSteps > 0 && (_maxSteps - CompliteSteps) >= restSteps;
        }

        public void RideGo(List<Ride> rides)
        {
            foreach (var ride in rides.ToList())
            {
                while (this.CompliteSteps < ride.EarliestStart)
                {
                    this.CompliteSteps++;
                    this.Position = ride.From;
                }
                if (!CanDoIt(ride))
                    continue;

                CompliteSteps += (ride.From - ride.To) + (this.Position - ride.From);
                Position = ride.To;
                RidesList.Add(ride);
                rides.Remove(ride);
            }
        }

        #endregion Public Methods

        #region Private Fields

        public override string ToString()
        {
            string[] a = RidesList.Select(x => x.Id.ToString()).ToArray();
            string b = string.Join(" ", a);
            return $"{_id} {b}";
        }

        private readonly int _id;
        private int _maxSteps;

        #endregion Private Fields
    }
}