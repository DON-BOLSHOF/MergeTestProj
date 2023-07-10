using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class MatrixUtils<T> where T : class
    {
        public static T FindObjectWithCondition(List<List<T>> matrix, Func<T, bool> condition)
        {
            T foundObject = null;

            foreach (var rows in matrix.Where(_ => foundObject == null))
            {
                foundObject =
                    rows.Find(slot => condition(slot));
            }

            return foundObject;
        }

        public static bool TryFindObjectWithCondition(List<List<T>> matrix, Func<T, bool> condition, out T result)
        {
            result = FindObjectWithCondition(matrix, condition);

            return result != null;
        }

        public static Vector2Int? FindIndexWithCondition(List<List<T>> matrix, Func<T, bool> condition)
        {
            for (int i = 0; i < matrix.Count; i++)
            for (int j = 0; j < matrix[0].Count; j++)
            {
                if (condition(matrix[i][j]))
                    return new Vector2Int(i, j);
            }

            return null;
        }
        
        public static bool TryFindIndexWithCondition(List<List<T>> matrix, Func<T, bool> condition, out Vector2Int position)
        {
            var pos = FindIndexWithCondition(matrix, condition);
            if (pos == null)
            {
                position = Vector2Int.zero;
                return false;
            }

            position = (Vector2Int)pos;
            return true;
        }

        public static bool IsPositionInMatrix(List<List<T>> matrix, Vector2Int position)
        {
            return position.x >= 0 && position.y >= 0 && position.x < matrix.Count
                   && position.y < matrix[0].Count;
        }

        public static Vector2Int? FindIndexOfObject(List<List<T>> matrix, T Object)
        {
            for (int i = 0; i < matrix.Count; i++)
            for (int j = 0; j < matrix[0].Count; j++)
            {
                if (Object.Equals(matrix[i][j]))
                    return new Vector2Int(i, j);
            }

            return null;
        }

        public static bool TryFindIndexOfObject(List<List<T>> matrix, T Object, out Vector2Int position)
        {
            var pos = FindIndexOfObject(matrix, Object);
            if (pos == null)
            {
                position = Vector2Int.zero;
                return false;
            }

            position = (Vector2Int)pos;
            return true;
        }

        public static T FindObjectSpyrallyByPosition(List<List<T>> matrix, Vector2Int position, Func<T, bool> condition)
        {
            if (matrix.Count <= position.x || matrix[0].Count <= position.y)
                throw new ArgumentException("Position is out of bounds");

            T result = null;

            var upThreashold = position.x;
            var leftThreashold = position.y;
            var bottomThreashold = matrix.Count - upThreashold;
            var rightThreashold = matrix.Count - leftThreashold;

            var spyralCount = new[] { upThreashold, leftThreashold, bottomThreashold, rightThreashold }.Max();

            var turnoverCount = 0;

            while (turnoverCount < spyralCount)
            {
                //Потом доделаешь пока времени не особо много

                turnoverCount++;
            }

            return result;
        }
    }
}