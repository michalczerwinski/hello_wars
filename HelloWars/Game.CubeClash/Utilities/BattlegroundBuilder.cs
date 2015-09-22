using System;
using Game.CubeClash.Enums;
using Game.CubeClash.Models;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash.Utilities
{
    public static class BattlegroundBuilder
    {
        //if you change this please change centerX in UserControls using RotationTransform for 5 (10/2) 
        private const int BATTLEFIELD_UNIT_HEIGTH = 10;
        private const int BATTLEFIELD_UNIT_WIDTH = 10;
        private static Random _rand = new Random(DateTime.Now.Millisecond);

        private static CubeClashViewModel _cubeClashViewModel;

        public static void CreateBattleground(int rows, int collumns, CubeClashViewModel cubeClashViewModel)
        {
            _cubeClashViewModel = cubeClashViewModel;

            _cubeClashViewModel.RowCount = rows;
            _cubeClashViewModel.ColumnCount = collumns;
            _cubeClashViewModel.BattlegroundWidth = _cubeClashViewModel.RowCount * BATTLEFIELD_UNIT_HEIGTH;
            _cubeClashViewModel.BattlegroundHeigth = _cubeClashViewModel.ColumnCount * BATTLEFIELD_UNIT_WIDTH;

            InitiateBattlefield();
        }

        private static void InitiateBattlefield()
        {
            for (int i = 0; i < _cubeClashViewModel.RowCount; i++)
            {
                for (int j = 0; j < _cubeClashViewModel.ColumnCount; j++)
                {
                    var gridUnit = new GridUnitModel(new GridUnitViewModel())
                    {
                        X = i,
                        Y = j,
                        Type = ReturnWithSomeProbability()
                    };

                    _cubeClashViewModel.BattlefieldObjectsCollection.Add(gridUnit);
                }
            }
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
    }
}
