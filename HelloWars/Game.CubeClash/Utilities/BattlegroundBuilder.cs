using System;
using Game.AntWars.Enums;
using Game.AntWars.Models;
using Game.AntWars.ViewModels;

namespace Game.AntWars.Utilities
{
    public static class BattlegroundBuilder
    {
        //if you change this please change centerX in UserControls using RotationTransform to 5 (10/2) 
        private const int BATTLEFIELD_UNIT_HEIGTH = 10;
        private const int BATTLEFIELD_UNIT_WIDTH = 10;
        private static Random _rand = new Random(DateTime.Now.Millisecond);

        private static AntWarsViewModel _antWarsViewModel;

        public static void CreateBattleground(int rows, int collumns, AntWarsViewModel antWarsViewModel)
        {
            _antWarsViewModel = antWarsViewModel;

            _antWarsViewModel.RowCount = rows;
            _antWarsViewModel.ColumnCount = collumns;
            _antWarsViewModel.BattlegroundWidth = _antWarsViewModel.RowCount * BATTLEFIELD_UNIT_HEIGTH;
            _antWarsViewModel.BattlegroundHeigth = _antWarsViewModel.ColumnCount * BATTLEFIELD_UNIT_WIDTH;

            InitiateBattlefield();
        }

        private static void InitiateBattlefield()
        {
            for (int i = 0; i < _antWarsViewModel.RowCount; i++)
            {
                for (int j = 0; j < _antWarsViewModel.ColumnCount; j++)
                {
                    var gridUnit = AddLawnUnit(i, j);

                    _antWarsViewModel.BattlefieldObjectsCollection.Add(gridUnit);

                    var obsticles = AddObsticlesWithProbability(i, j);

                    if (obsticles.Type != UnmovableObjectTypes.Lawn)
                    {
                        _antWarsViewModel.BattlefieldObjectsCollection.Add(obsticles);
                    }
                }
            }
        }

        private static GridUnitModel AddLawnUnit(int i, int j)
        {
            var gridUnit = new GridUnitModel(new GridUnitViewModel())
            {
                X = i,
                Y = j,
                Type = UnmovableObjectTypes.Lawn
            };

            return gridUnit;
        }

        private static GridUnitModel AddObsticlesWithProbability(int i, int j)
        {
            var gridUnit = new GridUnitModel(new GridUnitViewModel())
            {
                X = i,
                Y = j,
                Type = ReturnWithSomeProbability()
            };

            return gridUnit;
        }

        private static UnmovableObjectTypes ReturnWithSomeProbability()
        {
            var probability = _rand.NextDouble();

            if (probability < 0.15)
            {
                return UnmovableObjectTypes.Wood;
            }
            if (probability < 0.3)
            {
                return UnmovableObjectTypes.Rock;
            }

            return UnmovableObjectTypes.Lawn;
        }

        internal static void ReturnDefaultLogo()
        {
            throw new NotImplementedException();
        }
    }
}
